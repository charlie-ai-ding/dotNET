using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Step_ONE.src.nutshell
{
    class CreatingTypes
    {
        public static void Methods()
        {
            // Expression-bodied methods
            int Foo(int x) { return x * 2; }
            int Foo1(int x) => x * 2;
            void Foo2(int x) => WriteLine(x);
            // overload methods
           //  void Foo(double x) { } // local methods cannot be overloaded.
        }
        void Foo(int x) { }
        void Foo(double x) { }
        void Foo(int x, float y) { }
        void Foo(float x, int y) { }
       // float Foo(int x) { }  return type is not the part of method signature.
       void Goo(int[] x) { }
       // void Goo(params int[] x) { } // params is not the part of method signature
       void Foo( ref int x) { }
        //  void Foo(out int x) { } // out and ref can be saw as same of method signature
        // void Foo(in int x) { }  // out in ref are both same things for method signature
    }
    class ForConstants
    {
        // private internal protected new
        public const string Message = "Hello World"; // never chagne whenever next time loading.
        static readonly DateTime StartupTime = DateTime.Now; // the class next time loading, this time can be changed.
            
    }
    public class Panda
    {
        string name;
        // Instance constructors public internal private protected, unsafe extern
        public Panda(string n)
        {
            name = n;
        }
        //overload instance constructors and can be writen as expression-bodied members
        public Panda(int n) => name =""+n;

    }
    public class Wine
    {
        public decimal Price;
        public int Year;
        public Wine(decimal price) => Price = price;
        //When on constructor calls another,the called constructor executes first
        public Wine(decimal price, int year) : this(price) => Year = year;
        public Wine(decimal price, DateTime year) : this(price, year.Year) { }

    }
    public class Player
    {
        // Field initialization occur before the constructor is executed.
        int shields = 50; // Initialized first
        int health = 100; // Initialized second

        public Player() { }

    }
    public class Class1
    {
        Class1() { } // Private constructor
        public static Class1 Create()
        {
            //Perform custom logic here to return an instance of Class1
            return new Class1();
        }
    }

    public class TestCreatingType
    {
        /*
         *Although properties are accessed in the same way as fields,they differ in that they give the implementer
         *complete control over getting and setting its value. (offer a middle layer )
         *
         *promote encapsulation
         */
        public static void Test_Property()
        {
            Stock msft = new Stock();
            msft.CurrentPrice = 30;
            msft.CurrentPrice -= 5;
            WriteLine(msft.CurrentPrice);

            // These init-only properties act like read-only properties,except that they
            // can also be set via an object initializer.
            var note = new Note { Pitch = 50 };

            // 
            // note.Pitch = 100;

        }
        
        public static void Test_Indexers()
        {
            string s = "hello";
            WriteLine(s[2]);
            string s1 = null;
            WriteLine(s1?[0]); //Writes nothing; no error;

            Sentence sen = new Sentence();
            WriteLine(sen[2]);
            sen[3] = "Charlie Ding is real good";
            WriteLine(sen.getAllwords());
            WriteLine(sen[^2]);  // the class
            WriteLine(sen[..2]);

        }
   
        // Instantiating the type and Accessing a static member in the type
        public static void Test_Static_Constructor()
        {
            // StaticConstructors sc = new StaticConstructors();

            StaticConstructors.Test_Trigger_StaticConstructors();

            WriteLine("Should be 0 : {0}",StaticConstructorAndStaticFieldInitialization.X);

            FooStaticField f= FooStaticField.Instance;  // output 0
            WriteLine(FooStaticField.X); // output 3

        }
    
        public static void Test_Inheritance_Polymorphism()
        {
            Animal animal = new Animal("comment");
            Dog dog = new Dog("wangwang","dog1");
            if(dog is Animal) { WriteLine("dog is a Dog and it is animal"); }

            dog.Test_Base();

            Overrider over = new Overrider();
            BaseClass b1 = over;
            over.Foo();
            b1.Foo();

            /**
             *  Occasionally, you want to hide a member deliberately, in which case you can apply the new modifier to the member in the
             *  subclass.The new modifier does nothing more than suppress the compiler warning that would otherwise result
             */
            Hider h = new Hider();
            BaseClass b2 = h;
            h.Foo();
            b2.Foo();

            A a = new A();
            WriteLine("In A ,Counter is 1, {0}",a.Counter);
            B b = new B();
            A aa = b;
            WriteLine("In B ,Counter is 2(no effecive by parents' Counter, because use new modifier),{0}", b.Counter);
            WriteLine("In A aa  ,Counter is 1(no effecive by A's subclass B where Counter=2, because use new modifier),{0}", aa.Counter);

            C c = new C();
            c.Test_Sealed();

            // Subclass must hence "redefine" any constructors it wants to expose.In doing so,however,it can call any of the base class's
            // constructors via the base keyworld
            //  Subclass s = new Subclass(123);
            Subclass sc = new Subclass(10);
            sc.Test_Success();

            //Object
            Stack st = new Stack();
            st.Push(1);
            st.Push("string");
            st.Push(null);
            st.Push(DateTime.Now);
            
        }
    
        public static void BoxingAndUnBoxing()
        {
            int x = 9;
            object obj = x; //Box the int
            int y = (int) obj; // Unbox the int

            object obj1 = 9;  // 9 is inferred to be of type int
            // long x1 = (long)obj1; // InvalidCastException
            int y1 = (int)obj1;

            object obj2 = 3.5;// 3.5 is inferred to be of type double
            int y2 = (int)(double)obj2; // double performs an unboxing and int performs a numeric conversion

            object[] a1 = new string[4];
            // object[] a2 = new int[10];  

            // Boxing copies the value-type instance into the new object, and unboxing copies the contents of the objects back 
            //into a value-type instance.
            int i = 3;
            object boxed = i; // 
            i = 5;
            WriteLine(boxed); // 3
            
        }
  
        /**
         * Runtime type checking is possible beacause each object on the heap internally stores a little type token
         * You can retrieve thit token by calling the GetType method of object;
         */
        public static void StaticAndRuntimeTypeChecking()
        {
           // int x = "5"; // static type checking
            object y = "5";
            int z = (int)y; //Runtime error, downcast failed
        }
        /**
         * Call GetType one the instance  // runtime
         * Use the typeof operator on a type name  // compiler time
         */
        public static void Test_GetType()
        {
            Point p = new Point();
            WriteLine(p.GetType().Name); // Point
            WriteLine(typeof(Point).Name); // Point
            WriteLine(p.GetType() == typeof(Point));// True
        }

        public static void Test_ToString()
        {
            int i = 1;
            WriteLine(i.ToString());
            Panda2 p2 = new Panda2("panpan");
            WriteLine(p2); // 
        }
    }
    public class Panda2
    {
        public string name;
        public Panda2(string name) { this.name = name; }
        public override string ToString()
        {
            return name;
        }
    }
    public class Object
    {
        public Object() { }
        public extern Type GetType();
        public virtual bool Equals(object ojb) { return true; }
        public static bool Equals(object objA,object ojbB) { return false; }
        public static bool ReferenceEquals(object objA,object objB) { return true; }
        public virtual int GetHashCode() { return 1; }
        public virtual string ToString() { return ""; }
        protected virtual void Finalize() { }
        protected extern object MemberwiseClone();


    }
  
    public class Point { }
    public class Stock
    {
        decimal currentPrice; // The private "backing" field
        decimal sharesOwned;
        // The most common implementation for a property is a getter and/or setter that simply reads and writes 
        // to a private field of the same type as the property.
        public decimal CurrentPrice_Automatic { get; set; }
        public decimal CurrentPrice // The public property
        {
            get { return currentPrice; }
            set {
                if (value < 0) throw new Exception("negative is not available!");
                currentPrice = value; 
            }
        }
        // read-only  and calculated properties
        public decimal Worth
        {
            get { return currentPrice * sharesOwned; }
        }
        // Expression-bodied properties
        public decimal Worth2 => currentPrice * sharesOwned;

        public decimal Worth3
        {
            get => currentPrice * sharesOwned;
            set => sharesOwned = value / currentPrice;
        }

        // this gives CurrentPrice an initial value of 123;
        public decimal CurrentPrice_Initializer { get; set; } = 123;
        // this give CurrentPrice an initial value of 123 and it is read-only 
        // initial and set are totally different process.
        public decimal CurrentPrice_Initializer_CanReadOnly { get;} = 123;
    }

    public class Foo
    {
        private decimal x;
        public decimal X
        {
            get { return x; }
            private set { x = Math.Round(value, 2); }
        }
    }

    public class Note
    {
        public int Pitch { get; init; } = 20;
    }

    public class Note2
    {
        public int Pitch { get; }
        public int Duration { get; }
        readonly int _pitch;

        public int Pitch2 { get => _pitch; init => _pitch = value; }

        public Note2(int pitch =20, int duration = 100)
        {
            Pitch = pitch;Duration = duration;
           
        }
    }

    public class Sentence
    {
        string[] words = "The quick brown fox".Split();

        public string this[int wordNum]
        {
            get { return words[wordNum]; }
            set { words[wordNum] = value; }
        }
        // You can support indices and ranges by defining and indexer with a parameter type of Index and Range
        public string this[Index index] => words[index];
        public string[] this[Range range] => words[range];

           

       public string getAllwords()
        {
            string rs = "";
            foreach(var s in words)
            {
                rs = rs +" "+ s;
            }
            return rs;
        }
    }
    
    /**
     * A static constructor executes onece per type rather than onece per instance
     * It must be parameterless 
     */
    public class StaticConstructors
    {
        // the only modifiers allowed by static constructors are unsafe and extern.
        static StaticConstructors() { WriteLine("Type Initialized"); }

        public static void Test_Trigger_StaticConstructors()
        {
            WriteLine("I am a method for testing  an Trigger for a static constructor");
        }
    }

    /**
     *  1:Static field initializers run just before the static constructor is called.
     * */
    public class StaticConstructorAndStaticFieldInitialization {
        /**
         *  1: firstly declare and static field initializer by default
         *  2: assignment value to variable
         */
        public static int X = Y;
        public static int Y = 100;

         static StaticConstructorAndStaticFieldInitialization() { }
    }

    //assignment value to variable  which happens after the static field and static constructor initializers
    public class FooStaticField
    {
        public static FooStaticField Instance = new FooStaticField(); //step
        public static int X = 3; 

        FooStaticField() => WriteLine(X); //0

    }

    // be composed solely of static members and can not be subclassed
    public static class StaticClass
    {
        static void method1() { }
        static void method2() { }
        static void method3() { }

    }

    // class Test : StaticClass { }

    public class FinalizersClass
    {
        // the syntax for a finalizer is the name of the class prefixed with the ~ symbol
        // Unmanaged code modifier unsafe
        ~FinalizersClass()
        {
            WriteLine("I will be called before the garbage collector reclaims the memory for an unreference object");
        }
     //   ~FinalizersClass() => WriteLine("Finalizing");
    }

    /**
     * A partial method consist of two parts: a definition and an implementation
     */
    public class PartialClassAndMethods
    {
        // In auto-generated file   - a definition
        partial class PaymentForm {
            //...
            partial void ValidatePayment(decimal amount);
        } 
         
        //In hand-authored file  - an implementation
        // if an implementation is not provided,the definition of the partial method is compiled away(as is the code
        // that calls it),This allows autogenerated code to be liberal in providing hooks without having to worry about bloat;
        partial class PaymentForm
        {
            partial void ValidatePayment(decimal amount)
            {
                if(amount > 100)
                {

                }
            }
        }

    }

    public class Animal
    {
        public string name;
        public Animal(string name) { this.name = name; }
    }
    public class Dog : Animal {
        public string sound;
        public Dog(string sound,string name) : base(name)
        {
            this.sound = sound;
        }

        //base like super in java 
        public void  Test_Base()
        {
            WriteLine("This is verfiy can referenc the parent by using base keyword: shoud be dog1: {0}",base.name);
        }
    }
    public class Cat : Animal {
        public string sound;
        public Cat(string sound,string name) : base(name)
        {
            this.sound = sound;
        }
    }

    public class BaseClass
    {

        public virtual void Foo() { WriteLine("BaseClass.Foo"); }
    }
    public class Overrider : BaseClass
    {
        public override void Foo() { WriteLine("Overrider.Foo"); }
    }
    public class Hider : BaseClass
    {
        public new void Foo() { WriteLine("Hider.Foo"); }
    }

    public class A { 
        public int Counter = 1;
        public virtual void Test_Sealed() { WriteLine("This is used to test Sealed effect"); }
    }
    public class B : A { public new int Counter =2;
        public sealed override void Test_Sealed() { WriteLine("This is used to test Sealed effect"); }
    }
    public class C : B
    {
        //Although you can seal a function member against overriding, you can't seal a memeber against being hidden.
        public new void Test_Sealed() { WriteLine("I am in C , I have the same name of method which are sealed in my parent's context"); }

      //  public override void Test_Sealed() { }
    }
    
    // Constructors and Inheritance
    public class Baseclass
    {
        public int X;
        public Baseclass() { X = 1; }
        public Baseclass(int x) { this.X = x; }
    }
    // the base keyword works rather like the this keyword except that it calls a constructor in the base class
    public class Subclass : Baseclass 
    {
        public Subclass(int x) : base(x) { WriteLine("I am in Subclass constructor,"); }

        public void Test_Success() { WriteLine("X should have value 10: {0}", X); }
    }
    // Implicit calling of the parameterless base-class constructor
    public class Subclass2 : Baseclass
    {
        // If a constructor in a subclass omits the base keyword,the base type's parameterless constructor is implicitly called.
        public Subclass2() { WriteLine(X); }
    }

    //Constructor and field initialization order

    public class BB
    {
        int x = 1;    //Executes st
        public BB(int x)   
        {
            this.x = x; //Executes 4st
        }
    }

    public class DD : BB
    {
        int y = 1;  //Executes 1st
        public DD(int x) : base(x+1)  //Executes 2st
        {
            this.y = this.y + x;  //Executes 5st
        }
    }

    //The object Type

    public class Stack
    {
        int position;
        object[] data = new object[10];
        public void Push(object obj) { data[position++] = obj; }
        public object Pop() { return data[position--]; }
    }

}
