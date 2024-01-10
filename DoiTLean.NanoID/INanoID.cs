using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;

namespace DoiTLean.NanoID
{
    /// <summary>
    /// Nano ID is a library for generating random IDs. Likewise UUID, there is a probability of duplicate IDs. However, this probability is extremely small.
    /// </summary>
    [OSInterface(Description = "Nano ID is a library for generating random IDs. Likewise UUID, there is a probability of duplicate IDs. However, this probability is extremely small.", IconResourceName = "DoiTLean.NanoID.resources.icon.png", Name = "NanoID")]
    public interface INanoID

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
		void GenerateWithCustomRandomBytesGenerator(int RandomSize, out string NanoID, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// The default method uses URL-friendly symbols (A-Za-z0-9_-) and returns an ID with 21 characters (to have a collision probability similar to UUID v4).
        /// </summary>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet  you can pass alphabet as an argument.
        /// 
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="NanoID"></param>
        /// 
        
        void Generate(out string NanoID, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

    }
}