using System.Collections.Generic;
using Game_Nexus.API.Models;

namespace Game_Nexus.API.Services
{
    // Asegúrate de que diga "public interface" y NO "public class"
    public interface IReviewService
    {
        IEnumerable<Review> GetAll();
        Review? GetById(int id);
    }
}