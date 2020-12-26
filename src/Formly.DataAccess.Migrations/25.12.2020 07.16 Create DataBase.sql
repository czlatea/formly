CREATE TABLE dbo.Template
(
  Id BIGINT NOT NULL IDENTITY(1,1),
  Name NVARCHAR(255) NOT NULL,
  Description NVARCHAR(255),
  IsActive BIT NOT NULL,
  Content NVARCHAR(MAX) NOT NULL,
  CONSTRAINT PK_Template_ID PRIMARY KEY (Id)
)

INSERT INTO dbo.Template(Name,Description,IsActive,Content) VALUES('Contract Comodant','Contract Comodant',1,'Please fill in the required fields: **First Name**:{{FirstName}} **Last Name**:{{LastName}}');