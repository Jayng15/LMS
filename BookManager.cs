namespace LMS;
public class BookManager
{
    private Inventory _inventory;

    public BookManager()
    {
        _inventory = Inventory.GetInstance();
    }

    public void AddBook(Book book)
    {
        _inventory.AddBook(book);
        Console.WriteLine($"Book '{book.Title}' added to inventory.");
    }

    public void RemoveBook(Book book)
    {
        _inventory.RemoveBook(book);
        Console.WriteLine($"Book '{book.Title}' removed from inventory.");
    }

    public List<Book> GetBooks()
    {
        return _inventory.GetBooks();
    }

    public Book FindBook(int bookId)
    {
        return _inventory.GetBooks().Find(book => book.BookId == bookId);
    }
public void DisplayBooks()
{
    // Retrieve the list of books from the BookManager
    var books = this.GetBooks();

    // Check if there are any books in the inventory
    if (books.Count == 0)
    {
        Console.WriteLine("No books available in inventory.");
    }
    else
    {
        Console.WriteLine("Displaying all books in inventory:");
        foreach (var book in books)
        {
            // Display the details of each book
            Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Price: ${book.Price}, Quantity: {book.Quantity}");
        }
    }
}


    public void UpdateBookQuantity(int bookId, int quantityChange)
    {
        var book = FindBook(bookId);
        if (book != null)
        {
            if (book.Quantity + quantityChange >= 0)
            {
                book.Quantity += quantityChange;
                Console.WriteLine($"The quantity of '{book.Title}' has been updated. New quantity: {book.Quantity}");
            }
            else
            {
                Console.WriteLine($"Cannot reduce quantity. Not enough stock for '{book.Title}'. Current quantity: {book.Quantity}");
            }
        }
        else
        {
            Console.WriteLine($"Book with ID {bookId} not found in inventory.");
        }
    }

    public List<Book> SearchBooksByTitle(string title)
    {
        List<Book> matchingBooks = new List<Book>();

        foreach (var book in this.GetBooks())
        {
            if (book.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                matchingBooks.Add(book);
            }
        }

        return matchingBooks;
    }

}
