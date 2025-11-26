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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        // Biến để lưu form hiện tại đang mở
        private Form currentFormChild;

        // Hàm mở form con
        private void OpenChildForm(Form childForm)
        {
            // Nếu có form nào đang mở thì đóng lại trước
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            pnlHome.Visible = false;
            currentFormChild = childForm;

            // Thiết lập để form con hoạt động như một control
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ viền window
            childForm.Dock = DockStyle.Fill; // Lấp đầy panel

            // Thêm form con vào Panel chứa (giả sử tên là panelBody)
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;

            // Hiển thị lên
            childForm.BringToFront();
            childForm.Show();
        }
        void setButtonColor(Button btn)
        {   Main user = new Main();
            switch (btn.Name)
            {
              case "btnHome": btnHome.ImageIndex = 1;
                    break;
               case "btnUser": btnUser.ImageIndex = 3;
                    break;
                case "btnSetting": btnSetting.ImageIndex = 5;
                    break;
                default: break;

            }
         
            btn.BackColor = Color.FromArgb(24, 30, 54);
            panel1.Top = btn.Top;
            panel1.Height = btn.Height-30;
            panel1.Location = new Point(btn.Location.X ,btn.Location.Y-65);
            panel1.BringToFront();
           
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ban muon thoat chuong trinh", "Thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
            
        }
      
       
        private void btnHome_Click(object sender, EventArgs e)
        {
            // 1. Đóng form con đang mở (nếu có)
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }

            // 2. Hiện Panel Home lên
            pnlHome.Visible = true;

            // 3. Đưa nó lên trên cùng (đề phòng bị các control khác che)
            pnlHome.BringToFront();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            
            // Gọi hàm mở form User
            OpenChildForm(new User());

            // (Tuỳ chọn) Đổi màu nút hoặc tiêu đề nếu muốn
           
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {

        }

        private void btnMinisize_Click_1(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void Maxsize_Click(object sender, EventArgs e)
        {
            if(WindowState== FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
