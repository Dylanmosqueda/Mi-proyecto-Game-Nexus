using System.Collections.Generic;
using System.Linq;
using Game_Nexus.API.Data;
using Game_Nexus.API.Models;

namespace Game_Nexus.API.Services
{
    public class ReviewService : IReviewService
    {
        public IEnumerable<Review> GetAll() => GameNexusDb.Instance.Reviews;

        public Review? GetById(int id) => GameNexusDb.Instance.Reviews.FirstOrDefault(r => r.Id == id);

        public Review? Create(Review review)
        {
            var itemExists = GameNexusDb.Instance.Items.Any(i => i.Id == review.ItemId);
            if (!itemExists)
            {
                return null;
            }

            review.Id = GameNexusDb.Instance.Reviews.Any() ? GameNexusDb.Instance.Reviews.Max(r => r.Id) + 1 : 1;
            GameNexusDb.Instance.Reviews.Add(review);
            return review;
        }
    }
}
