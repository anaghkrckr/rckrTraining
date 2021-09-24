CREATE PROCEDURE [dbo].[proc_AddStaff]
@StaffName nvarchar(30),
@StaffAge int,
@StaffType varchar(30),
@StaffDepartment varchar(30)
as
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			INSERT INTO Staff VALUES(@StaffName,@StaffAge,@StaffType,@StaffDepartment,NULL) ;

			SELECT CONVERT(int,@@IDENTITY);
		Commit TRANSACTION
	END TRY
	BEGIN CATCH
		rollback TRANSACTION
		;THROW
	END CATCH

END