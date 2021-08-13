using System;

namespace training
{
    class Program
    {
        static void Main(string[] args)
        {
            Program staff=new Program();
            
            staff.SelectStaff();  
            
          
        }


        public void SelectStaff(){
            int Options;
            bool ContinueStaffSelection=true;
            do{
                Console.WriteLine("Select the type of staff\n1)Teacher\n2)Administrator\n3)Support\n4)QUIT");
                Options=Convert.ToInt32(Console.ReadLine());
                switch(Options){
                    case 1: 
                        Staff teacher=new Teacher();
                        this.MainActions(teacher);
                        break;
                    case 2: 
                        Staff administrator=new Administrator() ;
                        this.MainActions(administrator);
                        break;
                    case 3: 
                        Staff support=new Support() ;
                        this.MainActions(support);
                        break;
                    case 4: 
                        ContinueStaffSelection=false;
                        break;
                }

            }while(ContinueStaffSelection);


        }

        private void MainActions(Staff staff){
            int Options;
            bool ContinueMainActions=true;
            string staffType=staff.GetType().Name.ToUpper();
            do{
               

                Console.WriteLine("WLECOME TO {0} STAFF PORTAL\nselect Options\n1)Add staff\n2)Delete staff\n3)Update staff details\n4)View staff details",staffType);
                Options=Convert.ToInt32(Console.ReadLine());

                switch(Options){
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
                ContinueMainActions=Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            }while(ContinueMainActions);
        }



    }

    
}
