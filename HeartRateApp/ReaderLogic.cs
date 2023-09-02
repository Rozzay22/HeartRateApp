namespace HeartRateApp
{
    /// <summary>
    /// This is user interface for setting up the heart rates and displaying the heart rates
    /// </summary>
    public class ReaderLogic
    {
        private HeartRate heartRate;

        public ReaderLogic(HeartRate heartRate)
        {
            this.heartRate = heartRate;    
        }

        public void SetDailyHeartRates()
        {
            //get the days of the week
            string[] daysOfWeek = heartRate.GetDaysOfWeek();

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                //enforce only integer values are accepted
                do {
                    Console.WriteLine($"Enter your heart rate for {daysOfWeek[i]}: ");

                    if (int.TryParse(Console.ReadLine(), out int heartRates))
                    {
                        //This method AddHeartRate has been tested in unit test project
                        heartRate.AddHeartRate(heartRates);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid integer e.g. 56");
                        Console.WriteLine();
                    }
                    
                } while(true);

            }
        }

        public void DisplayDailyHeartRates()
        {
            //get the days of the week
            string[] daysOfWeek = heartRate.GetDaysOfWeek();

            //get the daily heart rates
            List<int> dailyHeartRates = heartRate.GetDailyHeartRates();

            // output the heart rates
            Console.WriteLine("Your heart rates for the week are: ");

            for (int i = 0; i < daysOfWeek.Length; i++)
            {
                Console.WriteLine(daysOfWeek[i] + ": " + dailyHeartRates[i]);
            }

        }
    }
}
