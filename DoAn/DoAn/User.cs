using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace DoAn
{
    public partial class User : Form
    {

        CXuLyDanhBa xuLyDanhBa = new CXuLyDanhBa();
        private string duongDanAnhTamThoi = "";
        public void hienDSDanhBa()
        {
            dgvDanhBa.DataSource = xuLyDanhBa.layDanhSachDanhBa().OrderBy(c => c.HoTen).OrderByDescending(c=>c.Favorite)
                .ToList();
            dgvDanhBa.Columns["Ten"].Visible = false;
            dgvDanhBa.Columns["Avatar"].Visible = false;
           
          
        }
        public User()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            loadFile();
            hienDSDanhBa();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            string sdt = txtSDT.Text.Trim();
            string hoTen = txtHoten.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtDiachi.Text.Trim();

            // --- BẮT ĐẦU KIỂM TRA SỐ ĐIỆN THOẠI (VALIDATION) ---

            // ĐK 1: Kiểm tra rỗng
            if (string.IsNullOrEmpty(sdt))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            // ĐK 2: Kiểm tra toàn bộ phải là số (không chứa chữ a,b,c...)
            if (!sdt.All(char.IsDigit))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa các chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            // ĐK 3: Kiểm tra độ dài (SĐT Việt Nam chuẩn là 10 số)
            // Nếu muốn cho phép cả sđt bàn (11 số) thì sửa thành: if (sdt.Length < 10 || sdt.Length > 11)
            if (sdt.Length != 10)
            {
                MessageBox.Show("Số điện thoại phải có đúng 10 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            // ĐK 4: Kiểm tra đầu số (Phải bắt đầu bằng số 0)
            if (!sdt.StartsWith("0"))
            {
                MessageBox.Show("Số điện thoại phải bắt đầu bằng số 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            // --- KIỂM TRA TRÙNG LẶP ---
            // Kiểm tra xem SĐT này đã có trong danh sách chưa trước khi tạo đối tượng mới
            if (xuLyDanhBa.tim(sdt) != null)
            {
                MessageBox.Show($"Số điện thoại {sdt} đã tồn tại trong danh bạ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            string finalAvatarPath = ""; // Mặc định là rỗng nếu không chọn ảnh

            if (!string.IsNullOrEmpty(duongDanAnhTamThoi) && File.Exists(duongDanAnhTamThoi))
            {
                try
                {
                    // Tạo thư mục Images nếu chưa có
                    string saveDirectory = Path.Combine(Application.StartupPath, "Images");
                    if (!Directory.Exists(saveDirectory)) Directory.CreateDirectory(saveDirectory);

                    // Tạo tên file mới duy nhất (VD: avatar_01234.jpg)
                    string extension = Path.GetExtension(duongDanAnhTamThoi);
                    string newFileName = $"avatar_{Guid.NewGuid()}{extension}";
                    string destPath = Path.Combine(saveDirectory, newFileName);

                    // Copy file
                    File.Copy(duongDanAnhTamThoi, destPath, true);
                    finalAvatarPath = destPath; // Lưu đường dẫn mới này vào database
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu ảnh: " + ex.Message);
                    return; // Dừng nếu lưu ảnh lỗi
                }
            }
            
            
                // --- THÊM MỚI ---
                // Nếu vượt qua hết các cửa ải trên thì mới tạo đối tượng
                CDanhBa db = new CDanhBa();
                db.SDT = sdt;
                db.HoTen = hoTen;
                db.Email = email;
                db.Diachi = diaChi;
                db.Avatar = finalAvatarPath;

                //Thêm đối tượng mới tạo vào danh sách
                xuLyDanhBa.them(db);
                hienDSDanhBa();
                xuLyDanhBa.ghiFile("dsDanhBa.bin");
                MessageBox.Show("Thêm số điện thoại: " + db.SDT + " thành công", "Thông báo");
            
        }
       
      
      
        public void loadFile()
        {
            xuLyDanhBa.docFile("dsDanhBa.bin");
        }


        public void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim().ToLower();

            // 2. Nếu ô tìm kiếm trống, trả về danh sách gốc
            if (string.IsNullOrEmpty(tuKhoa))
            {
                hienDSDanhBa();
                return;
            }

            // 3. Lọc dữ liệu bằng LINQ (Tìm gần đúng trong cả Tên và SĐT)
            var ketQuaTimKiem = xuLyDanhBa.layDanhSachDanhBa()
                .Where(c => c.HoTen.ToLower().Contains(tuKhoa) ||
                            c.SDT.Contains(tuKhoa))
                .ToList();

            // 4. Hiển thị kết quả
            dgvDanhBa.DataSource = null; // Reset để tránh lỗi hiển thị
            dgvDanhBa.DataSource = ketQuaTimKiem;
            dgvDanhBa.DataSource = ketQuaTimKiem.OrderBy(c => c.HoTen).OrderByDescending(c => c.Favorite)
               .ToList();
            dgvDanhBa.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvDanhBa.Columns["HoTen"].HeaderText = "Họ và tên";
            dgvDanhBa.Columns["Email"].HeaderText = "Email";
            dgvDanhBa.Columns["Diachi"].HeaderText = "Địa chỉ";
            dgvDanhBa.Columns["Ten"].Visible = false;
            dgvDanhBa.Columns["Avatar"].Visible = false;
            
            
            // QUAN TRỌNG: KHÔNG ĐƯỢC GỌI hienDSDanhBa() Ở ĐÂY
            // Vì nó sẽ load lại danh sách gốc, làm mất kết quả vừa tìm.

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //Lấy dữ liệu từ ô txtTimKiem.
            //Trim(): Cắt bỏ khoảng trắng thừa ở đầu và cuối (VD: " abc " -> "abc").
            //ToLower(): Chuyển tất cả thành chữ thường để dễ dàng tìm kiếm.
            
            string tukhoa = txtTimKiem.Text.Trim().ToLower();
           
            //Kiểm tra txtTimKiem có dữ liệu hay không.
            if (string.IsNullOrEmpty(tukhoa))
            {
                //Nếu rỗng, hiển thị lại danh sách gốc.
             
                hienDSDanhBa();
                return;//Dừng hàm tại đây.
            }
            //Thực hiện lọc dữ liệu.
            //Điều kiện 1: Số điện thoại chứa từ khóa.
            var ketQuaTimKiem = xuLyDanhBa.layDanhSachDanhBa().Where(c => c.SDT.ToLower().Contains(tukhoa)
                || c.HoTen.ToLower().Contains(tukhoa)).ToList();// Điều kiện 2: Họ tên chứa từ khóa.

            //Hiển thị kết quả lên giao diện.
            dgvDanhBa.DataSource = null; // Reset nguồn dữ liệu
            dgvDanhBa.DataSource = ketQuaTimKiem; // Gán danh sách đã lọc
            dgvDanhBa.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvDanhBa.Columns["HoTen"].HeaderText = "Họ và tên";
            dgvDanhBa.Columns["Email"].HeaderText = "Email";
            dgvDanhBa.Columns["Diachi"].HeaderText = "Địa chỉ";
            dgvDanhBa.Columns["Ten"].Visible = false;
            dgvDanhBa.Columns["Avatar"].Visible = false;
            dgvDanhBa.DataSource = ketQuaTimKiem.OrderBy(c => c.HoTen).OrderByDescending(c => c.Favorite)
               .ToList();


            //Thông báo khi không tìm thấy kết quả nào.
            if (ketQuaTimKiem.Count == 0)
            {
                MessageBox.Show("Không tìm thấy liên lạc nào khớp với từ khóa!", "Thông báo");
            }

        }

        //Hàm đưa dữ liệu từ bảng liên TextBox.
        private void dgvDanhBa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra xem người dùng có click vào dòng tiêu đề hay không (Index = -1)
            if (e.RowIndex == -1) return;

            // 2. Lấy dòng hiện tại đang được chọn
            DataGridViewRow row = dgvDanhBa.Rows[e.RowIndex];

            // 3. Đổ dữ liệu từ các ô (Cells) của dòng đó lên TextBox
            txtSDT.Text = row.Cells[0].Value.ToString();
            txtHoten.Text = row.Cells[1].Value.ToString();
            txtEmail.Text = row.Cells[2].Value.ToString();
            txtDiachi.Text = row.Cells[3].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 1. Hiện khung nhập liệu ra
            groupBox1.Visible = true;

            // 2. Xóa trắng các ô nhập liệu để nhập mới (Trải nghiệm người dùng tốt hơn)
            txtSDT.Text = "";
            txtHoten.Text = "";
            txtEmail.Text = "";
            txtDiachi.Text = "";

            // 3. Đưa con trỏ chuột vào ô đầu tiên để nhập luôn cho tiện
            txtSDT.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void dgvDanhBa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            CDanhBa contactDuocChon = dgvDanhBa.Rows[e.RowIndex].DataBoundItem as CDanhBa;

            if (contactDuocChon != null)
            {
                ThongTinUser formChiTiet = new ThongTinUser(contactDuocChon);

                if (formChiTiet.ShowDialog() == DialogResult.OK)
                {
                    // Nếu form con yêu cầu xóa
                    if (formChiTiet.YeuCauXoa == true)
                    {
                        xuLyDanhBa.xoa(contactDuocChon.SDT);
                        xuLyDanhBa.ghiFile("dsDanhBa.bin");
                        hienDSDanhBa();
                        MessageBox.Show("Đã xóa liên hệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Ngược lại là sửa
                        xuLyDanhBa.ghiFile("dsDanhBa.bin");
                        hienDSDanhBa();
                    }
                   

                    }

                }

            
        }
        private Image LoadImageSafe(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return Image.FromStream(fs);
            }
        }
       
        private void btnAnh_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                dlg.Title = "Chọn ảnh đại diện";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    duongDanAnhTamThoi = dlg.FileName; // Lưu đường dẫn tạm

                    // Hiển thị lên PictureBox bằng cách an toàn
                    ptbAvatar.Image = LoadImageSafe(duongDanAnhTamThoi);
                    ptbAvatar.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

