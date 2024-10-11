namespace LMS;

public class Admin : User
{
    public Admin(int userId, string username, string email, string password)
        : base(userId, username, email, password)
    {
    }
    public void AddBookToInventory(Book book)
    {
        Inventory inventory = Inventory.GetInstance();
        inventory.AddBook(book);
    }
    public void RemoveBookFromInventory(Book book)
    {
        Inventory inventory = Inventory.GetInstance();
        inventory.RemoveBook(book);
    }
    public void ViewInventory()
    {
        Inventory inventory = Inventory.GetInstance();
        inventory.ViewBooks();
    }
}
