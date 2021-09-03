using StaffManagementLibrary.Staffs;
using System.Collections.Generic;

namespace StaffManagementLibrary.SerializationHandler
{

    public interface ISerialiseStaff
    {

        public void StaffSerialize(List<Staff> staffs);

        public List<Staff> StaffDeSerialize();
    }
}