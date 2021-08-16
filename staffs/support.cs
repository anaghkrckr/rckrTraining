using System;
using System.Collections.Generic;


namespace training
{
    public class Support : Staff
    {
        public string SupportDepartment { get; set; }
        public int SupportStaffId { get; set; }



        public override void AddStaff()
        {
            SupportStaffId = Staff.StaffId;
            base.AddStaff();
            Console.WriteLine("Department:");
            this.SupportDepartment = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.SupportStaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.SupportDepartment);
            Program.supportList.Add(this);
        }

        public static void UpdateStaff()
        {
            var index = Program.supportList.FindIndex(c => c.SupportStaffId == Staff.UpdateDeleteId);
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
                        Program.supportList[index].StaffName = Console.ReadLine(); ;
                        break;
                    case 2:
                        Console.WriteLine("Enter Department:");
                        Program.supportList[index].SupportDepartment = Console.ReadLine(); ;
                        break;
                    case 3:
                        loop = false;
                        break;
                }

            } while (loop);

        }


    }

}