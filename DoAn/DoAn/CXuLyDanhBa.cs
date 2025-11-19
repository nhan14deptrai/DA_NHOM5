using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DoAn
{
    public class CXuLyDanhBa
    {
        private Dictionary<string, CDanhBa> dsDanhBa; // Khai báo Danh sách danh bạ.
        public CXuLyDanhBa()
        {
            dsDanhBa = new Dictionary<string, CDanhBa>();// Khởi tạo danh bạ
        }
        public List<CDanhBa> layDanhSachDanhBa()
        {
            return dsDanhBa.Values.ToList();// Hàm hiển thị Danh sách danh bạ.
        }
        public void them(CDanhBa db)
        {
            dsDanhBa.Add(db.SDT, db);//Thêm db vào Danh sách.
        }
        public CDanhBa tim(string m_sdt)
        {
            try
            {
                return dsDanhBa[m_sdt];
            }
            catch
            {

                return null;
            }
        }
        public void xoa(string m_sdt)
        {
            dsDanhBa.Remove(m_sdt);
        }
        public void sua(CDanhBa db)
        {
            dsDanhBa[db.SDT] = db;
        }
        public bool ghiFile(string tenfile)
        {
            using (Stream file = File.Open(tenfile, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, dsDanhBa);
                return true;
            }
        }
        public void docFile(string tenfile)

        {    // Xóa dữ liệu cũ trong Dictionary trước khi nạp file mới
            dsDanhBa.Clear();
            // Mở file văn bản để đọc 
            using (StreamReader sr = new StreamReader("dsdb.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Tách dòng theo dấu phẩy: MaSo, HoTen, SDT, DiaChi
                    string[] parts = line.Split(',');

                    if (parts.Length == 4) // Kiểm tra đúng số cột dữ liệu
                    {
                        CDanhBa db = new CDanhBa(parts[0], parts[1], parts[2], parts[3]); // Tạo một đối tượng danh bạ từ dữ liệu đã tách
                        dsDanhBa.Add(db.SDT, db);
                        // Thêm vào Dictionary với key = Số điện thoại
                        // (Cần đảm bảo không trùng SDT)
                    }
                }
            }


        }
    }
}
