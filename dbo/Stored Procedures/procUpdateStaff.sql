CREATE PROCEDURE [dbo].[procUpdateStaff] @Id int,@StaffName varchar(30),@StaffAge int,@StaffType varchar(20),@StaffDepartment varchar(50)
as
BEGIN
BEGIN TRY
	BEGIN TRANSACTION
		IF EXISTS (SELECT Id FROM Staff WHERE Id=@Id)
			BEGIN
				UPDATE Staff
					SET StaffName=@StaffName,StaffAge=@StaffAge,StaffType=@StaffType,StaffDepartment=@StaffDepartment,DOB=dbo.funcStaffDob(@StaffAge) WHERE Id=@Id
				Commit TRANSACTION
			END
		ELSE
			BEGIN
				;THROW 51000, 'The record does not exist update staff.', 1
			END
END TRY
BEGIN CATCH
	rollback TRANSACTION
	;THROW
END CATCH


END