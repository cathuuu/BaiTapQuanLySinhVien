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
    public partial class frmqlymonhoc : Form
    {
        public frmqlymonhoc()
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
            sql = "select * from MonHoc";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaMonHoc.DataBindings.Clear();
            txtMaSoMonHoc.DataBindings.Clear();
            txtTenMonHoc.DataBindings.Clear();
            txtSoTinChi.DataBindings.Clear();
            txtMaNhomMonHoc.DataBindings.Clear();
            txtSoBuoi.DataBindings.Clear();
            txtSoBuoiVangToiDa.DataBindings.Clear();
            txtDiemDat.DataBindings.Clear();
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaMonHoc.DataBindings.Add("text", ds.Tables[0], "MaMonHoc");
            txtMaSoMonHoc.DataBindings.Add("text", ds.Tables[0], "MaSoMonHoc");
            txtTenMonHoc.DataBindings.Add("text", ds.Tables[0], "TenMonHoc");
            txtSoTinChi.DataBindings.Add("text", ds.Tables[0], "SoTinChi");
            txtMaNhomMonHoc.DataBindings.Add("text", ds.Tables[0], "MaNhomMonHoc");
            txtSoBuoi.DataBindings.Add("text", ds.Tables[0], "SoBuoi");
            txtSoBuoiVangToiDa.DataBindings.Add("text", ds.Tables[0], "SoBuoiVangToiDa");
            txtDiemDat.DataBindings.Add("text", ds.Tables[0], "DiemDat");
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }
        private void LoadData()
        {
            string query = "SELECT * FROM MonHoc";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void frmqlymonhoc_Load(object sender, EventArgs e)
        {
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO MonHoc (MaSoMonHoc, TenMonHoc, SoTinChi, MaNhomMonHoc, SoBuoi, SoBuoiVangToiDa, DiemDat, DaXoa) " +
                               "VALUES (@MaSoMH, @TenMH, @SoTC, @MaNhom, @SoBuoi, @SoBuoiMax, @Diem, @DaXoa)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaSoMH", txtMaSoMonHoc.Text);
                    cmd.Parameters.AddWithValue("@TenMH", txtTenMonHoc.Text);
                    cmd.Parameters.AddWithValue("@SoTC", txtSoTinChi.Text);
                    cmd.Parameters.AddWithValue("@MaNhom", txtMaNhomMonHoc.Text);
                    cmd.Parameters.AddWithValue("@SoBuoi", txtSoBuoi.Text);
                    cmd.Parameters.AddWithValue("@SoBuoiMax", txtSoBuoiVangToiDa.Text);
                    cmd.Parameters.AddWithValue("@Diem", txtDiemDat.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"
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
                string query = "UPDATE MonHoc SET MaSoMonHoc = @MaSoMH, TenMonHoc = @TenMH, SoTinChi = @SoTC, MaNhomMonHoc = @MaNhom, SoBuoi = @SoBuoi, SoBuoiVangToiDa = @SoBuoiMax, DiemDat = @Diem, DaXoa = @DaXoa WHERE MaMonHoc = @MaMH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMonHoc.Text);
                    cmd.Parameters.AddWithValue("@MaSoMH", txtMaSoMonHoc.Text);
                    cmd.Parameters.AddWithValue("@TenMH", txtTenMonHoc.Text);
                    cmd.Parameters.AddWithValue("@SoTC", txtSoTinChi.Text);
                    cmd.Parameters.AddWithValue("@MaNhom", txtMaNhomMonHoc.Text);
                    cmd.Parameters.AddWithValue("@SoBuoi", txtSoBuoi.Text);
                    cmd.Parameters.AddWithValue("@SoBuoiMax", txtSoBuoiVangToiDa.Text);
                    cmd.Parameters.AddWithValue("@Diem", txtDiemDat.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"
                    cmd.ExecuteNonQuery();
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
                conn.Open();
                string query = "UPDATE MonHoc SET DaXoa = @DaXoa WHERE MaMonHoc = @MaMH";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMH", txtMaMonHoc.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", true); // Đánh dấu đã xóa thay vì xóa thực sự
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM MonHoc WHERE MaMonHoc = @MaMH AND DaXoa = 0"; // Chỉ tìm kiếm các nhóm môn học chưa bị xóa
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Kiểm tra nếu txtMaNhomMonHoc.Text có giá trị hợp lệ
                    if (!string.IsNullOrEmpty(txtMaMonHoc.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaMH", txtMaMonHoc.Text);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập mã nhóm môn học để tìm kiếm.");
                    }

                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
