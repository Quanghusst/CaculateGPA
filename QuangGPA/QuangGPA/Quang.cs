using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace QuangGPA
{
    public class Quang
    {
        private static Quang _instance;

        // CPA
        public double CPA { get; private set; }
        // Mảng GPA
        public Dictionary<string, double> GPABySemester { get; private set; }
        public Dictionary<string, int> TotalCreditsBySemester { get; private set; }

        private Quang()
        {
            GPABySemester = new Dictionary<string, double>();
            TotalCreditsBySemester = new Dictionary<string, int>();
            LoadDataFromCSV("data.csv");
        }

        public static Quang Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Quang();
                }
                return _instance;
            }
        }

        private void LoadDataFromCSV(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            }))
            {
                var subjects = csv.GetRecords<Subject>().ToList();

                // Tính CPA
                int tongTin = 0;
                double cpa = 0;

                foreach (var subject in subjects)
                {
                    tongTin += subject.TinChi;
                    cpa += subject.DiemSo * subject.TinChi;

                    // Tính GPA theo từng học kỳ
                    if (!GPABySemester.ContainsKey(subject.HocKy))
                    {
                        GPABySemester[subject.HocKy] = CalculateGPA(subjects, subject.HocKy);
                        TotalCreditsBySemester[subject.HocKy] = CalculateCredits(subjects, subject.HocKy);
                    }
                }

                CPA = cpa / tongTin;
            }
        }
        private int CalculateCredits(List<Subject> subjects, string hocKy)
        {
            int tongTin = 0;

            var subjectKy = subjects.Where(subject => subject.HocKy == hocKy).ToList();

            foreach (var subject in subjectKy)
            {
                tongTin += subject.TinChi;
            }

            return tongTin;
        }
        private double CalculateGPA(List<Subject> subjects, string hocKy)
        {
            int tongTin = 0;
            double gpa = 0;

            var subjectKy = subjects.Where(subject => subject.HocKy == hocKy).ToList();

            foreach (var subject in subjectKy)
            {
                tongTin += subject.TinChi;
                gpa += subject.DiemSo * subject.TinChi;
            }

            return gpa / tongTin;
        }
    }
}
