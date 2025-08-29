create database QLTuyenSinhLop10_TraVinh;
USE QLTuyenSinhLop10_TraVinh;
GO

-- Bảng TRUONG_HOC
CREATE TABLE TRUONG_HOC (
    MaTruong NVARCHAR(5) PRIMARY KEY, -- Mã trường ngắn (vd: '05')
    TenTruong NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(200),
    SoDienThoai NVARCHAR(20),
    Email NVARCHAR(100),
    ChiTieu INT DEFAULT 0,
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Bảng NGUOI_DUNG
CREATE TABLE NGUOI_DUNG (
    MaNguoiDung INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    MaTruong NVARCHAR(5) NOT NULL UNIQUE,
    NgayTao DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (MaTruong) REFERENCES TRUONG_HOC(MaTruong)
);
GO

-- Bảng HOC_SINH 
CREATE TABLE HOC_SINH (
    MaHocSinh INT PRIMARY KEY IDENTITY(1,1),
    MaSoBaoDanh NVARCHAR(8) NOT NULL UNIQUE,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(5) CHECK (GioiTinh IN ('Nam', 'Nữ')),
    DanToc NVARCHAR(30),
    NoiSinh NVARCHAR(100),
    TruongTHCS NVARCHAR(100), 
    MaTruong NVARCHAR(5) NOT NULL, 
    DiemToan DECIMAL(4,2),
    DiemVan DECIMAL(4,2),
    DiemAnh DECIMAL(4,2), 
    DiemTong DECIMAL(5,2), 
    PhongThi NVARCHAR(20), 
    SoBaoDanhTrongPhong INT, 
    TrangThai NVARCHAR(20) DEFAULT 'DangKy' CHECK (TrangThai IN ('DangKy', 'HopLe', 'KhongHopLe', 'TrungTuyen', 'Rot')),
    NgayDangKy DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(500),
    FOREIGN KEY (MaTruong) REFERENCES TRUONG_HOC(MaTruong)
);
GO

-- Bảng PHONG_THI 
CREATE TABLE PHONG_THI (
    MaPhong INT PRIMARY KEY IDENTITY(1,1),
    MaPhongThi NVARCHAR(20) NOT NULL UNIQUE, -- Định dạng: P0501 (P + Mã trường + STT)
    MaTruong NVARCHAR(5) NOT NULL, 
    DiaDiem NVARCHAR(100), 
    SoLuongToiDa INT DEFAULT 24, 
    SoLuongHienTai INT DEFAULT 0, 
    GiamThi1 NVARCHAR(100), 
    GiamThi2 NVARCHAR(100), 
    NgayThi DATETIME,
    FOREIGN KEY (MaTruong) REFERENCES TRUONG_HOC(MaTruong)
);
GO

-- Thêm dữ liệu mẫu
INSERT INTO TRUONG_HOC (MaTruong, TenTruong, DiaChi, ChiTieu)
VALUES 
('01', N'Trường THPT Chuyên Nguyễn Thiện Thành', N'123 Đường 3/2, TP. Trà Vinh', 300),
('05', N'Trường Thực hành Sư phạm', N'Đại học Trà Vinh', 250),
('10', N'Trường THPT Cầu Ngang', N'Huyện Cầu Ngang', 400);
GO

INSERT INTO NGUOI_DUNG (TenDangNhap, MatKhau, HoTen, MaTruong)
VALUES 
('truong01', '123456', N'Nguyễn Văn A', '01'),
('truong05', '123456', N'Trần Thị B', '05'),
('truong10', '123456', N'Lê Văn C', '10');
GO

-- =============================================
-- CÁC STORED PROCEDURE QUAN TRỌNG
-- =============================================

-- 1. Tạo mã báo danh tự động
CREATE PROCEDURE sp_TaoMaBaoDanh
    @MaTruong NVARCHAR(5), -- Nhận mã trường (vd: '05')
    @MaBaoDanh NVARCHAR(8) OUTPUT -- Trả về mã báo danh (vd: '05001')
AS
BEGIN
    DECLARE @SoThuTu INT
    
    -- Tìm số thứ tự tiếp theo
    SELECT @SoThuTu = ISNULL(MAX(CAST(RIGHT(MaSoBaoDanh, 3) AS INT)), 0) + 1
    FROM HOC_SINH
    WHERE MaTruong = @MaTruong
    
    -- Ghép mã trường + số thứ tự 3 chữ số
    SET @MaBaoDanh = @MaTruong + RIGHT('000' + CAST(@SoThuTu AS NVARCHAR(3)), 3)
END
GO

-- 3. Chia phòng thi tự động
CREATE PROCEDURE sp_ChiaPhongThi
    @MaTruong NVARCHAR(5)
AS
BEGIN
    DECLARE @MaPhongThi NVARCHAR(20)
    DECLARE @SoChoConLai INT
    DECLARE @MaHocSinh INT
    DECLARE @MaBaoDanh NVARCHAR(8)
    DECLARE @SoThuTu INT = 1
    
    -- Tạo bảng tạm chứa học sinh chưa chia phòng
    CREATE TABLE #HocSinhChuaChia (
        MaHocSinh INT,
        MaBaoDanh NVARCHAR(8)
    )
    
    INSERT INTO #HocSinhChuaChia
    SELECT MaHocSinh, MaSoBaoDanh 
    FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND (PhongThi IS NULL OR PhongThi = '')
    ORDER BY MaSoBaoDanh
    
    -- Duyệt qua các phòng còn chỗ trống
    DECLARE phong_cursor CURSOR FOR
    SELECT MaPhongThi, (SoLuongToiDa - SoLuongHienTai) 
    FROM PHONG_THI 
    WHERE MaTruong = @MaTruong AND SoLuongHienTai < SoLuongToiDa
    ORDER BY MaPhongThi
    
    OPEN phong_cursor
    FETCH NEXT FROM phong_cursor INTO @MaPhongThi, @SoChoConLai
    
    -- Duyệt qua học sinh chưa chia phòng
    DECLARE hs_cursor CURSOR FOR
    SELECT MaHocSinh, MaBaoDanh FROM #HocSinhChuaChia
    
    OPEN hs_cursor
    FETCH NEXT FROM hs_cursor INTO @MaHocSinh, @MaBaoDanh
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Nếu phòng hiện tại đã đầy, chuyển sang phòng mới
        IF @SoChoConLai <= 0 OR @MaPhongThi IS NULL
        BEGIN
            FETCH NEXT FROM phong_cursor INTO @MaPhongThi, @SoChoConLai
            
            -- Nếu không còn phòng nào, tạo phòng mới
            IF @@FETCH_STATUS != 0
            BEGIN
                -- Tạo mã phòng mới (vd: P0501)
                SELECT @MaPhongThi = 'P' + @MaTruong + RIGHT('00' + CAST(ISNULL(MAX(CAST(SUBSTRING(MaPhongThi, LEN('P' + @MaTruong) + 1, 2) AS INT)), 0) + 1 AS NVARCHAR(2)), 2)
                FROM PHONG_THI
                WHERE MaTruong = @MaTruong
                
                -- Thêm phòng mới
                INSERT INTO PHONG_THI (MaPhongThi, MaTruong, SoLuongToiDa, SoLuongHienTai)
                VALUES (@MaPhongThi, @MaTruong, 24, 0)
                
                SET @SoChoConLai = 24
                SET @SoThuTu = 1
            END
        END
        
        -- Cập nhật phòng thi cho học sinh
        UPDATE HOC_SINH
        SET PhongThi = @MaPhongThi, SoBaoDanhTrongPhong = @SoThuTu
        WHERE MaHocSinh = @MaHocSinh
        
        -- Cập nhật số lượng phòng thi
        UPDATE PHONG_THI
        SET SoLuongHienTai = SoLuongHienTai + 1
        WHERE MaPhongThi = @MaPhongThi
        
        SET @SoChoConLai = @SoChoConLai - 1
        SET @SoThuTu = @SoThuTu + 1
        
        FETCH NEXT FROM hs_cursor INTO @MaHocSinh, @MaBaoDanh
    END
    
    -- Dọn dẹp
    CLOSE hs_cursor
    DEALLOCATE hs_cursor
    CLOSE phong_cursor
    DEALLOCATE phong_cursor
    DROP TABLE #HocSinhChuaChia
