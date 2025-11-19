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
{
    public partial class Form1 : Form
    {

        private List<CDanhBa> dsDB = new List<CDanhBa>();
       
        public void hienDSDanhBa()
        {
            // Sắp xếp theo tên từ A->Z 
            dgvDanhBa.DataSource = dsDB.OrderBy(db => db.HoTen).ToList();

        }
        private CDanhBa timDanhBa(string sdt)
        {
            foreach (CDanhBa db in dsDB)
            {
                if (db.SDT == sdt)
                    return db;
            }
            return null;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dsDB = new List<CDanhBa>();
           
            hienDSDanhBa();
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {   // Tạo một đối tượng danh bạ mới
            CDanhBa db = new CDanhBa();
            // Gán thông tin từ các textbox trên form vào đối tượng danh bạ
            db.SDT = txtSDT.Text;
            db.HoTen = txtHoten.Text;
            db.Email = txtEmail.Text;
            db.Diachi = txtDiachi.Text;
            // Kiểm tra xem số điện thoại này đã tồn tại trong danh sách hay chưa
            if (timDanhBa(db.SDT) == null)
            {
                // Nếu chưa tồn tại thì thêm vào danh sách
                dsDB.Add(db);
                // Cập nhật lại danh sách hiển thị trên giao diện
                hienDSDanhBa();
                MessageBox.Show("Thêm số điện thoại  " + db.SDT + " thành công!",
                  "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Nếu số điện thoại đã tồn tại thì thông báo cho người dùng
                MessageBox.Show("Số điện thoại " + db.SDT + "đã tồn tại.\nKhông thể thêm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {  
            CDanhBa danhBa = new CDanhBa();
            danhBa.SDT = txtSDT.Text;
            // Kiểm tra nếu người dùng chưa nhập số điện thoại
            if (danhBa.SDT == null)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại cần xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra xem số điện thoại có tồn tại trong danh sách hay không
            if (timDanhBa(danhBa.SDT) != null)
            {    // Nếu có, thì xóa khỏi danh sách
                dsDB.Remove(timDanhBa(danhBa.SDT));
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
            CDanhBa dbCanSua = timDanhBa(sdtCanTim);

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
           
            using (StreamWriter sw = new StreamWriter("DanhBa.txt"))
            {
                // Duyệt qua tất cả các phần tử danh bạ trong danh sách
                foreach (CDanhBa db in dsDB)
                {
                    // Ghi từng dòng theo định dạng: SDT,HoTen,Email,DiaChi
                    sw.WriteLine("{0},{1},{2},{3}", db.SDT, db.HoTen, db.Email, db.Diachi);

                }
                MessageBox.Show("Lưu danh bạ thành công!");

            }
        }
        public void LoadDanhBa()
        {
          
            dsDB.Clear();
            using (StreamReader sr = new StreamReader("DanhBa.txt"))
            {
                string line;
             
                while ((line = sr.ReadLine()) != null)
                {   // Tách chuỗi theo dấu phẩy để lấy từng trường
                    string[] parts = line.Split(',');
                    // Kiểm tra đúng đủ 4 phần tử mới tạo đối tượng danh bạ
                    if (parts.Length == 4)
                    { // Khởi tạo đối tượng CDanhBa với các thông tin đọc được
                        CDanhBa db = new CDanhBa(parts[0], parts[1], parts[2], parts[3]);
                      
                        dsDB.Add(db);
                    }
                }
            }
            hienDSDanhBa();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra file có tồn tại hay không
                if (!File.Exists("DanhBa.txt"))
                {
                    MessageBox.Show("File DanhBa.txt không tồn tại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Gọi hàm load danh bạ
                LoadDanhBa();

                MessageBox.Show("Tải danh bạ thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải file!\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
