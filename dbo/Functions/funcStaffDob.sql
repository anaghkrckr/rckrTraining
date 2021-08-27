CREATE FUNCTION funcStaffDob (@age int)
RETURNS date
BEGIN
	DECLARE @dob date
	SET @dob=DATEADD(yy,-@age,getdate())
	RETURN  @dob

END