using StaffManagementApp.Staffs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace StaffManagementApp.Database
{
    public class DatabaseManagementSQL
    {


        private static SqlConnection Connection;

        public DatabaseManagementSQL(string conString)
        {
            try
            {
                Connection = new SqlConnection(conString);
            }
            catch (Exception e)
            {
                return;
            }
        }



        public int DatabaseAddStaff(Staff staff)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                Connection.Open();
                command = AddParameters(staff, command);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "procAddStaff";
                int staffId = (int)command.ExecuteScalar();
                Console.WriteLine("Added to database");
                return staffId;
            }
            catch (SqlException e)
            {
                return 0;
            }
            finally
            {
                Connection.Close();
            }
        }

        public void DatabaseDeleteStaff(int staffId)
        {
            try
            {
                SqlCommand command = Connection.CreateCommand();
                Connection.Open();
                command.CommandText = @"procDeleteStaff";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = staffId;
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return;
            }
            finally
            {
                Connection.Close();
            }
        }

        public Staff DatabaseGetStaff(int staffId)
        {
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "procGetStaff";
            command.Parameters.Add("@Id", SqlDbType.Int).Value = staffId;
            Connection.Open();
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                Staff staff = null;
                while (reader.Read())
                {
                    staff = GetStaff(reader);
                }
                return staff;
            }
            catch (SqlException e)
            {
                return null;
            }
            finally
            {
                Connection.Close();
            }
        }



        public void DatabaseUpdateStaff(Staff staff)
        {
            SqlCommand command = Connection.CreateCommand();
            command = AddParameters(staff, command);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = staff.StaffId;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "procUpdateStaff";
            Connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return;
            }
            finally
            {
                Connection.Close();
            }
        }

        public List<Staff> DatabaseViewAll()
        {
            List<Staff> staffs = new List<Staff>();
            try
            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "procGetAll";
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Staff staff = GetStaff(reader);
                    if (staff != null)
                    {
                        staffs.Add(staff);
                    }
                }
                return staffs;
            }
            catch (SqlException e)
            {
                return null;
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


        private static Staff GetStaff(SqlDataReader reader)
        {
            Object[] values;
            values = new Object[reader.FieldCount];
            reader.GetValues(values);
            Staff staff = null;
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

        public void DatabaseAddBulk()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("StaffName", typeof(string));
            tbl.Columns.Add("StaffAge", typeof(int));
            tbl.Columns.Add("StaffType", typeof(string));
            tbl.Columns.Add("StaffDepartment", typeof(string));
            foreach (Staff staff in Program.staffs)
            {
                switch (staff.StaffType)
                {
                    case nameof(Teacher):
                        tbl.Rows.Add(staff.StaffName, staff.StaffAge, staff.StaffType, ((Teacher)staff).Subject);
                        break;

                    case nameof(Administrator):
                        tbl.Rows.Add(staff.StaffName, staff.StaffAge, staff.StaffType, ((Administrator)staff).AdministratorDepartment);

                        break;

                    case nameof(Support):
                        tbl.Rows.Add(staff.StaffName, staff.StaffAge, staff.StaffType, ((Support)staff).SupportDepartment);
                        break;
                }
            }
            SqlCommand command = Connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "procInsertStaffs";
            command.Parameters.AddWithValue("tableStaff", tbl);
            Connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                return;
            }
            finally
            {
                Connection.Close();
            }

        }

    }
}

