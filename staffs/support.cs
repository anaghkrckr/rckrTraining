using System;
using System.Collections.Generic;


namespace training
{
    public class Support : Staff
    {
        public string SupportDepartment { get; set; }



        public override void AddStaff(List<Staff> staffs,String staffType)
        {
          
            base.AddStaff(staffs,staffType);
            Console.WriteLine("Department:");
            this.SupportDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.SupportDepartment);
            
        }

        public static Support UpdateStaff(Support support)
        {
           
            int updateOptions;
            bool loop = true;
            do
            {
                Console.WriteLine("Select value to update\n1)Name\n2)Department\n3)Go back\n");
                updateOptions = Convert.ToInt32(Console.ReadLine());
                switch (updateOptions)
                {
                    case 1:
                        Console.WriteLine("Enter new Name:");
                        support.StaffName = Console.ReadLine(); ;
                        break;
                    case 2:
                        Console.WriteLine("Enter Department:");
                        support.SupportDepartment = Console.ReadLine(); ;
                        break;
                    case 3:
                        loop = false;
                        break;
                }

            } while (loop);
            return support;
        }

        public static void ViewStaffs(Support support)
        {
           
           Console.WriteLine(support.StaffId + "\t" + support.StaffName + "\t" + support.StaffAge + "\t" + support.SupportDepartment);

        }


    }

}