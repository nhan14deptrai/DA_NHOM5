using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
   
    public partial class ThongTinUser : Form
    {
        public CDanhBa danhBaHienTai;
        public ThongTinUser()
        {
            InitializeComponent();
        }
        public ThongTinUser(CDanhBa db)
        {
            InitializeComponent();
            danhBaHienTai = db; // Nhận tham chiếu từ form cha

            // Load lên giao diện
            txtSDT.Text = db.SDT;
            txtHoTen.Text = db.HoTen;
            txtEmail.Text = db.Email;
            txtDiachi.Text = db.Diachi;

            // --- QUAN TRỌNG: KHÓA SỐ ĐIỆN THOẠI ---
            // Vì SĐT là khóa chính (Key), nếu sửa SĐT thì hệ thống sẽ không tìm ra người cũ để lưu.
            // Tốt nhất là không cho sửa SĐT ở đây.
            txtSDT.Enabled = false;
            txtSDT.BackColor = System.Drawing.Color.WhiteSmoke;



            // Khóa ô SDT lại không cho sửa (vì là khóa chính)

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CDanhBa danhBaHienTai = new CDanhBa();
            // Cập nhật ngược lại từ TextBox vào Đối tượng
            danhBaHienTai.HoTen= txtHoTen.Text;
            danhBaHienTai.Email = txtEmail.Text;
            danhBaHienTai.Diachi= txtDiachi.Text;

           

            // Đóng form và báo kết quả OK
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}

    

