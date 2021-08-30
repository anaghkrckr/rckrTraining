CREATE TABLE [dbo].[Staff] (
    [Id]              INT           IDENTITY (0, 1) NOT NULL,
    [StaffName]       NVARCHAR (30) NULL,
    [StaffAge]        INT           NULL,
    [StaffType]       VARCHAR (30)  NULL,
    [StaffDepartment] VARCHAR (30)  NULL,
    [DOB]             DATE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE TRIGGER [dbo].[trgAddUpdateDOB]
ON [dbo].[Staff]
INSTEAD OF INSERT
AS
BEGIN
	INSERT INTO Staff
	SELECT StaffName,StaffAge,StaffType,StaffDepartment,dbo.funcStaffDOB(StaffAge) FROM Inserted
END