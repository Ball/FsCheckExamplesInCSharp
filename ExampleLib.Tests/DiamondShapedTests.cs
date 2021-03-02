using System;
using NUnit.Framework;
using FsCheck;
using System.Linq;

using FProperty = FsCheck.NUnit.PropertyAttribute;

namespace ExampleLib.Tests
{
    public class DiamondShapedTests
    {

        public Arbitrary<char> UpperCase() =>
            Gen.Sized(n => Gen.OneOf<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                                .Select(c => Gen.Constant(c))))
            .ToArbitrary();

        [Test]
        public void ExampleA()
        {
            Assert.That(
                DiamondShaped.DiamonLines('A'),
                Is.EquivalentTo(new[] { "A" })
            );
        }

        [Test]
        public void ExampleB()
        {
            Assert.That(
                DiamondShaped.DiamonLines('B').ToArray(),
                Is.EquivalentTo(new[] { " A ", "B B", " A " })
            );
        }

        [Test]
        public void ExampleC()
        {
            Assert.That(
                DiamondShaped.DiamonLines('C').ToArray(),
                Is.EquivalentTo(new[] { "  A  ", " B B ", "C   C", " B B ", "  A  " })
            );
        }

        [FProperty]
        public Property ShouldAlwaysProduceLines() =>
            Prop.ForAll(UpperCase(), c =>
                DiamondShaped.DiamonLines(c).Count() != 0
            );

        [FProperty]
        public Property ShouldAlwaysProduceAnOddNumberOfLines() =>
            Prop.ForAll(UpperCase(), c =>
              DiamondShaped.DiamonLines(c).Count() % 2 == 1
            );

        [FProperty]
        public Property ShouldProduceASquareOutput() =>
            Prop.ForAll(UpperCase(), c =>
            {
                var lines = DiamondShaped.DiamonLines(c);
                var height = lines.Count();
                return lines.All(l => l.Length == height);
            });

        [FProperty]
        public Property EachLineShouldOnlyHaveOneLetter() =>
            Prop.ForAll(UpperCase(), c =>
            {
                var lines = DiamondShaped.DiamonLines(c);
                return lines.Select(l => l.Replace(" ", ""))
                        .Select(l => l.Distinct())
                        .All(l => l.Count() == 1);
            });

        [FProperty]
        public Property ShouldHaveVerticalSymetry() =>
            Prop.ForAll(UpperCase(), c =>
            {
                var lines = DiamondShaped.DiamonLines(c);
                return lines.Reverse()
                    .Zip(lines)
                    .All(tuple => tuple.First == tuple.Second);
            });

        [FProperty]
        public Property ShouldHaveHorizontalSymetry() =>
            Prop.ForAll(UpperCase(), c =>
            {
                DiamondShaped.DiamonLines(c)
                    .All(line => string.Join("", line.Reverse()) == line);
            });

        [FProperty]
        public Property ShouldPositionLettersCorrectly() =>
            Prop.ForAll(UpperCase(), c =>
            {
                var lines = DiamondShaped.DiamonLines(c).ToArray();
                var midPoint = lines.Length / 2 + 1;
                var matrix = lines.Take(midPoint).Reverse().Select(l => l.Substring(0, midPoint).ToCharArray()).ToArray();
                foreach (var i in Enumerable.Range(0, midPoint))
                {
                    var expectedC = (char)(c - i);
                    if (matrix[i][i] != expectedC)
                    {
                        return false;
                    }
                }
                return true;
            });
    }

}