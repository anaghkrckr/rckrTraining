
CREATE PROCEDURE [dbo].[procGetStaff] @Id int
as
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
				IF EXISTS (SELECT Id FROM Staff WHERE Id=@Id)
					BEGIN
						SELECT Id,StaffName,StaffAge,StaffType,StaffDepartment FROM Staff WHERE Id=@Id
						Commit TRANSACTION
					END
				ELSE
					BEGIN
						;THROW 51000, 'The record does not exist Get staff.', 1
					END
END TRY
BEGIN CATCH
	rollback TRANSACTION
	;THROW
END CATCH

END