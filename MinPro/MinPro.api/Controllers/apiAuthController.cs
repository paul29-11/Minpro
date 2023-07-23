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

        [HttpGet("MenuAccess/{IdRole}")]
        public List<VMMenuAccess> MenuAccess(int IdRole)
        {
            List<VMMenuAccess> listMenu = new List<VMMenuAccess>();

            listMenu = (from parent in db.MMenus
                        join ma in db.MMenuRoles
                        on parent.Id equals ma.MenuId
                        where parent.ParentId == parent.Id && ma.RoleId == 1
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

    }
}
