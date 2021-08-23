using System;
using System.Collections.Generic;

namespace StaffManagementApp.staffs {

    public class StaffHelper {

        public static Staff StaffAdd(string staffType, int StaffId, List<Staff> staffs) {
            switch (staffType) {
                case nameof(Teacher):
                    Staff teacher = new Teacher { StaffId = StaffId };
                    teacher.AddStaff(staffs, staffType);
                    return teacher;

                case nameof(Administrator):
                    Staff administrator = new Administrator { StaffId = StaffId };
                    administrator.AddStaff(staffs, staffType);
                    return administrator;

                case nameof(Support):
                    Staff support = new Support { StaffId = StaffId };
                    support.AddStaff(staffs, staffType);
                    return support;
            }
            return null;
        }

        public static void StaffUpdate(List<Staff> staffs, string staffType) {
            var index = GetStaff(staffs);
            Staff staff = staffs[index];
            if (staff.StaffType != staffType) {
                Console.WriteLine("ID not found");
                return;
            }
            staff = staff.UpdateStaff();
            staffs[index] = staff;

        }

        public static void StaffView(List<Staff> staffs, string staffType) {
            var index = GetStaff(staffs);
            Staff staff = staffs[index];
            if (staff.StaffType != staffType) {
                Console.WriteLine("ID not found");
                return;
            }
            staff.ViewStaff();
        }

        private static int GetStaff(List<Staff> staffs) {
            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            var index = staffs.FindIndex(c => c.StaffId == StaffId);
            return index;
        }

        public static void DeleteStaff(List<Staff> staffs, string staffType) {
            var index = GetStaff(staffs);
            if (staffs[index].StaffType != staffType) {
                Console.WriteLine("ID not found");
                return;
            }
            staffs.RemoveAt(index);
            Console.WriteLine("Deleted");
        }

    }
}