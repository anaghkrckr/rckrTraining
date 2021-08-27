using StaffManagementApp.Database;
using System;
using System.Linq;

namespace StaffManagementApp.staffs {

    public class Teacher : Staff {
        private string subject;
        public string Subject {
            get => subject;
            set {
                if (string.IsNullOrEmpty(Subject) && string.IsNullOrWhiteSpace(value)) {
                    throw new Exception($"{nameof(Subject)} cannot be empty");
                }
                else if (value.Any(char.IsDigit)) {
                    throw new Exception($"{nameof(Subject)} name should not contain digits");
                }
                else if (value.Length > 0 && value.Length <= 3) {
                    throw new Exception($"{nameof(Subject)} name length should be greater than 3");
                }
                else if (value.Length > 3) {
                    subject = value;
                }
            }
        }

        public override void AddStaff( string staffType) {
            base.AddStaff( staffType);
            do {
                try {
                    Console.WriteLine("Subject:");
                    Subject = Console.ReadLine();
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            } while (string.IsNullOrEmpty(Subject));
            StaffId = Sql.DatabaseAddStaff(this);
            Console.WriteLine("StaffId\tName\tAge\tSubject");
            Console.WriteLine(StaffId + "\t" + StaffName + "\t" + StaffAge + "\t" + Subject);
        }

        public override Teacher UpdateStaff() {
            base.UpdateStaff();
            do {
                try {
                    Console.WriteLine("Subject:");
                    Subject = Console.ReadLine();
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            } while (Subject.Length>0);
            return this;
        }

        public override void ViewStaff() {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2}  SUBJECT: {3} ", StaffId, StaffName, StaffAge, Subject);
        }








    }
}