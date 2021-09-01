using StaffManagementApp.Database;
using StaffManagementApp.Serialization;
using StaffManagementApp.Staffs;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace StaffManagementApp
{

    public class Program
    {

        static DatabaseManagementSQL DbHelper = new DatabaseManagementSQL(ConfigurationManager.AppSettings["ConnectionStringSQLDb"]);

        public static List<Staff> staffs = new List<Staff>();

        public static int StaffId { get; set; }

        private static void Main(string[] args)
        {

            SelectStaff();
        }



        public static void SelectStaff()
        {
            int staffOptions;
            bool continueStaffSelection = true;
            do
            {
                Console.WriteLine("Select the type of staff\n1){0}\n2){1}\n3){2}\n4)Export Data\n5)Import Data\n6)QUIT", nameof(Teacher), nameof(Administrator), nameof(Support));
                staffOptions = Convert.ToInt32(Console.ReadLine());
                ISerialiseStaff serialiseData;
                serialiseData = new SerializationJSONHelper();

                switch (staffOptions)
                {
                    case 1:
                        MainActions(nameof(Teacher));
                        break;

                    case 2:
                        MainActions(nameof(Administrator));
                        break;

                    case 3:
                        MainActions(nameof(Support));
                        break;

                    case 4:
                        DbHelper.DatabaseAddBulk();
                        break;

                    case 5:
                        staffs = serialiseData.StaffDeSerialize();
                        break;

                    case 6:
                        continueStaffSelection = false;
                        break;
                }
            } while (continueStaffSelection);
        }

        private static void MainActions(string staffType)
        {
            int mainOptions;
            bool continueMainActions;
            do
            {
                Console.WriteLine("WLECOME TO {0} STAFF PORTAL\nselect Options\n1)Add staff\n2)Delete staff\n3)Update staff details\n4)View staff details\n5)View All Staffs", staffType);
                mainOptions = Convert.ToInt32(Console.ReadLine());
                switch (mainOptions)
                {
                    case 1:
                        StaffId++;
                        Staff staff = StaffHelper.StaffAdd(staffType, DbHelper);
                        staffs.Add(staff);
                        break;

                    case 2:
                        StaffHelper.DeleteStaff(staffType, DbHelper);
                        break;

                    case 3:
                        StaffHelper.StaffUpdate(staffType, DbHelper);
                        break;

                    case 4:
                        StaffHelper.StaffView(staffType, DbHelper);
                        break;

                    case 5:
                        Staff.ViewAll(staffType, DbHelper);
                        break;
                }
                Console.WriteLine("Do you want to continue?(0/1):");
                continueMainActions = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            } while (continueMainActions);
        }
    }
}