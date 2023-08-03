using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiUserController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();

        public apiUserController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("DataById/{id}")]
        public VMUser DataById(int id)
        {
            VMUser data = (from User in db.MUsers
                             join Biodata in db.MBiodata on User.BiodataId equals Biodata.Id
                             join Role in db.MRoles on User.RoleId equals Role.Id

                             where User.IsDelete == false && User.Id == id

                             select new VMUser
                             {
                                 Id = User.Id,
                                 IdRole = Role.Id,
                                 Name = Biodata.Fullname,
                                 ImagePath = Biodata.ImagePath

                             }).FirstOrDefault()!;
            return data;
        }
    }
}
