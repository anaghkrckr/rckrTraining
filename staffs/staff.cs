using System;

namespace training{
    public class Staff{
        private int staffId=0;
        private string staffName;
        private int staffAge;
        

        public int StaffId{
            get{return staffId;}
            set{staffId=value;}
        }
        public string StaffName{
            get{return staffName;}
            set{staffName=value;}
        }
        public int StaffAge{
            get{return staffAge;}
            set{staffAge=value;}
        }







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
