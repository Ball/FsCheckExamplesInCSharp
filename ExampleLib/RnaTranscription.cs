using System.Linq;

namespace ExampleLib
{
    public class RnaTranscription
    {
        public static string Transcribe(string dna)
        {
            return string.Join("",
            dna
            .Select(x => x.ToString())
            .Select(x =>
           {
               return x switch
               {
                   "A" => "U",
                   "T" => "A",
                   "G" => "C",
                   "C" => "G",
                   _ => x
               };
           })
            );
        }
    }
}