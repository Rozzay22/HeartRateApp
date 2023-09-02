using Microsoft.Research.SEAL;
using System.Diagnostics;

namespace HeartRateApp
{
    /// <summary>
    /// This is the main logic that gets executed when the program runs
    /// </summary>
    public class Program
    {
        public static void Main() {

            //create a new heart rate instance
            HeartRate heartRate = new HeartRate();

            //create the reader for setting the heart rate data
            ReaderLogic readerLogic = new ReaderLogic(heartRate);

            //get the heart rate for the week
            readerLogic.SetDailyHeartRates();

            //display the data stored by the user
            readerLogic.DisplayDailyHeartRates();

            //create a new instance of EncryptorLogic
            EncryptorLogic encryptorLogic = new EncryptorLogic();

            //store heart rates
            List<int> dailyHeartRates = heartRate.GetDailyHeartRates();

            //encrypt the heartRates
            List<Ciphertext> encryptedHeartRates = encryptorLogic.Encrypt(dailyHeartRates);

            Stopwatch stopwatch = new Stopwatch();  

            stopwatch.Start();
            // Sum the encrypted heart rates.
            Ciphertext encryptedTotal = Calculations.SumEncryptedHeartRates(encryptedHeartRates, encryptorLogic.Context);
            stopwatch.Stop();

            Console.WriteLine($"Time taken to run encryption logic is {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Reset();
            stopwatch.Start();
            // Sum the encrypted heart rates.
            int _ = heartRate.GetDailyHeartRates().Sum();
            stopwatch.Stop();

            Console.WriteLine($"Time taken to run logic without encryption is {stopwatch.ElapsedMilliseconds} ms");

            //create a new instance of decryptor
            DecryptorLogic decryptorLogic = new DecryptorLogic(encryptorLogic.SecretKey, encryptorLogic.Context);

            //decrypt the encryptedTotal
            ulong totalHeartRate = decryptorLogic.Decrypt(encryptedTotal, encryptorLogic.Encoder);

            //Display the result of the sum
            Console.WriteLine($"Sum of Daily Heart Rate after decryption is {totalHeartRate}");

            // dividing the decrypted total by 7 to get weekly average
            Console.WriteLine($"Average Daily Heart Rate for {totalHeartRate} divided by 7 is: {(float) totalHeartRate / 7}");

            Console.ReadLine();
        }
    }
}