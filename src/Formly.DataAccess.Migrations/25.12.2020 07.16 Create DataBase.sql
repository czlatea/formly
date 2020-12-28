CREATE TABLE dbo.Template
(
  Id BIGINT NOT NULL IDENTITY(1,1),
  ExternalId NVARCHAR(50) NOT NULL,
  Name NVARCHAR(255) NOT NULL,
  Description NVARCHAR(255),
  IsActive BIT NOT NULL,
  IsPrivate BIT NOT NULL,
  Content NVARCHAR(MAX) NOT NULL,
  CreatedOn DATETIME2 NOT NULL,
  LastUpdatedOn DATETIME2,
  CONSTRAINT PK_Template_ID PRIMARY KEY (Id)
)

INSERT INTO dbo.Template(ExternalId,Name,Description,IsActive,IsPrivate,Content,CreatedOn) 
VALUES('51db571a-d871-4779-8f1b-fed3a3fdfaf0','Contract Comodant','Contract Comodant',1,0,'Please fill in the required fields: First Name:**{{FirstName}}** Last Name:**{{LastName}}**',GETDATE());