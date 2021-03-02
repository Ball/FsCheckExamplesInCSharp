using FsCheck;
using NProperty = FsCheck.NUnit.PropertyAttribute;

namespace ExampleLib.Tests
{

    public class CounterTest
    {
        [NProperty]
        public Property Counter_shouldFail()
        {
            return new CounterSpec().ToProperty();
        }
    }
}