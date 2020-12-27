CREATE TABLE dbo.Template
(
  Id BIGINT NOT NULL IDENTITY(1,1),
  ExternalId NVARCHAR(50) NOT NULL,
  Name NVARCHAR(255) NOT NULL,
  Description NVARCHAR(255),
  IsActive BIT NOT NULL,
  Content NVARCHAR(MAX) NOT NULL,
  CONSTRAINT PK_Template_ID PRIMARY KEY (Id)
)

INSERT INTO dbo.Template(ExternalId,Name,Description,IsActive,Content) 
VALUES('51db571a-d871-4779-8f1b-fed3a3fdfaf0','Contract Comodant','Contract Comodant',1,'Please fill in the required fields: **First Name**:{{FirstName}} **Last Name**:{{LastName}}');