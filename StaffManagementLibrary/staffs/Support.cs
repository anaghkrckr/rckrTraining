using StaffManagementLibrary.DbHandler;
using System;
using System.Linq;

namespace StaffManagementLibrary.Staffs
{

    public class Support : Staff
    {
        private string supportDepartment;

        public string SupportDepartment
        {
            get => supportDepartment;
            set
            {
                if (string.IsNullOrEmpty(SupportDepartment) && string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception($"{nameof(SupportDepartment)} cannot be empty");
                }
                else if (value.Any(char.IsDigit))
                {
                    throw new Exception($"{nameof(SupportDepartment)} name should not contain digits");
                }
                else if (value.Length > 0 && value.Length <= 3)
                {
                    throw new Exception($"{nameof(SupportDepartment)} name length should be greater than 3");
                }
                else if (value.Length > 3)
                {
                    supportDepartment = (value).First().ToString().ToUpper() + (value).Substring(1);
                }
            }
        }

        public override void AddStaff(string staffType, IDatabase dbHelper)
        {
            base.AddStaff(staffType,dbHelper);
            do
            {
                try
                {
                    Console.WriteLine("Department:");
                    SupportDepartment = Console.ReadLine();
                    break;
                }
                catch (Exception e)
                {
                    throw;
                }
            } while (string.IsNullOrEmpty(SupportDepartment));
            StaffId = dbHelper.AddStaff(this);
        }

        public override Support UpdateStaff()
        {
            base.UpdateStaff();
            Console.WriteLine("Enter Department:");
            SupportDepartment = Console.ReadLine();
            return this;
        }

        public override void ViewStaff()
        {
            Console.WriteLine("ID:{0}\tNAME: {1}\tAGE: {2} SUPPORT DEPARTMENT: {3} ", StaffId, StaffName, StaffAge, SupportDepartment);
        }
    }
}