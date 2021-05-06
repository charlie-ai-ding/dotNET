using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Console;

namespace Step_ONE.src.step_one
{
    class chapter02
    {
      public   static void print_zgding()
        {
            Console.WriteLine("Can I print out by the Visual studio ?");

        }
        public static void string_demo()
        {
            string fullname = "Charlie\tDing";
            // string filepath = "c:\televisions\sony\bravia.txt";
            string filepath = @"c:\televisions\sony\bravia.txt";
        }
        public static void number_demo()
        {
            uint naturalNumber = 23; // unsigned integer means positive whole number
            // naturalNumber = -23;
            float realNumber = 2.3F;
            double anotherRealNumber = 2.3;

            int decimalNotation = 2_000_000;
            int binaryNotation = 0b_0001_1110_1000_0100_1000_0000; // 0b
            int hexadecimalNotation = 0x_001E_8480;  //0x

            Console.WriteLine($"{decimalNotation == binaryNotation}");
            Console.WriteLine($"{decimalNotation == hexadecimalNotation}");

            Console.WriteLine($"int uses {sizeof(int)}bytes and can store numbers in the range {int.MinValue:N0}to {int.MaxValue:N0}");

            double a = 0.1;
            double b = 0.2;

            if(a+b == 0.3)
            {
                Console.WriteLine($"in double style {a}+{b} equals 0.3");
            }else
            {
                Console.WriteLine($"in double style {a}+{b} does NOT equals  0.3");

            }


            decimal a1 = 0.1M;
            decimal b1 = 0.2M;

            if (a + b == 0.3)
            {
                Console.WriteLine($" In decimal style: {a}+{b} equals 0.3");
            }
            else
            {
                Console.WriteLine($"In decimal style:  {a}+{b} does NOT equals  0.3");

            }


        }

        public static void storeAnyTypeOfObject()
        {
            object height = 1.88;
            object name = "Charlie";

            Console.WriteLine($"{name} is {height} metres tall");
           //  int length1 = name.length;
            int lenght2 = ((string)name).Length;
        }
        public static void dynamic_demo()
        {
            dynamic anotherName = "Daniel";

        }
        public static void var_demo()
        {
            var population = 66_000_000;
            var file1 = File.CreateText(@"C:\somthing.txt");
            StreamWriter file2 = File.CreateText(@"C:\sth.txt");

        }
        public static void default_demo()
        {
            Console.WriteLine($"default(int) ={default(int)}");
            Console.WriteLine($"default(bool) ={default(bool)}");
            Console.WriteLine($"default(DataTime) ={default(DateTime)}");
            Console.WriteLine($"default(string) ={default(string)}");
        }
        public static void array_demo()
        {
            string[] names;
            names = new string[4];
            names[0] = "a";
            names[1] = "a";
            names[2] = "a";
            names[3] = "a";

        }
        /**
         * Enabling nullable and non-nullable reference types 
         * 1: project level:  
         * <PropertyGroup>
         *  <Nullable>enable</Nullable>
         * </PropertyGroup>
         * 
         * 2:file level: on the top of the code file:
         * # nullable disable or  #nullable enable
         */
        public static void null_demo()
        {
            int thisCannotBeNull = 10;
            // thisCannotBeNull = null;

            int? thatCanBeNull = null;
            Console.WriteLine(thatCanBeNull.GetValueOrDefault());

            string authorName = string.Empty;
            string bookName = null;
            // int bookCount = bookName.Length;  this will throws a NullReferenceException

            int? y = bookName?.Length;


        }
        public static void formattingUsingNumberedPositionalArguments()
        {
            int numberOfApples = 12;
            decimal pricePerApple = 0.35M;

            Console.WriteLine(format: "{0} apples costs {1:C}", arg0: numberOfApples, arg1: pricePerApple * numberOfApples);

            string formatted = string.Format(format: "{0} apples costs {1:C}", arg0: numberOfApples, arg1: pricePerApple * numberOfApples);

        }
        public static void gettingTextInputFromTheUser()
        {
            Console.Write("Type your first name and press ENTER");
            string firstName = Console.ReadLine();

            Console.Write("Type your first age and press ENTER");
            string age = Console.ReadLine();

            Console.WriteLine($"Hello {firstName}, you look good for {age}");
        }
        public static void GettingKeyInputFromTheUser()
        {

            Write("Press any key combination: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            WriteLine("Key:{0}, Char:{1}, Modifiers:{2}", arg0: key.Key, arg1: key.KeyChar, arg2: key.Modifiers);

        }

        public static void GettingInputArgFromConsole(string[]args)
        {
            WriteLine($"There are {args.Length} arguments.");

            foreach(string s in args)
            {
                WriteLine($"Each param is :{s}");
            }
        }
        public static void SettingOptionsWithArguments(string[]args)
        {
            if(args.Length < 4)
            {
                WriteLine("You must specify two colors and dimensions,e.g");
                WriteLine("dotnet run red yellow 80 40");
                return; // stop running
            }
            ForegroundColor = (ConsoleColor)Enum.Parse(enumType: typeof(ConsoleColor), value: args[0], ignoreCase: true);
            BackgroundColor = (ConsoleColor)Enum.Parse(enumType: typeof(ConsoleColor), value: args[1], ignoreCase: true);
            WindowWidth = int.Parse(args[2]);
            WindowHeight = int.Parse(args[3]);


        }
    }

    public class Stack
    {
        Entry top;

        public void push(object data)
        {
            top = new Entry(top, data);
        }

        public object Pop()
        {
            if (top == null) throw new InvalidOperationException();
            object result = top.data;
            top = top.next;
            return result;
        }

        class Entry
        {
            public Entry next;
            public object data;

            public Entry(Entry next,object data)
            {
                this.next = next;
                this.data = data;
            }
        }
    }
    
}
