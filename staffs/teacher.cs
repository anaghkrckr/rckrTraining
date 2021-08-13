using System;

namespace training{
    public class Teacher :Staff{ 
        public string Subject{get;set;}

        public override void AddStaff() {
            base.AddStaff();
            Console.WriteLine("Subject:");
            this.Subject=Console.ReadLine();
            this.StaffId++;
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.Subject);
        }

        public override void ViewStaff(){
            Console.WriteLine("TEACHER");
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.Subject);
        }

        public override void UpdateStaff(){
            int updateOptions;
            bool loop=true;
            do{
                Console.WriteLine("Select value to update\n1)Name\n2)Subject\n3)Go back");
                updateOptions=Convert.ToInt32(Console.ReadLine());
                switch(updateOptions){
                case 1:
                    Console.WriteLine("Enter new Name:");
                    this.StaffName=Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter Subject:");
                    this.Subject=Console.ReadLine();
                    break;
                case 3:
                    loop=false;
                    break;
                }

            }while(loop);

        }
    }

}