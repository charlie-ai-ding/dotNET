
using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

//Aliasing Types and Namespaces
using ProertyInfo2 = System.Reflection.PropertyInfo;
using R = System.Reflection;

// All type names are converted to fully qualified names at compile time.
namespace Step_ONE.src.nutshell
{
    class CSharp_Basic
    {
        class Test { public static int X; }

        public static void NumericLiterals()
        {
            int x = 127;
            long y = 0x7F;
            int millon = 1_100_000;
            var b = 0b1010_1011_1100; // binary with the 0b prefix
            double d = 1.5;
            double millon2 = 1E06;

            WriteLine(1.0.GetType());
            WriteLine(1E06.GetType());
            WriteLine(1.GetType());
            WriteLine(0xF0000.GetType());
            WriteLine(0x10000000.GetType());

            // Numeric suffixes
            float f = 1.0F; // 4.5 would be inferred to be of type double
            double d2 = 1D;
            // decimal d2 = 12.4;
            decimal d3 = 1.9M;
            uint i = 1U;
            long l = 1L;
            ulong ui = 1UL;

            int x1 = 12345;
            long y1 = x1; // Implicit conversation
            short z1 = (short)x1; //Explicit conversation 

            int i2 = (int)f;// any fractional portion is truncated

            int i3 = 10000001;
            float f3 = i3;  // Magnitude preserved,precision lost
            int i4 = (int)f3; // 10000000

            int a5 = int.MinValue;
            a5--; // when overflow,"wraparound" behavior happens.
            WriteLine(a5 == int.MaxValue);
            WriteLine(a5);



        }

        public static void DefaultValues()
        {
            WriteLine(Test.X);
            WriteLine(default(decimal));

        }

        /*
         * A parameter can be passed by value or reference ,regardless of whether the
         * parameter type is a reference type or value type.
         */
        //By default arguments in C# are passed by value
        public static void PassingArgumentsByValue()
        {
            int x = 8;
            Foo(x);  // Make a copy of x
            WriteLine(x); // x will still be 8
            static void Foo(int x)
            {
                x = x + 1; // Increment x by 1 ,but here x is not the x outside
                WriteLine(x);
            }


            StringBuilder sb = new StringBuilder("hello");
            Foo1(sb); // Make a copy of sb reference type value
            WriteLine(sb);

            static void Foo1(StringBuilder fooSB)
            {
                fooSB.Append("test"); // sb and fooSB are different reference type vlaue
                WriteLine(fooSB); //but they are both pointing the same address in heap.
                fooSB = null;
            }

        }

        // ref is a kind of declaration to compiler to deal with by pass by reference.
        public static void PassByReference()
        {
            int x = 8;

            Foo(ref x); // when the ref appear before the param of calling method, pass by refence

            WriteLine("Using ref to make pass by reference:: {0}", x);

            // when the ref appear before the param of original method, 
            static void Foo(ref int p)
            {
                p = p + 1;
                WriteLine("Using ref to make pass by reference:: {0}", p);
            }

            string x1 = "Charlie";
            string y1 = "Lena";
            WriteLine("before swap x1 is{0},y1 is {1} ", x1, y1);
            SWap(ref x1, ref y1);
            WriteLine("after swap x1 is{0},y1 is {1} ", x1, y1);

            static void SWap(ref string a, ref string b)
            {
                string temp = a;
                a = b;
                b = temp;
            }
            // demo for out 
            string a, b;
            Split("Charlie Ding", out a, out b);
            WriteLine("fistname is {0}, and lastName is {1}", a, b);

            // Out variables and discards
            Split("Charlei Ding zhigang", out string a1, out string b1);
            // Split("Charlei Ding zhigang", out string a2, out _);// Discard 2nd param
            // SomeBigMethod(out _, out _, out _, out int x, out _, out _);

            //For backward compatibility,this language feature will not take effect if
            // a real underscore varialbe is in scope.
            string _;
            Split("Charle ding", out string a3, out _);

            WriteLine("is string _ is present, then out _ diseffective, {0}", _);


            static void Split(string name, out string firstNames, out string lastName)
            {
                int i = name.LastIndexOf(' ');
                firstNames = name.Substring(0, i);
                lastName = name.Substring(i + 1);
            }


        }

        /**
         * When you pass an argument by reference,you alias the storage location of an
         * existing variable rather than create a new storage location.
         */
        static int x; // by default x =0 default(int) =0 
        public static void ImplicationsOfPassingByReference()
        {
            Foo(out x);

            static void Foo(out int y)
            {
                WriteLine("before the y was assign a new value, {0}", x);
                y = 1;
                WriteLine("after the y was assign a new value, {0}", x);
            }


            // in 
            long bigNumber = long.MaxValue;

            Bar(in bigNumber);
            static void Bar(in long y)
            {
                long b = 100;
                b = y - b;
                // y = 100; y is readonly 
                WriteLine("just use in y get new b is  , {0}", b);
            }

            //params modifier

            int total = Sum(1, 2, 3, 4, 5);
            int total1 = Sum(new int[] { 12, 33, 334 });
            static int Sum(params int[] ints)
            {
                int sum = 0;
                for (int i = 0; i < ints.Length; i++)
                {
                    sum += i;
                }
                return sum;
            }

            // Optional parameters

            Foo3();
            static void Foo3(int x = 23)
            {
                WriteLine("The default value of x is {0}", x);
            }

            Foo4(10);
            //Named arguments
            Foo4(x: 100, y: 900);
            Foo4(y: 59);
            static void Foo4(int x = 0, int y = 0)
            {
                WriteLine("x is {0} and y is {1}", x, y);
            }

            //Ref Locals
            int[] numbers = { 0, 1, 2, 3, 4, 5 };
            ref int numRef = ref numbers[2];
            numRef *= 10;
            WriteLine("at here numRef should be 20 , {0}", numRef);
            WriteLine("in Array numbers[2] should be 20 , {0}", numbers[2]);



        }
        // Ref Returns  
        // Ref is avoiding copy big data times , directly reference it by reference type
        // this is not commonly used, it is a micro-optimization feature
        static string xx = "Old Value";
        static ref string GetXX() => ref xx; // This method returns a ref

