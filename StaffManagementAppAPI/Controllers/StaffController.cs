using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StaffManagementApp.Database;
using StaffManagementApp.Staffs;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaffManagementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {


        static IConfiguration ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        DatabaseManagementSQL DbHelper = new DatabaseManagementSQL(ConfigBuilder.GetValue<string>("ConnectionString"));




        [HttpGet]
        public ActionResult<List<Models.Staff>> GetStaff()
        {
            List<Staff> staffs = DbHelper.DatabaseViewAll().ToList();
            return staffs.ConvertAll(ConvertStaff);
        }


        [HttpGet("{staffId:int}")]
        public ActionResult<Models.Staff> GetStaffById(int staffId)
        {
            Staff staff = DbHelper.DatabaseGetStaff(staffId);
            if (staff == null)
            {
                return NotFound();
            }
            return ConvertStaff(staff);

        }

        [HttpGet("{staffType}")]
        public ActionResult<List<Models.Staff>> GetStaffByType(string staffType)
        {
            List<Models.Staff> staffs = DbHelper.DatabaseViewAll().ConvertAll(ConvertStaff);
            return staffs.Where(item => item.StaffType == staffType).ToList();
        }

        [HttpPost]
        public ActionResult<Staff> PostStaff(Models.Staff staff)
        {
            Staff StaffNew = null;
            switch (staff.StaffType)
            {
                case nameof(Teacher):
                    StaffNew = new Teacher
                    {

                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        Subject = staff.Subject,
                        StaffType = staff.StaffType,

                    };
                    break;

                case nameof(Administrator):
                    StaffNew = new Administrator
                    {

                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        AdministratorDepartment = staff.AdministratorDepartment,
                        StaffType = staff.StaffType,

                    };
                    break;

                case nameof(Support):
                    StaffNew = new Support
                    {

                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        SupportDepartment = staff.SupportDepartment,
                        StaffType = staff.StaffType,

                    };
                    break;
            }

            StaffNew.StaffId = DbHelper.DatabaseAddStaff(StaffNew);
            return Ok(StaffNew);
        }
        [HttpPut]
        public ActionResult<Staff> UpdateStaff(Models.Staff staff)
        {
            Staff StaffNew = null;
            switch (staff.StaffType)
            {
                case nameof(Teacher):
                    StaffNew = new Teacher
                    {
                       
                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        Subject = staff.Subject,
                        StaffType = staff.StaffType,

                    };
                    break;

                case nameof(Administrator):
                    StaffNew = new Administrator
                    {

                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        AdministratorDepartment = staff.AdministratorDepartment,
                        StaffType = staff.StaffType,

                    };
                    break;

                case nameof(Support):
                    StaffNew = new Support
                    {

                        StaffName = staff.StaffName,
                        StaffAge = staff.StaffAge,
                        SupportDepartment = staff.SupportDepartment,
                        StaffType = staff.StaffType,

                    };
                    break;
            }
            StaffNew.StaffId = staff.StaffId;
            DbHelper.DatabaseUpdateStaff(StaffNew);
            return Ok();

        }
        [HttpDelete("{staffId}")]
        public ActionResult<Staff> DeleteStaff(int staffId)
        {
            DbHelper.DatabaseDeleteStaff(staffId);
            return Ok("Deleted");

        }

        public static Models.Staff ConvertStaff(Staff staff)
        {
            Models.Staff staffNew = new Models.Staff
            {
                StaffId = staff.StaffId,
                StaffName = staff.StaffName,
                StaffAge = staff.StaffAge,
                StaffType = staff.StaffType,
            };

            switch (staff.StaffType)
            {

                case nameof(Teacher):
                    staffNew.Subject = ((Teacher)staff).Subject;
                    break;

                case nameof(Administrator):
                    staffNew.AdministratorDepartment = ((Administrator)staff).AdministratorDepartment;
                    break;

                case nameof(Support):
                    staffNew.SupportDepartment = ((Support)staff).SupportDepartment;
                    break;
            }

            return staffNew;
        }

    }
}
