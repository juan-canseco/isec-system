using DataAccess.Server;
using IsecService.Server.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace IsecService.Controllers
{
    [RoutePrefix("api/permisos")]
    public class AccesoUsuarioController : ApiController
    { 
        AccesoUsuarioRepository accesoUsuarioRepo = new AccesoUsuarioRepository(); 
        [HttpGet]
        [Route("all")]
        public JsonResult<List<AccesoUsuarioServer>> all()
        {
            return Json(accesoUsuarioRepo.GetAll());
        }

    }
}
