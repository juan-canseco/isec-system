using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Server;
using System.Web.Http.Results;
using DataAccess.Local;
namespace IsecService.Controllers
{
    [RoutePrefix("api/roles")]

    public class RolController : ApiController
    {
        RolRepository rolRepo = new RolRepository();
        [HttpGet]
        [Route("all")]
        public JsonResult<List<RolServer>> all()
        {
            return Json(rolRepo.GetAll());
        }
        [HttpPost]
        [Route("sync")]
        public JsonResult<List<int>> sync([FromBody] List<RolLocal> noSync)
        {
            return Json(rolRepo.RolesNoSync(noSync));
        }
        [HttpPost]
        [Route("syncInserted")]
        public JsonResult<int> syncInserted([FromBody] RolLocal rolLocal)
        {
            return Json(rolRepo.AddRolInserted(rolLocal));
            
        }
        [HttpPost]
        [Route("syncUpdated")]
        public JsonResult<int> syncUpdated([FromBody] RolLocal rolLocal)
        {
            return Json(rolRepo.AddRolUpdated(rolLocal)); 
        }
    }
}
