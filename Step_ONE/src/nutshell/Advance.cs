using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Dynamic;

using static System.Console;

namespace Step_ONE.src.nutshell.Advance

{
    public class Advance
    {

        public static void Test_Delegate_1()
        {
            DelegateContext dc = new DelegateContext();
            dc.Test_Delegate();

            // Test for delegate's target method can be instance method:
            PlugInMethodsWithDelegates pd = new PlugInMethodsWithDelegates();

            PlugInMethodsWithDelegates.Transform(pd.values2, pd.Cube);

            for (int i = 0; i < pd.values2.Length; i++)
            {
                WriteLine(pd.values2[i]);
            }

            //MulticastDelegateExample
            MulticastDelegateExample me = new MulticastDelegateExample();
            me.Test_Multicast_Delegate();
        }

    }
    public class DelegateContext
    {
        delegate int Transformer(int x);

        int Square(int x) { return x * x; }
        int Square2(int x) => x * x;

        public void Test_Delegate()
        {
            Transformer t = Square;
            int valueOfTrans1 = t.Invoke(5);
            WriteLine("Transformer 5 to 25: {0}", valueOfTrans1);
        }
    }
    // Instance and Static Method Targets
    public class PlugInMethodsWithDelegates
    {
        static int[] values = { 1, 2, 3, 4 };

        public int[] values2 = { 1, 2, 3, 4 };
        public delegate int Transformer(int x);

        //High order function

