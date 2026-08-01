namespace Game_Nexus.API
{
    public class RatingCalculator
    {
        public double CalculateAverage(List<int> ratings)
        {
            if (ratings == null || ratings.Count == 0) return 0.0;
            return Math.Round(ratings.Average(), 1);
        }
    }
}
