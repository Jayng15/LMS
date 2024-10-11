using System;

namespace LMS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the LMSFacade
            LMSFacade lmsFacade = new LMSFacade();

            // Sample books added to the inventory
            lmsFacade.AddBook(new Book(1, "1984", "George Orwell", 219, 20));
            lmsFacade.AddBook(new Book(2, "To Kill a Mockingbird", "Harper Lee", 102, 20));
            lmsFacade.AddBook(new Book(3, "The Great Gatsby", "F. Scott Fitzgerald", 293, 20));

            User admin = lmsFacade.CreateAdmin("admin1", "admin1@example.com", "admin123");
            User customer1 = lmsFacade.CreateCustomer("customer1", "customer1@example.com", "password1");
            User customer2 = lmsFacade.CreateCustomer("customer2", "customer2@example.com", "password2");

            Cart cart1 = customer1.Cart;
            cart1.AddBook(lmsFacade.FindBook(1)); // Customer 1 adds 1984
            cart1.AddBook(lmsFacade.FindBook(2));

            Cart cart2 = customer2.Cart;
            cart2.AddBook(lmsFacade.FindBook(3));

            Console.WriteLine("Initialization complete. Thank you for using the Library Management System!");
            DisplayMenu(lmsFacade);
        }

        public static void DisplayMenu(LMSFacade lmsFacade)
        {
            Console.WriteLine("Welcome to the Library Management System!");

            // Prompt for username and password
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            // Authenticate user
            User user = lmsFacade.AuthenticateUser(username, password);
            if (user != null)
            {
                // If the user is found, check their role and display the appropriate menu
                if (user is Admin)
                {
                    AdminMenu(lmsFacade);
                }
                else
                {
                    CustomerMenu(lmsFacade, user); // Pass the user for cart management
                }
            }
            else
            {
                Console.WriteLine("Invalid username or password. Please try again.");
            }
        }