END
GO

-- 4. Cập nhật điểm và xét tuyển
CREATE PROCEDURE sp_CapNhatDiem
    @MaBaoDanh NVARCHAR(8),
    @DiemToan DECIMAL(4,2),
    @DiemVan DECIMAL(4,2),
    @DiemAnh DECIMAL(4,2)
AS
BEGIN
    -- Cập nhật điểm
    UPDATE HOC_SINH
    SET 
        DiemToan = @DiemToan,
        DiemVan = @DiemVan,
        DiemAnh = @DiemAnh,
        DiemTong = @DiemToan + @DiemVan + @DiemAnh
    WHERE MaSoBaoDanh = @MaBaoDanh
    
    -- Trả về thông tin học sinh đã cập nhật
    SELECT * FROM HOC_SINH WHERE MaSoBaoDanh = @MaBaoDanh
END
GO


-- Cập nhật bảng HOC_SINH
ALTER TABLE HOC_SINH
DROP COLUMN SoBaoDanhTrongPhong; -- Bỏ trường SoBaoDanhTrongPhong

-- Thay đổi các trường điểm và phòng thi cho phép NULL
ALTER TABLE HOC_SINH
ALTER COLUMN DiemToan DECIMAL(4,2) NULL;

ALTER TABLE HOC_SINH
ALTER COLUMN DiemVan DECIMAL(4,2) NULL;

ALTER TABLE HOC_SINH
ALTER COLUMN DiemAnh DECIMAL(4,2) NULL;

ALTER TABLE HOC_SINH
ALTER COLUMN DiemTong DECIMAL(5,2) NULL;

ALTER TABLE HOC_SINH
ALTER COLUMN PhongThi NVARCHAR(20) NULL;


-- thêm điểm khuyên khích và điểm ưu tiên
ALTER TABLE HOC_SINH
ADD DiemKhuyenKhich DECIMAL(4,2) NULL;

ALTER TABLE HOC_SINH
ADD DiemUuTien DECIMAL(4,2) NULL;
GO

ALTER PROCEDURE sp_ChiaPhongThi
    @MaTruong NVARCHAR(5)
AS
BEGIN
    DECLARE @MaPhongThi NVARCHAR(20)
    DECLARE @SoChoConLai INT
    DECLARE @MaHocSinh INT
    DECLARE @MaBaoDanh NVARCHAR(8)

    -- Tạo bảng tạm chứa học sinh chưa chia phòng
    CREATE TABLE #HocSinhChuaChia (
        MaHocSinh INT,
        MaBaoDanh NVARCHAR(8)
    )
    
    INSERT INTO #HocSinhChuaChia
    SELECT MaHocSinh, MaSoBaoDanh 
    FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND (PhongThi IS NULL OR PhongThi = '')
    ORDER BY MaSoBaoDanh

    -- Duyệt qua các phòng còn chỗ trống
    DECLARE phong_cursor CURSOR FOR
    SELECT MaPhongThi, (SoLuongToiDa - SoLuongHienTai) 
    FROM PHONG_THI 
    WHERE MaTruong = @MaTruong AND SoLuongHienTai < SoLuongToiDa
    ORDER BY MaPhongThi
    
    OPEN phong_cursor
    FETCH NEXT FROM phong_cursor INTO @MaPhongThi, @SoChoConLai

    -- Duyệt qua học sinh chưa chia phòng
    DECLARE hs_cursor CURSOR FOR
    SELECT MaHocSinh, MaBaoDanh FROM #HocSinhChuaChia
    
    OPEN hs_cursor
    FETCH NEXT FROM hs_cursor INTO @MaHocSinh, @MaBaoDanh
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Nếu phòng hiện tại đã đầy, chuyển sang phòng mới
        IF @SoChoConLai <= 0 OR @MaPhongThi IS NULL
        BEGIN
            FETCH NEXT FROM phong_cursor INTO @MaPhongThi, @SoChoConLai
            
            -- Nếu không còn phòng nào, tạo phòng mới
            IF @@FETCH_STATUS != 0
            BEGIN
                -- Tạo mã phòng mới (vd: P0501)
                SELECT @MaPhongThi = 'P' + @MaTruong + RIGHT('00' + CAST(
                    ISNULL(MAX(CAST(SUBSTRING(MaPhongThi, LEN('P' + @MaTruong) + 1, 2) AS INT)), 0) + 1 
                    AS NVARCHAR(2)), 2)
                FROM PHONG_THI
                WHERE MaTruong = @MaTruong
                
                -- Thêm phòng mới
                INSERT INTO PHONG_THI (MaPhongThi, MaTruong, SoLuongToiDa, SoLuongHienTai)
                VALUES (@MaPhongThi, @MaTruong, 24, 0)
                
                SET @SoChoConLai = 24
            END
        END
        
        -- Cập nhật phòng thi cho học sinh
        UPDATE HOC_SINH
        SET PhongThi = @MaPhongThi
        WHERE MaHocSinh = @MaHocSinh
        
        -- Cập nhật số lượng phòng thi
        UPDATE PHONG_THI
        SET SoLuongHienTai = SoLuongHienTai + 1
        WHERE MaPhongThi = @MaPhongThi
        
        SET @SoChoConLai = @SoChoConLai - 1
        
        FETCH NEXT FROM hs_cursor INTO @MaHocSinh, @MaBaoDanh
    END

    -- Dọn dẹp
    CLOSE hs_cursor
    DEALLOCATE hs_cursor
    CLOSE phong_cursor
    DEALLOCATE phong_cursor
    DROP TABLE #HocSinhChuaChia
END
GO

-- PHẦN CẬP NHẬT QUẢN LÝ ĐỢT TUYỂN SINH


CREATE TABLE DOT_TUYEN_SINH (
    MaDot NVARCHAR(10) PRIMARY KEY,  -- Ví dụ: '2025'
    TenDot NVARCHAR(100) NOT NULL,
    Nam INT NOT NULL UNIQUE, 
    NgayBatDau DATE,
    NgayKetThuc DATE,
    TrangThai NVARCHAR(20) DEFAULT N'DangMo' CHECK (TrangThai IN (N'DangMo', N'DaDong'))
);

ALTER TABLE HOC_SINH
ADD MaDot NVARCHAR(10) NULL;  -- Cho phép NULL

ALTER TABLE PHONG_THI
ADD MaDot NVARCHAR(10) NULL;

