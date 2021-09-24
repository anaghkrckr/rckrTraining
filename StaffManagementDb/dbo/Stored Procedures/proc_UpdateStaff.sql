CREATE PROCEDURE [dbo].[proc_UpdateStaff] @Id int,@StaffName varchar(30),@StaffAge int,@StaffType varchar(20),@StaffDepartment varchar(50)
as
BEGIN
	BEGIN TRANSACTION
		IF EXISTS (SELECT Id FROM Staff WHERE Id=@Id)
			BEGIN
				UPDATE Staff
					SET StaffName=@StaffName,StaffAge=@StaffAge,StaffType=@StaffType,StaffDepartment=@StaffDepartment,DOB=dbo.funcStaffDob(@StaffAge) WHERE Id=@Id
				Commit TRANSACTION
			END
		ELSE
			BEGIN
				;THROW 51000, 'The record does not exist.', 1
				rollback TRANSACTION
			END
END