using System.ComponentModel;

namespace LMS;

public abstract class User
{
    // Private fields
    private int userId;
    private string username;
    private string email;
    private string password;

    // Public properties
    public int UserId {
        get { return userId; }
        private set { userId = value; }
    }
    
    public string Username {
        get { return username; }
        set { username = value; }
    }
    
    public string Email {
        get { return email; }
        set { email = value; }
    }
    
    public string Password {
        get { return password; }
        set { password = value; }
    }

    // Associated Cart object
    public Cart Cart { get; private set; }

    // Constructor
    public User(int userId, string username, string email, string password)
    {
        UserId = userId;
        Username = username;
        Email = email;
        Password = password;
        Cart = new Cart();
    }
}
