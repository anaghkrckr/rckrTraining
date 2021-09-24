using StaffManagementLibrary.Staffs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StaffManagementLibrary.DbHandler
{
    public class DatabaseSQLHandler : IDatabase
    {


        private static SqlConnection Connection;

        public DatabaseSQLHandler(string conString)
        {
            try
            {
                Connection = new SqlConnection(conString);
            }
            catch (Exception e)
            {
                throw;
            }
        }



        public int AddStaff(Staff staff)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                Connection.Open();
                command = AddParameters(staff, command);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_AddStaff";
                int staffId = (int)command.ExecuteScalar();
                return staffId;
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DeleteStaff(int staffId)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                Connection.Open();
                command.CommandText = @"proc_DeleteStaff";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = staffId;
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Staff GetStaff(int staffId)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_GetStaff";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = staffId;
            Connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                Staff staff = null;
                while (reader.Read())
                {
                    staff = CreateStaffObject(reader);
                }
                return staff;
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }



        public void UpdateStaff(Staff staff)
        {
            SqlCommand command = Connection.CreateCommand();
            command = AddParameters(staff, command);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = staff.StaffId;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_UpdateStaff";
            Connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<Staff> ViewAll()
        {
            List<Staff> staffs = new List<Staff>();
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "proc_GetAll";
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Staff staff = CreateStaffObject(reader);
                    if (staff != null)
                    {
                        staffs.Add(staff);
                    }
                }
                return staffs;
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void AddBulk(List<Staff> staffs)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Id", typeof(string));
            tbl.Columns.Add("StaffName", typeof(string));
            tbl.Columns.Add("StaffAge", typeof(int));
            tbl.Columns.Add("StaffType", typeof(string));
            tbl.Columns.Add("StaffDepartment", typeof(string));
            foreach (Staff staff in staffs)
            {
                switch (staff.StaffType)
                {
                    case nameof(Teacher):
                        tbl.Rows.Add(staff.StaffId, staff.StaffName, staff.StaffAge, staff.StaffType, ((Teacher)staff).Subject);
                        break;

                    case nameof(Administrator):
                        tbl.Rows.Add(staff.StaffId, staff.StaffName, staff.StaffAge, staff.StaffType, ((Administrator)staff).AdministratorDepartment);

                        break;

                    case nameof(Support):
                        tbl.Rows.Add(staff.StaffId, staff.StaffName, staff.StaffAge, staff.StaffType, ((Support)staff).SupportDepartment);
                        break;
                }
            }
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_InsertStaffs";
            command.Parameters.AddWithValue("tableStaff", tbl);
            Connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

        }
        
        public void DeleteBulk(List<Staff> staffs)
        {
            DataTable tbl = new DataTable();

            tbl.Columns.Add("Id", typeof(string));
            tbl.Columns.Add("StaffName", typeof(string));
            tbl.Columns.Add("StaffAge", typeof(int));
            tbl.Columns.Add("StaffType", typeof(string));
            tbl.Columns.Add("StaffDepartment", typeof(string));
            foreach (Staff staff in staffs)
            {
                switch (staff.StaffType)
                {
                    case nameof(Teacher):
                        tbl.Rows.Add(staff.StaffId,staff.StaffName, staff.StaffAge, staff.StaffType, ((Teacher)staff).Subject);
                        break;

                    case nameof(Administrator):
                        tbl.Rows.Add(staff.StaffId, staff.StaffName, staff.StaffAge, staff.StaffType, ((Administrator)staff).AdministratorDepartment);

                        break;

                    case nameof(Support):
                        tbl.Rows.Add(staff.StaffId, staff.StaffName, staff.StaffAge, staff.StaffType, ((Support)staff).SupportDepartment);
                        break;
                }
            }
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "proc_DeleteStaffs";
            command.Parameters.AddWithValue("tableStaff", tbl);
            Connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }

        }

        private static SqlCommand AddParameters(Staff staff, SqlCommand command)
        {
            command.Parameters.Add("@StaffName", SqlDbType.NVarChar, 30).Value = staff.StaffName;
            command.Parameters.Add("@StaffAge", SqlDbType.Int).Value = staff.StaffAge;
            command.Parameters.Add("@StaffType", SqlDbType.VarChar, 20).Value = staff.StaffType;
            switch (staff.StaffType)
            {
                case nameof(Teacher):
                    Teacher teacher = (Teacher)staff;
                    command.Parameters.Add("@StaffDepartment", SqlDbType.VarChar, 50).Value = teacher.Subject;
                    break;

                case nameof(Administrator):
                    Administrator administrator = (Administrator)staff;
                    command.Parameters.Add("@StaffDepartment", SqlDbType.VarChar, 50).Value = administrator.AdministratorDepartment;
                    break;

                case nameof(Support):
                    Support support = (Support)staff;
                    command.Parameters.Add("@StaffDepartment", SqlDbType.VarChar, 50).Value = support.SupportDepartment;
                    break;
            }
            return command;
        }


        private static Staff CreateStaffObject(SqlDataReader reader)
        {
            Object[] values;
            values = new Object[reader.FieldCount];
            reader.GetValues(values);
            Staff staff = null;
            string staffType = ((string)values[3]).ToLower();
            switch (values[3])
            {
                case nameof(Teacher):
                    staff = new Teacher();
                    ((Teacher)staff).Subject = (string)values[4];
                    break;

                case nameof(Administrator):
                    staff = new Administrator();
                    ((Administrator)staff).AdministratorDepartment = (string)values[4];
                    break;

                case nameof(Support):
                    staff = new Support();
                    ((Support)staff).SupportDepartment = (string)values[4];
                    break;
            }
            if (staff != null)
            {
                staff.StaffId = (int)values[0];
                staff.StaffName = (string)values[1];
                staff.StaffAge = (int)values[2];
                staff.StaffType = (string)values[3];
            }
            return staff;
        }


    }
}

