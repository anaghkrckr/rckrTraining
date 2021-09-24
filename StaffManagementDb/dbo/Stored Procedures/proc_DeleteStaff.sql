/****** Script for SelectTopNRows command from SSMS  ******/

CREATE PROCEDURE [dbo].[proc_DeleteStaff] @Id int
as
BEGIN

	
		DELETE FROM Staff WHERE Id=@Id
	

END