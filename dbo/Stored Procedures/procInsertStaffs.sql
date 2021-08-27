CREATE PROCEDURE [dbo].[procInsertStaffs] (@tableStaff Staffs readonly)
as
begin
	INSERT INTO Staff SELECT StaffName,StaffAge,StaffType,Staffdepartment,NULL FROM @tableStaff
END