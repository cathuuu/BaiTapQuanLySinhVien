using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon.net
{
    public partial class FrmQlyGhiDanh : Form
    {
        public FrmQlyGhiDanh()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        SqlConnection sqlcon;
        DataSet ds;
        SqlDataAdapter sqlda;
        public void Ketnoi()
        {
            sqlcon = new SqlConnection(DatabaseConnect.ConnectionString);
            sqlcon.Open();
        }
        public void Dongketnoi()
        {
            sqlcon.Close();
            sqlcon.Dispose();
        }
        //Ham hien thi du lieu ra luoi
        public void Hienthidlluoi()
        {
            Ketnoi();
            string sql;
            sql = "select * from GhiDanh";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaGhiDanh.DataBindings.Clear();
            txtMaSinhVien.DataBindings.Clear();
            txtMaLopHoc.DataBindings.Clear();
            txtSoBuoiVang.DataBindings.Clear();
            txtSoLanThi.DataBindings.Clear();
            txtLanThi.DataBindings.Clear();
            txtDiemThiCaoNhat.DataBindings.Clear();

            // Add new bindings
            txtMaGhiDanh.DataBindings.Add("text", ds.Tables[0], "MaGhiDanh");
            txtMaSinhVien.DataBindings.Add("text", ds.Tables[0], "MaSinhVien");
            txtMaLopHoc.DataBindings.Add("text", ds.Tables[0], "MaLopHoc");
            txtSoBuoiVang.DataBindings.Add("text", ds.Tables[0], "SoBuoiVang");
            txtSoLanThi.DataBindings.Add("text", ds.Tables[0], "SoLanThi");
            txtLanThi.DataBindings.Add("text", ds.Tables[0], "LanThi");
            txtDiemThiCaoNhat.DataBindings.Add("text", ds.Tables[0], "DiemThiCaoNhat");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO GhiDanh (MaSinhVien, MaLopHoc, SoBuoiVang, SoLanThi, DiemThiCaoNhat, LanThi) VALUES (@MaSinhVien, @MaLopHoc, @SoBuoiVang, @SoLanThi, @DiemThiCaoNhat, @LanThi)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaSinhVien", int.Parse(txtMaSinhVien.Text));
                    cmd.Parameters.AddWithValue("@MaLopHoc", int.Parse(txtMaLopHoc.Text));
                    cmd.Parameters.AddWithValue("@SoBuoiVang", int.Parse(txtSoBuoiVang.Text));
                    cmd.Parameters.AddWithValue("@SoLanThi", int.Parse(txtSoLanThi.Text));
                    cmd.Parameters.AddWithValue("@DiemThiCaoNhat", float.Parse(txtDiemThiCaoNhat.Text));
                    cmd.Parameters.AddWithValue("@LanThi", int.Parse(txtLanThi.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void FrmQlyGhiDanh_Load(object sender, EventArgs e)
        {
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }
        private void LoadData()
        {
            string query = "SELECT * FROM GhiDanh";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE GhiDanh SET MaSinhVien = @MaSinhVien, MaLopHoc = @MaLopHoc, SoBuoiVang = @SoBuoiVang, SoLanThi = @SoLanThi, DiemThiCaoNhat = @DiemThiCaoNhat, LanThi = @LanThi WHERE MaGhiDanh = @MaGhiDanh";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGhiDanh", int.Parse(txtMaGhiDanh.Text));
                    cmd.Parameters.AddWithValue("@MaSinhVien", int.Parse(txtMaSinhVien.Text));
                    cmd.Parameters.AddWithValue("@MaLopHoc", int.Parse(txtMaLopHoc.Text));
                    cmd.Parameters.AddWithValue("@SoBuoiVang", int.Parse(txtSoBuoiVang.Text));
                    cmd.Parameters.AddWithValue("@SoLanThi", int.Parse(txtSoLanThi.Text));
                    cmd.Parameters.AddWithValue("@DiemThiCaoNhat", float.Parse(txtDiemThiCaoNhat.Text));
                    cmd.Parameters.AddWithValue("@LanThi", int.Parse(txtLanThi.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM GhiDanh WHERE MaGhiDanh = @MaGhiDanh";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaGhiDanh", int.Parse(txtMaGhiDanh.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xóa thành công!");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
