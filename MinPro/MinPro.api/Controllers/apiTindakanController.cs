using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;
using System.Drawing;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiTindakanController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 1;
        private int DoctorId = 3;

        public apiTindakanController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetTreatment/{id}")]
        public List<VMDoctorTreatment> GetTreatment(int id)
        {
            List<VMDoctorTreatment> data = (from DoctorTreatment in db.TDoctorTreatments
                                            join Doctor in db.MDoctors on DoctorTreatment.DoctorId equals Doctor.Id
                                            join Biodata in db.MBiodata on Doctor.BiodataId equals Biodata.Id
                                            where DoctorTreatment.IsDelete == false && Doctor.IsDelete == false
                                            && DoctorTreatment.DoctorId == id
                                            select new VMDoctorTreatment
                                            {
                                                Id = DoctorTreatment.Id,
                                                DoctorId = Doctor.Id,
                                                Name = DoctorTreatment.Name,

                                                CreatedBy = DoctorTreatment.CreatedBy,
                                                CreatedOn = DoctorTreatment.CreatedOn

                                            }).ToList();
            return data;
   
        }

        [HttpGet("GetDataById/{id}")]
        public TDoctorTreatment DataById(int id)
        {
            TDoctorTreatment result = db.TDoctorTreatments.Where(a => a.Id == id).FirstOrDefault()!;
            return result;
        }

        [HttpGet("CheckByName/{name}/{id}")]
        public bool CheckByName(string name, int id)
        {
            TDoctorTreatment data = new TDoctorTreatment();
            if (id == 0)
            {
                data = db.TDoctorTreatments.Where(a => a.Name == name && a.IsDelete == false && a.DoctorId == id).FirstOrDefault()!;
            }
            else
            {
                data = db.TDoctorTreatments.Where(a => a.Name == name && a.IsDelete == false && a.DoctorId == id).FirstOrDefault()!;

            }
            if (data != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost("Save")]
        public VMResponse Save(TDoctorTreatment data)
        {
            //data.Image = data.Image ?? "";
            data.DoctorId = DoctorId;
            data.CreatedBy = IdUser;
            data.CreatedOn = DateTime.Now;
            data.IsDelete = false;

            try
            {
                db.Add(data);
                db.SaveChanges();

                respon.Message = "Data Berhasil Di Masukan";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "Data Gagal Di Simpan";
            }
            return respon;
        }

        [HttpDelete("Delete/{id}")]
        public VMResponse Delete(int id)
        {
            TDoctorTreatment dt = db.TDoctorTreatments.Where(a => a.Id == id).FirstOrDefault()!;
            if (dt != null)
            {
                dt.IsDelete = true;
                dt.DeletedBy = IdUser;
                dt.DeletedOn = DateTime.Now;

                try
                {
                    db.Update(dt);
                    db.SaveChanges();

                    respon.Message = "Berhasil Di Hapus";
                }
                catch (Exception e)
                {

                    respon.Success = false;
                    respon.Message = "Gagal Di Ubah" + e.Message;
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "Data Not Found";
            }
            return respon;
        }

    }
}
