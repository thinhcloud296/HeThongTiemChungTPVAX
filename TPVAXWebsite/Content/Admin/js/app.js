// Custom Admin JavaScript

// Base URL for AJAX requests
var BASE_URL = window.location.origin + "/";

// Initialize on document ready
$(document).ready(function () {
  // Initialize tooltips
  $('[data-toggle="tooltip"]').tooltip();

  // Initialize popovers
  $('[data-toggle="popover"]').popover();

  // Auto dismiss alerts after 5 seconds
  setTimeout(function () {
    $(".alert").fadeOut("slow");
  }, 5000);
});

// Global AJAX error handler
$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
  if (jqxhr.status === 401) {
    // Unauthorized - redirect to login
    window.location.href = BASE_URL + "Account/Login";
  } else if (jqxhr.status === 403) {
    // Forbidden
    Swal.fire("Lỗi", "Bạn không có quyền thực hiện thao tác này", "error");
  } else if (jqxhr.status === 500) {
    // Server error
    Swal.fire("Lỗi", "Lỗi máy chủ. Vui lòng thử lại sau", "error");
  }
});

// Format currency Vietnamese style
function formatCurrency(value) {
  return new Intl.NumberFormat("vi-VN", {
    style: "currency",
    currency: "VND",
  }).format(value);
}

// Format date Vietnamese style
function formatDate(dateString) {
  if (!dateString) return "";
  var date = new Date(dateString);
  return date.toLocaleDateString("vi-VN");
}

// Format datetime Vietnamese style
function formatDateTime(dateString) {
  if (!dateString) return "";
  var date = new Date(dateString);
  return date.toLocaleString("vi-VN");
}

// Validate file size
function validateFileSize(file, maxSizeMB) {
  var maxSize = maxSizeMB * 1024 * 1024; // Convert to bytes
  if (file.size > maxSize) {
    Swal.fire(
      "Lỗi",
      "Kích thước file không được vượt quá " + maxSizeMB + "MB",
      "error"
    );
    return false;
  }
  return true;
}

// Validate image file type
function validateImageType(file) {
  var allowedTypes = ["image/jpeg", "image/jpg", "image/png", "image/gif"];
  if (!allowedTypes.includes(file.type)) {
    Swal.fire("Lỗi", "Chỉ chấp nhận file ảnh (JPG, PNG, GIF)", "error");
    return false;
  }
  return true;
}

// Preview image before upload
function previewImage(input, previewElement) {
  if (input.files && input.files[0]) {
    var reader = new FileReader();
    reader.onload = function (e) {
      $(previewElement).attr("src", e.target.result).show();
    };
    reader.readAsDataURL(input.files[0]);
  }
}

// Confirm delete action
function confirmDelete(callback) {
  Swal.fire({
    title: "Xác nhận xóa",
    text: "Bạn có chắc chắn muốn xóa? Thao tác này không thể hoàn tác!",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#d33",
    cancelButtonColor: "#3085d6",
    confirmButtonText: "Xóa",
    cancelButtonText: "Hủy",
  }).then((result) => {
    if (result.isConfirmed && callback) {
      callback();
    }
  });
}

// Show loading overlay
function showLoading() {
  $("body").append(
    '<div class="overlay-loading"><div class="spinner-border text-primary" role="status"><span class="sr-only">Loading...</span></div></div>'
  );
}

// Hide loading overlay
function hideLoading() {
  $(".overlay-loading").remove();
}
