using HeartRateApp;

namespace TestHeartRateProject
{
    public class HeartRateTests
    {
        HeartRate heartRate;

        [SetUp]
        public void Setup()
        {
            heartRate = new HeartRate();

            //add the values
            heartRate.AddHeartRate(10);
            heartRate.AddHeartRate(12);
            heartRate.AddHeartRate(4);
            heartRate.AddHeartRate(8);
            heartRate.AddHeartRate(34);
            heartRate.AddHeartRate(7);
            heartRate.AddHeartRate(89);
        }

        [Test]
        public void TestAddHeartRateReturns7Elements()
        {
            int expectedOutcome = 7;
            int actual = heartRate.GetDailyHeartRates().Count();
            
            Assert.That(actual, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void TestDaysOfWeekReturns7Elements()
        {
            int expectedOutcome = 7;
            int actual = heartRate.GetDaysOfWeek().Length;

            Assert.That(actual, Is.EqualTo(expectedOutcome));
        }

        [Test]
        public void TestAddHeartRateThrowsErrorWhen8ElementsAreAdded()
        {
            Assert.Catch<IndexOutOfRangeException>(() => heartRate.AddHeartRate(2));
        }
    }
}