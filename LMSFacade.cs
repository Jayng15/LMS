namespace LMS
{
    public class LMSFacade
    {
        private UserManager _userManager;
        private BookManager _bookManager;
        private OrderManager _orderManager;

        public LMSFacade()
        {
            _userManager = new UserManager();
            _bookManager = new BookManager();
            _orderManager = new OrderManager();
        }
        public User GetUser(string username) {
            return _userManager.FindUser(username);
        }


        public User CreateCustomer(string username, string email, string password)
        {
            return _userManager.CreateCustomer(username, email, password);
        }

        public User CreateAdmin(string username, string email, string password)
        {
            return _userManager.CreateAdmin(username, email, password);
        }

        public void AddBook(Book book)
        {
            _bookManager.AddBook(book);  // Add book to the inventory via BookManager
        }

        // Method to place an order and update the inventory
        public void PlaceOrder(User user, Cart cart)
        {
            // Place the order
            _orderManager.PlaceOrder(user, cart);

            // Update the inventory via BookManager by decreasing the quantity of books ordered
            foreach (var bookInCart in cart.GetBooks())
            {
                // Use BookManager to update inventory
                var bookInInventory = _bookManager.FindBook(bookInCart.BookId);
                if (bookInInventory != null && bookInInventory.Quantity >= bookInCart.Quantity)
                {
                    _bookManager.UpdateBookQuantity(bookInCart.BookId, -bookInCart.Quantity);  // Decrease the quantity
                    Console.WriteLine($"{bookInCart.Quantity} copies of '{bookInCart.Title}' have been deducted from inventory.");
                }
                else
                {
                    Console.WriteLine($"Insufficient stock for '{bookInCart.Title}' or book not found in inventory.");
                }
            }

            cart.ClearCart();
        }

        // Method to cancel an order and update the inventory
        public void CancelOrder(Order order)
        {
            _orderManager.CancelOrder(order.OrderId);

            // Restore the inventory via BookManager by increasing the quantity of books in the canceled order
            foreach (var bookInOrder in order.Books)  // Assuming order has a method to retrieve ordered books
            {
                var bookInInventory = _bookManager.FindBook(bookInOrder.BookId);
                if (bookInInventory != null)
                {
                    _bookManager.UpdateBookQuantity(bookInOrder.BookId, bookInOrder.Quantity);  // Increase the quantity
                    Console.WriteLine($"{bookInOrder.Quantity} copies of '{bookInOrder.Title}' have been returned to inventory.");
                }
                else
                {
                    Console.WriteLine($"Book '{bookInOrder.Title}' not found in inventory.");
                }
            }
        }

        public Book FindBook(int bookId)
        {
            return _bookManager.FindBook(bookId);
        }

        public void DisplayBooks()
        {
            _bookManager.DisplayBooks();
        }

        public User AuthenticateUser(string username, string password)
        {
            // Use the UserManager to find the user by username
            User user = _userManager.FindUser(username);

            // Check if the user exists and verify the password
            if (user != null && user.Password == password) // Ensure you have a password property in the User class
            {
                return user; // Return the authenticated user
            }

            return null; // Return null if authentication fails
        }

        public List<Order> GetUserOrders(User user)
        {
            return _orderManager.GetOrdersByUser(user); // Calls OrderManager to get orders for the user
        }

        public Order FindOrder(int orderId)
        {
            return _orderManager.FindOrder(orderId); // Calls OrderManager to find the order by ID for the user
        }

        public List<Book> SearchBooksByTitle(string title)
        {
            return _bookManager.SearchBooksByTitle(title);
        }
        
        public void UpdateBookQuantity(int bookId, int quantityChange) {
            _bookManager.UpdateBookQuantity(bookId, quantityChange);
        }
    }
}
