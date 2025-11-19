using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{   public partial class Form1 : Form
    {
        int Index = -1;
        CXuLyDanhBa xuLyDanhBa = new CXuLyDanhBa();
        public void hienDSDanhBa()
        {
            dgvDanhBa.DataSource = xuLyDanhBa.layDanhSachDanhBa();
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            hienDSDanhBa();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            CDanhBa db = new CDanhBa();
            db.SDT = txtSDT.Text;
            db.HoTen = txtHoten.Text;
            db.Email = txtEmail.Text;
            db.Diachi = txtDiachi.Text;
            if (xuLyDanhBa.tim(db.SDT) == null)
            {
                xuLyDanhBa.them(db);
                hienDSDanhBa();
            }
            else
            {
                MessageBox.Show("Số điện thoại " + db.SDT + "đã tồn tại.\nKhông thể thêm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            CDanhBa danhBa = new CDanhBa();
            danhBa.SDT = txtSDT.Text;
            if (danhBa.SDT == null)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại cần xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (xuLyDanhBa.tim(danhBa.SDT) != null)
            {
                xuLyDanhBa.xoa(danhBa.SDT);
                hienDSDanhBa();
                MessageBox.Show("Xóa số điện thoại " + danhBa.SDT + " thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Số điện thoại " + danhBa.SDT + "không tồn tại.\nKhông thể xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

            string sdtCanTim = txtSDT.Text;
            if (string.IsNullOrWhiteSpace(sdtCanTim))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại của liên hệ cần sửa vào ô 'Số điện thoại'.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Tìm đối tượng Danh Bạ (CDanhBa) cần sửa trong List
            CDanhBa dbCanSua = xuLyDanhBa.tim(sdtCanTim);

            if (dbCanSua != null)
            {
                // 2. Cập nhật các thông tin khác từ TextBox (SDT vẫn giữ nguyên)
                dbCanSua.HoTen = txtHoten.Text;
                dbCanSua.Email = txtEmail.Text;
                dbCanSua.Diachi = txtDiachi.Text;

                // 3. Hiển thị lại danh sách để DataGridView được cập nhật
                hienDSDanhBa();

                MessageBox.Show($"Đã cập nhật thông tin thành công cho số điện thoại: {sdtCanTim}!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Số điện thoại " + sdtCanTim + " không tồn tại.\nKhông thể sửa!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        private void btnLuuFile_Click(object sender, EventArgs e)
        {
            xuLyDanhBa.ghiFile("dsDanhBa.bin");
            MessageBox.Show("Lưu thành công!!");
        }
        public void loadFile()
        {
            xuLyDanhBa.docFile("dsDanhBa.bin");
        }
        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            loadFile();
            hienDSDanhBa();
        }

       

        private void btnTimSDT_Click_1(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại cần tìm!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CDanhBa ketQua = xuLyDanhBa.tim(sdt);

            if (ketQua != null)
            {
                // Hiển thị lên TextBox
                txtHoten.Text = ketQua.HoTen;
                txtEmail.Text = ketQua.Email;
                txtDiachi.Text = ketQua.Diachi;

                // Hiển thị lên DataGridView (chỉ 1 kết quả)
                dgvDanhBa.DataSource = new List<CDanhBa> { ketQua };
            }
            else
            {
                MessageBox.Show("Không tìm thấy số điện thoại trong danh bạ!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

