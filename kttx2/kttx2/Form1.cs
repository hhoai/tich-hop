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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace kttx2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // tao doi tuong sd tai lieu
        XmlDocument doc = new XmlDocument();
        string tentep = "C:\\Users\\TGDD\\Desktop\\KTLT\\TichHop\\kttx2\\kttx2\\dsnhanvien.xml";


        int d; // xd chi so dong tren datagrid

        private void HienThi()
        {
            dataNV.Rows.Clear();
            doc.Load(tentep);

            XmlNodeList ds = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;

            dataNV.ColumnCount = 5;
            dataNV.Rows.Add();

            foreach (XmlNode nv in ds)
            {
                // truy xuat thuoc tinh
                XmlNode manv = nv.SelectSingleNode("@manv");
                // lay gtri manv dua vao cot 1 datagrid
                dataNV.Rows[sd].Cells[0].Value = manv.InnerText.ToString();

                XmlNode hoten = nv.SelectSingleNode("hoten");
                dataNV.Rows[sd].Cells[1].Value = hoten.InnerText.ToString();

                XmlNode gioitinh = nv.SelectSingleNode("gioitinh");
                dataNV.Rows[sd].Cells[2].Value = gioitinh.InnerText.ToString();

                XmlNode trinhdo = nv.SelectSingleNode("trinhdo");
                dataNV.Rows[sd].Cells[3].Value = trinhdo.InnerText.ToString();

                //cbTrinhdo.Items.Add(trinhdo.InnerText.ToString());

                XmlNode diachi = nv.SelectSingleNode("diachi");
                dataNV.Rows[sd].Cells[4].Value = diachi.InnerText.ToString();

                dataNV.Rows.Add();
                sd++;
            }
            
        }

        private void LoadComboBox()
        {
            cbTrinhdo.Items.Add("Cao dang");
            cbTrinhdo.Items.Add("Dai hoc");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
            LoadComboBox();
        }

        private void Clear()
        {
            txtDiachi.Text = string.Empty;
            txtHoten.Text = string.Empty;   
            txtMaNV.Text = string.Empty;
            rNam.Checked = false;
            rNu.Checked = false;
            cbTrinhdo.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;

            txtDiachi.Text = dataNV.Rows[d].Cells[4].Value.ToString();
            txtHoten.Text = dataNV.Rows[d].Cells[1].Value.ToString();
            txtMaNV.Text = dataNV.Rows[d].Cells[0].Value.ToString();

            cbTrinhdo.SelectedItem = dataNV.Rows[d].Cells[3].Value.ToString();

            if (dataNV.Rows[d].Cells[2].Value.ToString() == "Nam")
                rNam.Checked = true;
            else
                rNu.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            doc.Load(tentep); // doc tai lieu
            XmlElement goc = doc.DocumentElement; // truy xuat nut goc

            XmlNode nv = doc.CreateElement("nhanvien"); // tao nut nhan vien

            XmlAttribute manv = doc.CreateAttribute("manv"); // them attribute
            manv.InnerText = txtMaNV.Text; // xd gtri manv
            nv.Attributes.Append(manv); // them thuoc tinh vao nut

            XmlNode hoten = doc.CreateElement("hoten");
            hoten.InnerText = txtHoten.Text;    
            nv.AppendChild(hoten);

            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txtDiachi.Text;
            nv.AppendChild(diachi);

            XmlNode gioitinh = doc.CreateElement("gioitinh");
            if (rNam.Checked)
                gioitinh.InnerText = "Nam";
            else
                gioitinh.InnerText = "Nu";
            nv.AppendChild(gioitinh);

            XmlNode trinhdo = doc.CreateElement("trinhdo");
            trinhdo.InnerText = cbTrinhdo.Text;
            nv.AppendChild(trinhdo);

            goc.AppendChild(nv); // them nv vao goc

            doc.Save(tentep); // luu tep

            MessageBox.Show("Them nhan vien thanh cong!");
            Clear();
            HienThi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nvcu = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            XmlNode nv = doc.CreateElement("nhanvien");

            XmlAttribute manv = doc.CreateAttribute("manv"); // them attribute
            manv.InnerText = txtMaNV.Text; // xd gtri manv
            nv.Attributes.Append(manv); // them thuoc tinh vao nut

            XmlNode hoten = doc.CreateElement("hoten");
            hoten.InnerText = txtHoten.Text;
            nv.AppendChild(hoten);

            XmlNode diachi = doc.CreateElement("diachi");
            diachi.InnerText = txtDiachi.Text;
            nv.AppendChild(diachi);

            XmlNode gioitinh = doc.CreateElement("gioitinh");
            if (rNam.Checked)
                gioitinh.InnerText = "Nam";
            else
                gioitinh.InnerText = "Nu";
            nv.AppendChild(gioitinh);

            XmlNode trinhdo = doc.CreateElement("trinhdo");
            trinhdo.InnerText = cbTrinhdo.Text;
            nv.AppendChild(trinhdo);

            goc.ReplaceChild(nv, nvcu);

            doc.Save(tentep);

            MessageBox.Show("Sua nhan vien thanh cong!");
            Clear();
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNode nv = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");

            DialogResult rs = MessageBox.Show("ban co muon xoa khong?", "thong bao", MessageBoxButtons.YesNo);

            if (rs == DialogResult.Yes && nv != null)
            {
                goc.RemoveChild(nv);

                doc.Save(tentep);

                MessageBox.Show("Xoa nhan vien thanh cong!", "thong bao");
                Clear();
                HienThi();
            }
            else
            {
                MessageBox.Show("Xoa nhan vien khong thanh cong!", "thong bao");
            }

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;

            XmlNodeList dsnv = goc.SelectNodes("/ds/nhanvien[/trinhdo='" + cbTrinhdo.Text + "']");
            int sd = 0;
            dataNV.ColumnCount = 5;
            dataNV.Rows.Add();

            foreach (XmlNode nv in dsnv)
            {
                // truy xuat thuoc tinh
                XmlNode manv = nv.SelectSingleNode("@manv");
                // lay gtri manv dua vao cot 1 datagrid
                dataNV.Rows[sd].Cells[0].Value = manv.InnerText.ToString();

                XmlNode hoten = nv.SelectSingleNode("hoten");
                dataNV.Rows[sd].Cells[1].Value = hoten.InnerText.ToString();

                XmlNode gioitinh = nv.SelectSingleNode("gioitinh");
                dataNV.Rows[sd].Cells[2].Value = gioitinh.InnerText.ToString();

                XmlNode trinhdo = nv.SelectSingleNode("trinhdo");
                dataNV.Rows[sd].Cells[3].Value = trinhdo.InnerText.ToString();

                cbTrinhdo.Items.Add(trinhdo.InnerText.ToString());


                XmlNode diachi = nv.SelectSingleNode("diachi");
                dataNV.Rows[sd].Cells[4].Value = diachi.InnerText.ToString();

                dataNV.Rows.Add();
                sd++;
            }

        }
    }
}