-- RÀNG BUỘC KHÓA NGOẠI
ALTER TABLE HOC_SINH ADD FOREIGN KEY (MaDot) REFERENCES DOT_TUYEN_SINH(MaDot);
ALTER TABLE PHONG_THI ADD FOREIGN KEY (MaDot) REFERENCES DOT_TUYEN_SINH(MaDot);

--Cập nhật stored procedure TẠO MÃ BÁO DANH
ALTER PROCEDURE sp_TaoMaBaoDanh
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10),
    @MaBaoDanh NVARCHAR(8) OUTPUT
AS
BEGIN
    DECLARE @SoThuTu INT
    SELECT @SoThuTu = ISNULL(MAX(CAST(RIGHT(MaSoBaoDanh, 3) AS INT)), 0) + 1
    FROM HOC_SINH
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot

    SET @MaBaoDanh = @MaTruong + RIGHT('000' + CAST(@SoThuTu AS NVARCHAR(3)), 3)
END

-- CHÈN ĐỢT MẪU
INSERT INTO DOT_TUYEN_SINH (MaDot, TenDot, Nam, NgayBatDau, NgayKetThuc)
VALUES ('2025', N'Tuyển sinh lớp 10 năm 2025', 2025, '2025-04-01', '2025-06-30');

SELECT * FROM DOT_TUYEN_SINH WHERE MaDot = '2025';

UPDATE DOT_TUYEN_SINH
SET TrangThai = N'DangMo'
WHERE MaDot = '2025';


-- Cập nhật giám thị phòng thi
CREATE PROCEDURE sp_CapNhatGiamThi
    @MaPhongThi NVARCHAR(50),
    @GiamThi1 NVARCHAR(100),
    @GiamThi2 NVARCHAR(100)
AS
BEGIN
    UPDATE PHONG_THI
    SET GiamThi1 = @GiamThi1,
        GiamThi2 = @GiamThi2
    WHERE MaPhongThi = @MaPhongThi
END


-- Update sữa họ và tên riêng biệt để dễ sắp theo thứ tự

ALTER TABLE HOC_SINH
ADD Ho NVARCHAR(100), 
    Ten NVARCHAR(50);

ALTER TABLE HOC_SINH
DROP COLUMN HoTen;


-- update proceduce thêm học sinh
ALTER PROCEDURE sp_ThemHocSinh
    @Ho NVARCHAR(100),
    @Ten NVARCHAR(50),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10),
    @DanToc NVARCHAR(30) = NULL,
    @NoiSinh NVARCHAR(100) = NULL,
    @TruongTHCS NVARCHAR(100) = NULL,
    @DiemToan DECIMAL(4,2) = NULL,
    @DiemVan DECIMAL(4,2) = NULL,
    @DiemAnh DECIMAL(4,2) = NULL,
    @DiemKhuyenKhich DECIMAL(4,2) = NULL,
    @DiemUuTien DECIMAL(4,2) = NULL,
    @PhongThi NVARCHAR(20) = NULL,
    @TrangThai NVARCHAR(20) = 'DangKy',
    @GhiChu NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @DiemTong DECIMAL(5,2) = NULL;

    IF @DiemToan IS NOT NULL AND @DiemVan IS NOT NULL AND @DiemAnh IS NOT NULL
    BEGIN
        SET @DiemTong = ISNULL(@DiemToan, 0) + ISNULL(@DiemVan, 0) + ISNULL(@DiemAnh, 0)
                        + ISNULL(@DiemKhuyenKhich, 0) + ISNULL(@DiemUuTien, 0);
    END

    INSERT INTO HOC_SINH (
        Ho, Ten, NgaySinh, GioiTinh, DanToc, NoiSinh,
        TruongTHCS, MaTruong, MaDot,
        DiemToan, DiemVan, DiemAnh, DiemTong,
        DiemKhuyenKhich, DiemUuTien,
        PhongThi, TrangThai, GhiChu,
        MaSoBaoDanh -- Để NULL, sẽ được gán sau
    )
    VALUES (
        @Ho, @Ten, @NgaySinh, @GioiTinh, @DanToc, @NoiSinh,
        @TruongTHCS, @MaTruong, @MaDot,
        @DiemToan, @DiemVan, @DiemAnh, @DiemTong,
        @DiemKhuyenKhich, @DiemUuTien,
        @PhongThi, @TrangThai, @GhiChu,
        NULL
    );

    SELECT * FROM HOC_SINH WHERE MaHocSinh = SCOPE_IDENTITY();
END


-- Update phần thêm hông tin của học sinh trước, sau đó mới sắp xếp theo tên (theo bảng chữ cái tiếng việt) và cuối cùng mới thêm Masobaodanh tự động


ALTER PROCEDURE sp_GanMaSoBaoDanhHangLoat
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SoThuTu INT;

    -- Lấy số lớn nhất đã gán trong trường & đợt
    SELECT @SoThuTu = ISNULL(MAX(CAST(RIGHT(MaSoBaoDanh, 3) AS INT)), 0)
    FROM HOC_SINH
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND MaSoBaoDanh IS NOT NULL;

    DECLARE @MaSoBaoDanh NVARCHAR(8);
    DECLARE @MaHocSinh INT;

    -- Cursor duyệt danh sách học sinh chưa có mã báo danh, đã nhập cho trường & đợt này, sắp xếp tên + họ
    DECLARE hs_cursor CURSOR FOR
        SELECT MaHocSinh
        FROM HOC_SINH
        WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND MaSoBaoDanh IS NULL
        ORDER BY Ten COLLATE Vietnamese_CI_AI, Ho COLLATE Vietnamese_CI_AI;

    OPEN hs_cursor;
    FETCH NEXT FROM hs_cursor INTO @MaHocSinh;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SoThuTu = @SoThuTu + 1;
        SET @MaSoBaoDanh = @MaTruong + RIGHT('000' + CAST(@SoThuTu AS NVARCHAR(3)), 3);

        UPDATE HOC_SINH
        SET MaSoBaoDanh = @MaSoBaoDanh
        WHERE MaHocSinh = @MaHocSinh;

        FETCH NEXT FROM hs_cursor INTO @MaHocSinh;
    END

    CLOSE hs_cursor;
    DEALLOCATE hs_cursor;
END



-- sửa mã báo danh lại là null
ALTER TABLE HOC_SINH ALTER COLUMN MaSoBaoDanh NVARCHAR(8) NULL;
-- Chỉ đảm bảo giá trị không trùng khi cột đã được gán số báo danh
CREATE UNIQUE INDEX UQ_HOC_SINH_MaSoBaoDanh
ON HOC_SINH(MaSoBaoDanh)
WHERE MaSoBaoDanh IS NOT NULL;

-- Xóa dữ liệu bảng học sinh nếu cần làm lại từ đầu
 
DELETE FROM HOC_SINH;
DBCC CHECKIDENT ('HOC_SINH', RESEED, 0);

DELETE FROM PHONG_THI;
DBCC CHECKIDENT ('PHONG_THI', RESEED, 0);

-- Cập nhật lại chức năng chia phòng thi

