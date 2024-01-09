using api2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api2.Controllers
{
    public class DanhMucController : ApiController
    {
        CSDLtestEntities1 db = new CSDLtestEntities1();
        [HttpGet]
        public List<DanhMuc> LayDM()
        {
            return db.DanhMucs.ToList();
        }
    }
}
