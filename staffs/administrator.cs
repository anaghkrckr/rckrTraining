using StaffManagementApp.Database;
using System;   
using System.Linq;

namespace StaffManagementApp.staffs {

    public class Administrator : Staff {
        private string administratorDepartment;

        public string AdministratorDepartment {
            get => administratorDepartment;
            set {
                if (string.IsNullOrEmpty(AdministratorDepartment) && string.IsNullOrWhiteSpace(value)) {
                    throw new Exception($"{nameof(AdministratorDepartment)} cannot be empty");
                }
                else if (value.Any(char.IsDigit)) {
                    throw new Exception($"{nameof(AdministratorDepartment)} name should not contain digits");
                }
                else if (value.Length > 0 && value.Length <= 3) {
                    throw new Exception($"{nameof(AdministratorDepartment)} name length should be greater than 3");
                }
                else if (value.Length > 3) {
                    administratorDepartment = value;
                }
            }
         }
        

        public override void AddStaff( string stafftype) {
            base.AddStaff(stafftype);
            do {
                try {
                    Console.WriteLine("Department:");
                    AdministratorDepartment = Console.ReadLine();
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            } while (string.IsNullOrEmpty(AdministratorDepartment));
            StaffId = Sql.DatabaseAddStaff(this);
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(StaffId + "\t" + StaffName + "\t" + StaffAge + "\t" + AdministratorDepartment);
        }

        public override Administrator UpdateStaff() {
            base.UpdateStaff();
            Console.WriteLine("Enter Department:");
            AdministratorDepartment = Console.ReadLine();
            return this;
        }

        public override void ViewStaff() {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2} ADMINISTRATOR DEPARTMENT: {3} ", StaffId, StaffName, StaffAge, AdministratorDepartment);
        }
    }
}