using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiPasienController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;

        public apiPasienController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblPasien> GetAllData()
        {
            List<VMTblPasien> data = (from cm in db.MCustomerMembers
                                      join c in db.MCustomers on cm.CustomerId equals c.Id
                                      join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                                      join b in db.MBiodata on c.BiodataId equals b.Id
                                      join bg in db.MBloodGroups on c.BloodGroupId equals bg.Id into bloodGroups
                                      from bg in bloodGroups.DefaultIfEmpty()
                                      where cm.IsDelete == false
                                        select new VMTblPasien
            {
                                          Id = cm.Id,
                                          CustomerId = cm.CustomerId,
                                          ParentBiodataId = cm.ParentBiodataId,
                                          CustomerRelationId = cm.CustomerRelationId,

                                          Fullname = b.Fullname,
                                          Dob = c.Dob,
                                          Gender = c.Gender,
                                          BloodGroupId = c.BloodGroupId,
                                          RhesusType = c.RhesusType,
                                          Height = c.Height,
                                          Weight = c.Weight,
                                          Name = cr.Name,

                                          CreatedBy = c.Id,
                                          //Code = bg != null ? bg.Code : null,
                                        }).ToList();
            return data;
        }

        [HttpGet("GetDataById/{id}")]
        public VMTblPasien GetDataById(int id)
        {
            VMTblPasien data = (from cm in db.MCustomerMembers
                                join c in db.MCustomers on cm.CustomerId equals c.Id
                                join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                                join b in db.MBiodata on c.BiodataId equals b.Id
                                join bg in db.MBloodGroups on c.BloodGroupId equals bg.Id into bloodGroupJoin
                                from bg in bloodGroupJoin.DefaultIfEmpty() 
                                where cm.IsDelete == false && cm.Id == id
                                select new VMTblPasien
                                {
                                    Id = cm.Id,
                                    CustomerId = cm.CustomerId,
                                    ParentBiodataId = cm.ParentBiodataId,
                                    CustomerRelationId = cm.CustomerRelationId,
                                    Fullname = b.Fullname,
                                    Dob = c.Dob,
                                    Gender = c.Gender,
                                    BloodGroupId = c.BloodGroupId,
                                    Code = bg != null ? bg.Code : null, 
                                    RhesusType = c.RhesusType,
                                    Height = c.Height,
                                    Weight = c.Weight,
                                    CreatedBy = c.Id
                                }).FirstOrDefault()!;

            return data;
        }



        [HttpGet("GetDataByIdParent/{id}")]
        public List<VMTblPasien> GetDataByIdParent(int id)
        {
            List<VMTblPasien> data = (from cm in db.MCustomerMembers
                                      join c in db.MCustomers on cm.CustomerId equals c.Id
                                      join cr in db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                                      join b in db.MBiodata on c.BiodataId equals b.Id
                                      join bg in db.MBloodGroups on c.BloodGroupId equals bg.Id into bloodGroups
                                      from bg in bloodGroups.DefaultIfEmpty()
                                      where cm.IsDelete == false && cm.ParentBiodataId == id
                                      select new VMTblPasien
                                      {
                                          Id = cm.Id,
                                          CustomerId = cm.CustomerId,
                                          ParentBiodataId = cm.ParentBiodataId,
                                          CustomerRelationId = cm.CustomerRelationId,

                                          Fullname = b.Fullname,
                                          Dob = c.Dob,
                                          Gender = c.Gender,
                                          BloodGroupId = c.BloodGroupId,
                                          RhesusType = c.RhesusType,
                                          Height = c.Height,
                                          Weight = c.Weight,
                                          Name = cr.Name,

                                          CreatedBy = c.Id
                                      }).ToList();
            return (data);
        }

        [HttpPost("Save")]
        public VMResponse Save(VMTblPasien data)
        {
            try
            {
                MBiodata mBiodata = new MBiodata();
                mBiodata.Fullname = data.Fullname;
                mBiodata.CreatedBy = IdUser;
                mBiodata.CreatedOn = DateTime.Now;
                mBiodata.IsDelete = false;

                db.Add(mBiodata);
                db.SaveChanges();

                MCustomer mCustomer = new MCustomer();
                mCustomer.BiodataId = mBiodata.Id;
                mCustomer.Dob = data.Dob;
                mCustomer.Gender = data.Gender;
                mCustomer.Height = data.Height;
                mCustomer.Weight = data.Weight;
                mCustomer.BloodGroupId = data.BloodGroupId;
                mCustomer.RhesusType = data.RhesusType;
                mCustomer.CreatedBy = IdUser;
                mCustomer.CreatedOn = DateTime.Now;
                mCustomer.IsDelete = false;

                db.Add(mCustomer);
                db.SaveChanges();

                MCustomerMember mCustomerMember = new MCustomerMember();
                mCustomerMember.ParentBiodataId = data.ParentBiodataId;
                mCustomerMember.CustomerId = mCustomer.Id;
                mCustomerMember.CustomerRelationId = data.CustomerRelationId;
                mCustomerMember.CreatedBy = IdUser;
                mCustomerMember.CreatedOn = DateTime.Now;
                mCustomerMember.IsDelete = false;

                db.Add(mCustomerMember);
                db.SaveChanges();

                respon.Message = "Saved ya guys";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "failed save: " + e.Message;
            }
            return respon;
        }


        [HttpPut("Edit")]
        public VMResponse Edit(VMTblPasien data)
        {
            MCustomerMember dcm = db.MCustomerMembers.Where(c=>c.Id == data.Id).FirstOrDefault();
            dcm.CustomerRelationId = data.CustomerRelationId;
            dcm.ModifiedOn = DateTime.Now;
            dcm.ModifiedBy = data.ParentBiodataId;  
            db.Update(dcm);

            MCustomer dc = db.MCustomers.Where(b=>b.Id == dcm.CustomerId).FirstOrDefault();
            dc.Dob = data.Dob;
            dc.Gender = data.Gender;
            dc.BloodGroupId = data.BloodGroupId;
            dc.RhesusType = data.RhesusType;    
            dc.Height = data.Height;
            dc.Weight = data.Weight;
            dc.ModifiedBy = data.ParentBiodataId;
            dc.ModifiedOn = DateTime.Now;
            db.Update(dc);        

            MBiodata dt = db.MBiodata.Where(a => a.Id == dc.BiodataId).FirstOrDefault();
            dt.Fullname = data.Fullname;
            dt.ModifiedBy= data.ParentBiodataId;
            dt.ModifiedOn = DateTime.Now;
            db.Update(dt);

                try
                {
                    db.SaveChanges();
                    respon.Message = "Data Saved Guys";
                }
                catch (Exception)
                {

                    respon.Success = false;
                    respon.Message = "failed Gengs :(";
                }
            return respon;
        }


        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)

        {
            MCustomerMember dt = db.MCustomerMembers.Where(a => a.Id == id).FirstOrDefault();
            if (dt != null)
            {

                dt.IsDelete = true;
                dt.DeletedBy = IdUser;
                dt.DeletedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();
                    respon.Message = $"Success Deleted";
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


        [HttpPut("MultipleDelete")]
        public VMResponse MultipleDelete(List<int> listId)
        {
            if (listId.Count > 0)
            {
                foreach (int item in listId)
                {

                    MCustomerMember dt = db.MCustomerMembers.Where(a => a.Id == item).FirstOrDefault();

                    dt.IsDelete = true;
                    dt.DeletedBy = IdUser;
                    dt.DeletedOn = DateTime.Now;
                    db.Update(dt);
                }
                try
                {
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

            }
            return respon;

        }

    }
}