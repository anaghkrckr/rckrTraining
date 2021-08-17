using System;
using System.Collections.Generic;

namespace training{
    public class Staff{
        public int StaffId{get;set;}
        public string StaffName{get;set;}
        public int StaffAge{get;set;}
        public static int UpdateDeleteId { get; set; }
        public string StaffType { get; set; }

        public virtual void AddStaff(List<Staff> staffs,String staffType){
            Console.WriteLine("Name:");
            this.StaffName=Console.ReadLine();
            Console.WriteLine("Age:");
            this.StaffAge=Convert.ToInt32(Console.ReadLine());
            this.StaffType = staffType;
        }

        public virtual void UpdateStaff() { }

        public virtual void ViewStaff() { }
    }
}
