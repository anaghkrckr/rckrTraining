using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffManagementAppAPI.Models
{
    public class Staff
    {
        public enum StaffType { Teacher,Administrator,Support};
        public int Id { get; set; }
        public string Name { get; set; }
        public StaffType Type { get; set; }
    }
}
