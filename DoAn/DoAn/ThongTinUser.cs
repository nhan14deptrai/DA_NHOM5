using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;

namespace DoAn
{

    public partial class ThongTinUser : Form
    {
        private CDanhBa danhBaHienTai;
        public bool YeuCauXoa { get; set; } = false;



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
            txtHoten.Text = db.HoTen;
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
            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(txtHoten.Text))
            {
                MessageBox.Show("Họ tên không được để trống!");
                return;
            }
            

            // Cập nhật trực tiếp vào danhBaHienTai
            danhBaHienTai.HoTen = txtHoten.Text.Trim();
            danhBaHienTai.Email = txtEmail.Text.Trim();
            danhBaHienTai.Diachi = txtDiachi.Text.Trim();

            // Báo về form cha
            this.DialogResult = DialogResult.OK;


        }




        private void ThongTinUser_Load(object sender, EventArgs e)
        {
            // Hiển thị dữ liệu cũ lên textbox
            if (danhBaHienTai != null)
            {
                txtSDT.Text = danhBaHienTai.SDT;
                txtHoten.Text = danhBaHienTai.HoTen;
                txtEmail.Text = danhBaHienTai.Email;
                txtDiachi.Text = danhBaHienTai.Diachi;
                if (danhBaHienTai.Favorite == true)
                {
                    // Trạng thái ĐÃ THÍCH -> Hiện hình sao vàng
                    // 'Properties.Resources.star_filled' là tên file ảnh bạn vừa add vào ở Bước 1
                    btnThich.Image = Properties.Resources.star_24dp_FFFF55;
                }
                else
                {
                    // Trạng thái CHƯA THÍCH -> Hiện hình sao rỗng
                    btnThich.Image = Properties.Resources.star_24dp_000000_FILL0_wght0_GRAD0_opszNaN;
                }
                // Nếu là Sửa thì thường không cho sửa SDT (khóa lại)
                txtSDT.Enabled = false;
                if (!string.IsNullOrEmpty(danhBaHienTai.Avatar) && File.Exists(danhBaHienTai.Avatar))
                {
                    ptbAvatar.Image = Image.FromFile(danhBaHienTai.Avatar); // show user avatar
                    ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom; // fit avatar
                    ptbAvatar.ImageLocation = danhBaHienTai.Avatar;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
      "Bạn có chắc muốn xóa liên hệ này?",
      "Xác nhận",
      MessageBoxButtons.YesNo,
      MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                YeuCauXoa = true;   // báo cho Form cha biết là user muốn xóa
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
        private void CapNhatGiaoDienNutSao()
        {
            if (danhBaHienTai.Favorite == true)
            {
                // Trạng thái ĐÃ THÍCH -> Hiện hình sao vàng

                btnThich.Image = Properties.Resources.star_24dp_FFFF55;
            }
            else
            {
                // Trạng thái CHƯA THÍCH -> Hiện hình sao rỗng
                btnThich.Image = Properties.Resources.star_24dp_000000_FILL0_wght0_GRAD0_opszNaN;
            }
        }
        private void btnThich_Click(object sender, EventArgs e)
        {
            danhBaHienTai.Favorite = !danhBaHienTai.Favorite;
            CapNhatGiaoDienNutSao();
            MessageBox.Show(
                danhBaHienTai.Favorite ? "Đã thêm vào yêu thích!" : "Đã bỏ yêu thích!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information
            );


            this.DialogResult = DialogResult.OK;
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK) { imageLocation = dlg.FileName; }
                {
                    imageLocation = dlg.FileName;
                    danhBaHienTai.Avatar = imageLocation;
                    ptbAvatar.Image = Image.FromFile(imageLocation); // show user avatar
                    ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom; // fit avatar
                    ptbAvatar.ImageLocation= imageLocation;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
    

