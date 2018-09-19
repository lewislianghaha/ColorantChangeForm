--创建色母对照表
CREATE TABLE ColorantContrast(
								AkzoColorant nvarchar(100),
								Colorant nvarchar(100),
								Number decimal(32,12),
								CredateDate DATETIME,
								TypeId INT
					)
GO
CREATE INDEX ColorantIndex ON ColorantContrast (Colorant)
GO
CREATE INDEX AkzoColorantIndex on COlorantContrast(AkzoColorant)
GO

--对照色号对照表
CREATE TABLE ColorCodeContrast(
								AkzoCode nvarchar(100),
								InnerCode nvarchar(100)
					)
GO
CREATE INDEX AkzoCodeIndex ON ColorCodeContrast (AkzoCode)
GO
CREATE INDEX InnerCodeIndex on ColorCodeContrast(InnerCode)
GO
