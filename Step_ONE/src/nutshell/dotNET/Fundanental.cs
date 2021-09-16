using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Step_ONE.src.nutshell.dotNET
{
      public  class Fundanental
    {
        public static void Fundanental_Demo()
        {
            StringAndTextHandling.Char_Demo();
        }
    }

    class StringAndTextHandling
    {
        public static void Char_Demo()
        {
            Char c = 'c';

            char d = '\t'; // char is aliases the System.Char struct.

            //WriteLine(System.Char.ToUpper(c));
            //WriteLine(Char.IsWhiteSpace('\t'));

            string temp = "he2-,?0rmom";
            char[] chart = temp.ToCharArray();
            foreach(char c1 in chart)
            {
                if (char.IsLetter(c1))
                {
                    Write(c1);
                }
            }
        }

        public static void String_Demo()
        {
            string s1 = "Hello";
            string s2 = "First\r\nSecond Line";
            string s3 = @"\\server\fileshare\helloword.cs";
            string s4 = new string('*', 10);
            char[] ca = s1.ToCharArray();
            string s5 = new string(ca);
            string s6 = "";
            string s7 = null;
            WriteLine(s6 == string.Empty);
            WriteLine(s7 == null);

            char letter = s1[0];

            // sealed class String : IEnumerable<char>,
            foreach (char c in "123") WriteLine(c);
            s1.StartsWith("");
            s1.EndsWith("");
            s1.Contains("");

            WriteLine("pass5w0rd".IndexOfAny("0123456789".ToCharArray()));

        }
    }
}
