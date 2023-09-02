using HeartRateApp;
using Microsoft.Research.SEAL;

namespace TestHeartRateProject
{
    public class CalculationTests
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
        public void TestEncryptionAndDecryptionIsCorrect()
        {
            //create a new instance of EncryptorLogic
            EncryptorLogic encryptorLogic = new EncryptorLogic();

            //store heart rates
            List<int> dailyHeartRates = heartRate.GetDailyHeartRates();

            //encrypt the heartRates
            List<Ciphertext> encryptedHeartRates = encryptorLogic.Encrypt(dailyHeartRates);

            // Sum the encrypted heart rates.
            Ciphertext encryptedTotal = Calculations.SumEncryptedHeartRates(encryptedHeartRates, encryptorLogic.Context);

            //create a new instance of decryptor
            DecryptorLogic decryptorLogic = new DecryptorLogic(encryptorLogic.SecretKey, encryptorLogic.Context);

            //decrypt the encryptedTotal
            ulong totalHeartRate = decryptorLogic.Decrypt(encryptedTotal, encryptorLogic.Encoder);

            int expectedOutcome = heartRate.GetDailyHeartRates().Sum();

            int actual = (int)totalHeartRate;

            Assert.That(actual, Is.EqualTo(expectedOutcome));
        }

    }
}
