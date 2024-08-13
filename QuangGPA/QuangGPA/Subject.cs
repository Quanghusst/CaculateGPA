using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuangGPA
{
    internal class Subject
    {
        public string Hocky { get; set; }
        public string MaHP { get; set; }
        public string TenHP { get; set; }
        public int TinChi { get; set; }
        public string LopHoc { get; set; }
        public double? DiemQT { get; set; }
        public string DiemThi { get; set; }
        public string DiemChu { get; set; }

        // Điểm số tương ứng với điểm chữ
        public double DiemSo => ConvertGradeToGPA(DiemChu);

        private static double ConvertGradeToGPA(string grade)
        {
            return grade switch
            {
                "A+" => 4.0,
                "A" => 4.0,
                "B+" => 3.5,
                "B" => 3.0,           
                "C+" => 2.5,
                "C" => 2.0,
                "D+" => 1.5,
                "D" => 1.0,
                "F" => 0.0,
                _ => 0.0, // Giá trị mặc định nếu không khớp
            };
        }
    }
}
