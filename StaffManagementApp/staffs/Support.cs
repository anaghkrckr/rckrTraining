using StaffManagementApp.Database;
using System;
using System.Linq;

namespace StaffManagementApp.staffs {

    public class Support : Staff {
        private string supportDepartment;

        public string SupportDepartment {
            get => supportDepartment;
            set {
                if (string.IsNullOrEmpty(SupportDepartment) && string.IsNullOrWhiteSpace(value)) {
                    throw new Exception($"{nameof(SupportDepartment)} cannot be empty");
                }
                else if (value.Any(char.IsDigit)) {
                    throw new Exception($"{nameof(SupportDepartment)} name should not contain digits");
                }
                else if (value.Length > 0 && value.Length <= 3) {
                    throw new Exception($"{nameof(SupportDepartment)} name length should be greater than 3");
                }
                else if (value.Length > 3) {
                    supportDepartment = value;
                }
            }
        }

        public override void AddStaff(string staffType) {
            base.AddStaff(staffType);
            do {
                try {
                    Console.WriteLine("Department:");
                    SupportDepartment = Console.ReadLine();
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

            } while (string.IsNullOrEmpty(SupportDepartment));
            StaffId = DatabasManagemantSQL.DatabaseAddStaff(this);
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(StaffId + "\t" + StaffName + "\t" + StaffAge + "\t" + SupportDepartment);
        }

        public override Support UpdateStaff() {
            base.UpdateStaff();
            Console.WriteLine("Enter Department:");
            SupportDepartment = Console.ReadLine();
            return this;
        }

        public override void ViewStaff() {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2} SUPPORT DEPARTMENT: {3} ", StaffId, StaffName, StaffAge, SupportDepartment);
        }
    }
}