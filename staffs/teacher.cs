using System;
using System.Collections.Generic;


namespace training
{
    public class Teacher : Staff
    {
        public string Subject { get; set; }


        public override void AddStaff(List<Staff> staffs,string staffType)
        {
            base.AddStaff(staffs,staffType);
            Console.WriteLine("Subject:");
            this.Subject = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.Subject);
            
        }


        public static Teacher UpdateStaff(Teacher teacher)
        {
            int updateOptions;
            bool loop = true;
            do
            {
                Console.WriteLine("Select value to update\n1)Name\n2)Subject\n3)Go back");
                updateOptions = Convert.ToInt32(Console.ReadLine());
                switch (updateOptions)
                {
                    case 1:
                        Console.WriteLine("Enter new Name:");
                        teacher.StaffName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter Subject:");
                        teacher.Subject = Console.ReadLine();
                        break;
                    case 3:
                        loop = false;
                        break;
                }
            } while (loop);
            return teacher;


            
        }

        public static void ViewStaffs(Teacher teacher)
        {
           
            
            Console.WriteLine(teacher.StaffId + "\t" + teacher.StaffName + "\t" + teacher.StaffAge + "\t" + teacher.Subject);
            

        }
    }
}