using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using QuangGPA;

double CalculateGPA(List<Subject> subjects, string hocKy = null)
{
    int tongTin = 0;
    double cpa = 0;
    if (hocKy == null)
    {
        foreach (var subject in subjects)
        {
            tongTin += subject.TinChi;
            cpa += subject.DiemSo * subject.TinChi;
        }
    }
    else
    {
        // Lọc các môn học theo hocKy
        var subjectKy = subjects.Where(subject => subject.HocKy == hocKy).ToList();
        foreach (var subject in subjectKy)
        {
            tongTin += subject.TinChi;
            cpa += subject.DiemSo * subject.TinChi;
        }
    }

    return cpa / tongTin;
}
Console.OutputEncoding = System.Text.Encoding.UTF8;
string path = @"data.csv";


using (var reader = new StreamReader(path)) // tạo 1 người reader
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
{
    HasHeaderRecord = true
}))
{
    var subjects = csv.GetRecords<Subject>().ToList(); // chuyển kiểu thành tolist cho dễ 


    // Tính CPA 
    Console.WriteLine("CPA: " + CalculateGPA(subjects));
    Console.WriteLine("=================================");
    // Tính GPA cho từng học kỳ chỉ một lần
    var gpaBySemester = new Dictionary<string, double>();

    foreach (var subject in subjects)
    {
        if (!gpaBySemester.ContainsKey(subject.HocKy))
        {
            gpaBySemester[subject.HocKy] = CalculateGPA(subjects, subject.HocKy);
        }
    }

    // In kết quả
    foreach (var kvp in gpaBySemester)
    {
        Console.WriteLine("GPA " + kvp.Key + ": " + kvp.Value);
    }
    Console.ReadKey();
    /*  Code JavaScript lấy dữ liệu từ bảng HTML
     function tableToCSV() {
    const table = document.getElementById('ctl00_ctl00_contentPane_MainPanel_MainContent_gvCourseMarks_DXMainTable');
    let csv = [];
    for (let row of table.rows) {
        let rowData = [];
        for (let cell of row.cells) {
            rowData.push(cell.innerText);
        }
        csv.push(rowData.join(','));
    }
    return csv.join('\n');
}

// Gọi hàm và hiển thị kết quả trong console
console.log(tableToCSV());
     */


}
