using Microsoft.Research.SEAL;

namespace HeartRateApp
{
    /// <summary>
    /// class for project calculations
    /// </summary>
    public static class Calculations
    {

        public static Ciphertext SumEncryptedHeartRates(List<Ciphertext> encryptedData, SEALContext context)
        {
            Evaluator evaluator = new Evaluator(context);

            Ciphertext result = new Ciphertext(encryptedData[0]);

            for (int i = 1; i < encryptedData.Count; i++)
            {
                evaluator.AddInplace(result, encryptedData[i]);
            }
             
            return result;
        }
    }
}
