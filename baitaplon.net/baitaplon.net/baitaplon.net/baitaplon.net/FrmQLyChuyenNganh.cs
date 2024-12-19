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
    public partial class frmqlychuyennganh : Form
    {
        public frmqlychuyennganh()
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
            sql = "select * from ChuyenNganh";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaChuyenNganh.DataBindings.Clear();
            txtTenChuyenNganh.DataBindings.Clear();
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaChuyenNganh.DataBindings.Add("text", ds.Tables[0], "MaChuyenNganh");
            txtTenChuyenNganh.DataBindings.Add("text", ds.Tables[0], "TenChuyenNganh");
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }
        private void LoadData()
        {
            string query = "SELECT * FROM ChuyenNganh";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        private void frmqlychuyennganh_Load(object sender, EventArgs e)
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
                string query = "INSERT INTO ChuyenNganh (TenChuyenNganh, DaXoa) " +
                               "VALUES (@TenCN, @DaXoa)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenCN", txtTenChuyenNganh.Text);
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
                string query = "UPDATE ChuyenNganh SET TenChuyenNganh = @TenCN, DaXoa = @DaXoa WHERE MaChuyenNganh = @MaCN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCN", txtMaChuyenNganh.Text);
                    cmd.Parameters.AddWithValue("@TenCN", txtTenChuyenNganh.Text);
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
                string query = "UPDATE ChuyenNganh SET DaXoa = @DaXoa WHERE MaChuyenNganh = @MaCN";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCN", txtMaChuyenNganh.Text);
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
                string query = "SELECT * FROM ChuyenNganh WHERE MaChuyenNganh = @MaCN AND DaXoa = 0"; // Chỉ tìm kiếm các chuyên ngành chưa bị xóa
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaCN", txtMaChuyenNganh.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }
    }
}
