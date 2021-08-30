using StaffManagementApp.Database;
using System;

namespace StaffManagementApp.staffs {

    public static class StaffHelper {

        public static Staff StaffAdd(string staffType) {
            switch (staffType) {
                case nameof(Teacher):
                    Staff teacher = new Teacher();
                    teacher.AddStaff(staffType);
                    return teacher;

                case nameof(Administrator):
                    Staff administrator = new Administrator();
                    administrator.AddStaff(staffType);
                    return administrator;

                case nameof(Support):
                    Staff support = new Support();
                    support.AddStaff(staffType);
                    return support;
            }
            return null;
        }

        public static void StaffUpdate(string staffType) {

            Staff staff = GetStaff();
            if (staff != null) {

                if (staff.StaffType != staffType) {
                    Console.WriteLine("ID not found");
                    return;
                }
                staff = staff.UpdateStaff();
                DatabasManagemantSQL.DatabaseUpdateStaff(staff);
            }

        }

        public static void StaffView(string staffType) {
            Staff staff = GetStaff();
            if (staff != null) {
                if (staff.StaffType != staffType) {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }
                staff.ViewStaff();
            }


        }

        private static Staff GetStaff() {
            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            Staff staff = DatabasManagemantSQL.DatabaseGetStaff(StaffId);
            return staff;
        }

        public static void DeleteStaff(string staffType) {
            try {
                Staff staff = GetStaff();
                if (staff.StaffType != staffType) {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }
                DatabasManagemantSQL.DatabaseDeleteStaff(staff.StaffId);
                Console.WriteLine("Deleted");
            }
            catch (Exception) {
                Console.WriteLine("Staff id not found");
            }
        }

    }
}