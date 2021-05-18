using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Step_ONE.src.nutshell.Generics
{
    class Generics
    {
        public static void Test_Stack()
        {
            var stack = new Stack<int>();  // closed type
            stack.Push(1);
            stack.Push(2);
            int x = stack.Pop(); //2
            int y = stack.Pop();//1

           // var stack1 = new Stack<T>();

        }
        public static void Test_Generic_Method()
        {
            int a = 10;
            int b = 100;
            WriteLine("before swap a is {0}, b is {1}", a, b);
            GenericMethods.Swap(ref a, ref b);
            WriteLine("after swap a is {0}, b is {1}", a, b);

            Dictionary<int, string> dic = new Dictionary<int, string>();
        }
    }
    /*
     *  Methods and types are the only constructs that can introduce type parameters.
     *  Properties, indexers, events,fields, constructors,operators and so on cannot declare type parameters,
     *  although they can partake in any type parameters already declared by their enclosing type.
     */
    //Open type
    public class Stack<T>
    {
        int position;
        T[] data = new T[100];
        public void Push(T obj) => data[position++] = obj;
        public T Pop() => data[position--];

        // this is not declare type parameters, this is 
        public T this[int index] =>data[index];

        public Stack<T> Clone()
        {
            Stack<T> clone = new Stack<T>(); //Legal 
            return clone;
        }
    }
    public class GenericMethods
    {
      public  static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }

    // 1: Declaring Type Parameters  
    // can be introduced in the declaration of class, struct, interface,delegate and methods,other
    // constructs as properties, cannot introduce a type parameter,but they can use one.
    public struct Nullable<T>
    {
        public T Value { get; }
    }
    class Dictionary<TKey, TValue> { }
    class A { }
    class A<T> { }
    class A<T1, T2> { }
    // typeof and Unbound Generic Types
    public class UnboundGenericTypes
    {
        public static void Test_typeof()
        {
            Type a1 = typeof(A<>);
            Type a2 = typeof(A<,>);
            Type a3 = typeof(A<int, int>);


            WriteLine("What is the type of a3? {0}", a3);
        }
    
        public static void Zap<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = default(T);
                array[i] = default; //  the compiler can infer it by the reasonable context
            }
        }

    }
    //Generic Constraints
    /**
     * where T:base-class // Base-class constraint  is subclass
     * where T:interface  // Interface constraint    must implement the interface
     * where T:class      // Reference-type constraint
     * where T:class?     // See Nullable reference types
     * where T:struct  // value-type constraint (excludes Nullable types)
     * where T:unmanaged  // unmanage constraint
     * where T:new () // Parameterless cosntructor constraint
     * where U:T      // Naked type constraint
     * where T:notnull // a non-nullable reference type
     */

    class SomeClass { }
    interface Interface1 { }

    class GenericClass<T, U> where T : SomeClass, Interface1 where U : new() {

        public static void Initialize<T>(T[] array) where T:new(){
            for(int i=0;i< array.Length; i++)
            {
                array[i] = new T(); // here can be used ,becasue the constraint in method type parameter declare with where T:new()
            }
        }
    }

    public interface IComparable<T>
    {
        int CompareTo(T other);
    }

    class SomeOneCanComparable
    {

        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
    // naked type constraint requires one type parameter to derive from (or match)another type parameter.
    class Stack2<T>
    {
        Stack<U> FilteredStack<U>() where U : T { return null; }
    }
    //Subclassing Generice Types
    class Stack3<T> { }
    class SpecialStack<T> : Stack3<T> { }
    class IntStack : Stack3<int> { }
    class List<T> { }
    class KeyedList<T, Tkey> : List<T> { }  // a subtype can also introduce fresh type arguments
    class KeyedList2<TElement, Tkey> : List<TElement> { }

    //Self-Referencing Generic Declarations 
    public interface IEquatable<T> { bool Equals(T obj); }

    public class Ballon : IEquatable<Ballon>
    {
        public string Color { get; set; }
        public int CC { get; set; }
        public bool Equals(Ballon b)
        {
            if (b == null) return false;
            return b.Color == Color && b.CC == CC;
        }
    }

    public class Bob<T> { public static int Count; }
    //Static data is unique for each closed type:
    public class TestStaticData
    {
        public static void Test_Static_Data()
        {
            WriteLine(++Bob<int>.Count);
            WriteLine(++Bob<int>.Count);

            WriteLine(++Bob<string>.Count);
            WriteLine(++Bob<object>.Count);

        }
    }

    // Type Parameters and Conversions:

    public class TypeParmeterAndConversions
    {
        StringBuilder Foo<T>(T arg)
        {
            //if(arg is StringBuilder)
            //{
            //   // return (StringBuilder)arg; arg is 
            //}

            //StringBuilder sb = arg as StringBuilder;
            //if (sb != null) return sb;
            //else return new StringBuilder();

            return (StringBuilder)(object)arg;
        }
        int Foo2<T>(T x) => (int)(object)x;
    }

    // Variance is not automatic

    public  class Animal { }
    public class Bear : Animal { }
    public class Camel : Animal { }

    public class VarianceIsNotAutomatic
    {
        public static void Test_Variance_Is_Not_Automatice()
        {
            Stack<Bear> bears = new Stack<Bear>();
            // Stack<Animal> animals = bears;
            //  animals.Push(new Camel()); Trying to add Camel to bears

            // ZooCleaner.Wash(bears);

            ZooCleaner.Wash2(bears);

            //Arrays types support covariance;
            Bear[] arraybears = new Bear[3];
            Animal[] arrayAnimal = arraybears;
            // Array covariant type parameter are unsafe ,for example:
            // Attempted to  access an element as a type incompatible with the array.
            arrayAnimal[0] = new Camel(); // in compiler time, Array support covariance, so it is ok,but at runtime, error

        }

        
    }

    public class ZooCleaner
    {
       public  static void Wash(Stack<Animal> animals) { }

        public static void Wash2<T>(Stack<T> animals) where T : Animal { }
    }

    //Declaring a covariant type parameter

    public interface IPoppable<out T> { T Pop(); }
    public interface IPushable<in T> { void Push(T obj); }
    /**
     * Our Stack<T> class can implement both IPushable<T> and IPoppable<T> -despite T having opposing variance annotations in the two
     * interfaces! This works because you must exercise variance through the interface and not the class;therefore, you must commit to
     * the lens of either IPoppable or IPushable before performing a variant conversion.This lens then restricts you to the operations
     * that are legal under the appropriate variance rules. 
     * This also illustrates why classes do not allow variant type parameters:concrete implementations typically require data to flow 
     * int both directions
     * 
     */
   public class Stack_<T> :IPoppable<T>, IPushable<T>
    {
        static int count = 100;
        static T[] data = new T[count];
        int index=0 ;
        

        public T Pop() { return data[--index]; }

        public void Push(T t) { data[index ++] = t; }

    }

    // Because the interface has a contravariant T, we can use an IComparer<object> to compare two strings.

    public interface IComparer<in T> { int Compare(T a, T b); }

    public class Comparer<T> : IComparer<T>
    {
        int IComparer<T>.Compare(T a, T b)
        {
            return 1;
        }
    }

    /**
     * Max needs to be compiled once and work for all possible values of T, Compilation cannot succeed because there is no single
     * meaning for > across all values of T- in fact, not every T even has a > operator.
     */
    public class DeclareingCovariantTypeParameter
    {

        static T Max<T> (T a, T b) where T : IComparable<T> => a.CompareTo(b)>0?a:b;

     //   static T Max2<T>(T a, T b) => (a > b ? a : b); //  not work,Compile error

        // the following code shows the same Max method written with C++ template.
        //template <class T> T Max(T a,T b)
        //{
        //    return a>b? a:b
        //}


        public static void Test_DeclareingCovariantType()
        {
            var bears = new Stack_<Bear>();
            bears.Push(new Bear());
            bears.Push(new Bear());
            bears.Push(new Bear());
            
            //pushing a Camel onto the stack-can't occur,because there's no way to feed a Camel into an interface where T can 
            // appear only in the output positions which is 
            // bears.Push(new Camel()); 

            IPoppable<Animal> animals = bears; //Legal
            Animal a = animals.Pop();

            WriteLine("We should know the a is Bear , {0}", a.GetType());

            // Test Contravariance
            IPushable<Animal> animals2 = new Stack_<Animal>();
            IPushable<Bear> bears2 = animals2;

            bears2.Push(new Bear());

            var objectComparer = new Comparer<object>();
            IComparer<string> stringComparer = objectComparer;
            int result = stringComparer.Compare("Brett", "Jema");

        }
    }


}
