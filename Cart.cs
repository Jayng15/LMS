using System;
using System.Collections.Generic;

namespace LMS
{
    public class Cart
    {
        private List<Book> books;  // List to track books in the cart

        public Cart()
        {
            books = new List<Book>();
        }

        // Method to add a book to the cart, increasing the quantity if it already exists
        public void AddBook(Book book)
        {
            var bookInCart = books.Find(b => b.BookId == book.BookId);

            if (bookInCart != null)
            {
                // If the book is already in the cart, increase the quantity
                bookInCart.Quantity += 1;
                Console.WriteLine($"{book.Title} quantity increased to {bookInCart.Quantity}.");
            }
            else
            {
                // If the book is not in the cart, add a new instance with quantity = 1
                Book newBook = new Book(book.BookId, book.Title, book.Author, book.Price, 1);
                books.Add(newBook);
                Console.WriteLine($"{newBook.Title} has been added to your cart with quantity {newBook.Quantity}.");
            }
        }

        // Method to remove a book or decrease its quantity in the cart
        public void RemoveBook(Book book)
        {
            var bookInCart = books.Find(b => b.BookId == book.BookId);

            if (bookInCart != null)
            {
                // If the book is in the cart, decrease its quantity
                if (bookInCart.Quantity > 1)
                {
                    bookInCart.Quantity -= 1;
                    Console.WriteLine($"{book.Title} quantity decreased to {bookInCart.Quantity}.");
                }
                else
                {
                    // If the quantity is 1, remove the book from the cart
                    books.Remove(bookInCart);
                    Console.WriteLine($"{book.Title} has been removed from your cart.");
                }
            }
            else
            {
                Console.WriteLine("Book not found in cart.");
            }
        }

        // Method to display the books in the cart
        public void ViewCart()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                Console.WriteLine("Books in your cart:");
                foreach (var book in books)
                {
                    Console.WriteLine($"- {book.Title}, Price: ${book.Price}, Quantity: {book.Quantity}");
                }
            }
        }

        // Method to get the books in the cart
        public List<Book> GetBooks()
        {
            return books;
        }

        // Method to clear the cart after placing an order
        public void ClearCart()
        {
            books = new List<Book>();
        }

        // Method to calculate the total price of the books in the cart
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var book in books)
            {
                totalPrice += book.Price * book.Quantity;
            }
            return totalPrice;
        }
    }
}
