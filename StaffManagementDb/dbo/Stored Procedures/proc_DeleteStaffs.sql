CREATE PROCEDURE [dbo].[proc_DeleteStaffs] (@tableStaff Staffs readonly)
as
BEGIN
	MERGE Staff AS TARGET
	USING @tableStaff AS SOURCE
	ON (TARGET.Id=SOURCE.Id)
	WHEN MATCHED
	THEN DELETE;
END