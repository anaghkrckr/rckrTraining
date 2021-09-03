using StaffManagementLibrary.Staffs;


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Configuration;

namespace StaffManagementLibrary.SerializationHandler
{

    public class SerializationJSONHelper : ISerialiseStaff
    {

        public void StaffSerialize(List<Staff> staffs)
        {
            var fileName = ConfigurationManager.AppSettings["SerializationFilenameJSON"];
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Staff>));
                js.WriteObject(stream, staffs);
            }
        }

        public List<Staff> StaffDeSerialize()
        {
            var fileName = ConfigurationManager.AppSettings["SerializationFilenameJSON"];
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Staff>));
                try
                {
                    List<Staff> staffs = (List<Staff>)js.ReadObject(stream);
                    return staffs;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}