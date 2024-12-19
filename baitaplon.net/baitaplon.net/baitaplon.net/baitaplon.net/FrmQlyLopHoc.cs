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
    public partial class frmqlylophoc : Form
    {
        public frmqlylophoc()
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
            sql = "select * from LopHoc";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaLopHoc.DataBindings.Clear();
            txtMaMonHoc.DataBindings.Clear();
            txtMaKyHoc.DataBindings.Clear();
            txtTenLop.DataBindings.Clear();
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaLopHoc.DataBindings.Add("text", ds.Tables[0], "MaLopHoc");
            txtMaMonHoc.DataBindings.Add("text", ds.Tables[0], "MaMonHoc");
            txtMaKyHoc.DataBindings.Add("text", ds.Tables[0], "MaKyHoc");
            txtTenLop.DataBindings.Add("text", ds.Tables[0], "TenLop");
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }
        private void LoadData()
        {
            string query = "SELECT * FROM LopHoc";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void frmqlylophoc_Load(object sender, EventArgs e)
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
                string query = "INSERT INTO LopHoc (MaMonHoc, MaKyHoc, TenLop, DaXoa) " +
                               "VALUES (@MaMonHoc, @MaKyHoc, @TenLop, @DaXoa)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaMonHoc", txtMaMonHoc.Text);
                    cmd.Parameters.AddWithValue("@MaKyHoc", txtMaKyHoc.Text);
                    cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text);
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
                string query = "UPDATE LopHoc SET MaMonHoc = @MaMonHoc, MaKyHoc = @MaKyHoc, TenLop = @TenLop, DaXoa = @DaXoa WHERE MaLopHoc = @MaLopHoc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLopHoc", txtMaLopHoc.Text);
                    cmd.Parameters.AddWithValue("@MaKyHoc", txtMaKyHoc.Text);
                    cmd.Parameters.AddWithValue("@MaMonHoc", txtMaMonHoc.Text);
                    cmd.Parameters.AddWithValue("@TenLop", txtTenLop.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData(); // Cập nhật lại DataGridView
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE LopHoc SET DaXoa = @DaXoa WHERE MaLopHoc = @MaLopHoc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Kiểm tra nếu txtMaNhomMonHoc.Text có giá trị hợp lệ
                    if (!string.IsNullOrEmpty(txtMaLopHoc.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaLopHoc", txtMaLopHoc.Text);
                        cmd.Parameters.AddWithValue("@DaXoa", true); // Đánh dấu đã xóa
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập mã nhóm môn học.");
                    }
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
                string query = "SELECT * FROM LopHoc WHERE MaLopHoc = @MaLopHoc AND DaXoa = 0"; // Chỉ tìm kiếm các nhóm môn học chưa bị xóa
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Kiểm tra nếu txtMaNhomMonHoc.Text có giá trị hợp lệ
                    if (!string.IsNullOrEmpty(txtMaLopHoc.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaLopHoc", txtMaLopHoc.Text);

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
