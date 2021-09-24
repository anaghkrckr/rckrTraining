
CREATE PROCEDURE [dbo].[proc_GetStaff] @Id int
as
BEGIN
	
				IF EXISTS (SELECT Id FROM Staff WHERE Id=@Id)
					BEGIN
						SELECT Id,StaffName,StaffAge,StaffType,StaffDepartment FROM Staff WHERE Id=@Id
						
					END
				ELSE
					BEGIN
						;THROW 51000, 'The record does not exist.', 1
						
					END


END