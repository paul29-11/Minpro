using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiBloodGroupController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
        int IdUser = 1;

        public apiBloodGroupController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<MBloodGroup> GetAllData()
        {
            List<MBloodGroup> data = db.MBloodGroups.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MBloodGroup DataById(int id)
        {
            MBloodGroup result = new MBloodGroup();
            result = db.MBloodGroups.Where(a => a.Id == id).FirstOrDefault();
            return result;
        }

        [HttpGet("CheckCodeById/{code}/{id}")]
        public bool CheckName(string code, int id)
        {
            MBloodGroup data = new MBloodGroup();
            if (id == 0)
            {
                data = db.MBloodGroups.Where(a => a.Code == code && a.IsDelete == false).FirstOrDefault();
            }
            else
            {
                data = db.MBloodGroups.Where(a => a.Code == code && a.IsDelete == false && a.Id != id).FirstOrDefault();
            }

            if (data != null)
            {
                return true;
            }
            else { return false; }
        }

        [HttpPost("Save")]
        public VMResponse Save(MBloodGroup data)
        {
            data.Description = data.Description ?? "";
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();
                respon.Message = "OK";

            }
            catch (Exception e)
            {
                respon.Success = false;

                throw;
            }
            return respon;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(MBloodGroup data)
        {
            MBloodGroup dt = db.MBloodGroups.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                data.Description = data.Description ?? "";
                dt.Code = data.Code;
                dt.Description = data.Description;
                dt.ModifiedBy = IdUser;
                dt.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Data success saved";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "failed";
                    throw;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data Not Found";

            }
            return respon;

        }
        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)

        {
            MBloodGroup dt = db.MBloodGroups.Where(a => a.Id == id).FirstOrDefault();
            if (dt != null)
            {
                dt.IsDelete = true;
                dt.DeletedOn = DateTime.Now;
                dt.DeletedBy = IdUser;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();
                    respon.Message = $"Data {dt.Code} Success Deleted";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "failed";

                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "DataNotfound";
            }
            return respon;
        }
    }
}
