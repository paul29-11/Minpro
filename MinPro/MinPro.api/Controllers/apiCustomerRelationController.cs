using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiCustomerRelationController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
        int IdUser = 1;

        public apiCustomerRelationController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<MCustomerRelation> GetAllData()
        {
            List<MCustomerRelation> data = db.MCustomerRelations.Where(a => a.IsDelete == false).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public MCustomerRelation DataById(int id)
        {
            MCustomerRelation result = new MCustomerRelation();
            result = db.MCustomerRelations.Where(a => a.Id == id).FirstOrDefault();
            return result;
        }

        [HttpGet("CheckNameById/{name}/{id}")]
        public bool CheckNameById(string name, int id)
        {
            MCustomerRelation data = new MCustomerRelation();
            if (id == 0)
            {
                data = db.MCustomerRelations.Where(a => a.Name == name && a.IsDelete == false).FirstOrDefault();
            }
            else
            {
                data = db.MCustomerRelations.Where(a => a.Name == name && a.IsDelete == false && a.Id != id).FirstOrDefault();
            }
            return data != null;
            //if (data != null)
            //{
            //    return true;
            //}
            //else { return false; }
        }

        [HttpPost("Save")]
        public VMResponse Save(MCustomerRelation data)
        {
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
        public VMResponse Edit(MCustomerRelation data)
        {
            MCustomerRelation dt = db.MCustomerRelations.Where(a => a.Id == data.Id).FirstOrDefault();

            if (dt != null)
            {
                dt.Name = data.Name;
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
            MCustomerRelation dt = db.MCustomerRelations.Where(a => a.Id == id).FirstOrDefault();
            if (dt != null)
            {
                dt.IsDelete = true;
                dt.DeletedOn = DateTime.Now;
                dt.DeletedBy = IdUser;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();
                    respon.Message = $"Data {dt.Name} Success Deleted";
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
