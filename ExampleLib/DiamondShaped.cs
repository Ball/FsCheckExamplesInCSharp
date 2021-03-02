using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleLib{
    public class DiamondShaped {
        private static int Ord(char c) {
            return c - 'A';
        }
        private static string Repeat(string part, int times){
            var buffer = new StringBuilder();
            while(times > 0){
                buffer.Append(part);
                times--;
            }
            return buffer.ToString();
        }
        private static string LineFor(char maxLetter, char letter) {
            Console.WriteLine(" ----- ======== {0} {1}", letter, maxLetter);
            var midPoint = Ord(maxLetter);
            var letterIndex = Ord(letter) - midPoint;

            // let innerSpaceWidth = letterIndex * 2 - 1
            // let padding = String(' ', (width - 2 - innerSpaceWidth) / 2)
            // let innerSpace = String(' ', innerSpaceWidth)

            var halfLine = Repeat(" ", letterIndex) + letter + Repeat(" ", letterIndex);
            Console.WriteLine(" -> " + halfLine);
            return halfLine + String.Join("", halfLine.Reverse().Skip(1));
        }
        private static string MakeLine(int index,char c, int width){
            if(c == 'A'){
                var padding = new String(' ', (width - 1) / 2);
                return $"{padding}{c}{padding}";
            } else {
                var innerSpaceWidth = index * 2 - 1;
                var padding = new String(' ', (width - 2 - innerSpaceWidth) / 2);
                var innerSpace = new String(' ', innerSpaceWidth);
                return $"{padding}{c}{innerSpace}{c}{padding}";
            }

        }
        public static IEnumerable<string> DiamonLines(char c){
            var lines = new List<string>{};
            var indexPart = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
                            .TakeWhile( a => a <= c )
                            .Zip(Enumerable.Range(0,26))
                            .ToArray();
            var indexedLetters = indexPart.Concat( indexPart.Reverse().Skip(1)).ToArray();
            var width = indexedLetters.Length;

            return indexedLetters.Select(i => MakeLine(i.Second, i.First, width))
            .ToList();
        }
    }
}