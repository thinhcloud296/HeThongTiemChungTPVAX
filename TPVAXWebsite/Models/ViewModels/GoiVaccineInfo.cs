using System;

public class GoiVaccineInfo
{
    public string MaGoi { get; set; }
    public string TenGoi { get; set; }
    public string MoTa { get; set; }
    public string DoiTuongApDung { get; set; }
    public decimal GiaGoi { get; set; }
    public DateTime? NgayBatDau { get; set; } // <-- Add this property
    public DateTime? NgayKetThuc { get; set; } // <-- Add this property
    public string TrangThai { get; set; }
}