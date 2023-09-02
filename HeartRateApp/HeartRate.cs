namespace HeartRateApp
{
    /// <summary>
    /// This is the template class of the heart rate data
    /// </summary>
    public class HeartRate
    {
        private List<int> dailyHeartRates;

        private string[] daysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        public HeartRate()
        {
            dailyHeartRates = new List<int>();
        }

        public List<int> GetDailyHeartRates() => dailyHeartRates;

        /// <summary>
        /// Adds a value to the heart rate
        /// </summary>
        /// <param name="dailyHeartRate">value to be added</param>
        /// <exception cref="IndexOutOfRangeException">Exception thrown when heart rates values exceeds 7 elements</exception>
        public void AddHeartRate(int dailyHeartRate)
        {
            if (dailyHeartRates.Count >= 7) { 
                throw new IndexOutOfRangeException("Heart rates to be stored cannot be greater than 7");
            }
            dailyHeartRates.Add(dailyHeartRate);
        }

        public string[] GetDaysOfWeek() => daysOfWeek;
    }
}
