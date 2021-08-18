using System;
using System.Collections.Generic;


namespace training
{
    public class Teacher : Staff {
        private string subject;

        public string Subject { 
            get => subject;
            set {
                if (value != ""){
                    subject = value;
                }
            }
        }


        public override void AddStaff(List<Staff> staffs, string staffType) {
            base.AddStaff(staffs, staffType);
            Console.WriteLine("Subject:");
            this.Subject = Console.ReadLine();
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId + "\t" + this.StaffName + "\t" + this.StaffAge + "\t" + this.Subject);

        }


        public static Teacher UpdateStaff(Teacher teacher) {
            Console.WriteLine("Update values");
            Console.WriteLine("Name:");
            teacher.StaffName = Console.ReadLine();
            Console.WriteLine("Age: (enter 0 if not change needed)");
            if(int.TryParse(Console.ReadLine(), out int staffAge)){
                teacher.StaffAge = staffAge;
            }
            
            Console.WriteLine("Enter Subject:");
            teacher.Subject = Console.ReadLine();
            return teacher;
        }

        public static void ViewStaffs(Teacher teacher) {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2}  SUBJECT: {3} ", teacher.StaffId, teacher.StaffName, teacher.StaffAge, teacher.subject);
        }
    }
}