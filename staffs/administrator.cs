using System;
using System.Collections.Generic;


namespace training
{
    public class Administrator : Staff
    {
        public string AdministratorDepartment { get; set; }
        public int AdminStaffId { get; set; }

        public override void AddStaff()
        {
            AdminStaffId = Staff.StaffId;
            base.AddStaff();
            Console.WriteLine("Department:");
            this.AdministratorDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.AdminStaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.AdministratorDepartment);
            Program.administratorList.Add(this);
        }


        public static void UpdateStaff()
        {
            var index = Program.administratorList.FindIndex(c => c.AdminStaffId == Staff.UpdateDeleteId);
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
                        Program.administratorList[index].StaffName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter Department:");
                        Program.administratorList[index].AdministratorDepartment = Console.ReadLine();
                        break;
                    case 3:
                        administratorContinue = false;
                        break;
                }
            } while (administratorContinue);
            Console.WriteLine("Administrator");
        }
    }
}