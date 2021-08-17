using System;
using System.Collections.Generic;


namespace training
{
    public class Administrator : Staff
    {
        public string AdministratorDepartment { get; set; }

        public override void AddStaff(List<Staff> staffs,String stafftype)
        {
         
            base.AddStaff(staffs,stafftype);
            Console.WriteLine("Department:");
            this.AdministratorDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.AdministratorDepartment);
        }


        public static Administrator UpdateStaff(Administrator administartor)
        {
            int updateOptions;
            bool administratorContinue = true;
            do
            {
                Console.WriteLine("Select value to update\n1)Name\n2)Departnemnt\n3)Go back\n");
                updateOptions = Convert.ToInt32(Console.ReadLine());
                switch (updateOptions)
                {
                    case 1:
                        Console.WriteLine("Enter new Name:");
                        administartor.StaffName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter Department:");
                        administartor.AdministratorDepartment = Console.ReadLine();
                        break;
                    case 3:
                        administratorContinue = false;
                        break;
                }
            } while (administratorContinue);
            return administartor;
        }

        public static void ViewStaffs(Administrator administrator)
        {
            Console.WriteLine(administrator.StaffId + "\t" + administrator.StaffName + "\t" + administrator.StaffAge + "\t" + administrator.AdministratorDepartment);
        }
    }
}