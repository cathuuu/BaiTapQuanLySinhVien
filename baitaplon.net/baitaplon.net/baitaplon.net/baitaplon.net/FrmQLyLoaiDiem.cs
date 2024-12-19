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
    public partial class FrmQLyLoaiDiem : Form
    {
        public FrmQLyLoaiDiem()
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
            sql = "select * from LoaiDiem";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMaloaidiem.DataBindings.Clear();
            txtMamonhoc.DataBindings.Clear();
            txtTenloaidiem.DataBindings.Clear();
            txtTrongso.DataBindings.Clear();
            txtSolanthi.DataBindings.Clear();       
            chkDaXoa.DataBindings.Clear();

            // Add new bindings
            txtMaloaidiem.DataBindings.Add("text", ds.Tables[0], "MaLoaiDiem");
            txtMamonhoc.DataBindings.Add("text", ds.Tables[0], "MaMonHoc");
            txtTenloaidiem.DataBindings.Add("text", ds.Tables[0], "TenLoaiDiem");
            txtTrongso.DataBindings.Add("text", ds.Tables[0], "TrongSo");
            txtSolanthi.DataBindings.Add("text", ds.Tables[0], "SoLanThi");                  
            chkDaXoa.DataBindings.Add("Checked", ds.Tables[0], "DaXoa");
        }

        private void FrmQLyLoaiDiem_Load(object sender, EventArgs e)
        {
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }
        private void LoadData()
        {
            string query = "SELECT * FROM LoaiDiem";

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
                string query = "INSERT INTO LoaiDiem ( MaMonHoc, TenLoaiDiem, TrongSo, SoLanThi, DaXoa) " +
                               "VALUES ( @MaMonHoc, @TenLoaiDiem, @TrongSo, @SoLanThi , @DaXoa)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                    cmd.Parameters.AddWithValue("@MaMonHoc", txtMamonhoc.Text);
                    cmd.Parameters.AddWithValue("@TenLoaiDiem", txtTenloaidiem.Text);
                    cmd.Parameters.AddWithValue("@TrongSo", txtTrongso.Text);
                    cmd.Parameters.AddWithValue("@SoLanThi", txtSolanthi.Text);
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
                string query = "UPDATE LoaiDiem SET MaMonHoc = @MaMonHoc, TenLoaiDiem = @TenLoaiDiem, TrongSo = @TrongSo, SoLanThi = @SoLanThi ,DaXoa = @DaXoa WHERE MaLoaiDiem = @MaLoaiDiem";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                    cmd.Parameters.AddWithValue("@MaMonHoc", txtMamonhoc.Text);
                    cmd.Parameters.AddWithValue("@TenLoaiDiem", txtTenloaidiem.Text);
                    cmd.Parameters.AddWithValue("@TrongSo", txtTrongso.Text);
                    cmd.Parameters.AddWithValue("@SoLanThi", txtSolanthi.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", chkDaXoa.Checked); // Checkbox "Đã Xóa"                  
                    cmd.ExecuteNonQuery();
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

        private void btnXoamem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE LoaiDiem SET DaXoa = @DaXoa WHERE MaLoaiDiem = @MaLoaiDiem";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                    cmd.Parameters.AddWithValue("@DaXoa", true); // Đánh dấu đã xóa thay vì xóa thực sự
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData();
            Hienthidlluoi();
            hienthidltext();
        }
    }
}
