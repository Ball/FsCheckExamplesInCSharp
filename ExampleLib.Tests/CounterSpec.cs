using FsCheck;

namespace ExampleLib.Tests
{
    public class CounterSpec : ICommandGenerator<Counter, int>
    {
        public Counter InitialActual => new Counter();

        public int InitialModel => 0;

        public Gen<Command<Counter, int>> Next(int value)
        {
            return Gen.Elements(new Command<Counter, int>[] { new Inc(), new Dec() });
        }

        private class Inc : BaseCommand
        {
            public override Counter RunActual(Counter c)
            {
                c.Inc();
                return c;
            }

            public override int RunModel(int m)
            {
                return m + 1;
            }
        }
        private class Dec : BaseCommand
        {
            public override Counter RunActual(Counter value)
            {
                value.Dec();
                return value;
            }

            public override int RunModel(int value)
            {
                return value - 1;
            }
        }
        private abstract class BaseCommand : Command<Counter, int>
        {
            public override Property Post(Counter c, int m)
            {
                return (m == c.Get()).ToProperty();
            }

            public override string ToString()
            {
                return GetType().Name;
            }
        }

    }
}