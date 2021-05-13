using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;


namespace Step_ONE.src.step_one
{
    class BasicConcepts
    {
        // 1: declaration space
            class A1
        {
            void F()
            {
                int i = 0;
                if (true)
                {
                  //  i = 4;
                }
            }
            void G()
            {
                if (true)
                {
                   // int i = 0;
                }
                int i = 1;
            }
            void H()
            {
                if (true)
                {
                    int i = 0;
                }
                if (true)
                {
                    int i = 2;
                }
            }
            void I()
            {
                for(int i = 0; i < 10; i++) { }
                for (int i = 0; i < 10; i++) { }

            }
        }

        // 1:end of delcaraiton space
        //ValueType vt;
        //Object o;
        //Enum e;
        //String s;
        //Array a;
        //Delegate d;
       
        // 2: Accessibility domains

        /*
         * 
         * 
         */
        public static void test_accessibility_demo()
        {
            WriteLine(A.X);
            WriteLine(B.X);
            WriteLine(A.A_1.X);
           //  WriteLine(C.X);
            WriteLine(B.C.X);
          //  WriteLine(B.D.X);

        }
        /**
         * 
            The accessibility domain of 
            A and A.X is unlimited.
            A.Y, B, B.X, B.Y, B.C, B.C.X, and B.C.Y is the program text of the containing program.
            A.Z is the program text of A.
            B.Z and B.D is the program text of B, including the program text of B.C and B.D.
            B.C.Z is the program text of B.C.
            B.D.X and B.D.Y is the program text of B, including the program text of B.C and B.D.
            B.D.Z is the program text of B.D.
                     * */
        public class A
        {
            public static int X;
            internal static int Y;
            private static int Z;

            public class A_1
            {
                public static int X;
                internal static int Y;
                private static int Z; // just can be used in the same class inner and his subclass;
            }
            public class A_1_1 : A_1
            {
                public static void test_acc()
                {
                    WriteLine(X);
                    WriteLine(Y);
                    WriteLine(Z);


                }
            }

            public static void test_acc()
            {
              //  WriteLine(A_1.Z);
              //  WriteLine(A_1_1.Z);
            }
        }
        internal class B
        {
            public static int X;
            internal static int Y;
            private static int Z;

            public class C
            {
                public static int X; 
                internal static int Y;
                private static int Z;
            }
            private class D
            {
                public static int X; 
                internal static int Y; 
                private static int Z;
            }

            public static void test_acc_intermal()
            {

            }
        }
        public class AA
        {
            protected int x;
            static void F(AA a, BB b)
            {
                // the context is in AA, where the protected int x is defined
                a.x = 1;
                b.x = 2;
            }
        }
        public class BB : AA
        {
            // the context is in BB, which derives from AA, here is the area of BB,
            // so the protected int x in AA  could not be use directly.
            static void F(AA a ,BB b)
            {
                // according the unit of class to restrict the level of accessiable
                /**
                 * Must by BB instance b to access the x , the context where the code are.
                 */
               // a.x = 11;
                b.x = 3;
            }
        }
        class C<T>
        {
            protected T x;
        }
        class D<T> : C<T>
        {
            static void F()
            {
                D<T> dt = new D<T>();
                D<int> dint = new D<int>();
                D<string> ds = new D<string>();
                dt.x = default(T);
                dint.x = 12;
                ds.x = "hello";

            }
        }
        //end 2: Accessibility domains
        class AAA { }
        // Class accessibility should be consistent between the base class and inherited class
      //  public class BBB : AAA { }
      public class BBB 
        {
            A F() { return null; }

            // inconsistent accessibility 
           // public AAA H() { return null; }

        }
        // 3.Signatures and overlaoding
        interface ITest
        {
            void F();
            void F(int x);
           //  int F(int x);  the type of return value are not part of a signature.
            void F(ref int x);
            //  void F(out int x); // the CLI does not provide a way to define methods that differ solely in ref and out
            void F(int x, int y);
            int F(string x);
            void F(string[] x);
          //   void F(params string[] a);
                
         
        }
        // By using the params keyword, you can specify a method parameter that takes a variable
        // number of arguments
        public class MyClass
        {
            // here just one param which has the type params int[]
            public static void UserParam(params int[] list)
            {
                foreach( var i in list)
                {
                    Console.WriteLine(i);
                }
            }
            public static void test_UserParam()
            {
                // here params include 1,2,23,222;
                UserParam(1, 2, 23, 222);
            }
        }

        // 4: Scopes
        class E
        {
            void F()
            {
                i = 1;
            }
            // in class the process of init and loading and assignment
            int i = 0;

            void F1()
            {
                // here the context is local variable 
                string i;
                i = "hello";  
               
              //   i = 33;  class field  variable was repalced by local variable
            }
            int a = 10;
            void G()
            {
                int j = (j = 1);
                bool t = j == 1;

               //  int b = a++; // in the method beacuse there is int a=1 ,so outer space int a=10 can not be used.
               
                int a = 1; 

            }
        }

    }
}
