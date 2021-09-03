using StaffManagementLibrary.Staffs;
using System.Collections.Generic;

namespace StaffManagementLibrary.DbHandler
{
    public interface IDatabase
    {
        public int AddStaff(Staff staff);
        public void DeleteStaff(int staffId);
        public Staff GetStaff(int staffId);
        public void UpdateStaff(Staff staff);
        public List<Staff> ViewAll();
        public void AddBulk(List<Staff> staffs);
    }
}
