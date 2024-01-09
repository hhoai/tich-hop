using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api3.Models;

namespace api3.Controllers
{
    public class DanhMucController : ApiController
    {
        CSDLtestEntities1 db = new CSDLtestEntities1();
        [HttpGet] public List<DanhMuc> laydm()
        {
            return db.DanhMucs.ToList();
        }
    }
}
