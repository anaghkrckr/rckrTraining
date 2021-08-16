using System;
using System.Collections.Generic;


namespace training
{
    public class Teacher : Staff
    {
        public string Subject { get; set; }
        public int TeacherStaffId { get; set; }





        public override void AddStaff()
        {
            TeacherStaffId = Staff.StaffId;
            base.AddStaff();
            Console.WriteLine("Subject:");
            this.Subject = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.TeacherStaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.Subject);
            Program.teacherList.Add(this);
        }


        public static void UpdateStaff()
        {
            var index = Program.teacherList.FindIndex(c => c.TeacherStaffId == Staff.UpdateDeleteId);
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
                        Program.teacherList[index].StaffName = Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("Enter Subject:");
                        Program.teacherList[index].Subject = Console.ReadLine();
                        break;
                    case 3:
                        loop = false;
                        break;
                }
            } while (loop);
        }
    }
}