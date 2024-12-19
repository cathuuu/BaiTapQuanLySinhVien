using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon.net
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if(currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelbody.Controls.Add(childForm);
            panelbody.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnqlchuyennganh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmqlychuyennganh());
            label1.Text=btnqlchuyennganh.Text;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(currentFormChild!=null)
            {
                currentFormChild.Close();
            }
        }

        private void btnqlsinhvien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmqlysinhvien());
            label1.Text = btnqlsinhvien.Text;
        }

        private void btnGhiDanh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQlyGhiDanh());
            label1.Text = btnGhiDanh.Text;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnqlmonhoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmqlymonhoc());
            label1.Text = btnqlmonhoc.Text;
        }

        private void btnqlnhommonh_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQlyNhomMonHoc());
            label1.Text = btnqlnhommonh.Text;
        }

        private void btnqllophoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmqlylophoc());
            label1.Text = btnqllophoc.Text;
        }

        private void btnqlkyhoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQlyKyHoc());
            label1.Text = btnqlkyhoc.Text;
        }

        private void btnqlDiemSo_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQlyDiemSo());
            label1.Text = btnqlDiemSo.Text;
        }

        private void btnqlLoaiDiem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQLyLoaiDiem());
            label1.Text = btnqlLoaiDiem.Text;
        }

        private void btnThongke_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmThongKeChung());
            label1.Text = btnThongke.Text;
        }

        private void btnThongKeMonhoc_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmLietKeMon());
            label1.Text = btnThongKeMonhoc.Text;
        }

        private void btnQlyLopTheoMon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmQlyLopTheoMon());
            label1.Text = btnQlyLopTheoMon.Text;
        }
    }   
}
