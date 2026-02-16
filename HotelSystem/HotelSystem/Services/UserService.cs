using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class UserService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Users.json";
        private List<User> users = new();
        public static User? CurrentUser = null;

        public void LoadUsers() => users = _fs.Load(path, new List<User>());
        private void Save() => _fs.Save(path, users);

        public void Register()
        {
            Console.Write("Username: "); var u = Console.ReadLine() ?? "";
            Console.Write("Password: "); var p = Console.ReadLine() ?? "";
            Console.Write("Full name: "); var fn = Console.ReadLine() ?? "";
            Console.Write("Email: "); var email = Console.ReadLine() ?? "";
            Console.Write("Phone: "); var phone = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(u) || string.IsNullOrWhiteSpace(p))
                throw new Exception("Username and password required.");
            if (users.Any(x => x.Username.Equals(u, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("User already exists.");
            if (p.Length < 6)
                throw new Exception("Password must be at least 6 characters.");
            if (string.IsNullOrWhiteSpace(fn))
                throw new Exception("Full name required.");
            if (!IsValidEmail(email))
                throw new Exception("Invalid email format.");
            if (!IsValidPhone(phone))
                throw new Exception("Invalid phone number format.");

            users.Add(new User
            {
                Username = u,
                Password = p,
                FullName = fn,
                Email = email,
                Phone = phone,
                Role = "Customer",
                Status = "Active"
            });
            Save();
            Console.WriteLine("Registered successfully.");
        }

        public void Login()
        {
            Console.Write("Username: "); var u = Console.ReadLine() ?? "";
            Console.Write("Password: "); var p = Console.ReadLine() ?? "";

            var user = users.FirstOrDefault(x => x.Username == u && x.Password == p);
            if (user == null) throw new Exception("Invalid credentials.");
            if (user.Status == "Blocked") throw new Exception("User is blocked.");

            CurrentUser = user;
            Console.WriteLine($"Welcome {user.Username} ({user.FullName})");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch { return false; }
        }

        private bool IsValidPhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) &&
                   phone.All(char.IsDigit) &&
                   phone.Length >= 9;
        }
        public string GetLoginStatus()
        {
            if (CurrentUser == null) return "Not logged in.";
            return $"Logged in as: {CurrentUser.Username} (Role: {CurrentUser.Role})";
        }
        public void TopUpBalance()
        {
            EnsureLogged();
            Console.Write("Amount: "); double.TryParse(Console.ReadLine(), out var amt);
            if (amt <= 0) throw new Exception("Amount must be positive.");
            CurrentUser!.Balance += amt;
            Save();
            Console.WriteLine($"Balance updated: {CurrentUser!.Balance:F2}");
        }

        public void UpdateProfile()
        {
            EnsureLogged();
            Console.Write("Full name: "); var name = Console.ReadLine() ?? "";
            Console.Write("Email: "); var email = Console.ReadLine() ?? "";
            Console.Write("Phone: "); var phone = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                throw new Exception("All fields required.");
            CurrentUser!.FullName = name; CurrentUser!.Email = email; CurrentUser!.Phone = phone;
            Save();
            Console.WriteLine("Profile updated.");
        }

        public void BlockUser()
        {
            Console.Write("User Id: "); int.TryParse(Console.ReadLine(), out var id);
            var u = users.FirstOrDefault(x => x.Id == id) ?? throw new Exception("User not found.");
            u.Status = "Blocked";
            Save();
            Console.WriteLine("User blocked.");
        }

        public void UnblockUser()
        {
            Console.Write("User Id: "); int.TryParse(Console.ReadLine(), out var id);
            var u = users.FirstOrDefault(x => x.Id == id) ?? throw new Exception("User not found.");
            u.Status = "Active";
            Save();
            Console.WriteLine("User unblocked.");
        }

        public List<User> GetAll() => users;

        private void EnsureLogged()
        {
            if (CurrentUser == null) throw new Exception("Login first.");
        }
    }
}