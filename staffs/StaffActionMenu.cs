using System;

namespace training{
    public class StaffActionMenu{
        

        public void selectStaff(){
            int options;
            bool loop=true;
            do{
                Console.WriteLine("Select the type of staff\n1)Teacher\n2)Administrator\n3)Support\n4)QUIT");
                options=Convert.ToInt32(Console.ReadLine());
                switch(options){
                    case 1: 
                        Staff teacher=new Teacher();
                        this.mainActions(teacher);
                        break;
                    case 2: 
                        Staff administrator=new Administrator() ;
                        this.mainActions(administrator);
                        break;
                    case 3: 
                        Staff support=new Support() ;
                        this.mainActions(support);
                        break;
                    case 4:
                        loop=false;
                        break;
                }

            }while(loop);


        }

        private void mainActions(Staff staff){
            int options;
            bool loop=true;
            do{
                Console.WriteLine("WLECOME TO {0} STAFF PORTAL\nselect options\n1)Add staff\n2)Delete staff\n3)Update staff details\n4)View staff details\n5)Go back\n",staff.GetType().Name.ToUpper());
                options=Convert.ToInt32(Console.ReadLine());

                switch(options){
                    case 1: 
                        staff.addStaff();
                        break;
                    case 2: 
                    
                        staff=null;
                        break;
                    case 3: 
                        staff.update();
                        break;
                    case 4: 
                       staff.view();
                        break;
                    case 5:
                        loop=false;
                        break;

                }
            }while(loop);
        }


    }
}