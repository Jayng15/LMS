namespace LMS;
public class Book
{
    // Properties
    public int BookId { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Constructor
    public Book(int bookId, string title, string author, decimal price, int quantity)
    {
        BookId = bookId;
        Title = title;
        Author = author;
        Price = price;
        Quantity = quantity;
    }

    // Method to display book details
    public void DisplayBookInfo()
    {
        Console.WriteLine($"Book ID: {BookId}, Title: {Title}, Author: {Author}, Price: ${Price}");
    }
}
