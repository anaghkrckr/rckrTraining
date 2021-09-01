using StaffManagementApp.Database;
using System;

namespace StaffManagementApp.Staffs
{

    public static class StaffHelper
    {

        public static Staff StaffAdd(string staffType, DatabaseManagementSQL dbHelper)
        {
            switch (staffType)
            {
                case nameof(Teacher):
                    Staff teacher = new Teacher();
                    teacher.AddStaff(staffType,dbHelper);
                    return teacher;

                case nameof(Administrator):
                    Staff administrator = new Administrator();
                    administrator.AddStaff(staffType,dbHelper);
                    return administrator;

                case nameof(Support):
                    Staff support = new Support();
                    support.AddStaff(staffType,dbHelper);
                    return support;
            }
            return null;
        }

        public static void StaffUpdate(string staffType, DatabaseManagementSQL dbHelper)
        {
            Staff staff = GetStaff(dbHelper);
            if (staff != null)
            {
                if (staff.StaffType != staffType)
                {
                    Console.WriteLine("ID not found");
                    return;
                }
                staff = staff.UpdateStaff();
                dbHelper.DatabaseUpdateStaff(staff);
            }
        }

        public static void StaffView(string staffType, DatabaseManagementSQL dbHelper)
        {
            Staff staff = GetStaff(dbHelper);
            if (staff != null)
            {
                if (staff.StaffType != staffType)
                {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }
                staff.ViewStaff();
            }


        }

        private static Staff GetStaff(DatabaseManagementSQL dbHelper)
        {
            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            Staff staff = dbHelper.DatabaseGetStaff(StaffId);
            return staff;
        }

        public static void DeleteStaff(string staffType, DatabaseManagementSQL dbHelper)
        {
            try
            {
                Staff staff = GetStaff(dbHelper);
                if (staff.StaffType != staffType)
                {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }
                dbHelper.DatabaseDeleteStaff(staff.StaffId);
                Console.WriteLine("Deleted");
            }
            catch (Exception)
            {
                Console.WriteLine("Staff id not found");
            }
        }

    }
}