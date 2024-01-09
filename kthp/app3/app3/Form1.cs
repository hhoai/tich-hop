using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;

namespace app3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void hienthidl()
        {
            string link = "https://localhost:44391/api/sanpham";

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);

            WebResponse res = req.GetResponse();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(res.GetResponseStream());

            SanPham[] dssp = (SanPham[])data;

            dataSP.DataSource = dssp;
        }

        public void hienthicb()
        {
            string link = "https://localhost:44391/api/danhmuc";

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DanhMuc[]));

            object data = js.ReadObject(res.GetResponseStream());

            DanhMuc[] dsdm = (DanhMuc[])data;

            cbDM.DataSource = dsdm;
            cbDM.DisplayMember = "madm";
            cbDM.ValueMember = "tendm";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hienthidl();
            hienthicb();
        }

        private void dataSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtDongia.Text = dataSP.Rows[d].Cells[2].Value.ToString();
            txtMasp.Text = dataSP.Rows[d].Cells[0].Value.ToString();
            txtTenSp.Text = dataSP.Rows[d].Cells[1].Value.ToString();
            cbDM.Text = dataSP.Rows[d].Cells[3].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&dongia={2}&madm={3}", txtMasp.Text, txtTenSp.Text, txtDongia.Text, cbDM.Text);

            string link = "https://localhost:44391/api/sanpham/" + postString;

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);

            req.Method = "POST";

            Stream dataSream = req.GetRequestStream();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));

            object data = js.ReadObject(req.GetResponse().GetResponseStream());

            bool kq = (bool)data;

            if (kq)
            {
                hienthidl();
                MessageBox.Show("thanh cong");
            }
            else
                MessageBox.Show("that bai");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&dongia={2}&madm={3}", txtMasp.Text, txtTenSp.Text, txtDongia.Text, cbDM.Text);

            string link = "https://localhost:44391/api/sanpham" + postString;

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);

            req.Method = "PUT";

            Stream dataSream = req.GetRequestStream();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));

            object data = js.ReadObject(req.GetResponse().GetResponseStream());

            bool kq = (bool)data;

            if (kq)
            {
                hienthidl();
                MessageBox.Show("thanh cong");
            }
            else
                MessageBox.Show("that bai");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string put = string.Format("?ma={0}", txtMasp.Text);

            string link = "https://localhost:44391/api/sanpham" + put;

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);

            req.Method = "DELETE";

            Stream dataSream = req.GetRequestStream();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));

            object data = js.ReadObject(req.GetResponse().GetResponseStream());

            bool kq = (bool)data;

            if (kq)
            {
                hienthidl();
                MessageBox.Show("thanh cong");
            }
            else
                MessageBox.Show("that bai");
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string tim = txtTim.Text;

            string link = "https://localhost:44391/api/sanpham?madm=" + tim;

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);

            WebResponse res = req.GetResponse();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(res.GetResponseStream());

            SanPham[] dssp =data as SanPham[];

            dataSP.DataSource = dssp;

        }
    }
}
