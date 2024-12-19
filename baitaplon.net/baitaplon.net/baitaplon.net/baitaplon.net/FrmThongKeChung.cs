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
    public partial class FrmThongKeChung : Form
    {
        public FrmThongKeChung()
        {
            InitializeComponent();
            LoadKyHoc();
            LoadMonHoc();
            LoadNganhHoc();
            LoadLopHoc();
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
        private void FrmThongKeChung_Load(object sender, EventArgs e)
        {

        }
        private void LoadKyHoc()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenKyHoc, MaKyHoc FROM KyHoc WHERE DaXoa = 0"; // Lấy cả mã
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbKyHoc.DataSource = dt;
                    cbKyHoc.DisplayMember = "TenKyHoc"; // Hiển thị
                    cbKyHoc.ValueMember = "TenKyHoc";   // Giá trị
                    cbKyHoc.SelectedIndex = -1;        // Không chọn giá trị nào mặc định
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nạp dữ liệu kỳ học: " + ex.Message);
                }
            }
        }
        private void LoadMonHoc()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenMonHoc, MaMonHoc FROM MonHoc WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbMonHoc.DataSource = dt;
                    cbMonHoc.DisplayMember = "TenMonHoc";
                    cbMonHoc.ValueMember = "TenMonHoc";
                    cbMonHoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nạp dữ liệu môn học: " + ex.Message);
                }
            }
        }

        private void LoadNganhHoc()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenChuyenNganh, MaChuyenNganh FROM ChuyenNganh WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbNganhHoc.DataSource = dt;
                    cbNganhHoc.DisplayMember = "TenChuyenNganh";
                    cbNganhHoc.ValueMember = "TenChuyenNganh";
                    cbNganhHoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nạp dữ liệu ngành học: " + ex.Message);
                }
            }
        }
        private void LoadLopHoc()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TenLop, MaLopHoc FROM LopHoc WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbLopHoc.DataSource = dt;
                    cbLopHoc.DisplayMember = "TenLop";
                    cbLopHoc.ValueMember = "TenLop";
                    cbLopHoc.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi nạp dữ liệu lớp học: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // Truy vấn SQL
                    string query = @"
        SELECT 
            SV.MaSinhVien,
            SV.Ho + ' ' + SV.Ten AS HoTen,
            CN.TenChuyenNganh AS TenNganhHoc,
            MH.TenMonHoc,
            KH.TenKyHoc,
            LH.TenLop,
            MH.SoTinChi,
            GD.SoBuoiVang,
            GD.SoLanThi,
            GD.DiemThiCaoNhat,
            SUM(DS.GiaTriDiem * LD.TrongSo) / SUM(LD.TrongSo) AS DiemTrungBinh,
            CASE 
                WHEN SUM(DS.GiaTriDiem * LD.TrongSo) / SUM(LD.TrongSo) > 4 THEN N'Đỗ'
                ELSE N'Trượt'
            END AS KetQua
        FROM GhiDanh GD
        JOIN SinhVien SV ON GD.MaSinhVien = SV.MaSinhVien
        JOIN LopHoc LH ON GD.MaLopHoc = LH.MaLopHoc
        JOIN MonHoc MH ON LH.MaMonHoc = MH.MaMonHoc
        JOIN KyHoc KH ON LH.MaKyHoc = KH.MaKyHoc
        JOIN ChuyenNganh CN ON SV.MaChuyenNganh = CN.MaChuyenNganh
        JOIN DiemSo DS ON SV.MaSinhVien = DS.MaSinhVien
        JOIN LoaiDiem LD ON DS.MaLoaiDiem = LD.MaLoaiDiem
        WHERE 
            (@TenKyHoc IS NULL OR KH.TenKyHoc = @TenKyHoc)
            AND (@TenMonHoc IS NULL OR MH.TenMonHoc = @TenMonHoc)
            AND (@TenChuyenNganh IS NULL OR CN.TenChuyenNganh = @TenChuyenNganh)
            AND (@TenLop IS NULL OR LH.TenLop = @TenLop)
        GROUP BY 
            SV.MaSinhVien, SV.Ho, SV.Ten, CN.TenChuyenNganh, 
            MH.TenMonHoc, KH.TenKyHoc, LH.TenLop, MH.SoTinChi,
            GD.SoBuoiVang, GD.SoLanThi, GD.DiemThiCaoNhat;";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Thêm tham số tìm kiếm từ các ComboBox
                    cmd.Parameters.AddWithValue("@TenKyHoc", cbKyHoc.SelectedValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenMonHoc", cbMonHoc.SelectedValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenChuyenNganh", cbNganhHoc.SelectedValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenLop", cbLopHoc.SelectedValue.ToString() ?? (object)DBNull.Value);

                    //Alert(cbKyHoc.SelectedValue);

                    //MessageBox.Show(cbLopHoc.SelectedValue.ToString() );
                    //MessageBox.Show(cbMonHoc.SelectedValue?.ToString() ?? "SelectedValue is null");
                    //MessageBox.Show(cbNganhHoc.SelectedValue?.ToString() ?? "SelectedValue is null");
                    //MessageBox.Show(cbLopHoc.SelectedValue?.ToString() ?? "SelectedValue is null");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Kiểm tra nếu không có dữ liệu
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu phù hợp với điều kiện lọc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Hiển thị dữ liệu lên DataGridView
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }

        }

    
    }
    
}
