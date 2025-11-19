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
            // 1. Lấy dữ liệu và làm sạch (xóa khoảng trắng thừa ở 2 đầu)
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
            else
            {
                // --- THÊM MỚI ---
                // Nếu vượt qua hết các cửa ải trên thì mới tạo đối tượng
                CDanhBa db = new CDanhBa();
                db.SDT = sdt;
                db.HoTen = hoTen; 
                db.Email = email;
                db.Diachi = diaChi;
                //Thêm đối tượng mới tạo vào danh sách
                xuLyDanhBa.them(db);
                hienDSDanhBa();
                MessageBox.Show("Thêm số điện thoại: " + db.SDT + " thành công", "Thông báo");
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            CDanhBa danhBa = new CDanhBa();
            danhBa.SDT = txtSDT.Text;
            if (danhBa.SDT == "")
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
 
        public void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            List<CDanhBa> dsTimKiemTheoTen = new List<CDanhBa>();
            foreach (var item in xuLyDanhBa.layDanhSachDanhBa())
            {
                if (item.HoTen == txtTimKiem.Text)
                {
                    dsTimKiemTheoTen.Add(item);
                }
            }
            dgvDanhBa.DataSource = dsTimKiemTheoTen;
            hienDSDanhBa();
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
                dgvDanhBa.DataSource = null;
                dgvDanhBa.DataSource = xuLyDanhBa.layDanhSachDanhBa();
                return;//Dừng hàm tại đây.
            }
            //Thực hiện lọc dữ liệu.
            //Điều kiện 1: Số điện thoại chứa từ khóa.
            var ketQuaTimKiem = xuLyDanhBa.layDanhSachDanhBa().Where(c =>c.SDT.ToLower().Contains(tukhoa) 
                || c.HoTen.ToLower().Contains(tukhoa)).ToList();// Điều kiện 2: Họ tên chứa từ khóa.

            //Hiển thị kết quả lên giao diện.
            dgvDanhBa.DataSource = null; // Reset nguồn dữ liệu
            dgvDanhBa.DataSource = ketQuaTimKiem; // Gán danh sách đã lọc

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


    }
}

