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
    public partial class FrmQlyDiemSo : Form
    {
        public FrmQlyDiemSo()
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
            sql = "select * from DiemSo";
            sqlda = new SqlDataAdapter(sql, sqlcon);
            ds = new DataSet();
            sqlda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        public void hienthidltext()
        {
            // Clear existing bindings before adding new ones
            txtMadiemso.DataBindings.Clear();
            txtMasv.DataBindings.Clear();
            txtMaloaidiem.DataBindings.Clear();
            txtGiatridem.DataBindings.Clear();


            // Add new bindings
            txtMadiemso.DataBindings.Add("text", ds.Tables[0], "MaDiemSo");
            txtMasv.DataBindings.Add("text", ds.Tables[0], "MaSinhVien");
            txtMaloaidiem.DataBindings.Add("text", ds.Tables[0], "MaLoaiDiem");
            txtGiatridem.DataBindings.Add("text", ds.Tables[0], "GiaTriDiem");           
        }
        private void LoadData()
        {
            string query = "SELECT * FROM DiemSo";

            using (SqlConnection connection = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private void FrmQlyDiemSo_Load(object sender, EventArgs e)
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
                string query = "INSERT INTO DiemSo (  MaSinhVien, MaLoaiDiem, GiaTriDiem) " +
                               "VALUES (  @MaSinhVien, @MaLoaiDiem, @GiaTriDiem )";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                   // cmd.Parameters.AddWithValue("@MaDiemSo", txtMadiemso.Text);
                    cmd.Parameters.AddWithValue("@MaSinhVien", txtMasv.Text);
                    cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                    cmd.Parameters.AddWithValue("@GiaTriDiem", txtGiatridem.Text);               
                    cmd.ExecuteNonQuery();
                }
            }
            LoadData(); 
            Hienthidlluoi();
            hienthidltext();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE DiemSo SET MaSinhVien = @MaSinhVien, MaLoaiDiem = @MaLoaiDiem, GiaTriDiem = @GiaTriDiem WHERE MaDiemSo = @MaDiemSo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDiemSo", txtMadiemso.Text);
                    cmd.Parameters.AddWithValue("@MaSinhVien", txtMasv.Text);
                    cmd.Parameters.AddWithValue("@MaLoaiDiem", txtMaloaidiem.Text);
                    cmd.Parameters.AddWithValue("@GiaTriDiem", txtGiatridem.Text);
                            
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
                string query = "DELETE FROM DiemSo  WHERE MaDiemSo = @MaDiemSo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaDiemSo", txtMadiemso.Text);
                 
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
    }
}
