using System;

namespace training{
    public class Teacher :Staff{
        private string subject;
        
        public string Subject{
            get{ return subject;}
            set{subject=value;}
        }



        public override void AddStaff() {
            base.AddStaff();
            Console.WriteLine("Subject:");
            this.subject=Console.ReadLine();
            this.StaffId++;
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.subject);
        }

        public override void ViewStaff(){
            Console.WriteLine("TEACHER");
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(this.StaffId+"\t"+this.StaffName+"\t"+this.StaffAge+"\t"+this.subject);
        }

        public override void UpdateStaff(){
            int UpdateOptions;
            bool loop=true;
            do{
                Console.WriteLine("Select value to update\n1)Name\n2)Subject\n3)Go back");
                UpdateOptions=Convert.ToInt32(Console.ReadLine());
                switch(UpdateOptions){
                case 1:
                    Console.WriteLine("Enter new Name:");
                    this.StaffName=Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter Subject:");
                    this.subject=Console.ReadLine();
                    break;
                case 3:
                    loop=false;
                    break;
                }

            }while(loop);

        }
    }

}