ALTER PROCEDURE sp_ChiaPhongThi
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaPhongThi NVARCHAR(20);
    DECLARE @SoChoConLai INT = 0;
    DECLARE @MaHocSinh INT;
    DECLARE @SoPhong INT = 0;

    DECLARE @TongHS INT;
    SELECT @TongHS = COUNT(*) FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '');

    IF @TongHS = 0 RETURN; -- Không có học sinh cần chia

    -- Tìm số phòng đã có của trường & đợt này (nếu muốn tạo lại hết thì bỏ qua bước này)
    SELECT @SoPhong = COUNT(*) FROM PHONG_THI WHERE MaTruong = @MaTruong AND MaDot = @MaDot;

    -- Tạo bảng tạm lưu học sinh chưa có phòng
    CREATE TABLE #HSChuaChia (MaHocSinh INT IDENTITY(1,1), MaHS INT);

    INSERT INTO #HSChuaChia(MaHS)
    SELECT MaHocSinh
    FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '')
    ORDER BY MaSoBaoDanh;

    DECLARE @i INT = 1;
    DECLARE @maxHSPhong INT = 24; -- số học sinh mỗi phòng

    WHILE @i <= @TongHS
    BEGIN
        SET @SoPhong = @SoPhong + 1; -- Tăng số phòng lên
        SET @MaPhongThi = CAST(@SoPhong AS NVARCHAR(20)); -- mã phòng chỉ là số tăng dần từ 1

        -- Thêm phòng mới
        INSERT INTO PHONG_THI (MaPhongThi, MaTruong, MaDot, SoLuongToiDa, SoLuongHienTai)
        VALUES (@MaPhongThi, @MaTruong, @MaDot, @maxHSPhong, 0);

        -- Gán tối đa 24 học sinh vào phòng này
        DECLARE @j INT = 0;
        WHILE @j < @maxHSPhong AND @i <= @TongHS
        BEGIN
            DECLARE @CurrentMaHS INT;
            SELECT @CurrentMaHS = MaHS FROM #HSChuaChia WHERE MaHocSinh = @i;

            UPDATE HOC_SINH
            SET PhongThi = @MaPhongThi
            WHERE MaHocSinh = @CurrentMaHS;

            UPDATE PHONG_THI
            SET SoLuongHienTai = SoLuongHienTai + 1
            WHERE MaPhongThi = @MaPhongThi AND MaTruong = @MaTruong AND MaDot = @MaDot;

            SET @i = @i + 1;
            SET @j = @j + 1;
        END
    END

    DROP TABLE #HSChuaChia;
END



-- sp chia phòng thi fix lỗi chưa chia được nếu đăng nhập khác trường

ALTER PROCEDURE sp_ChiaPhongThi
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaPhongThi NVARCHAR(20);
    DECLARE @SoChoConLai INT = 0;
    DECLARE @MaHocSinh INT;
    DECLARE @SoPhong INT = 0;

    DECLARE @TongHS INT;
    SELECT @TongHS = COUNT(*) FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '');

    IF @TongHS = 0 RETURN; -- Không có học sinh cần chia

    -- Tìm số phòng đã có của trường & đợt này (nếu muốn tạo lại hết thì bỏ qua bước này)
    SELECT @SoPhong = COUNT(*) FROM PHONG_THI WHERE MaTruong = @MaTruong AND MaDot = @MaDot;

    -- Tạo bảng tạm lưu học sinh chưa có phòng
    CREATE TABLE #HSChuaChia (MaHocSinh INT IDENTITY(1,1), MaHS INT);

    INSERT INTO #HSChuaChia(MaHS)
    SELECT MaHocSinh
    FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '')
    ORDER BY MaSoBaoDanh;

    DECLARE @i INT = 1;
    DECLARE @maxHSPhong INT = 24; -- số học sinh mỗi phòng

    WHILE @i <= @TongHS
    BEGIN
        SET @SoPhong = @SoPhong + 1;
SET @MaPhongThi = 'P' + @MaTruong + RIGHT('00' + CAST(@SoPhong AS NVARCHAR(2)), 2);

INSERT INTO PHONG_THI (MaPhongThi, MaTruong, MaDot, SoLuongToiDa, SoLuongHienTai)
VALUES (@MaPhongThi, @MaTruong, @MaDot, @maxHSPhong, 0);

        -- Gán tối đa 24 học sinh vào phòng này
        DECLARE @j INT = 0;
        WHILE @j < @maxHSPhong AND @i <= @TongHS
        BEGIN
            DECLARE @CurrentMaHS INT;
            SELECT @CurrentMaHS = MaHS FROM #HSChuaChia WHERE MaHocSinh = @i;

            UPDATE HOC_SINH
            SET PhongThi = @MaPhongThi
            WHERE MaHocSinh = @CurrentMaHS;

            UPDATE PHONG_THI
            SET SoLuongHienTai = SoLuongHienTai + 1
            WHERE MaPhongThi = @MaPhongThi AND MaTruong = @MaTruong AND MaDot = @MaDot;

            SET @i = @i + 1;
            SET @j = @j + 1;
        END
    END

    DROP TABLE #HSChuaChia;
END



-- Xóa unique để tạo mã số báo danh trùng lặp được

CREATE UNIQUE INDEX UQ_HOC_SINH_MaSoBaoDanh
ON HOC_SINH (MaTruong, MaDot, MaSoBaoDanh)
WHERE MaSoBaoDanh IS NOT NULL;

SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('HOC_SINH');

IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID('HOC_SINH') AND name = 'UQ_HOC_SINH_MaSoBaoDanh')
    DROP INDEX UQ_HOC_SINH_MaSoBaoDanh ON HOC_SINH;




ALTER PROCEDURE sp_ChiaPhongThi
    @MaTruong NVARCHAR(5),
    @MaDot NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaPhongThi NVARCHAR(20);
    DECLARE @SoChoConLai INT = 0;
    DECLARE @MaHocSinh INT;
    DECLARE @SoPhong INT = 0;

    DECLARE @TongHS INT;
    SELECT @TongHS = COUNT(*) FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '');

    IF @TongHS = 0 RETURN; -- Không có học sinh cần chia

    -- Lấy số lớn nhất của phòng thi đã có với trường & đợt này
    SELECT @SoPhong = ISNULL(MAX(CAST(SUBSTRING(MaPhongThi, LEN('P'+@MaTruong)+2, 2) AS INT)), 0)
    FROM PHONG_THI
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot
          AND MaPhongThi LIKE 'P'+@MaTruong+'%'+'_'+@MaDot;

    -- Tạo bảng tạm lưu học sinh chưa có phòng
    CREATE TABLE #HSChuaChia (MaHocSinh INT IDENTITY(1,1), MaHS INT);

    INSERT INTO #HSChuaChia(MaHS)
    SELECT MaHocSinh
    FROM HOC_SINH 
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND (PhongThi IS NULL OR PhongThi = '')
    ORDER BY MaSoBaoDanh;

    DECLARE @i INT = 1;
    DECLARE @maxHSPhong INT = 24; -- số học sinh mỗi phòng

    WHILE @i <= @TongHS
    BEGIN
        SET @SoPhong = @SoPhong + 1;
        -- Tạo mã phòng: P + MaTruong + số phòng 2 số + _ + MaDot, ví dụ: P0501_2025
        SET @MaPhongThi = 'P' + @MaTruong + RIGHT('00' + CAST(@SoPhong AS NVARCHAR(2)), 2) + '_' + @MaDot;

        -- Thêm phòng mới
        INSERT INTO PHONG_THI (MaPhongThi, MaTruong, MaDot, SoLuongToiDa, SoLuongHienTai)
        VALUES (@MaPhongThi, @MaTruong, @MaDot, @maxHSPhong, 0);

        -- Gán tối đa 24 học sinh vào phòng này
        DECLARE @j INT = 0;
        WHILE @j < @maxHSPhong AND @i <= @TongHS
        BEGIN
            DECLARE @CurrentMaHS INT;
            SELECT @CurrentMaHS = MaHS FROM #HSChuaChia WHERE MaHocSinh = @i;

            UPDATE HOC_SINH
            SET PhongThi = @MaPhongThi
            WHERE MaHocSinh = @CurrentMaHS;

            UPDATE PHONG_THI
            SET SoLuongHienTai = SoLuongHienTai + 1
            WHERE MaPhongThi = @MaPhongThi AND MaTruong = @MaTruong AND MaDot = @MaDot;

            SET @i = @i + 1;
            SET @j = @j + 1;
        END
    END

    DROP TABLE #HSChuaChia;
