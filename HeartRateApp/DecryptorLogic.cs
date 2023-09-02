using Microsoft.Research.SEAL;

namespace HeartRateApp
{
    /// <summary>
    /// This is the class for the decryption code
    /// </summary>
    public class DecryptorLogic
    {
        private Decryptor decryptor;

        public DecryptorLogic(SecretKey secretKey, SEALContext context)
        {
            decryptor = new Decryptor(context, secretKey);
        }

        public ulong Decrypt(Ciphertext encryptedTotal, BatchEncoder encoder)
        {
            // Decrypting the sum of the encrypted total for the week
            Plaintext decryptedTotal = new Plaintext();
            decryptor.Decrypt(encryptedTotal, decryptedTotal);
            List<ulong> decodedTotals = new List<ulong>();
            encoder.Decode(decryptedTotal, decodedTotals);
            ulong totalHeartRate = decodedTotals[0];

            return totalHeartRate;  
        }
    }
}
