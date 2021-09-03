using StaffManagementLibrary.Staffs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace StaffManagementLibrary.SerializationHandler
{

    public class SerializationXMLHelper : ISerialiseStaff
    {

        public void StaffSerialize(List<Staff> staffs)
        {
            var fileName =  ConfigurationManager.AppSettings["SerializationFilenameXML"];
            using var stream = new FileStream(fileName, FileMode.Create);
            XmlSerializer XML = new XmlSerializer(typeof(List<Staff>));
            XML.Serialize(stream, staffs);
            Console.WriteLine("Export Succesfull");
        }

        public List<Staff> StaffDeSerialize()
        {
            var  fileName = ConfigurationManager.AppSettings["SerializationFilenameXML"];
            using var stream = new FileStream(fileName, FileMode.Open);
            XmlSerializer XML = new XmlSerializer(typeof(List<Staff>));
            List<Staff> staffs = (List<Staff>)XML.Deserialize(stream);
            Console.WriteLine("Import Succesfull");
            return staffs;
        }
    }
}