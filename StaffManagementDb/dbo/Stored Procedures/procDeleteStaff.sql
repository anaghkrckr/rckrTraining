/****** Script for SelectTopNRows command from SSMS  ******/

CREATE PROCEDURE [dbo].[procDeleteStaff] @Id int
as
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
		DELETE FROM Staff WHERE Id=@Id
	Commit TRANSACTION
END TRY
BEGIN CATCH
	rollback TRANSACTION
	;THROW
END CATCH
END