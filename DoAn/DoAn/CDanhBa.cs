using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    [Serializable]
    public class CDanhBa
    {
        private string m_sdt;
        private string m_hoten;
        private string m_email;
        private string m_diachi;
        private  bool m_isFavorite;
        public bool IsFavorite
        {
            get { return m_isFavorite; }
            set { m_isFavorite = value; }
        }
        public string Ten
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HoTen))
                    return "";

                // Tách theo khoảng trắng
                string[] parts = HoTen.Trim().Split(' ');
                return parts[parts.Length - 1];   // Lấy tên cuối
            }
        }

        public string SDT
        {
            get { return m_sdt; }
            set { m_sdt = value; }
        }public string HoTen
        {
            get { return m_hoten; }
            set { m_hoten = value; }
        }public string Email
        {
            get { return m_email; } 
            set { m_email = value; }
        }public string Diachi
        {
            get { return m_diachi; }
            set { m_diachi = value; }
        }public CDanhBa()
        {
            m_sdt = "";
            m_hoten = "";
            m_email = "";
            m_diachi = "";
        }public CDanhBa(string Sdt,string Hoten, string Email,string Diachi, bool favorite = false)
        {
            m_sdt = Sdt;
            m_hoten = Hoten;
            m_email= Email;
            m_diachi = Diachi;
            IsFavorite = favorite;
        }
    }
}
