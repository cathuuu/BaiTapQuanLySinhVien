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
    public partial class FrmLietKeMon : Form
    {
        public FrmLietKeMon()
        {
            InitializeComponent();
            LoadNhomMonHoc();
        }
        private void LoadNhomMonHoc()
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    string query = "SELECT MaNhomMonHoc, TenNhomMonHoc FROM NhomMonHoc WHERE DaXoa = 0";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbNhomMonHoc.DataSource = dt;
                    cbNhomMonHoc.DisplayMember = "TenNhomMonHoc";
                    cbNhomMonHoc.ValueMember = "MaNhomMonHoc";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải nhóm môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbNhomMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNhomMonHoc.SelectedValue != null)
            {
                // Kiểm tra và chuyển đổi giá trị của SelectedValue
                int maNhomMonHoc;
                if (int.TryParse(cbNhomMonHoc.SelectedValue.ToString(), out maNhomMonHoc))
                {
                    // Gọi hàm LoadMonHocByNhom với giá trị chuyển đổi được
                    LoadMonHocByNhom(maNhomMonHoc);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhóm môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void LoadMonHocByNhom(int maNhomMonHoc)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    string query = @"SELECT MaMonHoc, MaSoMonHoc, TenMonHoc, SoTinChi
                                     FROM MonHoc
                                     WHERE MaNhomMonHoc = @MaNhomMonHoc AND DaXoa = 0";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNhomMonHoc", maNhomMonHoc);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMonHoc.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
