using BUMA.Data;
using BUMA.Models;
using System.Security.Cryptography;
using System.Text;

public class UserService
{
    private readonly BUMADbContext _context;

    public UserService(BUMADbContext context)
    {
        _context = context;
    }
    public bool AuthenticateUser(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username);

        if (user == null)
        {
            return false;
        }

        string passwordHash = ComputeMD5Hash(password);
        return passwordHash == user.PasswordHash;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
         var existingUser = _context.Users.Find(user.UserId);
        if (existingUser != null)
        {
            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.AccessLevel = user.AccessLevel;
            _context.SaveChanges();
        }
    }

    public void DeleteUser(int? userId)
    {
        var user = _context.Users.Find(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    private string ComputeMD5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }

    public List<User> GetAllUsers()
    {
        return _context.Users
            .Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                AccessLevel = u.AccessLevel
            })
            .ToList();
    }

    public User GetUser(string username, string password)
    {

        string passwordHash = ComputeMD5Hash(password);

        var user = _context.Users
            .Where(u => u.Username == username && u.PasswordHash == passwordHash)
            .Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                AccessLevel = u.AccessLevel
            })
            .FirstOrDefault();

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password."); 
        }
        return user;
    }

    public bool ExistingUser(string username)
    {

        var user = _context.Users
            .Where(u => u.Username == username)
            .Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                AccessLevel = u.AccessLevel
            })
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }
        return true;
    }
}