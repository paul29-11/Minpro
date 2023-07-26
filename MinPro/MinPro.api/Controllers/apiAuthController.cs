using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinPro.datamodels;
using MinPro.viewmodels;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiAuthController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        //private VMResponse respon = new VMResponse();Z
        private int IdUser = 1;

        public apiAuthController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("CheckLogin/{email}")]
        public VMUser CheckLogin(string email)
        {
            VMUser data = (from cs in db.MUsers
                                  join r in db.MRoles on cs.RoleId equals r.Id
                                  where cs.IsDelete == false && r.IsDelete == false && cs.Email == email
                                  select new VMUser
                                  {
                                      Id = cs.Id,
                                      Email = cs.Email,
                                      IdRole = r.Id,

                                  }).FirstOrDefault()!;
            return data;
        }

        [HttpGet("MenuAccess/{IdRole}")]
        public List<VMMenuAccess> MenuAccess(int IdRole)
        {
            List<VMMenuAccess> listMenu = new List<VMMenuAccess>();

            listMenu = (from parent in db.MMenus
                        join ma in db.MMenuRoles
                        on parent.Id equals ma.MenuId
                        where parent.ParentId == parent.Id && ma.RoleId == IdRole
                        && parent.IsDelete == false && ma.IsDelete == false
                        select new VMMenuAccess
                        {
                            Id = parent.Id,
                            MenuName = parent.Name,
                            MenuAction = parent.Url,
                            MenuController = parent.Url,
                            IdRole = ma.Id
                            
                        }).OrderBy(a => a.MenuName).ToList();

            return listMenu;
        }

        [HttpGet("Menu/{IdRole}")]
        public List<VMMenuAccess> Menu(int IdRole)
        {
            List<VMMenuAccess> listMenu = 
                
            (from MenRol in db.MMenuRoles
             join Men in db.MMenus on MenRol.MenuId equals Men.Id
             join Rol in db.MRoles on MenRol.RoleId equals Rol.Id
             where MenRol.RoleId == IdRole && MenRol.IsDelete == false
             select new VMMenuAccess
             {
                 Id = Men.Id,
                 MenuName = Men.Name,
                 MenuAction = Men.Url,
                 MenuController = Men.Name,
                 MenuIcon = Men.SmallIcon,
                 IdRole = IdRole,
             }).ToList();

            return listMenu;
        }

    }
}