        public static void RefReturn()
        {
            ref string xRef = ref GetXX(); //Assign result to a ref local
            xRef = "New Value";
            WriteLine("We should get the xx is New Value : {0}", xx);
        }

        //Expressions and Operators

        public static void ExpressionsExamples()
        {
            int a = 1 * (199 - 79);
            Math.Log(1);
            // a Void expression is an expression that has no value.
            WriteLine(1);
            //  1 + WriteLine(1);
            a = a * 199;
            a = 100 * (a = 9); // expression is evaluate from right to left.
            WriteLine("here a should be 900, is right? {0}", a);
            int b, c, d, e, f;
            b = c = d = e = f = 100;
            WriteLine("here b should be 100, is right? {0}", b);
            b *= 2; // b= b*2
            c <<= 1; // c= c<<1;
            // A subtle exception to this rule is with events, +=, -= map to event's add and remove accessors

        }

        public static void OperatorsExamples()
        {
            // Precedence
            int a = 1 + 2 * 3;
            //Left-associative operators (except for assignment,lambda, and null-coalescing operators)are left-associate
            int b = 8 / 4 / 2; // =1 
            int c = 8 / (4 / 2); // =2 using parentheses to change the actual order of evaluation
            // Null Operators
            string s1 = null;
            string s2 = s1 ?? "nothing"; // s2 evaluates to "nothing"
            object myVariable = null, someDefault = null;

            myVariable ??= someDefault;
            if (myVariable == null) myVariable = someDefault;
            //Null-Conditional Operator
            System.Text.StringBuilder sb = null;
            string s = sb?.ToString(); //No error;s instead evaluates to null just compiler benifit
            string s3 = sb?.ToString().ToUpper();
            // int length = sb?.ToString().Length; int cannot be null
            int? length = sb?.ToString().Length; // defensive programming style
            string s4 = sb?.ToString() ?? "nothing"; // combines well with the null-coalescing operator


        }
        public static void StatementsExamples()
        {
            //switch
            int cardNumber =0;
            switch (cardNumber)
            {
                case 13:
                    WriteLine("King");
                    break;
                case 12:
                case 11:
                case 10:
                    goto case 13;
                default:
                    throw new Exception("some exception");
         
            }
             void TellMeTheType(object x)
            {
                switch (x)
                {
                    case int i:
                        WriteLine("It's an int!");
                        break;
                    case string s:
                        WriteLine("It's an string of {0}", s);
                        break;
                    case bool b when b == true:
                        WriteLine("It's an bool of {0}", b);
                        break;
                    case DateTime _:
                        WriteLine("It's an DateTime ");
                        break;
                    case float f when f > 1000:
                    case double d when d > 1000:
                    case decimal m when m > 1000:
                        WriteLine("We can refer to x here but not f or d or m");
                        break;
                }

                string cardName = cardNumber switch
                {
                    13 => "KIng",
                    12 => "Queen",
                    11 => "Jack",
                    _ => "Pid card"
                };
                int carN = 12;
                string suite = "spaces";
                string cardName2 = (cardNumber, suite) switch
                {
                    (13, "spades") => "King of spades",
                    (13, "clubs") => "king of clubs",
                    _ => "dd"
                };

            }

            // an example for goto 
            int i = 1;
            startLoop:
            if (i <= 5)
            {
                Write(i + " ");
                i++;
                goto startLoop;
            }

        }
   
       public static void NameSpace()
        {
          
        } 
    }
}

namespace Outer
{
    class Program { R.PropertyInfo p; }
    class Class1 { }
    namespace Inner
    {
        class Class2 : Class1 { }
    }
}
namespace MyTradingCompany
{
    namespace Common
    {
        class ReportBase { }
    }
    namespace ManagementReporting
    {
        class SalesReport : Common.ReportBase { }

    }
}
namespace OUTer
{
    class Foo { }
    namespace INNer
    {
        class Foo { }
        class Test
        {
            Foo f1; // OUTer.INNer.Foo 
            OUTer.Foo f2; // = OUTer.Foo
        }
    }
}
//Nested using directives

namespace N1
{
    class Class1 { }
}
namespace N2
{
    using N1;
    class Class2 : Class1 { }
}
namespace N2
{
    // class Class3 : Class1 { }  Compile-time error not found
}

