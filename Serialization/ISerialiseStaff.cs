using StaffManagementApp.staffs;
using System.Collections.Generic;

namespace StaffManagementApp.Serialization {

    public interface ISerialiseStaff {

        public void StaffSerialize(List<Staff> staffs);

        public List<Staff> StaffDeSerialize();
    }
}