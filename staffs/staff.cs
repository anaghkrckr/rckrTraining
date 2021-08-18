using System;
using System.Collections.Generic;

namespace training{
    public class Staff {
      
        private string staffName;
        private int staffAge;

        public int StaffId { get; set; }
        public string StaffName {
            get => staffName;
            set {
                if (value != "") {
                    staffName = value;
                }
            }
        }

        public int StaffAge {
            get => staffAge;
            set {
                if (value != 0) {
                    staffAge = value;
                }
            }
        }
        public string StaffType { get; set; }

        public virtual void AddStaff(List<Staff> staffs, String staffType) {
            Console.WriteLine("Name:");
            this.StaffName = Console.ReadLine();
            Console.WriteLine("Age:");
            this.StaffAge = Convert.ToInt32(Console.ReadLine());
            this.StaffType = staffType;
        }

        public virtual void UpdateStaff() { }

        public static void ViewAll(List<Staff> staffs,string staffType) {
            foreach(Staff staff in staffs) {
                if (staff.StaffType == staffType) {
                    switch (staffType) {
                            case "Teacher":
                                Teacher teacher = (Teacher)staff;
                                Teacher.ViewStaffs(teacher);
                                break;

                            case "Administrator":
                                Administrator administrator = (Administrator)staff;
                                Administrator.ViewStaffs(administrator);
                                break;

                            case "Support":
                                Support support = (Support)staff;
                                Support.ViewStaffs(support);
                                break;
                    }
                
                }


            }
        }
    }
}
