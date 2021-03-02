using System;
using System.Linq;
using NProperty = FsCheck.NUnit.PropertyAttribute;

namespace ExampleLib.Tests
{
    public class ExampleTests
    {
        [NProperty]
        public bool SomeTest(int[] xs) =>
                xs.Reverse()
                  .Reverse()
                  .SequenceEqual(xs);
    }
}