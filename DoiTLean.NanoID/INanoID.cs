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
        /// Generates a NanoID whose sequence is deterministic for a given <paramref name="Seed"/>: calling this
        /// repeatedly with the same <paramref name="Seed"/>, <paramref name="Size"/> and <paramref name="CustomAlphabet"/>
        /// always returns the same ID. This is intentional and useful for reproducible scenarios (e.g. tests, fixtures),
        /// but the output is NOT cryptographically random and must not be used where uniqueness/collision-avoidance
        /// guarantees are required (e.g. primary keys). Use <see cref="Generate"/> for that instead.
        /// </summary>
        /// <param name="RandomSize">Seed for the underlying pseudo-random generator. The same value always yields the same ID.</param>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet  you can pass alphabet as an argument.
        ///
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="result"></param>
        void GenerateWithCustomRandomBytesGenerator(int RandomSize, out string result, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// The default method uses URL-friendly symbols (A-Za-z0-9_-) and returns an ID with 21 characters (to have a collision probability similar to UUID v4).
        /// </summary>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet  you can pass alphabet as an argument.
        ///
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="result"></param>
        void Generate(out string result, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// Clearly-named equivalent of <see cref="GenerateWithCustomRandomBytesGenerator"/>: generates a deterministic,
        /// reproducible NanoID from a <paramref name="Seed"/>. Same seed, size and alphabet always produce the same ID.
        /// Not cryptographically secure — do not use where uniqueness guarantees are required.
        /// </summary>
        /// <param name="Seed">Seed for the underlying pseudo-random generator. The same seed always yields the same ID.</param>
        /// <param name="Size">If you want to reduce ID length (and increase collisions probability), you can pass the size as an argument</param>
        /// <param name="CustomAlphabet">If you want to change the ID&apos;s alphabet you can pass alphabet as an argument.
        ///
        /// Alphabet must contain 256 symbols or less. Otherwise, the generator will not be secure.</param>
        /// <param name="result"></param>
        void GenerateDeterministic(int Seed, out string result, int Size = 21, string CustomAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");
    }
}
