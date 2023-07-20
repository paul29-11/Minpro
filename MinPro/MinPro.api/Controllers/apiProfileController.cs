
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;
using System.Drawing;
using xpos319.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiProfileController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
      

        public apiProfileController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblProfile> GetAllData()
        {
            List<VMTblProfile> data = (from u in db.MUsers
                                       join b in db.MBiodata on u.BiodataId equals b.Id
                                       join r in db.MRoles on u.RoleId equals r.Id
                                       join c in db.MCustomers on b.Id equals c.BiodataId
                                       where u.IsDelete == false
                                       select new VMTblProfile
                                       {
                                           Id = u.Id,
                                           BiodataId = b.Id,
                                           RoleId = r.Id,
                                           CustomerId = c.Id,
                                           Fullname = b.Fullname,
                                           Dob = c.Dob,
                                           MobilePhone = b.MobilePhone,
                                           Email = u.Email,
                                           Password = u.Password,

                                           CreatedBy = u.CreatedBy,
                                           IsDelete = u.IsDelete

                                       }).ToList();
            return data;

        }

        [HttpGet("GetDataById/{Id}")]
        public VMTblProfile GetDataById(int Id)
        {
            VMTblProfile data = (from u in db.MUsers
                                 join b in db.MBiodata on u.BiodataId equals b.Id
                                 join r in db.MRoles on u.RoleId equals r.Id
                                 join c in db.MCustomers on b.Id equals c.BiodataId
                                 where u.IsDelete == false && u.Id == Id
                                 select new VMTblProfile
                                 {
                                     Id = u.Id,
                                     BiodataId = b.Id,
                                     RoleId = r.Id,
                                     CustomerId = c.Id,
                                     Fullname = b.Fullname,
                                     Dob = c.Dob,
                                     MobilePhone = b.MobilePhone,
                                     Email = u.Email,
                                     Password = u.Password,

                                     CreatedBy = u.CreatedBy,
                                     IsDelete = u.IsDelete

                                 }).FirstOrDefault()!;
            return data;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(VMTblProfile data)
        {
            MBiodata dt = db.MBiodata.Where(a => a.Id == data.BiodataId).FirstOrDefault();
            if (dt != null)
            {
                dt.Fullname = data.Fullname;
                dt.MobilePhone = data.MobilePhone;
                dt.ModifiedBy = data.Id;
                dt.ModifiedOn = DateTime.Now;
                db.Update(dt);
                
                MCustomer dc = db.MCustomers.Where(a=>a.Id == data.CustomerId).FirstOrDefault();    
                dc.Dob = data.Dob;
                dc.ModifiedBy = data.Id;
                dc.ModifiedOn = DateTime.Now;
                db.Update(dc);

                try
                {
                    db.SaveChanges();
                    respon.Message = "Data Saved Guys";
                }
                catch (Exception)
                {

                    respon.Success = false;
                    respon.Message = "failed Gegns :(";
                }
            }
            else 
            { respon.Success = false;
              respon.Message = "Data Gada Guys";
            }
            return respon;
     
        }

        [HttpPut("EditM")]
        public VMResponse EditM(VMTblProfile data)
        {
            MUser dt = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.Email = data.Email;
                dt.ModifiedBy = data.Id;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Data Saved Guys";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed Guys";

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data Gada Guys";

            }
            return respon;

        }
        [HttpPut("EditP")]
        public VMResponse EditP(VMTblProfile data)
        {
            MUser dt = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.Password = data.Password;
                dt.ModifiedBy = data.Id;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Data Saved Guys";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed Guys";

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data Gada Guys";

            }
            return respon;
        }
    }
}