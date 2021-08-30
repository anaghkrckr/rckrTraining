using StaffManagementApp.Staffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Configuration;

namespace StaffManagementApp.Serialization
{

    public class SerializationJSONHelper : ISerialiseStaff
    {

        public void StaffSerialize(List<Staff> staffs)
        {
            Console.Write("Enter the output filenme: ");
            var fileName = Console.ReadLine();
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Staff>));
                js.WriteObject(stream, staffs);
            }
        }

        public List<Staff> StaffDeSerialize()
        {
            var fileName = ConfigurationManager.AppSettings["SerializationFilename"];
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Staff>));
                try
                {
                    List<Staff> staffs = (List<Staff>)js.ReadObject(stream);
                    Console.WriteLine("Import Succesfull");
                    return staffs;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}