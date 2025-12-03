using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;


namespace DoAn
{
    public partial class Main : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public Main()
        {
            InitializeComponent();
            OpenChildForm(new User());
           



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
          
            currentFormChild = childForm;

            // Thiết lập để form con hoạt động như một control
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ viền window
            childForm.Dock = DockStyle.Fill; // Lấp đầy panel

            // Thêm form con vào Panel chứa (giả sử tên là panelBody)
            pnlHome.Controls.Add(childForm);
            pnlHome.Tag = childForm;

            // Hiển thị lên
            childForm.BringToFront();
            childForm.Show();
        }
       


        private void btnExit_Click(object sender, EventArgs e)
        {
            
                Close();
            
            
        }
      
       
        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new User());



        }

        private void btnUser_Click(object sender, EventArgs e)
        {
       

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

       
        private void TLPTab_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
    }
}
