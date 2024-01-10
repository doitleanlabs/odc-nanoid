using System;
using System.IO;
using System.Xml.Linq;
using NanoidDotNet;

namespace DoiTLean.NanoID
{
    /// <summary>
    ///  Nano ID is a library for generating random IDs. Likewise UUID, there is a probability of duplicate IDs. However, this probability is extremely small.
    /// </summary>
    public class NanoID : INanoID
    {


        /// <summary>
        /// The default method uses URL-friendly symbols (A-Za-z0-9_-) and returns an ID with 21 characters (to have a collision probability similar to UUID v4).
        /// </summary>
        /// <param name="RandomSize"></param>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet  you can pass alphabet as an argument.
        /// 
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="ssNanoID"></param>
        public void GenerateWithCustomRandomBytesGenerator(int RandomSize, out string NanoID, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            Random random = new Random(RandomSize);
            NanoID = Nanoid.Generate(random, CustomAlphabet , Size);

        } // GenerateWithCustomRandomBytesGenerator

        /// <summary>
        /// The default method uses URL-friendly symbols (A-Za-z0-9_-) and returns an ID with 21 characters (to have a collision probability similar to UUID v4).
        /// </summary>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet  you can pass alphabet as an argument.
        /// 
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="NanoID"></param>
        public void Generate(out string NanoID,int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
        {
            NanoID = Nanoid.Generate(CustomAlphabet, Size); //=> "Uakgb_J5m9g-0JDMbcJqLJ"
        } // Generate



    }
}
