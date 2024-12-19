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
    public partial class frmqlysinhvien : Form
    {
        public frmqlysinhvien()
        {
            InitializeComponent();
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
            sql = "select * from SinhVien";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaSinhVien.DataBindings.Clear();
            txtHoSinhVien.DataBindings.Clear();
            txtTenSinhVien.DataBindings.Clear();
            txtMaSoSV.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            txtMaChuyenNganh.DataBindings.Clear();
            txtGioiTinh.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtKhoaHoc.DataBindings.Clear();
            txtNgaySinh.DataBindings.Clear();
            txtGhiChu.DataBindings.Clear();
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaSinhVien.DataBindings.Add("text", ds.Tables[0], "MaSinhVien");
            txtHoSinhVien.DataBindings.Add("text", ds.Tables[0], "Ho");
            txtTenSinhVien.DataBindings.Add("text", ds.Tables[0], "Ten");
            txtMaSoSV.DataBindings.Add("text", ds.Tables[0], "MaSoSinhVien");
            txtEmail.DataBindings.Add("text", ds.Tables[0], "Email");
            txtSDT.DataBindings.Add("text", ds.Tables[0], "SoDienThoai");
            txtMaChuyenNganh.DataBindings.Add("text", ds.Tables[0], "MaChuyenNganh");
            txtGioiTinh.DataBindings.Add("text", ds.Tables[0], "GioiTinh");
            txtDiaChi.DataBindings.Add("text", ds.Tables[0], "DiaChi");
            txtCCCD.DataBindings.Add("text", ds.Tables[0], "CCCD");
            txtKhoaHoc.DataBindings.Add("text", ds.Tables[0], "KhoaHoc");
            txtNgaySinh.DataBindings.Add("text", ds.Tables[0], "NgaySinh");
            txtGhiChu.DataBindings.Add("text", ds.Tables[0], "GhiChu");
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }
        private void frmqlysinhvien_Load(object sender, EventArgs e)
        {
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void LoadData()
        {
            string query = "SELECT * FROM SinhVien";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO SinhVien (Ten, Ho, MaSoSinhVien, GioiTinh, CCCD, NgaySinh, SoDienThoai, MaChuyenNganh, KhoaHoc, Email, DaXoa, GhiChu, DiaChi) " +
                               "VALUES (@TenSV, @HoSV, @MaSoSV, @GioiTinh, @CCCD, @NgaySinh, @SDT, @MaChuyenNganh, @KhoaHoc, @Email, @DaXoa, @GhiChu, @DiaChi)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenSV", txtTenSinhVien.Text);
                    cmd.Parameters.AddWithValue("@HoSV", txtHoSinhVien.Text);
                    cmd.Parameters.AddWithValue("@MaSoSV", txtMaSoSV.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", txtGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@MaChuyenNganh", txtMaChuyenNganh.Text);
                    cmd.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData(); // Cập nhật lại DataGridView
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE SinhVien SET Ten = @TenSV, Ho = @HoSV, MaSoSinhVien = @MaSoSV, GioiTinh = @GioiTinh, CCCD = @CCCD, NgaySinh = @NgaySinh, SoDienThoai = @SDT, MaChuyenNganh = @MaChuyenNganh, KhoaHoc = @KhoaHoc, Email = @Email, DaXoa = @DaXoa, GhiChu = @GhiChu, DiaChi = @DiaChi WHERE MaSinhVien = @MaSV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSinhVien.Text);
                    cmd.Parameters.AddWithValue("@TenSV", txtTenSinhVien.Text);
                    cmd.Parameters.AddWithValue("@HoSV", txtHoSinhVien.Text);
                    cmd.Parameters.AddWithValue("@MaSoSV", txtMaSoSV.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", txtGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@CCCD", txtCCCD.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", txtNgaySinh.Text);
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text);
                    cmd.Parameters.AddWithValue("@MaChuyenNganh", txtMaChuyenNganh.Text);
                    cmd.Parameters.AddWithValue("@KhoaHoc", txtKhoaHoc.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"
                    cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {

        }

        private void tnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE SinhVien SET DaXoa = @DaXoa WHERE MaSinhVien = @MaSV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSV", txtMaSinhVien.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", true); // Đánh dấu đã xóa thay vì xóa thực sự
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Dispose();
        }
    }
}
