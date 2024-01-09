using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace app1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void LoadDataGridView()
        {
            string link = "https://localhost:44391/api/sanpham";    // goi api lay toan bo sp

            HttpWebRequest rq = WebRequest.CreateHttp(link);       // tao yc gui toi web

            WebResponse rs = rq.GetResponse();      // lay thong tin tra ve

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(rs.GetResponseStream());

            SanPham[] dssp = (SanPham[]) data;

            dataSP.DataSource = dssp;
        }
        public void loadComBoBox()
        {
            string link = "https://localhost:44391/api/danhmuc";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DanhMuc[]));
            object data = js.ReadObject(response.GetResponseStream());
            DanhMuc[] arr1 = data as DanhMuc[];
            cbDM.DataSource = arr1;

            cbDM.ValueMember = "MaDanhMuc";
            cbDM.DisplayMember = "TenDanhMuc";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            loadComBoBox();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string postString = string.Format("?ma={0}&ten={1}&gia={2}&madm={3}", txtMaSp.Text, txtTenSp.Text, txtDongia.Text, cbDM.SelectedValue);

            string link = "https://localhost:44345/api/sanpham/" + postString;

            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            req.Method = "POST";
            
            // tao luong yeu cau
            Stream dataStream = req.GetRequestStream();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));

            object data = js.ReadObject(req.GetResponse().GetResponseStream());

            bool kq = (bool)data;

            if (kq)
            {
                LoadDataGridView();
                MessageBox.Show("them san pham thanh cong");
            }
            else
                MessageBox.Show("them san pham khong thanh cong");
        }

        private void dataSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtDongia.Text = dataSP.Rows[d].Cells[2].Value.ToString();
            txtMaSp.Text = dataSP.Rows[d].Cells[0].Value.ToString();
            txtTenSp.Text = dataSP.Rows[d].Cells[1].Value.ToString();
            cbDM.Text = dataSP.Rows[d].Cells[3].Value.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string madm = txtTim.Text;
            string link = "https://localhost:44345/api/sanpham?madm=" + madm;

            HttpWebRequest req = WebRequest.CreateHttp(link);

            WebResponse res = req.GetResponse();

            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));

            object data = js.ReadObject(res.GetResponseStream());

            SanPham[] dssp = data as SanPham[];

            dataSP.DataSource = dssp;
        }
    }
}
