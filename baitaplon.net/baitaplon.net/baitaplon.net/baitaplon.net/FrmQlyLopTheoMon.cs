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
    public partial class FrmQlyLopTheoMon : Form
    {
        public FrmQlyLopTheoMon()
        {
            InitializeComponent();
            LoadMonHoc();
            cbMonHoc.SelectedIndexChanged += cbMonHoc_SelectedIndexChanged;
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
        private void LoadLopHocByMonHoc(string tenMonHoc)
        {
            using (SqlConnection conn = new SqlConnection(DatabaseConnect.ConnectionString))
            {
                try
                {
                    string query = @"

SELECT 
    MH.TenMonHoc AS TenMonHoc,
    LH.TenLop AS TenLop,
    KH.TenKyHoc AS TenKyHoc,
    KH.Nam AS NamHoc
FROM 
    LopHoc LH
JOIN 
    MonHoc MH ON LH.MaMonHoc = MH.MaMonHoc
JOIN 
    KyHoc KH ON LH.MaKyHoc = KH.MaKyHoc
WHERE 
    MH.TenMonHoc = @TenMonHoc AND
    MH.DaXoa = 0 AND 
    LH.DaXoa = 0
ORDER BY 
    MH.TenMonHoc, LH.TenLop;

";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenMonHoc", tenMonHoc);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLopHoc.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách môn học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMonHoc.SelectedValue != null)
            {
                string tenMonHoc = Convert.ToString(cbMonHoc.SelectedValue);
                LoadLopHocByMonHoc(tenMonHoc);
            }

        }
    }
}
