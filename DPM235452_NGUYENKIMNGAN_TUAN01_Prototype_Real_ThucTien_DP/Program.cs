using System;

public class Document : ICloneable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }

    public object Clone() => this.MemberwiseClone();

    public void Display() =>
        Console.WriteLine($"[Tài liệu] Tiêu đề: {Title} | Tác giả: {Author} | Nội dung: {Content}");
}

class Program
{
    static void Main()
    {
        Document templateDoc = new Document
        {
            Title = "Báo Cáo Mẫu",
            Author = "Nguyễn Kim Ngân",
            Content = "Nội dung chuẩn của báo cáo..."
        };

        Document docWeek1 = (Document)templateDoc.Clone();
        docWeek1.Title = "Báo Cáo Tuần 01"; // Đổi tên tài liệu được sao chép

        Console.WriteLine("--- Bản gốc ---");
        templateDoc.Display();

        Console.WriteLine("\n--- Bản sao chép đã chỉnh sửa ---");
        docWeek1.Display();
    }
}