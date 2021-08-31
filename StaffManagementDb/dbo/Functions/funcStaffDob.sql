CREATE FUNCTION funcStaffDob (@age INT)
RETURNS DATE

BEGIN
	DECLARE @dob DATE

	SET @dob = DATEADD(yy, - @age, getdate())

	RETURN @dob
END
