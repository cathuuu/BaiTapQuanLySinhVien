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
    public partial class FrmQlyKyHoc : Form
    {
        public FrmQlyKyHoc()
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
            sql = "select * from KyHoc";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaKyHoc.DataBindings.Clear();
            txtTenKyHoc.DataBindings.Clear();
            txtNam.DataBindings.Clear();
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaKyHoc.DataBindings.Add("text", ds.Tables[0], "MaKyHoc");
            txtTenKyHoc.DataBindings.Add("text", ds.Tables[0], "TenKyHoc");
            txtNam.DataBindings.Add("text", ds.Tables[0], "Nam");
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }
        private void LoadData()
        {
            string query = "SELECT * FROM KyHoc";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void FrmQlyKyHoc_Load(object sender, EventArgs e)
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
                string query = "INSERT INTO KyHoc (TenKyHoc, Nam, DaXoa) " +
                               "VALUES (@TenKyHoc, @Nam, @DaXoa)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenKyHoc", txtTenKyHoc.Text);
                    cmd.Parameters.AddWithValue("@Nam", txtNam.Text);
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
                string query = "UPDATE KyHoc SET TenKyHoc = @TenKyHoc, Nam = @Nam, DaXoa = @DaXoa WHERE MaKyHoc = @MaKyHoc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKyHoc", txtMaKyHoc.Text);
                    cmd.Parameters.AddWithValue("@TenKyHoc", txtTenKyHoc.Text);
                    cmd.Parameters.AddWithValue("@Nam", txtNam.Text);
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
                string query = "UPDATE KyHoc SET DaXoa = @DaXoa WHERE MaKyHoc = @MaKyHoc";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Kiểm tra nếu txtMaNhomMonHoc.Text có giá trị hợp lệ
                    if (!string.IsNullOrEmpty(txtMaKyHoc.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaKyHoc", txtMaKyHoc.Text);
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
                string query = "SELECT * FROM KyHoc WHERE MaKyHoc = @MaKyHoc AND DaXoa = 0"; // Chỉ tìm kiếm các nhóm môn học chưa bị xóa
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Kiểm tra nếu txtMaNhomMonHoc.Text có giá trị hợp lệ
                    if (!string.IsNullOrEmpty(txtMaKyHoc.Text))
                    {
                        cmd.Parameters.AddWithValue("@MaKyHoc", txtMaKyHoc.Text);

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
