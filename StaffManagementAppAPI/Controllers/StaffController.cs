using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StaffManagementLibrary.DbHandler;
using StaffManagementLibrary.Staffs;
using StaffManagementLibrary.Staffs.HelperClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StaffManagementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class StaffController : ControllerBase
    {

        
        static IConfiguration ConfigBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        StaffHelper StaffHelper = new StaffHelper();
        DatabaseSQLHandler DbHelper = new DatabaseSQLHandler(ConfigBuilder.GetValue<string>("ConnectionString"));




        [HttpGet]
        public ActionResult<List<Models.Staff>> GetStaff()
        {
            List<Staff> staffs = DbHelper.ViewAll().ToList();
            return staffs.ConvertAll(ConvertStaff);
        }


        [HttpGet("{staffType}/{staffId:int}")]
        public ActionResult<Models.Staff> GetStaffById(int staffId,string staffType)
        {
            try
            {
                Staff staff = StaffHelper.StaffGet(staffId, DbHelper, staffType);
                return ConvertStaff(staff);

            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

        }

        [HttpGet("{staffType}")]
        public ActionResult<List<Models.Staff>> GetStaffByType(string staffType)
        {
            List<Models.Staff> staffs = StaffHelper.StaffGetAllByType(DbHelper).ConvertAll(ConvertStaff);
            return staffs.Where(item => (item.StaffType).ToLower() == staffType.ToLower()).ToList();
        }

       

        [HttpPost]
        public ActionResult<Staff> AddStaff(Models.Staff staff)
        {
            try { 
                Staff StaffNew=ConvertToStaffManagementStaff(staff);
                StaffNew = StaffHelper.StaffAdd(StaffNew,DbHelper,StaffNew.StaffType);
                return Ok(StaffNew);

            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

        }

        [HttpPut("{staffType}/{staffId:int}")]
        public ActionResult<Staff> UpdateStaff(Models.Staff staff,string staffType,int staffId)
        {
            if (staff?.StaffType!=staffType )
            {
                return NotFound($"Staff is not a {staffType}");
            }

            try
            {
                Staff StaffNew = ConvertToStaffManagementStaff(staff);
                StaffNew.StaffId = staffId;
                StaffNew=StaffHelper.StaffUpdate(StaffNew, DbHelper);
                return Ok(StaffNew);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }
        }


        [HttpDelete("{staffType}/{staffId}")]
        public ActionResult<Staff> DeleteStaff(int staffId,string staffType)
        {
            try
            {
                Staff staff = StaffHelper.StaffGet(staffId, DbHelper, staffType);
                StaffHelper.DeleteStaff(staff, DbHelper, staffType);
                return Ok("Deleted");
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

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

        private static Staff ConvertToStaffManagementStaff(Models.Staff staff)
        {
           
            Staff StaffNew = null;
            switch (staff.StaffType)
            {
                case nameof(Teacher):
                    StaffNew = new Teacher
                    {
                        Subject = staff.Subject,
                    };
                    break;

                case nameof(Administrator):
                    StaffNew = new Administrator
                    {
                        AdministratorDepartment = staff.AdministratorDepartment,
                    };
                    break;

                case nameof(Support):
                    StaffNew = new Support
                    {
                        SupportDepartment = staff.SupportDepartment,
                    };
                    break;
            }
            if (StaffNew==null)
            {
                throw new Exception("Staff Type Not Found");
            }
            StaffNew.StaffName = staff.StaffName;
            StaffNew.StaffAge = staff.StaffAge;
            StaffNew.StaffType = staff.StaffType;
            return StaffNew;
        }

    }
}
