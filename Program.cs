using System;

namespace training
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.SelectStaff();    
        }

        public static void SelectStaff(){
            int staffOptions;
            bool continueStaffSelection=true;
            do{
                Console.WriteLine("Select the type of staff\n1)Teacher\n2)Administrator\n3)Support\n4)QUIT");
                staffOptions=Convert.ToInt32(Console.ReadLine());
                switch(staffOptions){
                    case 1: 
                        Staff teacher=new Teacher();
                        Program.MainActions(teacher);
                        break;
                    case 2: 
                        Staff administrator=new Administrator() ;
                        Program.MainActions(administrator);
                        break;
                    case 3: 
                        Staff support=new Support() ;
                        Program.MainActions(support);
                        break;
                    case 4: 
                        continueStaffSelection=false;
                        break;
                }
            }while(continueStaffSelection);
        }

        private static void MainActions(Staff staff){
            int mainOptions;
            bool continueMainActions=true;
            string staffType=staff.GetType().Name.ToUpper();
            do{
                Console.WriteLine("WLECOME TO {0} STAFF PORTAL\nselect Options\n1)Add staff\n2)Delete staff\n3)Update staff details\n4)View staff details",staffType);
                mainOptions=Convert.ToInt32(Console.ReadLine());
                switch(mainOptions){
                    case 1: 
                        staff.AddStaff();
                        break;
                    case 2: 
                        // staff.DeleteStaff();
                        break;
                    case 3: 
                        staff.UpdateStaff();
                        break;
                    case 4: 
                       staff.ViewStaff();
                        break;
                    }
                Console.WriteLine("Do you want to continue?(0/1):");
                continueMainActions=Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            }while(continueMainActions);
        }
    }  
}
