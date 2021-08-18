using System;
using System.Collections.Generic;


namespace training
{
    public class Administrator : Staff
    {
        private string administratorDepartment;
        public string AdministratorDepartment {
            get => administratorDepartment;
            set {
                if (value != "") {
                    administratorDepartment = value;
                }
            }
        }

        public override void AddStaff(List<Staff> staffs,String stafftype)
        {
         
            base.AddStaff(staffs,stafftype);
            Console.WriteLine("Department:");
            this.AdministratorDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.AdministratorDepartment);
        }


        public static Administrator UpdateStaff(Administrator administrator)
        {
            Console.WriteLine("Update values");
            Console.WriteLine("Name:");
            administrator.StaffName = Console.ReadLine();
            Console.WriteLine("Age: (enter 0 if not change needed)");
            administrator.StaffAge = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Department:");
            administrator.AdministratorDepartment = Console.ReadLine();
            return administrator;
        }

        public static void ViewStaffs(Administrator administrator)
        {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2} ADMINISTRATOR DEPARTMENT: {3} ", administrator.StaffId, administrator.StaffName, administrator.StaffAge, administrator.AdministratorDepartment);
        }
    }
}