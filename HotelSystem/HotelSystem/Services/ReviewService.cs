using HotelSystem.Models;

namespace HotelSystem.Services
{
    internal class ReviewService
    {
        private readonly FileService _fs = new();
        private readonly string path = "Reviews.json";
        private List<Review> reviews = new();

        public void LoadReviews() => reviews = _fs.Load(path, new List<Review>());
        private void Save() => _fs.Save(path, reviews);

        public void RateRoom()
        {
            if (UserService.CurrentUser == null) throw new Exception("Login first.");
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            Console.Write("Rating (1-5): "); int.TryParse(Console.ReadLine(), out var rating);
            Console.Write("Comment: "); var comment = Console.ReadLine() ?? "";
            if (roomId <= 0 || rating < 1 || rating > 5 || string.IsNullOrWhiteSpace(comment))
                throw new Exception("Invalid review data.");

            reviews.Add(new Review { UserId = UserService.CurrentUser!.Id, RoomId = roomId, Rating = rating, Comment = comment });
            Save();
            Console.WriteLine("Review submitted.");
        }

        public void ViewRoomReviews()
        {
            Console.Write("Room Id: "); int.TryParse(Console.ReadLine(), out var roomId);
            var list = reviews.Where(r => r.RoomId == roomId).ToList();
            if (!list.Any()) { Console.WriteLine("No reviews."); return; }
            foreach (var r in list) Console.WriteLine($"#{r.Id} User:{r.UserId} {r.Rating}/5 {r.Comment}");
        }
    }
}