using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace bai3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XmlDocument doc = new XmlDocument();
        string tentep = "C:\\Users\\TGDD\\Desktop\\KTLT\\TichHop\\kttx2\\kttx2\\nhanvien.xml";
        int d;
        public void HienThi()
        {
            dataNV.Rows.Clear();

            doc.Load(tentep);
            //XmlElement goc = doc.DocumentElement;

            XmlNodeList dsnv = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            dataNV.Rows.Add();
            foreach (XmlNode nv in dsnv) 
            {
                XmlNode manv = nv.SelectSingleNode("@manv");
                dataNV.Rows[sd].Cells[0].Value = manv.InnerText;

                XmlNode ho = nv.SelectSingleNode("hoten/ho");
                dataNV.Rows[sd].Cells[1].Value = ho.InnerText;

                XmlNode ten = nv.SelectSingleNode("hoten/ten");
                dataNV.Rows[sd].Cells[2].Value = ten.InnerText;

                XmlNode diachi = nv.SelectSingleNode("diachi");
                dataNV.Rows[sd].Cells[3].Value = diachi.InnerText;

                dataNV.Rows.Add();
                sd++;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void Clear()
        {
            txtDiaChi.Text = string.Empty;
            txtHo.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            txtTen.Text = string.Empty;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nv = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txtMaNV.Text;
            nv.Attributes.Append(manv);

            XmlNode hoten = doc.CreateElement("hoten");
            XmlNode ho = doc.CreateElement("ho");
            ho.InnerText = txtHo.Text;
            hoten.AppendChild(ho);

            XmlNode ten = doc.CreateElement("ten");
            ten.InnerText = txtTen.Text;
            hoten.AppendChild(ten);

            nv.AppendChild(hoten);

            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txtDiaChi.Text;

            nv.AppendChild(diachi);
            goc.AppendChild(nv);

            doc.Save(tentep);
            Clear();
            MessageBox.Show("them nhan vien thanh cong");
            HienThi();
        }

        private void dataNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txtMaNV.Text = dataNV.Rows[d].Cells[0].Value.ToString();
            txtHo.Text = dataNV.Rows[d].Cells[1].Value.ToString();
            txtTen.Text = dataNV.Rows[d].Cells[2].Value.ToString();
            txtDiaChi.Text = dataNV.Rows[d].Cells[3].Value.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            //XmlNodeList dsnvxoa = goc.SelectNodes("/ds/nhanvien[/diachi='" + txtDiaChi.Text + "']");

            XmlNode nv = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");

            string dc = txtDiaChi.Text;

            //foreach (var employee in goc.SelectNodes("nhanvien"))
            //{
            //    XmlNode addressElement = employee.SelectSingleNode("diachi") as XmlElement;

            //    if (addressElement != null && addressElement.Value == dc)
            //    {
            //        employeesToDelete.Add(employee);
            //    }
            //}

            DialogResult rs = MessageBox.Show("Ban co muon xoa?", "Thong bao", MessageBoxButtons.YesNo);

            if (rs == DialogResult.Yes && nv != null)
            {
                //foreach (XmlNode nv in dsnvxoa)
                //{
                //    goc.RemoveChild(nv);
                //    doc.Save(tentep);
                //}

                goc.RemoveChild(nv);
                doc.Save(tentep);

                MessageBox.Show("xoa thanh cong!", "Thong bao");
                Clear();
                HienThi();
            }
            else
                MessageBox.Show("xoa khong thanh cong!", "Thong bao");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nvcu = goc.SelectSingleNode("/ds/nhanvien[/diachi='" + txtDiaChi.Text + "']");
            //XmlNode dc = nvcu.SelectSingleNode("diachi");

            XmlNode nv = doc.CreateElement("nhanvien");
            XmlAttribute manv = doc.CreateAttribute("manv");
            manv.InnerText = txtMaNV.Text;
            nv.Attributes.Append(manv);

            XmlNode hoten = doc.CreateElement("hoten");
            XmlNode ho = doc.CreateElement("ho");
            ho.InnerText = txtHo.Text;
            hoten.AppendChild(ho);

            XmlNode ten = doc.CreateElement("ten");
            ten.InnerText = txtTen.Text;
            hoten.AppendChild(ten);

            nv.AppendChild(hoten);

            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txtDiaChi.Text;

            nv.AppendChild(diachi);

            if (nvcu == null)
            {
                MessageBox.Show("sua khong thanh cong!", "Thong bao");
            }
            else
            {
                goc.ReplaceChild(nv, nvcu);
                doc.Save(tentep);

                MessageBox.Show("sua thanh cong!", "Thong bao");
                Clear();
                HienThi();

            }
        }
    }
}
