using System;
using System.Collections.Generic;


namespace training
{
    public class Support : Staff {
        private string supportDepartment;

        public string SupportDepartment {
            get => supportDepartment;
            set {
                if (value != "") {
                    supportDepartment = value;
                }
            }
        }

        public override void AddStaff(List<Staff> staffs, String staffType) {
            base.AddStaff(staffs, staffType);
            Console.WriteLine("Department:");
            this.SupportDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.SupportDepartment);
        }

        public static Support UpdateStaff(Support support) {
            Console.WriteLine("Update values");
            Console.WriteLine("Name:");
            support.StaffName = Console.ReadLine();
            Console.WriteLine("Age: (enter 0 if not change needed)");
            support.StaffAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Department:");
            support.SupportDepartment = Console.ReadLine(); ;
            return support;
        }

        public static void ViewStaffs(Support support) {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2} SUPPORT DEPARTMENT: {3} ",support.StaffId ,support.StaffName ,support.StaffAge ,support.SupportDepartment );
        }
    }
}