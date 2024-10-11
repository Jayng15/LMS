namespace LMS;

public class Inventory
{
    private static Inventory _instance;
    private List<Book> books;
    private Inventory()
    {
        books = new List<Book>();
    }
    public static Inventory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Inventory();
        }
        return _instance;
    }
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"{book.Title} has been added to the inventory.");
    }
    public void RemoveBook(Book book)
    {
        if (books.Contains(book))
        {
            books.Remove(book);
            Console.WriteLine($"{book.Title} has been removed from the inventory.");
        }
        else
        {
            Console.WriteLine("Book not found in inventory.");
        }
    }
    public void ViewBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("The inventory is empty.");
        }
        else
        {
            Console.WriteLine("Books in the inventory:");
            foreach (var book in books)
            {
                Console.WriteLine($"- {book.Title}, Price: ${book.Price}");
            }
        }
    }

    public List<Book> GetBooks() {
        return books;
    }
    public bool IsBookInInventory(Book book)
    {
        return books.Contains(book);
    }

    public int BookCount()
    {
        return books.Count;
    }
}

