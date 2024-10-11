using System;
using System.Collections.Generic;

namespace LMS
{
    public class OrderManager
    {
        private List<Order> orders; // List to store placed orders
        private int nextOrderId; // To generate unique order IDs
        public OrderManager()
        {
            orders = new List<Order>();
            nextOrderId = 1; // Initialize the next order ID
        }
        // Method to place an order
        public Order PlaceOrder(User user, Cart cart)
        {
            // Ensure the cart is not empty
            if (cart.GetBooks().Count == 0)
            {
                Console.WriteLine("Cannot place order. The cart is empty.");
                return null; // Return null if no items in the cart
            }
            // Create a new order
            Order newOrder = new Order(nextOrderId, user, cart);
            orders.Add(newOrder); // Add the new order to the orders list
            nextOrderId++; // Increment the order ID for the next order

            Console.WriteLine($"Order placed for user '{user.Username}' with {cart.GetBooks().Count} items.");
            return newOrder; // Return the newly created order
        }

        public void CancelOrder(int orderId)
        {
            Order orderToCancel = orders.Find(o => o.OrderId == orderId);
            if (orderToCancel != null)
            {
                orderToCancel.CancelOrder(); // Call the CancelOrder method on the found order
                orders.Remove(orderToCancel); // Remove the order from the list
                Console.WriteLine($"Order with ID '{orderId}' cancelled.");
            }
            else
            {
                Console.WriteLine($"Order with ID '{orderId}' not found.");
            }
        }
        public void DisplayOrders()
        {
            foreach (var order in orders)
            {
                order.DisplayOrderDetails(); // Display each order's details
            }
        }

        public List<Order> GetOrdersByUser(User user)
        {
            return orders.Where(order => order.User.UserId == user.UserId).ToList(); // Assuming Order has a UserId property
        }

        // Method to find an order by ID and check if it belongs to a specific user
        public Order FindOrder(int orderId)
        {
            return orders.FirstOrDefault(order => order.OrderId == orderId); 
        }
    }
}