END

SELECT DISTINCT GioiTinh FROM HOC_SINH


--Tách bảng chỉ tiêu để làm chức năng 

CREATE TABLE CHI_TIEU_TUYEN_SINH (
    MaTruong NVARCHAR(5),
    MaDot NVARCHAR(10),
    ChiTieu INT,
    PRIMARY KEY (MaTruong, MaDot),
    FOREIGN KEY (MaTruong) REFERENCES TRUONG_HOC(MaTruong),
    FOREIGN KEY (MaDot) REFERENCES DOT_TUYEN_SINH(MaDot)
);


INSERT INTO CHI_TIEU_TUYEN_SINH (MaTruong, MaDot, ChiTieu)
SELECT MaTruong, '2025', ChiTieu FROM TRUONG_HOC WHERE ChiTieu IS NOT NULL;

ALTER TABLE TRUONG_HOC DROP COLUMN ChiTieu;


-- SP xét học sinh trúng tuyển

CREATE OR ALTER PROCEDURE sp_XetTrungTuyen
    @MaTruong NVARCHAR(5),
    @MaDot    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ChiTieu INT =
        (SELECT ChiTieu FROM CHI_TIEU_TUYEN_SINH
         WHERE MaTruong = @MaTruong AND MaDot = @MaDot);

    IF @ChiTieu IS NULL OR @ChiTieu <= 0
    BEGIN
        UPDATE HOC_SINH
        SET TrangThai = N'Rot'
        WHERE MaTruong = @MaTruong AND MaDot = @MaDot;

        SELECT DiemChuan = CAST(NULL AS DECIMAL(5,2)),
               SoTrungTuyen = CAST(0 AS INT);
        RETURN;
    END


    UPDATE HOC_SINH
    SET TrangThai = N'Rot'
    WHERE MaTruong = @MaTruong AND MaDot = @MaDot;

    ;WITH DS AS (
        SELECT TOP (@ChiTieu) WITH TIES MaHocSinh, DiemTong
        FROM HOC_SINH
        WHERE MaTruong = @MaTruong
          AND MaDot    = @MaDot
          AND DiemTong IS NOT NULL
        ORDER BY DiemTong DESC
    )
    UPDATE HS
    SET TrangThai = N'TrungTuyen'
    FROM HOC_SINH HS
    JOIN DS ON DS.MaHocSinh = HS.MaHocSinh;

    -- Trả về thông tin tham khảo để hiển thị
    DECLARE @DiemChuan DECIMAL(5,2) =
        (SELECT MIN(DiemTong)
         FROM HOC_SINH
         WHERE MaTruong = @MaTruong AND MaDot = @MaDot AND TrangThai = N'TrungTuyen');

    SELECT DiemChuan     = @DiemChuan,
           SoTrungTuyen  = (SELECT COUNT(*)
                            FROM HOC_SINH
                            WHERE MaTruong = @MaTruong AND MaDot = @MaDot
                              AND TrangThai = N'TrungTuyen');
END



-- Sp thống kê điểm theo môn

CREATE OR ALTER PROCEDURE sp_ThongKeDiemTheoMon
    @MaTruong NVARCHAR(5),
    @MaDot    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Raw AS (
        SELECT N'Toán' AS Mon, hs.DiemToan AS Diem
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot
        UNION ALL SELECT N'Văn',  hs.DiemVan
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot
        UNION ALL SELECT N'Anh',  hs.DiemAnh
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot
    )
    -- Phân bố điểm (loại 0)
    SELECT Mon, Muc = CAST(Diem AS DECIMAL(4,2)), SoLuong = COUNT(*), BoThi = 0
    FROM Raw WHERE Diem IS NOT NULL AND Diem > 0
    GROUP BY Mon, CAST(Diem AS DECIMAL(4,2))

    UNION ALL
    -- Bỏ thi (điểm = 0)
    SELECT Mon, Muc = NULL, SoLuong = COUNT(*), BoThi = 1
    FROM Raw WHERE Diem = 0
    GROUP BY Mon

    ORDER BY Mon, BoThi, Muc;
END


-- Thống kê điểm theo trường THCS

