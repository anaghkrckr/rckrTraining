using StaffManagementLibrary.DbHandler;
using StaffManagementLibrary.SerializationHandler;
using StaffManagementLibrary.Staffs;
using StaffManagementLibrary.Staffs.HelperClasses;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace StaffManagementApp
{

    public class Program
    {

        static string DatabaseClassName = ConfigurationManager.AppSettings["DatabaseClassName"];
        static Type DatabaseType = Type.GetType(DatabaseClassName);
        static IDatabase DbHelper = (IDatabase)Activator.CreateInstance(DatabaseType, ConfigurationManager.AppSettings["ConnectionStringSQLDb"]);

        static string className = ConfigurationManager.AppSettings["SerializeClassName"];
        static Type SeriliazationType = Type.GetType(className);
        static ISerialiseStaff serialiseData = (ISerialiseStaff)Activator.CreateInstance(SeriliazationType);

        public static List<Staff> staffs = new List<Staff>();

        public static int StaffId { get; set; }
        static StaffHelper StaffHelper = new StaffHelper();
        

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

                        try
                        {
                            serialiseData.StaffSerialize(staffs);
                            Console.WriteLine("Export Succesfull");
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 5:
                        try
                        {
                            staffs = serialiseData.StaffDeSerialize();
                            Console.WriteLine("Import Succesfull");
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }
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
                        try
                        {
                            Staff staff=null;
                            staff = StaffHelper.StaffAdd(staff, DbHelper, staffType);
                            staff.ViewStaff();
                            staffs.Add(staff);

                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e);
                        }
                        break;

                    case 2:
                        try
                        {
                            Staff staffDelete = StaffHelper.GetStaff(DbHelper);
                            StaffHelper.DeleteStaff(staffDelete, DbHelper, staffType);
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 3:
                        try
                        {
                            Staff staffUpdate = StaffHelper.GetStaff(DbHelper);
                            staffUpdate.UpdateStaff();
                            StaffHelper.StaffUpdate(staffUpdate, DbHelper);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;

                    case 4:
                        try
                        {
                            StaffHelper.StaffView(staffType, DbHelper);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
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