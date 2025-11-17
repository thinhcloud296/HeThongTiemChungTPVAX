-- =============================================
-- Script: T?o Stored Procedure ?? l?y chi ti?t phi?u nh?p
-- Mô t?: L?y danh sách chi ti?t vaccine trong m?t phi?u nh?p kèm thông tin vaccine
-- =============================================

CREATE PROCEDURE dbo.usp_ChiTietPhieuNhap_GetByMaPN
    @MaPN NVARCHAR(50)
AS
BEGIN
  SET NOCOUNT ON;

    SELECT
        ct.MaCTPN AS [Mã Chi Ti?t],
      ct.MaVC AS [Mã Vaccine],
        vc.TenVC AS [Tên Vaccine],
    ct.NuocSanXuat AS [N??c S?n Xu?t],
        ct.SoLuong AS [S? L??ng],
        ct.GiaNhap AS [Giá Nh?p],
        ct.HanSuDung AS [H?n S? D?ng],
   (ct.SoLuong * ct.GiaNhap) AS [Thành Ti?n]
    FROM
        dbo.ChiTietPhieuNhap AS ct
    
    -- Join ?? l?y tên Vaccine
    LEFT JOIN
 dbo.Vaccine AS vc ON ct.MaVC = vc.MaVC
        
    WHERE
        ct.MaPN = @MaPN
 
    ORDER BY
        ct.MaCTPN;
        
END
GO
