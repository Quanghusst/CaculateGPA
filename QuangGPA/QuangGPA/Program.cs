/*using System;

class Program
{
    static void Main()
    {
                    Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Tiêu đề bảng
        string[] headers = { "STT", "Họ Tên", "Tuổi", "Điểm" };

        // Dữ liệu bảng
        string[,] data = {
            { "1", "Anh Vu", "29", "9.5" },
            { "2", "Saori Hara", "20", "8.5" },
            { "3", "Doremon", "30", "5" },
            { "4", "Duong Qua", "45", "6" },
            { "5", "Obama", "59", "9.5" }
        };

        // Chiều rộng cột
        int[] colWidths = { 5, 12, 6, 6 };

        // In dòng tiêu đề
        PrintLine(colWidths);
        PrintRow(headers, colWidths);
        PrintLine(colWidths);

        // In các dòng dữ liệu
        for (int i = 0; i < data.GetLength(0); i++)
        {
            string[] row = new string[data.GetLength(1)];
            for (int j = 0; j < data.GetLength(1); j++)
            {
                row[j] = data[i, j];
            }
            PrintRow(row, colWidths);
        }
        PrintLine(colWidths);
    }

    static void PrintLine(int[] colWidths)
    {
        Console.Write("+");
        foreach (int width in colWidths)
        {
            Console.Write(new string('-', width) + "+");
        }
        Console.WriteLine();
    }

    static void PrintRow(string[] columns, int[] colWidths)
    {
        Console.Write("|");
        for (int i = 0; i < columns.Length; i++)
        {
            Console.Write(columns[i].PadRight(colWidths[i]) + "|");
        }
        Console.WriteLine();
}
}
*/

using System;

namespace QuangGPA
{
    class Program
    {
        static void PrintLine(int[] colWidths)
        {
            Console.Write("+");
            foreach (int width in colWidths)
            {
                Console.Write(new string('-', width) + "+");
            }
            Console.WriteLine();
        }
        static void PrintRow(string[] columns, int[] colWidths)
        {
            Console.Write("|");
            for (int i = 0; i < columns.Length; i++)
            {
                string cell = " " + columns[i] + " "; // Thêm dấu cách trước và sau giá trị
                Console.Write(cell.PadRight(colWidths[i]) + "|");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var quang = Quang.Instance;
            int totalCredits = 0;
            string[] headers = { "Semester", "Total credit", "GPA" };
            int[] colWidths = { 10, 15, 23 };

            PrintLine(colWidths);
            PrintRow(headers, colWidths);
            PrintLine(colWidths);

            foreach (var kvp in quang.GPABySemester)
            {
                totalCredits += quang.TotalCreditsBySemester[kvp.Key];
                string[] row = { kvp.Key, quang.TotalCreditsBySemester[kvp.Key].ToString(), kvp.Value.ToString() };
                PrintRow(row, colWidths);
                PrintLine(colWidths);
            }
            string[] footer = { "TOTAL", totalCredits.ToString() , ""};
            PrintRow(footer, colWidths);
            PrintLine(colWidths);

            Console.WriteLine();
            Console.WriteLine("CPA: " + quang.CPA);
            Console.ReadKey();
        }
    }
}



//    /*  Code JavaScript lấy dữ liệu từ bảng HTML
//     function tableToCSV() {
//    const table = document.getElementById('ctl00_ctl00_contentPane_MainPanel_MainContent_gvCourseMarks_DXMainTable');
//    let csv = [];
//    for (let row of table.rows) {
//        let rowData = [];
//        for (let cell of row.cells) {
//            rowData.push(cell.innerText);
//        }
//        csv.push(rowData.join(','));
//    }
//    return csv.join('\n');
//}

//// Gọi hàm và hiển thị kết quả trong console
//console.log(tableToCSV());
//     */
