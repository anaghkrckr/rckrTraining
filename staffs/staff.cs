using System;

namespace training{
    public class Staff{
        public static int StaffId{get;set;}
        public string StaffName{get;set;}
        public int StaffAge{get;set;}
        public static int UpdateDeleteId { get; set; }


        public virtual void AddStaff(){
            Staff.StaffId++;
            Console.WriteLine("Name:");
            this.StaffName=Console.ReadLine();
            Console.WriteLine("Age:");
            this.StaffAge=Convert.ToInt32(Console.ReadLine());
        }

        public static void StaffAdd(String staffType){
            switch (staffType){
                case "Teacher":
                    Staff teacher = new Teacher();
                    teacher.AddStaff();
                    break;

                case "Administrator":
                    Staff administrator = new Administrator();
                    administrator.AddStaff();
                    break;

                case "Support":
                    Staff support = new Support();
                    support.AddStaff();
                    break;
            }
        }

        public static void DeleteStaff(String staffType) {
            Console.Write("Enter staff Id:");
            UpdateDeleteId = Convert.ToInt32(Console.ReadLine());
            switch (staffType)
            {
                case "Teacher":
                    Program.teacherList.RemoveAt(UpdateDeleteId);
                    break;

                case "Administrator":
                    Program.administratorList.RemoveAt(UpdateDeleteId);
                    break;

                case "Support":
                    Program.supportList.RemoveAt(UpdateDeleteId);
                    break;
            }
        }

        public static void UpdateStaff(String staffType){
            Console.Write("Enter staff Id:");
            UpdateDeleteId = Convert.ToInt32(Console.ReadLine());
            switch (staffType)
            {
                case "Teacher":
                    Teacher.UpdateStaff();
                    break;

                case "Administrator":
                    Administrator.UpdateStaff();
                    break;

                case "Support":
                    Support.UpdateStaff();
                    break;
            }
        }

        public static void StaffView(String staffType)
        {
            switch (staffType)
            {
                case "Teacher":
                    foreach (Teacher teacher in Program.teacherList){
                        Console.WriteLine(teacher.TeacherStaffId + "\t" + teacher.StaffName + "\t" + teacher.StaffAge + "\t" + teacher.Subject);
                    }
                    break;

                case "Administrator":
                    foreach (Administrator administrator in Program.administratorList){
                        Console.WriteLine(administrator.AdminStaffId + "\t" + administrator.StaffName + "\t" + administrator.StaffAge + "\t" + administrator.AdministratorDepartment);
                    }
                    break;

                case "Support":
                    foreach (Support support in Program.supportList){
                        Console.WriteLine(support.SupportStaffId + "\t" + support.StaffName + "\t" + support.StaffAge + "\t" + support.SupportDepartment);
                    }
                    break;
            }


        }

    } 
}
