using System;
using DoiTLean.NanoID;

namespace DoiTLean.NanoID.Tests
{
    public class NanoIDTests
    {
        private const string DefaultAlphabet = "_-0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly INanoID _nanoID = new NanoID();

        [Fact]
        public void Generate_DefaultSize_ReturnsIdWith21Characters()
        {
            _nanoID.Generate(out string result);

            Assert.Equal(21, result.Length);
        }

        [Fact]
        public void Generate_CustomSize_ReturnsIdWithRequestedLength()
        {
            _nanoID.Generate(out string result, Size: 10);

            Assert.Equal(10, result.Length);
        }

        [Fact]
        public void Generate_OnlyUsesCharactersFromAlphabet()
        {
            _nanoID.Generate(out string result, Size: 100);

            Assert.All(result, c => Assert.Contains(c, DefaultAlphabet));
        }

        [Fact]
        public void Generate_CustomAlphabet_OnlyUsesProvidedCharacters()
        {
            const string alphabet = "ABC";

            _nanoID.Generate(out string result, Size: 50, CustomAlphabet: alphabet);

            Assert.All(result, c => Assert.Contains(c, alphabet));
        }

        [Fact]
        public void Generate_TwoCalls_ProduceDifferentIds()
        {
            _nanoID.Generate(out string first);
            _nanoID.Generate(out string second);

            Assert.NotEqual(first, second);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Generate_NonPositiveSize_Throws(int size)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _nanoID.Generate(out _, Size: size));
        }

        [Fact]
        public void Generate_NullAlphabet_Throws()
        {
            Assert.Throws<ArgumentException>(() => _nanoID.Generate(out _, CustomAlphabet: null!));
        }

        [Fact]
        public void Generate_EmptyAlphabet_Throws()
        {
            Assert.Throws<ArgumentException>(() => _nanoID.Generate(out _, CustomAlphabet: ""));
        }

        [Fact]
        public void Generate_AlphabetLongerThan256_Throws()
        {
            string tooLong = new string('a', 257);

            Assert.Throws<ArgumentException>(() => _nanoID.Generate(out _, CustomAlphabet: tooLong));
        }

        [Fact]
        public void GenerateWithCustomRandomBytesGenerator_SameSeed_ProducesSameId()
        {
            _nanoID.GenerateWithCustomRandomBytesGenerator(42, out string first);
            _nanoID.GenerateWithCustomRandomBytesGenerator(42, out string second);

            Assert.Equal(first, second);
        }

        [Fact]
        public void GenerateWithCustomRandomBytesGenerator_DifferentSeeds_ProduceDifferentIds()
        {
            _nanoID.GenerateWithCustomRandomBytesGenerator(1, out string first);
            _nanoID.GenerateWithCustomRandomBytesGenerator(2, out string second);

            Assert.NotEqual(first, second);
        }

        [Fact]
        public void GenerateWithCustomRandomBytesGenerator_NonPositiveSize_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _nanoID.GenerateWithCustomRandomBytesGenerator(1, out _, Size: 0));
        }

        [Fact]
        public void GenerateDeterministic_SameSeed_ProducesSameId()
        {
            _nanoID.GenerateDeterministic(7, out string first);
            _nanoID.GenerateDeterministic(7, out string second);

            Assert.Equal(first, second);
        }

        [Fact]
        public void GenerateDeterministic_MatchesLegacyMethodForSameSeed()
        {
            _nanoID.GenerateDeterministic(99, out string viaNewMethod);
            _nanoID.GenerateWithCustomRandomBytesGenerator(99, out string viaLegacyMethod);

            Assert.Equal(viaLegacyMethod, viaNewMethod);
        }

        [Fact]
        public void GenerateDeterministic_NonPositiveSize_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _nanoID.GenerateDeterministic(1, out _, Size: -5));
        }

        [Fact]
        public void GenerateDeterministic_EmptyAlphabet_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                _nanoID.GenerateDeterministic(1, out _, CustomAlphabet: ""));
        }
    }
}
