using StaffManagementApp.Staffs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace StaffManagementApp.Serialization
{

    public class SerializationXMLHelper : ISerialiseStaff
    {

        public void StaffSerialize(List<Staff> staffs)
        {
            Console.Write("Enter the output filenme: ");
            var fileName = Console.ReadLine();
            using var stream = new FileStream(fileName, FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(List<Staff>));
            XML.Serialize(stream, staffs);
            Console.WriteLine("Export Succesfull");
        }

        public List<Staff> StaffDeSerialize()
        {
            Console.Write("Enter the filenme: ");
            var fileName = Console.ReadLine();
            using var stream = new FileStream(fileName, FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(List<Staff>));
            List<Staff> staffs = (List<Staff>)XML.Deserialize(stream);
            Console.WriteLine("Import Succesfull");
            return staffs;
        }
    }
}