using StaffManagementLibrary.DbHandler;
using System;
using System.Collections.Generic;

namespace StaffManagementLibrary.Staffs.HelperClasses
{
    public class StaffHelper
    {
        public Staff StaffAdd(Staff staff, IDatabase dbHelper, string staffType)
        {

            switch (staffType)
            {
                case nameof(Teacher):

                    if (staff?.StaffName == null)
                    {
                        staff = new Teacher();
                        staff.AddStaff(staffType, dbHelper);
                    }

                    staff = (Teacher)staff;
                    break;

                case nameof(Administrator):
                    staff = (Administrator)staff;
                    if (staff?.StaffName == null)
                    {

                        staff = new Administrator();
                        staff.AddStaff(staffType, dbHelper);
                    }

                    break;

                case nameof(Support):
                    staff = (Support)staff;
                    if (staff?.StaffName == null)
                    {
                        staff = new Support();
                        staff.AddStaff(staffType, dbHelper);
                    }
                    break;
            }
            staff.StaffId = dbHelper.AddStaff(staff);
            return staff;
        }


        public Staff StaffUpdate(Staff staff, IDatabase dbHelper)
        {
            if (staff != null)
            {
                dbHelper.UpdateStaff(staff);
                return staff;
            }
            else
            {
                throw new Exception("Empty Staff");
            }
        }

        public Staff StaffGet(int staffId, IDatabase dbHelper, string staffType)
        {
            Staff staff = dbHelper.GetStaff(staffId);
            if (staff != null)
            {
                if ((staff.StaffType).ToLower() != staffType.ToLower())
                {
                    throw new Exception("Staff belongs to another type");
                }
                return staff;
            }
            throw new Exception("Staff Not Found");
        }

        public void DeleteStaff(Staff staff, IDatabase dbHelper, string staffType)
        {
            if (staff?.StaffType != staffType)
            {
                throw new Exception("Staff Not found");
            }
            dbHelper.DeleteStaff(staff.StaffId);
        }

        public List<Staff> StaffGetAllByType(IDatabase dbHelper)
        {
            List<Staff> staffs = dbHelper.ViewAll();
            if (staffs != null)
            {
                return staffs;
            }
            throw new Exception("Staff List empty");
        }

        public void StaffView(string staffType, IDatabase dbHelper)
        {
            Staff staff = GetStaff(dbHelper);
            if (staff != null)
            {
                if (staff.StaffType != staffType)
                {
                    Console.WriteLine("Staff belongs to another type");
                    return;
                }

                staff.ViewStaff();
            }


        }

        public Staff GetStaff(IDatabase dbHelper)
        {
            Console.Write("Enter staff Id:");
            int StaffId = Convert.ToInt32(Console.ReadLine());
            Staff staff = dbHelper.GetStaff(StaffId);
            return staff;
        }

        


    }
}