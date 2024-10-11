using System;
using System.Collections.Generic;

namespace LMS
{
    public class Order
    {
        public int OrderId { get; private set; }  // Unique identifier for the order
        public User User { get; private set; }     // User who placed the order
        public List<Book> Books { get; private set; } // List of books in the order
        public DateTime OrderDate { get; private set; } // Date when the order was placed
        public string Status { get; private set; } // Order status (e.g., "Pending", "Completed", "Cancelled")

        // Constructor to initialize the Order with Cart
        public Order(int orderId, User user, Cart cart)
        {
            OrderId = orderId;
            User = user;
            Books = new List<Book>(cart.GetBooks()); // Get books from the user's cart
            OrderDate = DateTime.Now;
            Status = "Pending"; // Default status
        }

        // Method to complete the order
        public void CompleteOrder()
        {
            Status = "Completed";
            Console.WriteLine($"Order {OrderId} completed.");
        }

        // Method to cancel the order
        public void CancelOrder()
        {
            Status = "Cancelled";
            Console.WriteLine($"Order {OrderId} cancelled.");
        }

        // Method to display order details
        public void DisplayOrderDetails()
        {
            Console.WriteLine($"Order ID: {OrderId}");
            Console.WriteLine($"User: {User.Username}");
            Console.WriteLine($"Order Date: {OrderDate}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine("Books in Order:");
            foreach (var book in Books)
            {
                Console.WriteLine($"- {book.Title} (ID: {book.BookId})");
            }
            Console.WriteLine("=====================================================");
        }

        public decimal GetTotalPrice() {
            decimal totalPrice = 0;
            foreach (var book in Books) {
                totalPrice += book.Price;
            }
            return totalPrice;
        }
    }
}
