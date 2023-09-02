using Microsoft.Research.SEAL;

namespace HeartRateApp
{
    /// <summary>
    /// This is the class for the encryption code
    /// </summary>
    public class EncryptorLogic
    {
        private EncryptionParameters parms;
        private KeyGenerator keygen;
        private Encryptor encryptor;

        public SecretKey SecretKey { get; }
        public SEALContext Context { get; }
        public BatchEncoder Encoder { get; }

        public EncryptorLogic()
        {
            parms = new EncryptionParameters(SchemeType.BFV);
            ulong polyModulusDegree = 8192;
            parms.PolyModulusDegree = polyModulusDegree;
            parms.CoeffModulus = CoeffModulus.BFVDefault(polyModulusDegree);

            // Adjust for range & precision.
            parms.PlainModulus = PlainModulus.Batching(polyModulusDegree, 20);

            Context = new SEALContext(parms);
            
            keygen = new KeyGenerator(Context);
            keygen.CreatePublicKey(out PublicKey publicKey);

            SecretKey = keygen.SecretKey;
            
            encryptor = new Encryptor(Context, publicKey);

            // Initialize BatchEncoder
            Encoder = new BatchEncoder(Context);
        }

        public List<Ciphertext> Encrypt(List<int> heartRates)
        {
            List<Ciphertext> encryptedHeartRates = new List<Ciphertext>();
            // Encrypting the daily heart rates.
            foreach (var rate in heartRates)
            {
                // Convert int to ulong for encoding
                List<ulong> rateList = new List<ulong> { (ulong)rate }; 

                Plaintext plainRate = new Plaintext();

                Encoder.Encode(rateList, plainRate);

                Ciphertext encryptedRate = new Ciphertext();

                encryptor.Encrypt(plainRate, encryptedRate);

                //store encrypted data
                encryptedHeartRates.Add(encryptedRate);
            }

            return encryptedHeartRates;
        }

    }
}
