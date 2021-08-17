using System;
using System.Collections.Generic;


namespace training{
    class HelperMethods:Staff
    {
        public static Staff StaffAdd(String staffType, int StaffId, List<Staff> staffs)
        {
            switch (staffType)
            {
                case "Teacher":
                    Staff teacher = new Teacher();
                    teacher.StaffId = StaffId;
                    teacher.AddStaff(staffs,staffType);
                    return teacher;

                case "Administrator":
                    Staff administrator = new Administrator();
                    administrator.StaffId = StaffId;
                    administrator.AddStaff(staffs,staffType);
                    return administrator;

                case "Support":
                    Staff support = new Support();
                    support.StaffId = StaffId;
                    support.AddStaff(staffs,staffType);
                    return support;
            }
            return null;

        }

        public static void StaffUpdate(List<Staff> staffs)
        {
            var index=HelperMethods.getStaff(staffs);
            String staffType = staffs[index].StaffType;
            switch (staffType)
            {
                case "Teacher":
                    Teacher teacher = (Teacher)staffs[index];
                    teacher=Teacher.UpdateStaff(teacher);
                    staffs[index] = teacher;
                    break;
                   

                case "Administrator":
                    Administrator administrator = (Administrator)staffs[index];
                    administrator=Administrator.UpdateStaff(administrator);
                    staffs[index] = administrator;
                    break;
                   

                case "Support":
                    Support support = (Support)staffs[index];
                    support=Support.UpdateStaff(support);
                    staffs[index] = support;
                    break;
                   
            }
        }

        public static void StaffView(List<Staff> staffs)
        {
            var index = HelperMethods.getStaff(staffs);
            string staffType = staffs[index].StaffType;
            switch (staffType)
            {
                case "Teacher":
                    Teacher teacher = (Teacher)staffs[index];
                    Teacher.ViewStaffs(teacher);
                    break;

                case "Administrator":
                    Administrator administrator = (Administrator)staffs[index];
                    Administrator.ViewStaffs(administrator);
                    break;

                case "Support":
                    Support support = (Support)staffs[index];
                    Support.ViewStaffs(support);
                    break;
            }
        }

        private static int getStaff(List<Staff> staffs)
        {
            Console.Write("Enter staff Id:");
            int UpdateDeleteViewId = Convert.ToInt32(Console.ReadLine());
            var index = staffs.FindIndex(c => c.StaffId == UpdateDeleteViewId);
            return index;
        }

        public static void DeleteStaff(List<Staff> staffs)
        {
            Console.Write("Enter staff Id:");
            int UpdateDeleteViewId = Convert.ToInt32(Console.ReadLine());
            staffs.RemoveAt(UpdateDeleteId);
        }
    }
}
