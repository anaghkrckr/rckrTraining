using StaffManagementApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace StaffManagementApp.Staffs
{

    [XmlInclude(typeof(Teacher))]
    [XmlInclude(typeof(Administrator))]
    [XmlInclude(typeof(Support))]
    [KnownType(typeof(Teacher))]
    [KnownType(typeof(Administrator))]
    [KnownType(typeof(Support))]
    public class Staff
    {

        private string staffName;
        private int staffAge;

        public int StaffId { get; set; }
        public string StaffType { get; set; }

        public string StaffName
        {
            get => staffName;
            set
            {
                if (string.IsNullOrEmpty(StaffName) && string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name cannot be empty");
                }
                else if (value.Any(char.IsDigit))
                {
                    throw new Exception(" Name should not contain digits");
                }
                else if (value.Length > 0 && value.Length <= 3)
                {
                    throw new Exception("Name length should be greater than 3");
                }
                else if (value.Length > 3)
                {
                    staffName = value;
                }
            }
        }

        public int StaffAge
        {
            get => staffAge;
            set
            {
                if (value < 20 || value > 80)
                {
                    throw new Exception("Age should be between 20-80");
                }
                else
                {
                    this.staffAge = value;
                }
            }
        }

        public virtual void AddStaff(string staffType, DatabaseManagementSQL dbHelper)
        {
            do
            {
                try
                {
                    Console.WriteLine("Name:");
                    this.StaffName = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (string.IsNullOrEmpty(this.StaffName));
            do
            {
                try
                {
                    Console.WriteLine("Age:");
                    this.StaffAge = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (this.StaffAge == 0);

            this.StaffType = staffType;
        }


        public virtual Staff UpdateStaff()
        {
            Console.WriteLine("Update values");
            do
            {
                try
                {
                    Console.WriteLine("Name:");
                    this.StaffName = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (this.StaffName.Length > 0);
            bool Updated = true;
            do
            {
                Console.WriteLine("Age:");
                try
                {
                    Updated = int.TryParse(Console.ReadLine(), out this.staffAge);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (Updated);
            return this;
        }

        public virtual void ViewStaff() { }



        public static void ViewAll(string staffType, DatabaseManagementSQL dbHelper)
        {
            List<Staff> staffs = dbHelper.DatabaseViewAll();
            if (staffs != null)
            {
                foreach (Staff staff in staffs)
                {
                    if (staff.StaffType == staffType)
                    {
                        switch (staffType)
                        {
                            case nameof(Teacher):
                                Teacher teacher = (Teacher)staff;
                                teacher.ViewStaff();
                                break;

                            case nameof(Administrator):
                                Administrator administrator = (Administrator)staff;
                                administrator.ViewStaff();
                                break;

                            case nameof(Support):
                                Support support = (Support)staff;
                                support.ViewStaff();
                                break;
                        }
                    }
                }
            }
        }
    }
}