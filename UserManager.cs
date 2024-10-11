using System.Collections.Generic;

namespace LMS
{
    public class UserManager
    {
        private List<User> customers;
        private List<User> admins;

        public UserManager()
        {
            customers = new List<User>();
            admins = new List<User>();
        }

        public User CreateCustomer(string username, string email, string password)
        {
            int userId = customers.Count + 1; // Unique user ID based on the list count
            Customer newCustomer = new Customer(userId, username, email, password);
            customers.Add(newCustomer); // Add the new customer to the list
            return newCustomer;
        }

        public User CreateAdmin(string username, string email, string password)
        {
            int userId = admins.Count + 1; // Unique user ID based on the list count
            Admin newAdmin = new Admin(userId, username, email, password);
            admins.Add(newAdmin); // Add the new admin to the list
            return newAdmin;
        }



        public User FindUser(string username)
        {
            // Search for the user in the customers list
            User foundUser = customers.Find(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
            
            // If not found in customers, search in the admins list
            if (foundUser == null)
            {
                foundUser = admins.Find(u => u.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
            }

            return foundUser; // Return the found user or null if not found
        }



        public void DeleteUser(User user)
        {
            if (user is Customer)
            {
                if (customers.Remove(user))
                {
                    Console.WriteLine($"Customer '{user.Username}' deleted.");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            else if (user is Admin)
            {
                if (admins.Remove(user))
                {
                    Console.WriteLine($"Admin '{user.Username}' deleted.");
                }
                else
                {
                    Console.WriteLine("Admin not found.");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        public List<User> GetCustomers()
        {
            return customers;
        }

        public List<User> GetAdmins()
        {
            return admins;
        }
    }
}