CREATE OR ALTER PROCEDURE sp_ThongKeTheoTHCS
    @MaTruong NVARCHAR(5),
    @MaDot    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH HS AS (
        SELECT *
        FROM HOC_SINH
        WHERE MaTruong = @MaTruong AND MaDot = @MaDot
    ),
    -- Tổng theo THCS (dùng làm mẫu số cho mọi tỷ lệ)
    TONG AS (
        SELECT 
            TruongTHCS,
            TongTS = COUNT(*),
            TSNu   = SUM(CASE WHEN GioiTinh = N'Nữ' THEN 1 ELSE 0 END),
            Dau    = SUM(CASE WHEN TrangThai = N'TrungTuyen' THEN 1 ELSE 0 END),
            Hong   = SUM(CASE WHEN TrangThai = N'Rot' THEN 1 ELSE 0 END)
        FROM HS
        GROUP BY TruongTHCS
    ),
    -- Unpivot điểm theo môn
    RAW AS (
        SELECT TruongTHCS, N'Văn'   AS Mon, Diem = CAST(DiemVan AS DECIMAL(4,2)) FROM HS
        UNION ALL
        SELECT TruongTHCS, N'A.Văn' AS Mon, Diem = CAST(DiemAnh AS DECIMAL(4,2)) FROM HS
        UNION ALL
        SELECT TruongTHCS, N'Toán'  AS Mon, Diem = CAST(DiemToan AS DECIMAL(4,2)) FROM HS
    ),
    -- Đếm theo các mức
    PHANBO AS (
        SELECT 
            r.TruongTHCS, r.Mon,
            DuThi   = SUM(CASE WHEN r.Diem IS NOT NULL THEN 1 ELSE 0 END),
            BoThi   = SUM(CASE WHEN r.Diem = 0 THEN 1 ELSE 0 END),
            M0_3    = SUM(CASE WHEN r.Diem > 0 AND r.Diem < 3  THEN 1 ELSE 0 END),
            M3_5    = SUM(CASE WHEN r.Diem >=3 AND r.Diem < 5 THEN 1 ELSE 0 END),
            M5_7    = SUM(CASE WHEN r.Diem >=5 AND r.Diem < 7 THEN 1 ELSE 0 END),
            M7_9    = SUM(CASE WHEN r.Diem >=7 AND r.Diem < 9 THEN 1 ELSE 0 END),
            M9_10   = SUM(CASE WHEN r.Diem >=9 AND r.Diem <=10 THEN 1 ELSE 0 END)
        FROM RAW r
        GROUP BY r.TruongTHCS, r.Mon
    )
    SELECT 
        pb.TruongTHCS,
        pb.Mon,
        t.TongTS, 
        t.TSNu,
        pb.DuThi,
        pb.BoThi,
        pb.M0_3,   TyLe0_3  = CAST(pb.M0_3  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M3_5,   TyLe3_5  = CAST(pb.M3_5  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M5_7,   TyLe5_7  = CAST(pb.M5_7  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M7_9,   TyLe7_9  = CAST(pb.M7_9  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M9_10,  TyLe9_10 = CAST(pb.M9_10 * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        t.Dau,     TyLeDau  = CAST(t.Dau   * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        t.Hong,    TyLeHong = CAST(t.Hong  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2))
    FROM PHANBO pb
    INNER JOIN TONG t ON t.TruongTHCS = pb.TruongTHCS
    ORDER BY pb.TruongTHCS,
             CASE pb.Mon WHEN N'Văn' THEN 1 WHEN N'A.Văn' THEN 2 ELSE 3 END;
END



-- Phần sửa lại phân quyền admin, cán bộ trường cũng như thư ký.
SET XACT_ABORT ON;
BEGIN TRAN;

-- 0) Bỏ unique index/constraint cũ trên MaTruong (nếu có)
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql = @sql + N'DROP INDEX [' + i.name + N'] ON dbo.NGUOI_DUNG;'
FROM sys.indexes i
JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
JOIN sys.columns c        ON c.object_id=ic.object_id AND c.column_id=ic.column_id
WHERE i.object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
  AND i.is_unique = 1
  AND i.is_primary_key = 0
  AND c.name = N'MaTruong'
GROUP BY i.name;

IF @sql <> N'' EXEC(@sql);



-- Không còn unique trên MaTruong
SELECT kc.name, kc.type_desc
FROM sys.key_constraints kc
WHERE kc.parent_object_id = OBJECT_ID('dbo.NGUOI_DUNG')
  AND kc.type='UQ';


-- Kiểm tra chính xác tên constraint trước
ALTER TABLE dbo.NGUOI_DUNG
DROP CONSTRAINT UQ__NGUOI_DU__56F68C29556E4ED;


/* 1) GỠ UNIQUE chỉ trên cột MaTruong của bảng NGUOI_DUNG (nếu có) */
DECLARE @sql NVARCHAR(MAX) = N'';

;WITH UQ AS (
  SELECT kc.name AS uq_name
  FROM sys.key_constraints kc
  JOIN sys.index_columns ic
    ON ic.object_id = kc.parent_object_id
   AND ic.index_id   = kc.unique_index_id
  JOIN sys.columns c
    ON c.object_id   = ic.object_id
   AND c.column_id   = ic.column_id
  WHERE kc.parent_object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
    AND kc.[type] = 'UQ'
    AND c.name = N'MaTruong'
)
SELECT @sql = STRING_AGG(N'ALTER TABLE dbo.NGUOI_DUNG DROP CONSTRAINT [' + uq_name + N'];', CHAR(10))
FROM UQ;

IF @sql IS NOT NULL AND LEN(@sql) > 0
BEGIN
    PRINT N'Dropping UNIQUE constraint(s) on MaTruong…';
    EXEC sp_executesql @sql;
END
ELSE
    PRINT N'Không tìm thấy UNIQUE CONSTRAINT trên MaTruong.';

/* Phòng khi có ai đó tạo UNIQUE INDEX trực tiếp trên MaTruong (không qua CONSTRAINT) */
SET @sql = N'';
;WITH UX AS (
  SELECT i.name AS idx_name
  FROM sys.indexes i
  JOIN sys.index_columns ic
    ON ic.object_id = i.object_id AND ic.index_id = i.index_id
  JOIN sys.columns c
    ON c.object_id = ic.object_id AND c.column_id = ic.column_id
  WHERE i.object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
    AND i.is_unique = 1
    AND i.is_primary_key = 0
    AND c.name = N'MaTruong'
)
SELECT @sql = STRING_AGG(N'DROP INDEX [' + idx_name + N'] ON dbo.NGUOI_DUNG;', CHAR(10))
FROM UX;

IF @sql IS NOT NULL AND LEN(@sql) > 0
BEGIN
    PRINT N'Dropping UNIQUE index(es) on MaTruong…';
    EXEC sp_executesql @sql;
END
ELSE
    PRINT N'Không tìm thấy UNIQUE INDEX độc lập trên MaTruong.';

/* Kiểm tra lại: không còn unique liên quan MaTruong */
PRINT N'== Kiểm tra sau khi gỡ ==';
SELECT kc.name, kc.type_desc
FROM sys.key_constraints kc
JOIN sys.index_columns ic
  ON ic.object_id = kc.parent_object_id AND ic.index_id = kc.unique_index_id
JOIN sys.columns c
  ON c.object_id = ic.object_id AND c.column_id = ic.column_id
WHERE kc.parent_object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
  AND kc.[type] = 'UQ'
  AND c.name = N'MaTruong';

SELECT i.name, i.is_unique
FROM sys.indexes i
JOIN sys.index_columns ic
  ON ic.object_id = i.object_id AND ic.index_id = i.index_id
JOIN sys.columns c
  ON c.object_id = ic.object_id AND c.column_id = ic.column_id
WHERE i.object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
  AND i.is_unique = 1
  AND i.is_primary_key = 0
  AND c.name = N'MaTruong';

/* 2) THÊM CỘT VaiTro (nếu chưa có) – không áp ràng buộc mỗi trường chỉ 1 người */
IF COL_LENGTH('dbo.NGUOI_DUNG','VaiTro') IS NULL
BEGIN
    ALTER TABLE dbo.NGUOI_DUNG
      ADD VaiTro NVARCHAR(20) NOT NULL
          CONSTRAINT DF_ND_VaiTro DEFAULT (N'CanBoTruong');
    PRINT N'Đã thêm cột VaiTro.';
END
ELSE
    PRINT N'Cột VaiTro đã tồn tại.';


-- Thêm tài khoản admin để kiểm tra

-- 1) Cho phép MaTruong = NULL (admin sở không thuộc trường nào)
IF EXISTS (
    SELECT 1
    FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'dbo.NGUOI_DUNG')
      AND name = N'MaTruong'
      AND is_nullable = 0
)
BEGIN
    ALTER TABLE dbo.NGUOI_DUNG ALTER COLUMN MaTruong NVARCHAR(5) NULL;
END
GO

-- 2) Thêm hoặc cập nhật tài khoản admin sở
IF EXISTS (SELECT 1 FROM dbo.NGUOI_DUNG WHERE TenDangNhap = N'adminso')
BEGIN
    UPDATE dbo.NGUOI_DUNG
    SET MatKhau  = N'123456',           -- demo thôi, sau nhớ đổi sang mật khẩu mạnh
        HoTen    = N'Quản trị Sở',
        MaTruong = NULL,
        VaiTro   = N'AdminSo'
    WHERE TenDangNhap = N'adminso';
END
ELSE
BEGIN
    INSERT INTO dbo.NGUOI_DUNG (TenDangNhap, MatKhau, HoTen, MaTruong, VaiTro)
    VALUES (N'adminso', N'123456', N'Quản trị Sở', NULL, N'AdminSo');
END
GO


-- Phần danh sách vắng, sửa kiểu dữ liệu cho phép nhập vắng:

ALTER TABLE dbo.HOC_SINH
  ALTER COLUMN DiemToan NVARCHAR(20) NULL;
ALTER TABLE dbo.HOC_SINH
  ALTER COLUMN DiemVan  NVARCHAR(20) NULL;
ALTER TABLE dbo.HOC_SINH
  ALTER COLUMN DiemAnh  NVARCHAR(20) NULL;


ALTER TABLE dbo.HOC_SINH DROP CONSTRAINT CK_HS_DiemToan_TextOrNum;
ALTER TABLE dbo.HOC_SINH DROP CONSTRAINT CK_HS_DiemVan_TextOrNum;
ALTER TABLE dbo.HOC_SINH DROP CONSTRAINT CK_HS_DiemAnh_TextOrNum;

SELECT
  dbo.fn_IsVang(N'v')        AS V1,
  dbo.fn_IsVang(N'Vắng')     AS V2,
  dbo.fn_IsVang(N'vắng thi') AS V3;


CREATE OR ALTER FUNCTION dbo.fn_TryToDec_10 (@s NVARCHAR(50))
RETURNS DECIMAL(4,2)
AS
BEGIN
    RETURN TRY_CONVERT(DECIMAL(4,2), REPLACE(@s, ',', '.'));
END
GO

CREATE OR ALTER FUNCTION dbo.fn_IsVang (@s NVARCHAR(50))
RETURNS BIT
AS
BEGIN
    IF @s IS NULL RETURN 0;

    DECLARE @t NVARCHAR(50) = UPPER(LTRIM(RTRIM(@s)));

    -- bỏ khoảng trắng thường & NBSP + vài ký tự hay gõ kèm
    SET @t = REPLACE(@t, N' ', N'');
    SET @t = REPLACE(@t, NCHAR(160), N'');   -- NBSP
    SET @t = REPLACE(@t, NCHAR(9),  N'');    -- TAB
    SET @t = REPLACE(@t, NCHAR(13), N'');    -- CR
    SET @t = REPLACE(@t, NCHAR(10), N'');    -- LF
    SET @t = REPLACE(@t, N'.', N'');
    SET @t = REPLACE(@t, N'-', N'');
    SET @t = REPLACE(@t, N'_', N'');

    IF @t COLLATE Vietnamese_CI_AI IN
       (N'V'        COLLATE Vietnamese_CI_AI,
        N'VANG'     COLLATE Vietnamese_CI_AI,
        N'VANGTHI'  COLLATE Vietnamese_CI_AI)
        RETURN 1;

    RETURN 0;
END
GO


-- CHECK cho từng môn
ALTER TABLE dbo.HOC_SINH WITH NOCHECK
ADD CONSTRAINT CK_HS_DiemToan_TextOrNum
CHECK (
    DiemToan IS NULL
    OR dbo.fn_IsVang(DiemToan) = 1
    OR (dbo.fn_TryToDec_10(DiemToan) BETWEEN 0 AND 10)
);

ALTER TABLE dbo.HOC_SINH WITH NOCHECK
ADD CONSTRAINT CK_HS_DiemVan_TextOrNum
CHECK (
    DiemVan IS NULL
    OR dbo.fn_IsVang(DiemVan) = 1
    OR (dbo.fn_TryToDec_10(DiemVan) BETWEEN 0 AND 10)
);

ALTER TABLE dbo.HOC_SINH WITH NOCHECK
ADD CONSTRAINT CK_HS_DiemAnh_TextOrNum
CHECK (
    DiemAnh IS NULL
    OR dbo.fn_IsVang(DiemAnh) = 1
    OR (dbo.fn_TryToDec_10(DiemAnh) BETWEEN 0 AND 10)
);

-- Cập nhật lại sp thêm học sinh

CREATE OR ALTER PROCEDURE dbo.sp_CapNhatDiem
    @MaBaoDanh        NVARCHAR(8),
    @DiemToan         NVARCHAR(20) = NULL,
    @DiemVan          NVARCHAR(20) = NULL,
    @DiemAnh          NVARCHAR(20) = NULL,
    @DiemKhuyenKhich  DECIMAL(4,2) = NULL,
    @DiemUuTien       DECIMAL(4,2) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Chuẩn hoá chuỗi
    SET @DiemToan = NULLIF(LTRIM(RTRIM(@DiemToan)), N'');
    SET @DiemVan  = NULLIF(LTRIM(RTRIM(@DiemVan)),  N'');
    SET @DiemAnh  = NULLIF(LTRIM(RTRIM(@DiemAnh)),  N'');

    DECLARE
        @isVToan BIT = dbo.fn_IsVang(@DiemToan),
        @isVVan  BIT = dbo.fn_IsVang(@DiemVan),
        @isVAnh  BIT = dbo.fn_IsVang(@DiemAnh),

        @nToan DECIMAL(4,2) = CASE WHEN dbo.fn_IsVang(@DiemToan)=1 OR @DiemToan IS NULL
                                   THEN NULL ELSE dbo.fn_TryToDec_10(@DiemToan) END,
        @nVan  DECIMAL(4,2) = CASE WHEN dbo.fn_IsVang(@DiemVan)=1  OR @DiemVan  IS NULL
                                   THEN NULL ELSE dbo.fn_TryToDec_10(@DiemVan)  END,
        @nAnh  DECIMAL(4,2) = CASE WHEN dbo.fn_IsVang(@DiemAnh)=1  OR @DiemAnh  IS NULL
                                   THEN NULL ELSE dbo.fn_TryToDec_10(@DiemAnh)  END;

    -- Validate miền 0..10 cho các môn có số
    IF (@nToan IS NOT NULL AND (@nToan < 0 OR @nToan > 10)) OR
       (@nVan  IS NOT NULL AND (@nVan  < 0 OR @nVan  > 10)) OR
       (@nAnh  IS NOT NULL AND (@nAnh  < 0 OR @nAnh  > 10))
    BEGIN
        RAISERROR(N'Điểm phải trong khoảng 0..10 hoặc nhập "Vắng".', 16, 1);
        RETURN;
    END

    DECLARE @AllNull BIT =
        CASE WHEN @nToan IS NULL AND @nVan IS NULL AND @nAnh IS NULL THEN 1 ELSE 0 END;

    DECLARE @DiemTong DECIMAL(5,2) =
        CASE
            WHEN @AllNull = 1 THEN NULL
            ELSE ISNULL(@nToan,0) + ISNULL(@nVan,0) + ISNULL(@nAnh,0)
               + ISNULL(@DiemKhuyenKhich,0) + ISNULL(@DiemUuTien,0)
        END;

    UPDATE dbo.HOC_SINH
    SET DiemToan        = @DiemToan,
        DiemVan         = @DiemVan,
        DiemAnh         = @DiemAnh,
        DiemKhuyenKhich = @DiemKhuyenKhich,
        DiemUuTien      = @DiemUuTien,
        DiemTong        = @DiemTong
    WHERE MaSoBaoDanh   = @MaBaoDanh;

    SELECT * FROM dbo.HOC_SINH WHERE MaSoBaoDanh = @MaBaoDanh;
END
GO


-- thống kê điểm theo môn

CREATE OR ALTER PROCEDURE sp_ThongKeDiemTheoMon
    @MaTruong NVARCHAR(5),
    @MaDot    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Raw AS (
        SELECT N'Toán' AS Mon, hs.DiemToan AS DiemStr,
               dbo.fn_IsVang(hs.DiemToan) AS IsVang,
               dbo.fn_TryToDec_10(hs.DiemToan) AS DiemNum
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot

        UNION ALL
        SELECT N'Văn', hs.DiemVan, dbo.fn_IsVang(hs.DiemVan), dbo.fn_TryToDec_10(hs.DiemVan)
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot

        UNION ALL
        SELECT N'Anh', hs.DiemAnh, dbo.fn_IsVang(hs.DiemAnh), dbo.fn_TryToDec_10(hs.DiemAnh)
        FROM HOC_SINH hs WHERE hs.MaTruong=@MaTruong AND hs.MaDot=@MaDot
    )
    SELECT Mon, Muc = CAST(DiemNum AS DECIMAL(4,2)), SoLuong = COUNT(*), BoThi = 0
    FROM Raw
    WHERE IsVang = 0 AND DiemNum IS NOT NULL   -- chỉ những bạn có điểm số
    GROUP BY Mon, CAST(DiemNum AS DECIMAL(4,2))

    UNION ALL

    SELECT Mon, Muc = NULL, SoLuong = COUNT(*), BoThi = 1
    FROM Raw
    WHERE IsVang = 1                            -- đếm vắng
    GROUP BY Mon

    ORDER BY Mon, BoThi, Muc;
END
GO


-- Sửa lại phần điểm thống kê theo trường THCS:

CREATE OR ALTER PROCEDURE sp_ThongKeTheoTHCS
    @MaTruong NVARCHAR(5),
    @MaDot    NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH HS AS (
        SELECT *
        FROM HOC_SINH
        WHERE MaTruong = @MaTruong AND MaDot = @MaDot
    ),
    TONG AS (
        SELECT 
            TruongTHCS,
            TongTS = COUNT(*),
            TSNu   = SUM(CASE WHEN GioiTinh = N'Nữ' THEN 1 ELSE 0 END),
            Dau    = SUM(CASE WHEN TrangThai = N'TrungTuyen' THEN 1 ELSE 0 END),
            Hong   = SUM(CASE WHEN TrangThai = N'Rot' THEN 1 ELSE 0 END)
        FROM HS
        GROUP BY TruongTHCS
    ),
    RAW AS (
        SELECT TruongTHCS, N'Văn' AS Mon,
               dbo.fn_IsVang(DiemVan) AS IsVang,
               dbo.fn_TryToDec_10(DiemVan) AS Diem
        FROM HS
        UNION ALL
        SELECT TruongTHCS, N'Anh' AS Mon,
               dbo.fn_IsVang(DiemAnh),
               dbo.fn_TryToDec_10(DiemAnh)
        FROM HS
        UNION ALL
        SELECT TruongTHCS, N'Toán' AS Mon,
               dbo.fn_IsVang(DiemToan),
               dbo.fn_TryToDec_10(DiemToan)
        FROM HS
    ),
    PHANBO AS (
        SELECT 
            r.TruongTHCS, r.Mon,
            DuThi = SUM(CASE WHEN r.IsVang=0 AND r.Diem IS NOT NULL THEN 1 ELSE 0 END),
            BoThi = SUM(CASE WHEN r.IsVang=1 THEN 1 ELSE 0 END),

            M0_3  = SUM(CASE WHEN r.IsVang=0 AND r.Diem >= 0  AND r.Diem < 3  THEN 1 ELSE 0 END),
            M3_5  = SUM(CASE WHEN r.IsVang=0 AND r.Diem >= 3  AND r.Diem < 5  THEN 1 ELSE 0 END),
            M5_7  = SUM(CASE WHEN r.IsVang=0 AND r.Diem >= 5  AND r.Diem < 7  THEN 1 ELSE 0 END),
            M7_9  = SUM(CASE WHEN r.IsVang=0 AND r.Diem >= 7  AND r.Diem < 9  THEN 1 ELSE 0 END),
            M9_10 = SUM(CASE WHEN r.IsVang=0 AND r.Diem >= 9  AND r.Diem <=10 THEN 1 ELSE 0 END)
        FROM RAW r
        GROUP BY r.TruongTHCS, r.Mon
    )
    SELECT 
        pb.TruongTHCS,
        pb.Mon,
        t.TongTS, 
        t.TSNu,
        pb.DuThi,
        pb.BoThi,
        pb.M0_3,   TyLe0_3  = CAST(pb.M0_3  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M3_5,   TyLe3_5  = CAST(pb.M3_5  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M5_7,   TyLe5_7  = CAST(pb.M5_7  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M7_9,   TyLe7_9  = CAST(pb.M7_9  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        pb.M9_10,  TyLe9_10 = CAST(pb.M9_10 * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        t.Dau,     TyLeDau  = CAST(t.Dau   * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2)),
        t.Hong,    TyLeHong = CAST(t.Hong  * 100.0 / NULLIF(t.TongTS,0) AS DECIMAL(6,2))
    FROM PHANBO pb
    INNER JOIN TONG t ON t.TruongTHCS = pb.TruongTHCS
    ORDER BY pb.TruongTHCS,
             CASE pb.Mon WHEN N'Văn' THEN 1 WHEN N'Anh' THEN 2 ELSE 3 END;
END
GO


-- Thống kê học sinh vắng theo đợt:
CREATE OR ALTER PROCEDURE sp_ThongKeVangThi
    @MaDot    NVARCHAR(10),
    @MaTruong NVARCHAR(5) = NULL     -- NULL = toàn đợt; có mã = theo trường
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Base AS (
        SELECT *
        FROM HOC_SINH
        WHERE MaDot = @MaDot
          AND (@MaTruong IS NULL OR MaTruong = @MaTruong)
    ),
    Flags AS (
        SELECT 
            MaHocSinh, MaSoBaoDanh, Ho, Ten, TruongTHCS, PhongThi, MaTruong,
            VangToan = dbo.fn_IsVang(DiemToan),
            VangVan  = dbo.fn_IsVang(DiemVan),
            VangAnh  = dbo.fn_IsVang(DiemAnh)
        FROM Base
    )
    -- RS1: Danh sách vắng
    SELECT 
        f.MaHocSinh, f.MaSoBaoDanh, f.Ho, f.Ten, f.TruongTHCS, f.PhongThi, f.MaTruong,
        f.VangToan, f.VangVan, f.VangAnh,
        VangAny = CASE WHEN f.VangToan=1 OR f.VangVan=1 OR f.VangAnh=1 THEN 1 ELSE 0 END
    FROM Flags f
    WHERE f.VangToan=1 OR f.VangVan=1 OR f.VangAnh=1
    ORDER BY f.MaTruong, f.PhongThi, f.MaSoBaoDanh;

    -- RS2: Tổng hợp
    SELECT 
        TongVangAny = SUM(CASE WHEN f.VangToan=1 OR f.VangVan=1 OR f.VangAnh=1 THEN 1 ELSE 0 END),
        VangToan    = SUM(CASE WHEN f.VangToan=1 THEN 1 ELSE 0 END),
        VangVan     = SUM(CASE WHEN f.VangVan =1 THEN 1 ELSE 0 END),
        VangAnh     = SUM(CASE WHEN f.VangAnh =1 THEN 1 ELSE 0 END)
    FROM Flags f;
END
GO


SELECT dbo.fn_IsVang(N'v'), dbo.fn_IsVang(N'Vắng'), dbo.fn_IsVang(N'vắng thi');

SELECT TOP 50 MaHocSinh, MaDot, MaTruong, DiemToan, DiemVan, DiemAnh
FROM HOC_SINH
WHERE MaTruong = '05' AND MaDot = '2025'
ORDER BY MaHocSinh DESC;

