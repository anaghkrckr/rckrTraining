using StaffManagementApp.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace StaffManagementApp.staffs {

    public class StaffHelper {

        public static Staff StaffAdd(string staffType, int StaffId, List<Staff> staffs) {
            switch (staffType) {
                case nameof(Teacher):
                    Staff teacher = new Teacher { StaffId = StaffId };
                    teacher.AddStaff(staffs, staffType);
                    Sql.DatabaseAddStaff(teacher);
                    return teacher;

                case nameof(Administrator):
                    Staff administrator = new Administrator { StaffId = StaffId };
                    administrator.AddStaff(staffs, staffType);
                    Sql.DatabaseAddStaff(administrator);
                    return administrator;

                case nameof(Support):
                    Staff support = new Support { StaffId = StaffId };
                    support.AddStaff(staffs, staffType);
                    Sql.DatabaseAddStaff(support);
                    return support;
            }
            return null;
        }

        public static void StaffUpdate(List<Staff> staffs, string staffType) {
            //var index = GetStaff(staffs);
            //Staff staff = staffs[index];

            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            
            Staff staff = Sql.DatabaseGetStaff(StaffId);
            if (staff != null) {

                if (staff.StaffType != staffType) {
                    Console.WriteLine("ID not found");
                    return;
                }
            staff = staff.UpdateStaff();
            Sql.DatabaseUpdateStaff(staff);
             }
            

            
            //staffs[index] = staff;

        }

        public static void StaffView(List<Staff> staffs, string staffType) {
            //var index = GetStaff(staffs);
            //Staff staff = staffs[index];


            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
          
            Staff staff=Sql.DatabaseGetStaff(StaffId);
            if (staff != null) {
                if (staff.StaffType != staffType) {
                    Console.WriteLine("ID not found");
                    return;
                }
                staff.ViewStaff();
            }
            
           
        }

        private static int GetStaff(List<Staff> staffs) {
            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            Sql.DatabaseGetStaff(StaffId);
            var index = staffs.FindIndex(c => c.StaffId == StaffId);
            return index;
        }

        public static void DeleteStaff(List<Staff> staffs, string staffType) {
            try {
                var index = GetStaff(staffs);
                if (staffs[index].StaffType != staffType) {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }
                Sql.DatabaseDeleteStaff(staffs[index].StaffId);
                staffs.RemoveAt(index);
                Console.WriteLine("Deleted");
            }
            catch (Exception) {
                Console.WriteLine("Staff id not found");
            }
        }

    }
}