        /**
         *  PlugInMethodsWithDelegates pd =new PlugInMethodsWithDelegates();      
            PlugInMethodsWithDelegates.Transform(pd.values2, pd.Cube);
        *  When an instance method is assigned to a delegate object,the latter maintains a reference not only to the method but also 
        *  to the instance to which the method belongs.
        *  The System.Delegate class's Target property represents this instance.
        *
        *   Because the instance is stored in the delegate's Target property, its lifetime is extended to (at least as long as)the 
        *   delegate's lifetime
         */
        public static void Transform(int[] values, Transformer t)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]);
            }

            PlugInMethodsWithDelegates pd = (PlugInMethodsWithDelegates)t.Target; //The System.Delegate class's Target property represents this instance.
            WriteLine("We can know the typeof t is {0}", t.GetType());
            WriteLine("We can know the typeof t is {0}", t.Target?.GetType());

            Delegate d;
            MulticastDelegate md;


            WriteLine("Can I print out, values2's length is: {0} ?", pd?.values2.Length);
        }

        static int Square(int x) => x * x;

        public int Cube(int x) => x * x * x;

        public static void Test_Plug_In()
        {
            Transform(values, Square);
            for (int i = 0; i < values.Length; i++)
            {
                WriteLine(values[i]);
            }
        }

    }

    // Multicast delegate example
    public class MulticastDelegateExample
    {
        public delegate void ProgressReporter(int percentComplete);

        //All delegate types implicitly derive from System.MulticastDelegate, which inherits from System.Delegate.C# compiles +,-,+=,-=
        // operations made on a delegate to the static Combine and Remove methods of the System.Delegate calss.
        MulticastDelegate md;
        Delegate d;

        public class Util
        {
            public static void HardWork(ProgressReporter p)
            {
                for (int i = 0; i < 10; i++)
                {
                    p(i * 10);                          //Invoke delegate
                    System.Threading.Thread.Sleep(100); // Simulate hard work
                }
            }
        }

        void WriteProgressToConsole(int percentComplete) => WriteLine(percentComplete);
        void WriteProgressToFile(int percentComplete) => System.IO.File.AppendAllText("progress.txt", percentComplete.ToString());


        public void Test_Multicast_Delegate()
        {
            ProgressReporter p = WriteProgressToConsole;
            p += WriteProgressToFile;
            Util.HardWork(p);

        }
    }

    //Generic Delegate Types

    public class GenericDelegateTypes
    {
        public delegate T Transformer<T>(T arg);



        public class Util
        {
            public static void Transform<T>(T[] values, Transformer<T> t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = t(values[i]);
                }
            }
            public static void Transform2<T>(T[] values, Func<T, T> t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = t(values[i]);
                }
            }
        }

        int[] values = { 1, 2, 3, 4 };

        public static void Test_Generic_DeleateTypes()
        {
            int Square(int x) => x * x;

            GenericDelegateTypes gd = new GenericDelegateTypes();
            GenericDelegateTypes.Util.Transform(gd.values, Square);

            foreach (int i in gd.values)
            {
                WriteLine(i);
            }
        }
    }

    // Delegates Versus Interfaces
    /** Delegate can be better than interface
     * 1: The interface defines only a single method,
     * 2: Multicast capability is needed
     * 3: The subscriber needs to implement the interface mulitple times
     */
    public class DelegatesVersusInterfaces
    {
        static int[] values = { 1, 2, 3, 4, 5 };
        public interface ITransformer { int Transform(int x); }

        public class Util
        {
            public static void TransformAll(int[] values, ITransformer t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = t.Transform(values[i]);
                }
            }
        }

        class Squarer : ITransformer
        {
            public int Transform(int x) => x * x;
        }

        public static void Test_DelegateVersusInterface()
        {
            Util.TransformAll(values, new Squarer());

            foreach (var i in values)
            {
                WriteLine(i);
            }
        }
    }


    // Parameter compatibility

    public class ParameterCompatibility
    {
        // Delegate parameter type are contravariance  => Parameter type  can be subType narrow
        delegate void StringAction(string s); // 

        static void ActOnObject(object o) => WriteLine(o);  // that method which parameter type is object

        public static void Test_ParameterCompatibility()
        {
            // When you call a method, you can supply arguments that have more specific types than the parameters of that method.
            StringAction sa = new StringAction(ActOnObject);  //
            sa("Hello"); // When you call a method, "Hello" is more specific types than object in ActOnObject
        }

        // Return type compatibility  Delegate return types are covariant. =>Return type can be upperType extend
        delegate object ObjectRetriever();
        static string RetriveString() => "Hello";

        public static void Test_Return_Type_Compatibility()
        {
            ObjectRetriever ortr = new ObjectRetriever(RetriveString);
            object result = ortr(); // ObjectRetriever expects to get back an object, but an object subclass will also do:
            WriteLine(result); //Hello
        }
    }

    //Generic delegate type parameter variance

    /**
     * Mark a type parameter used only on the return value as covariant(out)
     * Mark any type parameter used only on the parameters as contravariant(int)
     */
    public class GenericDelegateTypeParameterVariance
    {
        delegate TResult Func<out TResult>();

        delegate void Action<in T>(T arg);
    }

    // 2 EVENT

    public class Event_Example
    {
        public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);

        public class Stock
        {
            string symbol;
            decimal price;
            public Stock(string symbol) => this.symbol = symbol;
            public event PriceChangedHandler PriceChanged;
            public decimal Price
            {
                get => price;
                set
                {
                    if (price == value) return; //Exit if nothing has changed
                    decimal oldPrice = price;
                    price = value;
                    if (PriceChanged != null)  // If invocation list not empty,fire event
                    {
                        PriceChanged(oldPrice, price);
                    }
                }
            }
        }
    }

    // An complete example
    // Standard Event Pattern  1,2,3
    public class EventExampleComplete
    {
        public static void Test_EventExampleComplete()
        {
            Stock stock = new Stock("THPM");
            stock.Price = 27.10M;
            //Register with the PriceChanged event
            stock.PriceChanged += stock_PriceChanged;
            stock.Price = 31.59M;

        }
        static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
            {
                WriteLine("Alert,10% stock price increase!!");
            }
        }
        // 1 EventArgs : base class for conveying information for an event  
        // subclass is named according to the information it contains
        public class PriceChangedEventArgs : EventArgs
        {
            public readonly decimal LastPrice;
            public readonly decimal NewPrice;

            public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
            {
                this.LastPrice = lastPrice;
                this.NewPrice = newPrice;
            }

        }
        // This provides a central point from which subclass can invoke or override the event(assuming the class is not sealed).
        public class Stock
        {
            string symbol;
            decimal price;
            public Stock(string symbol) => this.symbol = symbol;

            //2 define a delegate for the event,  void return type, two arguments sender and EventArgs, endwith EventHandler
            public event EventHandler<PriceChangedEventArgs> PriceChanged;


            //3 Finally,write a protected virtual method that fires the event.
            // the name must match the name of the event,prefixed with the word On and accept a single EventArgs argument

            protected virtual void OnPriceChanged(PriceChangedEventArgs e)
            {
                PriceChanged?.Invoke(this, e);
            }

            public decimal Price
            {
                get => price;
                set
                {
                    if (price == value) return;
                    decimal oldPrice = price;
                    price = value;
                    OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
                }
            }
        }

        // prefifined nongeneric EventHandler delegate can be used when an event doesn't carry extra information

        public class Stock2
        {
            string symbol;
            decimal price;

            public Stock2(string symbol) { this.symbol = symbol; }
            public event EventHandler PriceChanged;
            protected virtual void OnPriceChanaged(EventArgs e)
            {
                PriceChanged?.Invoke(this, e);
            }
            public decimal Price
            {
                get { return price; }
                set
                {
                    if (price == value) return;
                    price = value;
                    OnPriceChanaged(EventArgs.Empty);
                }
            }
        }
    }

    //Event Accessors
    public class EventAccessors
    {
        // By default, accessors are implemented implicitly by the compiler,
        public event EventHandler Price1Changed;

        //You can take over this process by defining explicit event accessors 
        private EventHandler priceChanged;
        public event EventHandler PriceChanged
        {
            add { priceChanged += value; }
            remove { priceChanged -= value; }
        }
    }

    //with explicit event accessors, you can apply more complex strategies to the storage and access of the underlying delegate.

    // When explicity implementing an interface that declares an event
    public interface IFoo { event EventHandler Ev; }
    class Foo : IFoo
    {
        private EventHandler ev;
        event EventHandler IFoo.Ev
        {
            add { ev += value; }  // The add and reomve parts of an event are compiled to add_XXX and remove_XXX methods
            remove { ev -= value; }
        }
        // Like methods, events can be virtual,overridden, abstract,or sealed.
        public static event EventHandler<EventArgs> StaticEvent;
        public virtual event EventHandler<EventArgs> VirtualEvent;
    }

    // Lambda Expressions 
    /**
     * A lambda expression is an unnamed method, which is writen in place of a delegate instance.
     * The Compiler resolves lambda expressions of this type by writing a private method and then moving the expression's code
     * into that method.
     * */
    public class LambdaExpressions
    {
        delegate int Transformer(int i);
        static Transformer sqr = x => x * x;

        static Func<int, int> sqr2 = x => x * x;

        static Func<string, string, int> totalLength = (s1, s2) => s1.Length + s2.Length;

        //do not need to use the parameters,you can discard them with an underscore
        static Func<string, string, int> totalLength2 = (_, _) => 100;

        static void Foo<T>(T x) { }
        static void Bar<T>(Action<T> a) { }

        //   Bar(x=>Foo(x)); // What type is x?

        /**
         * Capturing is internally implemented by "hoisting" the captured variables into fields of a private calss,When the method
         * is called, the calss is instantiated and lifetime-bound to the delegate instance.
         */
        static Func<int> Natural()
        {
            int seed = 0;    // Because seed has been captured,its lifetime is extended to that of the capturing delegate,natural
            int test = 100;  // the local variable test would ordinarily disappear fromt the scope when Natural finished executing.
            WriteLine("Let's see what is the test, {0}", test);
            return () => seed++;
        }

        public static void Test_Lambad_1()
        {
            WriteLine(sqr(12));
            WriteLine(sqr2(12));
            WriteLine(totalLength("Hello", "Charlie"));

            //  Bar(x => Foo(x)); // the type can not be infer from the useing context.
            Bar((int x) => Foo(x));

            Bar<int>(x => Foo(x)); // Specify type parameter fro Bar
            Bar<int>(Foo);         // As above, but with method group
            // Capturing Outer Variables
            int factor = 2; // Outer variables referenced by a lambda expression are called captured variables.
            Func<int, int> multiplier = n => n * factor; // A lambda experssion that captures variables is called a closure.
            factor = 100;  // Captured variables are evaluated when the delegate is actually invoked, not when the variable were captured

            WriteLine(multiplier(10));

            // Lambada experssion can themselves update captured variables
            int seed = 0;
            Func<int> natural = () => seed++;
            WriteLine(natural()); //0
            WriteLine(natural()); //1
            WriteLine(seed); //2

            //Captured variables have their lifetimes extends to that of  the delegate.
            Func<int> natural2 = Natural();
            WriteLine(natural2());
            WriteLine(natural2());

            // Static lambdas,
            Func<int, int> multiplier3 = static n => n * 2;
            int factor2 = 10;

            // The lambda itself evaluates to a delegate instance, which requires a memory allocation.However, if the lambda doesn't 
            // capture variable, the compiler will reuse a single cached instance across the life of the application,so there will be
            // no cost in practice
            //  multiplier3 = static n => n * factor2;


        }
        void Foo()
        {
            int factor = 123;
            static int Multiply(int x) => x * 2; // Local static method
        }

        // Capturing iteration variables
        public static void CapturingIterationVariables()
        {
            Action[] actions = new Action[3];
            //for(int i = 0; i < 3; i++)
            //{
            //    actions[i] = () => Write(i);
            //}

            //foreach (Action a in actions) a();

            Action[] actions2 = new Action[3];

            int i = 0;
            actions2[0] = () => Write(i);
            i = 1;
            actions2[1] = () => Write(i);
            i = 2;
            actions2[2] = () => Write(i);

            // The consequence is that when the delegates are later invoked, each delegate sees i's value at the time of invocation
            foreach (var a in actions2) a();

            Action[] actions3 = new Action[3];
            for (int j = 0; j < 3; j++)
            {
                int loopScoped = j; // is freshly created on every iteration, each closure captures a different variable
                actions3[j] = () => Write(loopScoped);
            }
            foreach (var b in actions3) b();
        }


        // Anonymous Methods
        delegate int Transformer2(int i);
        public static void AnonymousMethods()
        {
            Transformer2 sqr = delegate (int x) { return x * x; };

            Transformer2 sqr1 = (int x) => { return x * x; };

            Transformer sqr2 = x => x * x; // 

        }
    }

    //Try Statements and Exceptions

    public class TryStatementsAndExceptions
    {
        static int Calc(int x) => 10 / x;

        /**
         * Checking for preventable errors is preferable to relying on try/catch blocks because exceptions are relatively expensive
         * to handle, taking hundreds of clock cycles or more
         */
        public static void TryCatch()
        {
            try
            {
                int y = Calc(0);
            }
            catch (DivideByZeroException ex)
            {
                WriteLine("X cannot be zeor");
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout) // Excepiton filters
            {
                WriteLine("this Exception filters works well");
            }

            // finally block can be defeated by  an infinite loop or the process ending abruptly.
            StreamReader reader = null;
            try
            {
                // while (1 == 1) { } // this infinite loop defeat the finally block
                reader = File.OpenText("file.txt"); // can lead to the process ending abruptly, and if no correct catch to deal with it,t
                // the finally block also no chance to be called.
                return;
            }
            catch (Exception e)
            {
                WriteLine("make sure the program will be not ending abruptly,and than the finally can be called");
            }
            finally
            {
                WriteLine("I will so anything I need to do ");
            }

            using (StreamReader sr = File.OpenText("o.txt"))
            {
                WriteLine("Do I have chance to be run?");
            }

            //throw expressions

            string ProperCase(string value) => value == null ? throw new ArgumentException("value") :
                value == "" ? "" : new String("dd");

            //Rethrowing an exception

            try { }
            catch (Exception ex)
            {
                // Log error
                throw new Exception("Rethrow same exception again");
            }
        }
        public string Foo() => throw new NotImplementedException();
    }


    //Enumeration and iterators
    public class Enumerator<T> : IEnumerable<T>
    {

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }


        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public static void Test_Enumeration()
        {
            foreach (char c in "beer")
            {
                WriteLine(c);
            }

            using (var enumerator = "beer".GetEnumerator())
                while (enumerator.MoveNext())
                {
                    var element = enumerator.Current;
                    WriteLine(element);
                }

            // Collection Initializers
            List<int> list = new List<int> { 1, 2, 3, 4 };
            Dictionary<int, string> dict = new Dictionary<int, string>()
            { { 1, "Ding" }, {2,  "Yan" }};

            Dictionary<int, string> dict2 = new Dictionary<int, string>() { [1] = "Hello", [10] = "world" };


        }

        /**
         * The compiler converts iterator methods into private classes that implement IEnumerable<T> and/or IEnumerator<T>.The logic 
         * within the iteraotr block is "inverted" and spliced into the MoveNext method and Current property on the compiler-written enumerator class
         * 
         * */
        // an iterator is a producer of an enumerator.
        public static  IEnumerable<int> Fibs (int fibCount){
            for(int i=0, prevFib =1, curFib = 1; i < fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }

        public static void Test_Print_Fib()
        {
            foreach( int fib in Fibs(6))  // foreach statement is a consumer of an enumerator
            {
                WriteLine(fib);
            }
        }
   
        //Iterator Semantics

        public static void Test_Iterator_Semantics()
        {
            foreach (string s in Foo2())
            {
                WriteLine(s);
            }
            foreach (string s in Foo2_Break(true))
            {
                WriteLine(s);
            }
        }
       public static IEnumerable<string> Foo2()
        {
            yield return "One";
            yield return "Two";
            yield return "Three";
        }
        // A return statement is illegal in an iterator block; instead you must use the yield break statement to 
        // indicate that the iterator block should exit early.
        public static IEnumerable<string> Foo2_Break(bool breakEarly)
        {
            yield return "One";
            yield return "Two";
            if (breakEarly)
            {
                yield break;
            }
            yield return "Three";
        }
    }

    //
    public class IteratorAndTryCatchFinally
    {
        //method
        static IEnumerable<string> Foo4()
        {
            try { 
                yield return "One";
                yield return "One1";
                yield return "One2"; 
                yield return "One3";
            }
            finally { WriteLine("finally"); }
        }
        public static void Test_IteratorAndTryCatch()
        {
            string firstElement = null;
            var sequence = Foo4();
            using(var enumerator = sequence.GetEnumerator())
                while (enumerator.MoveNext())
                {
                    firstElement = enumerator.Current;
                    WriteLine("Can we get the element from the enumerator?,{0}", firstElement);
                }
        }

    }

   // Composing Sequences

    public class ComposingSequences
    {
      static  IEnumerable<int> Fibs(int fibCount)
        {
            for (int i=0, prevFib=1,curFib =1; i< fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }
      static  IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
        {
            foreach(int x in sequence)
            {
                if((x%2 == 0))
                {
                    yield return x;
                }
            }
     
        }

        public static void Test_Composing_Sequences()
        {
            IEnumerable<int> fib1 = Fibs(10);

            foreach( var i in EvenNumbersOnly(fib1))
            {
                WriteLine(i);
            }
        }
    }

 // Nullabel Value Types

    public static class NullableValueTypes
    {
        public static void Test_Nullable_Value_Types()
        {
            string s = null;
            //   int i = null;
            // To represent null in a value type, you must use a special construct called a nullable type.
            int? i = null; //nullable type

            WriteLine(i == null);

            object o = "string";
            int? x = o as int?;
            WriteLine(x.HasValue);
        }
     
    }


    // Tuples

    public class TuplesExample
    {
        public static void Test_Tuple()
        {
            var bob = ("Charlie", 42);
            WriteLine(bob.Item1);
            WriteLine(bob.Item2);

            bob.Item1 = "Daniel";
            bob.Item2 = 9;

            WriteLine(bob.Item1);

            (string, int) nameAndAge = ("fiona", 23);

            var mao = GetPerson();

            WriteLine(mao.Item1);

            Dictionary<(string,int),Uri> di;
            IEnumerable<(int id, string name)> ii;

            // You can optionally give meaningful names to elements when creating tuple literals
            var tuple = (name: "Bob", age: 34);

            WriteLine(tuple.name);

            var charlie = GetPerson2();

            WriteLine(charlie.name);

            var now = DateTime.Now;
            var timeTuple = (now.Day, now.Month, now.Year);
            WriteLine(timeTuple.Item1);

            (string name, int age, char sex) bob1 = ("bob",23,'M');
            (string age, int sex2, char name1) bob2 = bob1;

            WriteLine("Here should be name like bob, {0}",bob2.age);

            // ValueTuple.Create
            ValueTuple<string, int> john1 = ValueTuple.Create("John", 34);
            (string ,int) john2 = ValueTuple.Create("John", 34);  //Declaring a new  tuple

            (string name,int age) john3 = ValueTuple.Create("John", 34);

            //Deconstructing Tuples

            (string name, int age) = john1;  // Deconstructing a tuple
            (string name2, int age2) = john2;

            WriteLine(name);
            var (name3, age3, sex) = GetJob();
            // Equality Comparison
            // Tuples also override the GetHashCode method,making it practical to use tuples as keys in dictionaries.
            var t1 = ("one", 1);
            var t2 = ("one", 1);
            WriteLine(t1.Equals(t2));

        }
        static (string,int,char) GetJob()=> ("Developer",23,'M');
         static (string, int) GetPerson() => ("mao", 109);
        static (string name, int age) GetPerson2() => ("Charlie", 42);

        class Point
        {
            public readonly int X, Y;
            public Point(int x, int y) => (X, Y) = (x, y);
        }
        
  
    }
   
    //Record 

    public class DefiningARecord
    {
        record Point
        {
            public Point(double x, double y) => (X, Y) = (x, y);

            public double X { get; init; }
            public double Y { get; init; }

        }
        record Point3D(double X,double Y,double Z) : Point(X, Y);

      

        /**
         * A good pattern is leave them of the constructor and expose them purely as init-only properties:
         * 
         * Required1 and Required2 is pass by constructor 
         * Optional1 and Optional2 is pass by init-only proterties
         * 
         * 
         */
        record Foo
        {
            // Here is  Parameter lists
            public Foo(int required1, int required2) => (Required1, Required2) = (required1,required2);

            // You can optionally define additional class members here...
            public int Required1 { get; init; }
            public int Required2 { get; init; }
            public int Optional1 { get; init; }
            public int Optional2 { get; init; }
            
        }

        record Test(int A,int B, int C, int D, int E, int F);

        record PointWithVerifiy
        {
            public PointWithVerifiy(double x, double y) => (X, Y) = (x, y);

            double _x;
            public double X
            {
                get => _x;
                init
                {
                    if (double.IsNaN(value))
                    {
                        throw new ArgumentException("X Cannot be NaN");
                    }
                    _x = value;
                }
            }
            public double Y { get; init; }
        }

        record Point_Distance
        {
            public double X { get; }
            public double Y { get; init; }
            public double DistanceFromOrigin { get; }

            

           double GetDistance() {

                WriteLine("IN the GetDistance ...");
                return Math.Sqrt(X * X+ Y * Y);
            }
           // public Point_Distance(double x, double y) => (X, Y, DistanceFromOrigin) = (x, y, Math.Sqrt(x * x + y * y));
            public Point_Distance(double x, double y) => (X, Y, DistanceFromOrigin) = (x, y, GetDistance());


        }

        record Point_Distance2
        {
            public double X { get; }
            public double Y { get; init; }
        

            double? _distance;

            public double DistanceFromOrigin {
               
                get
                {
                    if(_distance == null)
                    {
                        WriteLine("Run this code to generate the distance,");
                        _distance = Math.Sqrt(X * X + Y * Y);
                    }
                    return _distance.Value;
                }
                    
            }


            double GetDistance()
            {

                WriteLine("IN the GetDistance ...");
                return Math.Sqrt(X * X + Y * Y);
            }
            // public Point_Distance(double x, double y) => (X, Y, DistanceFromOrigin) = (x, y, Math.Sqrt(x * x + y * y));
            public Point_Distance2(double x, double y) => (X, Y) = (x, y);


        }

        record Point_3
        {
            public Point_3(double x, double y) => (X, Y) = (x, y);
            double _x, _y;
            double? _distance;
            public double X { get => _x; init { _x = value;_distance = null; } } //  when the _x changed, the _distance should be changed to null
            public double Y { get => _y; init { _y = value;_distance = null; } } 

            public double DistanceFromOrigin => _distance ??= Math.Sqrt(X * X + Y * Y);

        }

        record Point_4(double X,double Y)
        {
            protected Point_4(Point_4 other) => (X, Y) = other;
      
            double? _distance;
        
            public double DistanceFromOrigin => _distance ??= Math.Sqrt(X * X + Y * Y);

            // Define a public Equals method must be virtual, must be strongly typed , it accepted the actual record type
            // Once you get the signature right, the compiler will automatically patch in your method
            public virtual bool Equals(Point_4 other) => other != null && X == other.X && Y == other.Y;

        }

        public static void Test_Record()
        {
            Point p = new Point(1, 2);
            Point p2 = p;
            //  p2.X = 100;   init can be happend at contracotr 
            WriteLine("record is for value,{0},{1}", p2.X, p2.Y);
            WriteLine(p == p2);

            // Test for purely init-only properties.
            Foo foo1 = new Foo(123, 234) { Optional1 = 100, Optional2 = 200 };

            WriteLine(foo1.Required1);
            WriteLine(foo1.Optional1);
            WriteLine(foo1.Optional2);

            Point3D p3d = new Point3D(1, 2, 323);

            WriteLine(p3d.Z);

            // Nondestructive Mutation

            Point p11 = new Point(3, 3);
            Point p12 = p11 with { Y = 989 };

            WriteLine("P11.Y should be 3, {0}", p11.Y);
            WriteLine("P12.Y should be 989, {0}", p12.Y);

            Test t1 = new Test(1, 2, 3, 4, 5, 6);
            Test t2 = t1 with { A = 99, C = 88 };

            WriteLine("t2 A should be 99 ,{0},and C should be 88 {1}", t2.A, t2.C);


            // PointWithVerifiy pwv = new PointWithVerifiy(double.NaN, 11) ;

            // WriteLine(pwv.Y);

            PointWithVerifiy pwv2 = new PointWithVerifiy(2, 44);

            //  PointWithVerifiy pwv3 = pwv2 with { X = double.NaN };

            Point_Distance pd = new(3, 4);

            //  WriteLine("point(3,4)'s distance is 5 {0}", pd.DistanceFromOrigin);

            Point_Distance pd2 = pd with { Y = 90 };

            WriteLine("pd2's distance is {0}", pd2.DistanceFromOrigin);

            Point_Distance2 pd22 = new Point_Distance2(12, 13);

            Point_Distance2 pd33 = pd22;


            WriteLine("pd22's distance is {0}", pd22.DistanceFromOrigin);

            WriteLine("pd23's distance is {0}", pd33.DistanceFromOrigin);

            // Point_3 can be mutated nondestructively:

            Point_3 p31 = new Point_3(2, 3);
            WriteLine(p31.DistanceFromOrigin);
            Point_3 p32 = p31 with { Y = 9 };  // with create new instance  the original instance keep same
            WriteLine(p31.DistanceFromOrigin);
            WriteLine(p32.DistanceFromOrigin);

            //Point_4 's distance will be not change when the Y will be changed;

            Point_4 p41 = new Point_4(2, 3);
            WriteLine(p41.DistanceFromOrigin);

            Point_4 p42 = p41 with { Y = 20 };

            WriteLine(p41.DistanceFromOrigin);
            WriteLine(p42.DistanceFromOrigin);


        }
    }

    // Patterns

    public class Patterns
    {
        public static void Test_Patterns()
        {
            Is_pattern("hell");
            Is_Var_pattern("jhon");
            Is_Constant_pattern(3);
            Is_Ralational_pattern(223);

            Is_Tuple_Pattern();

        }

        public static void Is_pattern<T>(T obj)
        {
            if(obj is string)
            {
                WriteLine( ((string)(object)obj).Length);
            }

            if(obj is string s)
            {
                WriteLine("More Concisely,{0}",s);
            }

            if(obj is string { Length: 4 })
            {
                WriteLine("A string with 4 characters");
            }
        }
        bool IsJanetOrJohn(string name) => name.ToUpper() is var upper && (upper == "JANET" || upper == "JOHN");

        public static void Is_Var_pattern(string name)
        {
            bool IsJanetOrJohn(string name) => name.ToUpper() is var upper && (upper == "JANET" || upper == "JHON");

            if (IsJanetOrJohn("JHON"))
            {
                WriteLine("Hi, I am JHON,");
            }
        }

        public static void Is_Constant_pattern<T>(T obj)
        {
            if (3.Equals(obj))
            {
                WriteLine("Hi ,I am 3 , {0}", obj);
            }
        }

        // < > <= and >=
        public static void Is_Ralational_pattern<T>(T obj)
        {
            if(obj is > 100)
            {
                WriteLine("obj is greater than 100 ,{0}", obj);
            }

            string GetWeightCategory(decimal bmi) => bmi switch
            {
                < 18.5m => "undeweight",
                < 25m => "normal",
                _ => "obese"
            };

            WriteLine(GetWeightCategory(19m));
      
        }
        enum Season { Spring, Summer, Fall, Winter };

        public static void Is_Pattern_Combinators<T>(T obj)
        {
            bool IsJaneOrJohn(string name) => name.ToUpper() is "JANET" or "JOHN";
            bool IsVowel(char c) => c is 'a' or 'e' or 'i' or 'o' or 'u';
            bool Between1And9(int n) => n is >= 1 and <= 9;
            bool IsLetter(char c) => c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';

            if(obj is not string)
            {
                WriteLine("obj is not string");
            }
            // Tuple and Positional Patterns
            var p = (2,3);
            WriteLine(p is (2, 3)); //True

            int AverageCelsiusTemperature(Season season, bool daytime) => (season, daytime) switch {
                (Season.Spring, true) => 20,
                (Season.Spring, false) => 16,
                (Season.Fall, true) => 18,
                (Season.Fall, false) => 18,
                _ => throw new Exception("Unexpected combination")
            };

           

        }
        record Point(int X,int Y);

        public static void Is_Tuple_Pattern()
        {
            var p = new Point(2, 2);
            WriteLine(p is (2, 2));
            WriteLine(p is (var x, var y) && x == y);

            WriteLine("should be Diagonal,{0}", Print(p));
            WriteLine("should be Empty point,{0}",Print(new Point(0, 0)));
        }

       static string Print(object obj) => obj switch
        {
            Point(0, 0) => "Empty point",
            Point(var x, var y) when x == y => "Diagonal",
            _ => "Default"
        };

        public static void Is_Property_Patterns<T>(T obj)
        {
            if(obj is string { Length: 4 })
            {
                WriteLine("I am length at 4");
            }
            if(obj is string s && s.Length == 4)
            {
                WriteLine("The same thing");
            }
            bool ShouldAllow(Uri uri) => uri switch
            {
                { Scheme: "http", Port: 80 } => true,
                { Scheme: "https", Port: 443 } => true,
                { Scheme: "ftp", Port: 21 } => true,
                { Scheme:{ Length:4},Port:80 }=>true,
                { Host: { Length: <1000} ,Port: >0} =>true,
                { Scheme:"http"}when string.IsNullOrWhiteSpace(uri.Query)=>true,
                { Scheme:"http",Port:81,Host:var host} => host.Length <1000, // Implicit typing is permitted, so you can substitute string with var,
                { Scheme: "http", Port:82 }=>uri.Host.Length <1000 ,
                { Scheme: "https", Port: 83, Host: {Length: < 2000 } } => true,
              
                { IsLoopback: true } => true,
                _ => false
            };

            bool ShouldAllowCombinePropertyPattern(object uri) => uri switch
            {
                Uri { Scheme: "http", Port: 80 } => true,
                Uri { Scheme: "https", Port: 443 } => true,
                Uri { Scheme:"https",Port:80} httpUri =>httpUri.Host.Length <1000,
                Uri { Scheme:"http",Port:81} httpUri when httpUri.Host.Length <1000 => true,

            };
          
        }


    }

   //Attributes
   // 
   // static void Main() => Foo("Main",@"c:\Program.cs",6);
   public class Attributes
    {
      public   static void Foo2([CallerMemberName]string memberName=null,
            [CallerFilePath]string filePath=null,
            [CallerLineNumber]int lineNumber =0
            )
        {
            WriteLine("MemberName is {0}", memberName);
            WriteLine("filepath is {0}", filePath);
            WriteLine("LineNumber  is {0}", lineNumber);

            INotifyPropertyChanged inc;
            PropertyChangedEventHandler pceh;
            PropertyChangedEventArgs pcea;
        }

    public class Foo: INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged = delegate { WriteLine("I catch some change in Property change"); };

            void RaisePropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                WriteLine("Can I get the CallerMemberName ? {0}", propertyName);
            }
               

            string customerName;
            public string CustomerName
            {
                get => customerName;
                set
                {
                    if (value == customerName) return;
                    customerName = value;
                    RaisePropertyChanged();
                    //The  compiler converts the above line to :
                    RaisePropertyChanged("CustomerName");
                }
            }

            public static void Test_Attribute()
            {
                Foo f1 = new Foo();
                f1.CustomerName = "CharlieDing";
            }

        }

      
    }

    // Dynamic Binding
    public class DynamicBinding {

       

       static object GetSomeObject() { return null; }

        public static void Test_Dynamic()
        {
            dynamic d = GetSomeObject(); // Tells the compiler to relax,
                                         // d.Quack(); // throw RuntimeBinderException

            dynamic d1 = 5;
           //  d1.Hello(); // int type has no Hello method, throw RuntimeBindeException



            dynamic d2 = new Duck();
            d2.Quack();
            d2.Waddle();

            int x = 3, y = 4;
            DynamicBinding db = new DynamicBinding();
            WriteLine("Can Mean method work normally ? {0}",db.Mean(x, y));

            // Runtime Representation of Dynamic
            // typeof(dynamic)== typeof(object) 
            bool test1 = typeof(dynamic[]) == typeof(object[]);
            bool test2 = typeof(List<dynamic>) == typeof(List<object>);

            dynamic x1 = "hello";
            WriteLine(x.GetType().Name);

            x1 = 123;
            WriteLine(x1.GetType().Name);

        }
        
        /**
         * At runtime, if a dynamic object implement IDynamicMetaObjectProvider, that interface is used to perform the binding.If not, bingding occurs in almost
         * the same way as it would have had the compiler know the dynamic object's runtime type.There two alternatives are called custom binding and 
         * language binding
         * 
         */
        //public class DynamicObject : IDynamicMetaObjectProvider
        public class Duck : DynamicObject
        {
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                WriteLine(binder.Name +" method was called");
                result = null;
                return true;
            }
        }
        // The benifit is obvious - you don't need to duplicate code for each numeric type.However, you lose static type safety,risking runtime exceptions
        // rather than compile-time errors
        dynamic Mean(dynamic x, dynamic y) => (x + y) / 2;

        
        public class Test1
        {
            public dynamic Foo;
        }

        public class Test2
        {
          //   [System.Runtime.CompilerServices.DynamicAttribute]
          
            public object Foo;
        }

        public static void DynamicConversions()
        {
            try {
            int i = 7;
            dynamic d = i;
            long j = d;

            short k = d;  // throws RuntimeBinderException , int is not implicitly convertible to a short

            WriteLine(d);
            // var says, "let the compiler figure out the type"
            // dynamic says, "let the runtime figure out the type"
            dynamic x = "hello"; // Static type is dynamic ,runtime type is string
            var y = "hello"; //Static type is string, runtime type is string;
            int m = x; // Runtime error (cannot convert string to int)
           //  int n = y;// Compile-time error(cannot convert string to int)

            var y1 = x; // Static type of y is dynamic
            int z = y1; // Runtime error(cannot convert string to int)
                        // Dynamic Expressions
            }catch(Exception e)
            {
                dynamic list = new List<int>();
                var result = list.Add(5);  // can not implicityly convert type 'void' to 'object'
            }
           
        }
    }
}
    
