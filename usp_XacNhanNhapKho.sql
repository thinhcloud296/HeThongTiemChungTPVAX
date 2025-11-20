-- Stored Procedure: Xác nh?n nh?p kho
-- Ch?c n?ng: C?p nh?t s? l??ng t?n kho c?a vaccine và tr?ng thái phi?u nh?p
CREATE OR ALTER PROCEDURE dbo.usp_XacNhanNhapKho
    @MaPN CHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
  BEGIN TRY
        BEGIN TRANSACTION;
      
        -- 1. Ki?m tra phi?u nh?p t?n t?i
  IF NOT EXISTS (SELECT 1 FROM dbo.PhieuNhapVaccine WHERE MaPN = @MaPN)
        BEGIN
   RAISERROR(N'Phi?u nh?p không t?n t?i!', 16, 1);
        RETURN;
        END
        
        -- 2. Ki?m tra phi?u nh?p ?ã ???c xác nh?n ch?a
        IF EXISTS (SELECT 1 FROM dbo.PhieuNhapVaccine WHERE MaPN = @MaPN AND TrangThai = 1)
        BEGIN
      RAISERROR(N'Phi?u nh?p ?ã ???c xác nh?n tr??c ?ó!', 16, 1);
     RETURN;
        END
  
        -- 3. C?p nh?t s? l??ng t?n kho t? ChiTietPhieuNhap vào Vaccine
     UPDATE V
        SET V.SoLuongTon = V.SoLuongTon + CTPN.SoLuong
        FROM dbo.Vaccine V
        INNER JOIN dbo.ChiTietPhieuNhap CTPN ON V.MaVC = CTPN.MaVC
        WHERE CTPN.MaPN = @MaPN;
        
   -- 4. C?p nh?t tr?ng thái phi?u nh?p thành "?ã xác nh?n"
        UPDATE dbo.PhieuNhapVaccine
        SET TrangThai = 1
      WHERE MaPN = @MaPN;
    
        COMMIT TRANSACTION;
        
        PRINT N'Xác nh?n nh?p kho thành công!';
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
    
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
     DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
      
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO
