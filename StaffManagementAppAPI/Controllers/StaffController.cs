using Microsoft.AspNetCore.Mvc;
using StaffManagementAppAPI.Models;
using StaffManagementAppAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using static StaffManagementAppAPI.Models.Staff;

namespace StaffManagementAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {

        [HttpGet]
        public ActionResult<List<Staff>> GetStaff()
        {
            return StaffServices.staffs;
        }


        [HttpGet("{staffId:int}")]
        public ActionResult<Staff> GetStaffById(int staffId)
        {
            Staff staff = StaffServices.staffs.Find(item => item.Id == staffId);
            if (staff == null)
            {
                return NotFound();
            }
            return Ok(staff);
        }

        [HttpGet("{staffType}")]
        public ActionResult<List<Staff>> GetStaffByType(string staffType)
        {

            var staffs = StaffServices.staffs.Where(item => Enum.GetName(typeof(StaffType), item.Type) == staffType).ToList();
            
            return Ok(staffs);
        }

        [HttpPost]
        public ActionResult<Staff> PostStaff(Staff staff)
        {
            
            StaffServices.staffs.Add(staff);
            return Ok(staff);
        }
        [HttpPut]
        public ActionResult<Staff> UpdateStaff(Staff staff)
        {
            StaffServices.staffs[StaffServices.staffs.FindIndex(index => index.Id == staff.Id)] = staff;
            return Ok(staff);

        }
        [HttpDelete("{staffId}")]
        public ActionResult<Staff> DeleteStaff(int staffId)
        {
            int index = StaffServices.staffs.FindIndex(index => index.Id == staffId);
            Staff staff = StaffServices.staffs[index];
            StaffServices.staffs.RemoveAt(index);
            return Ok(staff);

        }


        //[HttpGet("{responseCode}")]
        //public ActionResult<HttpStatusCode> GetStaff(int responseCode)
        //{


        //    //Enum.TryParse(responseCode.ToString(),true, out HttpStatusCode  c);
        //    return HttpStatusCode.BadRequest;
        //}

    }
}
