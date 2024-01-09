using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using api3.Models;

namespace api3.Controllers
{
    public class SanPhamController : ApiController
    {
        CSDLtestEntities1 db = new CSDLtestEntities1();

        [HttpGet]
        public List<SanPham> laysp()
        {
            return db.SanPhams.ToList();
        }
        [HttpGet]
        public List<SanPham> laysptheomadm(int madm)
        {
            return db.SanPhams.Where(x => x.madm == madm).ToList();
        }
        [HttpGet]
        public SanPham laysptheomasp(int ma)
        {
            return db.SanPhams.FirstOrDefault(x => x.ma == ma);
        }

        [HttpDelete] public bool Delete(int ma)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.ma == ma);
            if (sp != null)
            {
                db.SanPhams.Remove(sp);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost] // them
        public bool themsp(int ma, string ten, int dongia, int madm)
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.ma == ma);
            if (sp == null)
            {
                SanPham spm = new SanPham();
                spm.ten = ten;
                spm.dongia = dongia;
                spm.ma = ma;
                spm.madm = madm;

                db.SanPhams.Add(spm);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpPut]
        public bool suasp(int ma, string ten, int dongia, int madm) 
        {
            SanPham sp = db.SanPhams.FirstOrDefault(x => x.ma == ma);
            if (sp != null)
            {
                sp.ten = ten;
                sp.dongia = dongia;
                sp.ma = ma;
                sp.madm = madm;

                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
