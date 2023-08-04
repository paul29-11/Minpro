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

        [HttpGet("MenuAccess/{IdRole}")]
        public List<VMMenuAccess> MenuAccess(int IdRole)
        {
            List<VMMenuAccess> listMenu = new List<VMMenuAccess>();

            listMenu = (from parent in db.MMenus
                        join mr in db.MMenuRoles
                        on parent.Id equals mr.MenuId
                        where parent.ParentId == null && mr.RoleId == IdRole
                        && parent.IsDelete == false && mr.IsDelete == false
                        select new VMMenuAccess
                        {
                            Id = parent.Id,
                            MenuName = parent.Name,
                            MenuUrl = parent.Url,
                            MenuIcon = parent.SmallIcon,
                            IdRole = mr.RoleId,
                            ListChild = (from child in db.MMenus
                                         where child.ParentId == parent.Id && child.IsDelete == false && mr.RoleId == IdRole
                                         select new VMMenuAccess
                                         {
                                             Id = child.Id,
                                             ParentId = parent.ParentId,
                                             MenuName = child.Name,
                                             MenuUrl = child.Url,
                                             MenuIcon = child.SmallIcon,
                                             IdRole = IdRole,
                                         }).ToList(),
                        }).ToList();

            return listMenu;
        }

    }
}
