using System;
using System.Collections.Generic;


namespace training
{
    class Program
    {
        public static int StaffId;
        public static List<Staff> staffs = new List<Staff>();

        static void Main(string[] args){
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
                            Program.MainActions("Teacher");
                            break;
                        case 2: 
                            Program.MainActions("Administrator");
                            break;
                        case 3: 
                            Program.MainActions("Support");
                            break;
                        case 4:
                            continueStaffSelection=false;
                            break;
                        }
            }while(continueStaffSelection);
        }


        private static void MainActions(String staffType){

            int mainOptions;
            bool continueMainActions;
           
            do{
                Console.WriteLine("WLECOME TO {0} STAFF PORTAL\nselect Options\n1)Add staff\n2)Delete staff\n3)Update staff details\n4)View staff details\n5)View All Staffs", staffType);
                mainOptions = Convert.ToInt32(Console.ReadLine());
                switch (mainOptions){
                    case 1:
                        Program.StaffId++;
                        Staff staff = StaffHelper.StaffAdd(staffType, Program.StaffId,Program.staffs);
                        staffs.Add(staff);

                        break;
                    case 2:
                        StaffHelper.DeleteStaff(Program.staffs);
                        break;
                    case 3:
                        StaffHelper.StaffUpdate( Program.staffs);
                        break;
                    case 4:
                        StaffHelper.StaffView( Program.staffs);
                        break;
                    case 5:
                        Staff.ViewAll(Program.staffs,staffType);
                        break;
                }
                Console.WriteLine("Do you want to continue?(0/1):");
                continueMainActions = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            } while (continueMainActions);
        }
    }  
}
