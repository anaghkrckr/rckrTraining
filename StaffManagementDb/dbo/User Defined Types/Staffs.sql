CREATE TYPE [dbo].[Staffs] AS TABLE (
    [Id]              INT           NOT NULL,
    [StaffName]       NVARCHAR (30) NULL,
    [StaffAge]        INT           NULL,
    [StaffType]       VARCHAR (30)  NULL,
    [StaffDepartment] VARCHAR (30)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC));



