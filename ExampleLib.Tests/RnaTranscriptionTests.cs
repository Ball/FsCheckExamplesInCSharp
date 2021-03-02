using NUnit.Framework;
using FsCheck;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ExampleLib.Tests
{
    public class RnaTransactionTests
    {
        private Regex _validRnaCharacters;

        private Arbitrary<string> Dna() =>
            Gen.Sized(n =>
                Gen.ArrayOf(n, Gen.OneOf(new List<Gen<string>>{
                    Gen.Constant("A"),
                    Gen.Constant("C"),
                    Gen.Constant("G"),
                    Gen.Constant("T")
                        }))
                    .Select(r => string.Join("", r)))
                        .Select(r => r ?? "")
            .ToArbitrary();

        [NUnit.Framework.SetUp]
        public void Setup()
        {
            _validRnaCharacters = new Regex(@"^[ACGU]*$");
        }

        [FsCheck.NUnit.Property()]
        public Property TranscribedRNAShouldBeTheSameLength() =>
            Prop.ForAll(Dna(), dna =>
               dna.Length == RnaTranscription.Transcribe(dna).Length);

        [FsCheck.NUnit.Property()]
        public Property TranscribedRNAShouldOnlyContainCGAU() =>
            Prop.ForAll(Dna(), dna =>
               (_validRnaCharacters.IsMatch(RnaTranscription.Transcribe(dna))).When(dna != null));

        [FsCheck.NUnit.Property()]
        public Property ShouldMatchAAndUCounts() =>
            Prop.ForAll(Dna(), dna =>
            {
                var aCount = dna.ToCharArray()
                                .Count(c => c == 'A');
                var uCount = RnaTranscription
                                .Transcribe(dna)
                                .ToCharArray()
                                .Count(c => c == 'U');
                return aCount == uCount;
            });

        [FsCheck.NUnit.Property()]
        public Property ShouldMatchGAndCCounts() =>
            Prop.ForAll(Dna(), dna =>
            {
                var gCount = dna.ToCharArray()
                                .Count(c => c == 'G');
                var cCount = RnaTranscription
                                .Transcribe(dna)
                                .ToCharArray()
                                .Count(c => c == 'C');
                return gCount == cCount;
            });
        [FsCheck.NUnit.Property( )]
        public Property ShouldMatchCAndGCounts() =>
            Prop.ForAll(Dna(), dna =>
            {
                var gCount = RnaTranscription
                                .Transcribe(dna)
                                .ToCharArray()
                                .Count(c => c == 'G');
                var cCount = dna.ToCharArray()
                                .Count(c => c == 'C');
                return gCount == cCount;
            });

        [FsCheck.NUnit.Property()]
        public Property ShouldMatchTAndACounts() =>
            Prop.ForAll(Dna(), dna =>
            {
                var TCount = dna.ToCharArray()
                                .Count(c => c == 'T');
                var aCount = RnaTranscription
                                .Transcribe(dna)
                                .ToCharArray()
                                .Count(c => c == 'A');
                return aCount == TCount;
            });

        [TestCase("CAT")]
        public void ShouldMatchCatToOtherSide(string dna)
        {
            var rna = RnaTranscription.Transcribe(dna);
            Assert.That(rna,
                Is.EqualTo("GUA")
            );
        }

        [TestCase("AT")]
        [TestCase("GTG")]
        public void ShouldTranscribeAT(string dna)
        {
            Assert.That(
                _validRnaCharacters.IsMatch(RnaTranscription.Transcribe(dna)),
                Is.True
            );
        }

    }
}