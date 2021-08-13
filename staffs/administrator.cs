
using System;
namespace training{
    public class Administrator :Staff{
        private string administratorDepartment;

        public string AdministratorDepartment{
            get{return administratorDepartment;}
            set{administratorDepartment=value;}
        }

        public override void AddStaff(){
            base.AddStaff();
            Console.WriteLine("Department:");
            this.AdministratorDepartment=Console.ReadLine();
            this.StaffId++;
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.AdministratorDepartment);
        }


         public override void ViewStaff(){
            Console.WriteLine("ADMINISTRATOR");
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.AdministratorDepartment);
        }


        public override void UpdateStaff(){
            int UpdateOptions;
            bool loop=true;
            do{
                Console.WriteLine("Select value to update\n1)Name\n2)Departnemnt\n3)Go back\n");
                UpdateOptions=Convert.ToInt32(Console.ReadLine());
                switch(UpdateOptions){
                case 1:
                    Console.WriteLine("Enter new Name:");
                    this.StaffName=Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter Department:");
                    this.AdministratorDepartment=Console.ReadLine();
                    break;
                case 3:
                    loop=false;
                    break;
                }

            }while(loop);

        Console.WriteLine("Administrator");
        }



    }

}