public static void AdminMenu(LMSFacade lmsFacade)
{
    bool exit = false;

    while (!exit)
    {
        Console.WriteLine("\n--- Admin Menu ---");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Update Book Quantity");
        Console.WriteLine("3. View All Books");
        Console.WriteLine("4. View User Orders");
        Console.WriteLine("5. Cancel an Order");
        Console.WriteLine("6. Exit");

        Console.Write("Choose an option (1-6): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Add Book
                Console.Write("Enter Book ID: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    Console.Write("Enter Book Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter Author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity))
                    {
                        Console.Write("Enter Price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Book newBook = new Book(bookId, title, author, price, quantity);
                            lmsFacade.AddBook(newBook); // Add book to the system
                            Console.WriteLine($"Book '{title}' added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid price entered. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity entered. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Book ID entered. Please try again.");
                }
                break;

            case "2":
                Console.Write("Enter Book ID to update quantity: ");
                if (int.TryParse(Console.ReadLine(), out int updateBookId))
                {
                    Console.Write("Enter the additional quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int newQuantity))
                    {
                        lmsFacade.UpdateBookQuantity(updateBookId, newQuantity);
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity entered. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Book ID entered. Please try again.");
                }
                break;

            case "3":
                // View All Books
                lmsFacade.DisplayBooks();
                break;

            case "4":
                // View User Orders
                Console.Write("Enter username to view their orders: ");
                string username = Console.ReadLine();
                if (username != null)
                {
                    User user = lmsFacade.GetUser(username); // Assuming you have a method to get user by ID
                    if (user != null)
                    {
                        List<Order> userOrders = lmsFacade.GetUserOrders(user);
                        Console.WriteLine($"\n--- Orders for User: {user.Username} ---");
                        if (userOrders.Count > 0)
                        {
                            foreach (var order in userOrders)
                            {
                                Console.WriteLine($"Order ID: {order.OrderId}, Total Price: ${order.GetTotalPrice()}, Date: {order.OrderDate}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No orders found for this user.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid User ID entered. Please try again.");
                }
                break;

            case "5":
                // Cancel an Order
                Console.Write("Enter Order ID to cancel: ");
                if (int.TryParse(Console.ReadLine(), out int orderId))
                {
                    Order order = lmsFacade.FindOrder(orderId); // Assuming you have a method to find an order by ID
                    if (order != null)
                    {
                        lmsFacade.CancelOrder(order); // Assuming you have a CancelOrder method
                        Console.WriteLine($"Order ID {orderId} has been cancelled.");
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Order ID entered. Please try again.");
                }
                break;

            case "6":
                exit = true;
                Console.WriteLine("Exiting the admin menu. Thank you for using the Library Management System!");
                break;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }
}


        public static void CustomerMenu(LMSFacade lmsFacade, User user)
        {
            bool exit = false;
            Cart userCart = user.Cart; // Get the user's cart

            while (!exit)
            {
                Console.WriteLine("\n--- Customer Menu ---");
                Console.WriteLine("1. View Available Books");
                Console.WriteLine("2. Search Book by Title");
                Console.WriteLine("3. Add Book to Cart");
                Console.WriteLine("4. View Cart");
                Console.WriteLine("5. Place Order");
                Console.WriteLine("6. View Orders");
                Console.WriteLine("7. Cancel Order");
                Console.WriteLine("8. Exit");

                Console.Write("Choose an option (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        lmsFacade.DisplayBooks(); // Show available books
                        break;
                    case "2":
                        Console.Write("Enter the title of the book to search: ");
                        string title = Console.ReadLine();
                        var foundBooks = lmsFacade.SearchBooksByTitle(title); // Call search method
                        if (foundBooks.Count > 0)
                        {
                            Console.WriteLine("\n--- Search Results ---");
                            foreach (var book in foundBooks)
                            {
                                Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Quantity: {book.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No books found with the specified title.");
                        }
                        break;
                    case "3":
                        Console.Write("Enter the Book ID to add to your cart: ");
                        if (int.TryParse(Console.ReadLine(), out int bookId))
                        {
                            Book bookToAdd = lmsFacade.FindBook(bookId);
                            if (bookToAdd != null && bookToAdd.Quantity > 0)
                            {
                                userCart.AddBook(bookToAdd); // Add the book to the cart
                                Console.WriteLine($"'{bookToAdd.Title}' has been added to your cart.");
                            }
                            else
                            {
                                Console.WriteLine("Book not found or out of stock.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Book ID.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("\n--- Your Cart ---");
                        foreach (var book in userCart.GetBooks())
                        {
                            Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Quantity: {book.Quantity}, Price: ${book.Price}");
                        }
                        Console.WriteLine($"Total Price: ${userCart.GetTotalPrice()}");
                        break;
                    case "5":
                        if (userCart.GetBooks().Count > 0)
                        {
                            lmsFacade.PlaceOrder(user, userCart); // Place the order
                            Console.WriteLine("Order placed successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Your cart is empty. Please add books to your cart before placing an order.");
                        }
                        break;
                    case "6":
                        // View Orders
                        var orders = lmsFacade.GetUserOrders(user); // Retrieve orders associated with the user
                        if (orders.Count > 0)
                        {
                            Console.WriteLine("\n--- Your Orders ---");
                            foreach (var order in orders)
                            {
                                order.DisplayOrderDetails(); // Display order details
                            }
                        }
                        else
                        {
                            Console.WriteLine("You have no orders.");
                        }
                        break;
                    case "7":
                        // Cancel Order
                        Console.Write("Enter the Order ID to cancel: ");
                        if (int.TryParse(Console.ReadLine(), out int orderId))
                        {
                            var orderToCancel = lmsFacade.FindOrder(orderId); // Find the order by ID for this user
                            if (orderToCancel != null)
                            {
                                lmsFacade.CancelOrder(orderToCancel); // Cancel the order
                                Console.WriteLine($"Order ID {orderId} has been canceled.");
                            }
                            else
                            {
                                Console.WriteLine("Order not found or it does not belong to you.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid Order ID.");
                        }
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting the customer menu. Thank you for using the Library Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }


    }
}


