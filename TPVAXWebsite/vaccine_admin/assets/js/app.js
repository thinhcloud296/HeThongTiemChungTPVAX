// Self-executing anonymous function to avoid polluting the global namespace
(function () {
  'use strict';

  // =================================================
  // MAIN INITIALIZATION ON DOM CONTENT LOADED
  // =================================================
  document.addEventListener('DOMContentLoaded', function () {
    // Determine the current page to run specific scripts
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';

    // Functions to run on ALL pages
    handleSidebarActiveState(currentPage);
    initializeFormValidation();
    
    // A router to call page-specific initialization functions
    const pageInitializers = {
      'index.html': initializeDashboard,
      'vaccines.html': initializeVaccinePage,
      'customers.html': initializeCustomerPage,
      'appointments.html': () => initializeDataTables('#appointmentsTable'),
      'reports.html': initializeReportsPage,
      'calendar.html': initializeCalendar
    };

    // Run the initializer if it exists for the current page
    if (pageInitializers[currentPage]) {
      pageInitializers[currentPage]();
    }
  });

  // =================================================
  // PAGE-SPECIFIC INITIALIZATION FUNCTIONS
  // =================================================

  /** Initializes charts on the dashboard page. */
  function initializeDashboard() {
    if (document.getElementById('revenueChart')) {
      const revenueChartCanvas = document.getElementById('revenueChart').getContext('2d');
      new Chart(revenueChartCanvas, {
        type: 'bar',
        data: {
          labels: ['Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10'],
          datasets: [{
            label: 'Doanh thu (Triệu VNĐ)',
            backgroundColor: 'rgba(0, 119, 182, 0.7)',
            borderColor: 'rgba(0, 119, 182, 1)',
            data: [28, 48, 40, 19, 86, 90]
          }]
        },
        options: { responsive: true, maintainAspectRatio: false }
      });
    }

    if (document.getElementById('appointmentsChart')) {
      const appointmentsChartCanvas = document.getElementById('appointmentsChart').getContext('2d');
      new Chart(appointmentsChartCanvas, {
        type: 'doughnut',
        data: {
          labels: ['Hoàn thành', 'Đã hủy', 'Sắp tới'],
          datasets: [{
            data: [700, 50, 100],
            backgroundColor: ['#148e63', '#dc3545', '#d4a000'],
          }]
        },
        options: { responsive: true, maintainAspectRatio: false }
      });
    }
  }

  /** Initializes DataTables and modals for the vaccine management page. */
  function initializeVaccinePage() {
    initializeDataTables('#vaccineTable');
    initializeDataTables('#vaccineLotsTable');
    setupModalInteraction(
        '#vaccineTable', 
        '#vaccineModal', 
        'vắc xin', 
        '#modalTitle',
        function(modal, form) { // onShow callback
            const modalTitle = form.querySelector('#modalTitle');
            modalTitle.textContent = 'Chỉnh sửa Vắc xin';
            // Demo: populate form with data. In a real app, you'd fetch this.
            $('#vaccineId').val('VC001'); 
            $('#vaccineName').val('Vắc xin 6 trong 1 (Demo)');
            $('#vaccineQuantity').val(250);
            $('#vaccinePrice').val(1015000);
        },
        function(form) { // onHide callback
            const modalTitle = form.querySelector('#modalTitle');
            const vaccineIdInput = form.querySelector('#vaccineId');
            vaccineIdInput.value = '';
            modalTitle.textContent = 'Thêm Vắc Xin Mới';
        }
    );
  }

  /** Initializes DataTable and modal for the customer management page. */
  function initializeCustomerPage() {
    initializeDataTables('#customersTable');
    setupModalInteraction(
        '#customersTable', 
        '#customerModal', 
        'khách hàng', 
        '#customerModalTitle',
        function(modal, form) { // onShow callback
            const modalTitle = form.querySelector('#customerModalTitle');
            modalTitle.textContent = 'Chỉnh sửa Khách hàng';
            // Demo: populate form
            $('#customerId').val('KH001');
            $('#customerName').val('Nguyễn Văn An (Demo)');
            $('#customerPhone').val('0901234567');
        }
    );
  }
  
  /** Initializes daterangepicker and chart for the reports page. */
  function initializeReportsPage() {
    if (typeof daterangepicker !== 'undefined') {
        $('#daterange-btn').daterangepicker({ 
            ranges: { 'Hôm nay': [moment(), moment()], '7 ngày qua': [moment().subtract(6, 'days'), moment()], '30 ngày qua': [moment().subtract(29, 'days'), moment()], 'Tháng này': [moment().startOf('month'), moment().endOf('month')] }, 
            startDate: moment().subtract(29, 'days'), 
            endDate: moment() 
        }, 
        (start, end) => $('#daterange-btn span').html(start.format('D/M/YYYY') + ' - ' + end.format('D/M/YYYY')));
    }
    if (document.getElementById('trendsChart')) {
      new Chart(document.getElementById('trendsChart').getContext('2d'), {
        type: 'line', 
        data: { labels: ['Tuần 1', 'Tuần 2', 'Tuần 3', 'Tuần 4', 'Tuần 5', 'Tuần 6'], datasets: [{ label: 'Lượt tiêm', backgroundColor: 'rgba(0, 119, 182, 0.1)', borderColor: 'rgba(0, 119, 182, 1)', data: [120, 150, 110, 180, 210, 190], fill: true, tension: 0.3 }] },
        options: { responsive: true, maintainAspectRatio: false }
      });
    }
  }

  /** Initializes the FullCalendar instance on the calendar page. */
  function initializeCalendar() {
    if (typeof FullCalendar === 'undefined' || !document.getElementById('calendar')) return;
    
    const calendarEl = document.getElementById('calendar');
    const calendar = new FullCalendar.Calendar(calendarEl, {
      headerToolbar: { left: 'prev,next today', center: 'title', right: 'dayGridMonth,timeGridWeek,timeGridDay' },
      initialView: 'dayGridMonth',
      locale: 'vi',
      editable: true,
      droppable: true,
      events: [
        { title: 'Tiêm 6 trong 1 - Nguyễn Văn An', start: new Date().toISOString().slice(0, 10) + 'T09:30:00', backgroundColor: '#007bff', borderColor: '#007bff' },
        { title: 'Tiêm Cúm - Trần Thị Bình', start: new Date().toISOString().slice(0, 10) + 'T10:00:00', backgroundColor: '#148e63', borderColor: '#148e63' }
      ],
      eventClick: function(info) {
        Swal.fire({
          title: info.event.title,
          html: `<b>Thời gian:</b> ${info.event.start.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'})}<br><b>Trạng thái:</b> Sắp tới`,
          icon: 'info',
          confirmButtonText: 'Xem chi tiết phiếu tiêm',
          showCancelButton: true,
          cancelButtonText: 'Đóng'
        }).then((result) => {
            if (result.isConfirmed) {
                // Chuyển hướng đến trang chi tiết (ví dụ)
                window.location.href = 'invoice-details.html';
            }
        });
      },
      eventDrop: function(info) {
        showToast('Thành công!', `Đã dời lịch hẹn của "${info.event.title}"`);
      }
    });
    calendar.render();
  }

  // =================================================
  // CORE & REUSABLE FUNCTIONS
  // =================================================

  /** Sets the 'active' class on the correct sidebar link based on the current page. */
  function handleSidebarActiveState(currentPage) {
    document.querySelectorAll('.sidebar .nav-link').forEach(link => {
      link.classList.remove('active');
      if (link.getAttribute('href') === currentPage) {
        link.classList.add('active');
        const parentMenu = link.closest('.nav-item.has-treeview');
        if (parentMenu) {
            parentMenu.classList.add('menu-open');
            parentMenu.querySelector('.nav-link').classList.add('active');
        }
      }
    });
  }

  /** Initializes a DataTable on a given table selector with Vietnamese language settings. */
  function initializeDataTables(tableSelector) {
    if ($(tableSelector).length > 0 && !$.fn.DataTable.isDataTable(tableSelector)) {
        $(tableSelector).DataTable({
          "responsive": true,
          "lengthChange": false,
          "autoWidth": false,
          "language": {
            "search": "Tìm kiếm:",
            "paginate": { "next": "Sau", "previous": "Trước" },
            "info": "Hiển thị _START_ đến _END_ của _TOTAL_ mục",
            "infoEmpty": "Không có dữ liệu",
            "infoFiltered": "(lọc từ _MAX_ mục)",
            "zeroRecords": "Không tìm thấy kết quả phù hợp"
          }
        });
    }
  }
  
  /** Sets up event listeners for modal interactions (add, edit, delete). */
  function setupModalInteraction(tableSelector, modalSelector, itemName, titleSelector, onShowCallback, onHideCallback) {
      const modalEl = document.querySelector(modalSelector);
      if (!modalEl) return;

      const modal = new bootstrap.Modal(modalEl);
      const form = modalEl.querySelector('form');

      // Edit button listener
      $(tableSelector).on('click', '.btn-edit, .btn-edit-customer', function () {
          if (onShowCallback) onShowCallback(modal, form);
          modal.show();
      });
      
      // Delete button listener
      $(tableSelector).on('click', '.btn-delete, .btn-delete-customer', function () {
          showDeleteConfirmation(itemName);
      });

      // Form submission
      if (form) {
        form.addEventListener('submit', function (event) {
          event.preventDefault();
          if (!form.checkValidity()) {
            event.stopPropagation();
          } else {
            const actionText = form.querySelector('input[type="hidden"][id*="Id"]')?.value ? 'cập nhật' : 'thêm mới';
            modal.hide();
            showToast('Thành công!', `Đã ${actionText} ${itemName} thành công.`);
          }
          form.classList.add('was-validated');
        });
      }

      // Reset form on modal hide
      modalEl.addEventListener('hidden.bs.modal', () => {
          if (form) {
              form.reset();
              form.classList.remove('was-validated');
          }
          if (onHideCallback) onHideCallback(form);
      });
  }

  /** Enables Bootstrap's native form validation styles. */
  function initializeFormValidation() {
    document.querySelectorAll('.needs-validation').forEach(form => {
      form.addEventListener('submit', event => {
        if (!form.checkValidity()) {
          event.preventDefault();
          event.stopPropagation();
        }
        form.classList.add('was-validated');
      }, false);
    });
  }
  
  /** Shows a confirmation dialog before deleting an item. */
  function showDeleteConfirmation(itemName) {
     Swal.fire({
            title: 'Bạn có chắc chắn?',
            text: `Hành động này không thể hoàn tác!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#0077b6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Vâng, xóa nó!',
            cancelButtonText: 'Hủy'
        }).then((result) => {
            if (result.isConfirmed) {
                showToast('Đã xóa!', `${itemName} đã được xóa.`, 'success');
            }
        })
  }

  /** Shows a toast notification using SweetAlert2. */
  function showToast(title, text, icon = 'success') {
     const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer);
            toast.addEventListener('mouseleave', Swal.resumeTimer);
        }
    });
    Toast.fire({ icon, title, text });
  }

})();