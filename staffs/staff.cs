using System;

namespace training{
    public class Staff{
        public int StaffId{get;set;}
        public string StaffName{get;set;}
        public int StaffAge{get;set;}

        public virtual void AddStaff(){
            Console.WriteLine("Name:");
            this.StaffName=Console.ReadLine();
            Console.WriteLine("Age:");
            this.StaffAge=Convert.ToInt32(Console.ReadLine());
        }

        public virtual void ViewStaff(){}

        // public abstract void delete();
        
        public virtual void UpdateStaff(){}
    } 
}
