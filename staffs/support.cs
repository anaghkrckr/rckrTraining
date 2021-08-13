using System;

namespace training{
    public class Support :Staff{
        public string SupportDepartment{get;set;}

        public override void AddStaff(){
            base.AddStaff();
            Console.WriteLine("Department:");
            this.SupportDepartment=Console.ReadLine();
            this.StaffId++;
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.SupportDepartment);
        }
         public override void ViewStaff(){
            Console.WriteLine("SUPPORT");
            Console.WriteLine("StaffId\tName\tAge\tDepartment");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.SupportDepartment);

        Console.WriteLine("Support");
        }

        public override void UpdateStaff(){
            int updateOptions;
            bool loop=true;
            do{
                Console.WriteLine("Select value to update\n1)Name\n2)Department\n3)Go back\n");
                updateOptions=Convert.ToInt32(Console.ReadLine());
                switch(updateOptions){
                case 1:
                    Console.WriteLine("Enter new Name:");
                    this.StaffName=Console.ReadLine();;
                    break;
                case 2:
                    Console.WriteLine("Enter Department:");
                    this.SupportDepartment=Console.ReadLine();;
                    break;
                case 3:
                    loop=false;
                    break;
                }

            }while(loop);

        }


    }

}