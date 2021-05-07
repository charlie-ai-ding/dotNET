using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using static System.Console;

namespace Step_ONE.src.step_one
{
    class Introduction
    {
        public static void BoxingAndUnboxing()
        {
            int i = 123;
            object o = i;  //Boxing
            int j = (int)o; //Unboxing
        }
        public static void statement_demo(string[] args)
        {
            int n = args.Length;
            switch (n)
            {
                case 0:
                    WriteLine("No arguments");
                    break;
                default:
                    WriteLine("{0} arguments", n);
                    break;

            }
            foreach (string s in args)
            {
                WriteLine(s);
            }
        }
        public static void goto_statement(string[] args)
        {
            int i = 0;
            goto check;
        loop:
            WriteLine(args[i++]);
        check:
            if (i < args.Length) goto loop;
        }
        /**
         * You use a yield return statement to return each element one at a time.

The sequence returned from an iterator method can be consumed by using a foreach statement or LINQ query.
        Each iteration of the foreach loop calls the iterator method. 
        When a yield return statement is reached in the iterator method, expression is returned, 
        and the current location in code is retained. Execution is restarted from that location the next time 
        that the iterator function is called.
You can use a yield break statement to end the iteration.
         */
        static IEnumerable<int> Range(int from, int end)
        {
            for (int i = from; i <= end; i++)
            {
                yield return i;
            }
            yield break;
        }
        public static void test_range()
        {
            foreach (int x in Range(1, 20))
            {
                WriteLine(x);
            }
        }
        public static void checked_and_unchecked()
        {
            int i = int.MaxValue;
            checked
            {
                WriteLine(i + 1);
            }
            unchecked
            {
                WriteLine(i + 1);
            }
        }

        public static void test_class_type()
        {
            Pair<int, string> pair = new Pair<int, string> { First = 1, Second = "charlie" };

            WriteLine(pair.First);
            WriteLine(pair.Second);
        }

        public static void test_class_base()
        {
            Point3D p3d = new Point3D(1, 2, 3);
            WriteLine($"point3D's x is {p3d.x} and y is {p3d.y}");
        }

        static void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }


        public static void test_ref_param()
        {
            int i = 1, j = 10;
            WriteLine("before swap {0},{1}", i, j);
            Swap(ref i, ref j);
            WriteLine("after swap {0},{1}", i, j);
        }
        static void Divide(int x, int y, out int result, out int remainder)
        {
            result = x / y;
            remainder = x % y;
        }

        public static void test_Divide_param()
        {
            int res, rem;
            Divide(10, 3, out res, out rem);
            WriteLine("{0}{1}", res, rem);
        }

        public static void test_static_instance_method_in_class()
        {
            Entity.SetNextSerialNo(1000);
            Entity e1 = new Entity();
            Entity e2 = new Entity();

            WriteLine(e1.GetSerialNo());
            WriteLine(e2.GetSerialNo());
            WriteLine(Entity.GetNextSerialNo());
            WriteLine(Entity.GetNextSerialNo());

        }
        // x *(y+2);
        public static void test_abstract_method()
        {
            Expression e = new Operation(new VariableReference("x"), '*', new Operation(new VariableReference("y"), '+', new Constant(2)));

            Hashtable vars = new Hashtable();
            vars["x"] = 3;
            vars["y"] = 5;
            WriteLine("x*(y+2) should equal 21");
            WriteLine(e.Evaluate(vars));


        }

        public static void test_method_overload()
        {
            MethodOverload.F<int>(1);
        }
    
        public static void test_list()
        {
            WriteLine("test list");
            // first one is calling the static constrators
            List<string> list1 = new List<string>();
            //The new keyword will  triggle the instance constracors which has params or not 
            List<string> list2 = new List<string>(10);

            list1.Capacity = 100;    // Invokes set accessor
            int i = list1.Count;     // Invokes get accessor
            int j = list1.Capacity;  // Invokes get accessor

            list1.Add("Charlie");
            list1.Add("Daniel");
            for(int k = 0; k < list1.Count; k++) // Invokes Count Properties get accessor
            {
                string s = list1[k];    // Invoke List  indexers get accessor
                list1[k] = s.ToUpper(); // Invokes list indexers set accessor
            }


        }
        //test for event in List
        
        static int changeCount_for_Link=10;

        static void ListChange(object sender,EventArgs e) {
            changeCount_for_Link++;
        }
        public static void test_event_in_list()
        {
            List<string> names = new List<string>(10);
            names.Changed += new EventHandler(ListChange); // compiler support 
            names.Add("Liz");
            names.Add("Liz");
            names.Add("Liz");
            names.Add("Liz");

            WriteLine($"changeCount is {changeCount_for_Link}");

        }
      
        public static void test_Operators()
        {
            List<int> a = new List<int>(1);
            a.Add(1);
            a.Add(2);

            List<int> b = new List<int>(1);
            b.Add(1);
            b.Add(2);

            WriteLine(a == b);
            b.Add(3);
            WriteLine(a == b);
        }
        
        /**
         * Struct constructors are invoked with the new operator,
         */
        public static void test_class_struct_demo()
        {
            Point[] p_instances = new Point[100];
            Point_Struct[] p_structs = new Point_Struct[100];

            for(int i = 0; i < 100; i++)
            {
                p_instances[i] = new Point(i, i + 10);
                p_structs[i] = new Point_Struct(i, i + 10);
            }

            foreach(Point p_i in p_instances)
            {
              //  WriteLine("Point's hashcode is: {0}",p_i);
            }
            WriteLine("---------->>>>>>>>>>>>");
            foreach (Point_Struct p_i in p_structs)
            {
               // WriteLine("Point_Struct's hashcode is: {0}", p_i.x);
            }

            Point a = new Point(10, 10);
            Point b = a;
            a.x = 20;
            WriteLine("a and b are class references which pointed the same address in heap, so b.x shoud be 20: {0}",b.x);

            Point_Struct a1 = new Point_Struct(10, 10);
            Point_Struct b1 = a1;
            a1.x = 20;
            WriteLine("a1 and b1 are a copy of struct value,but the value of a1 has been altered , but b.x do not change,so shoud be 10: {0}", b1.x);

        }
  
        public static void test_array_demo()
        {
            int[] a1 = new int[10];    // one dimension
            int[,] a2 = new int[10, 5];  // two dimensions
            int[,,] a3 = new int[10, 5, 2];  // three dimensions

            // the element in array can be array
            int[][] a = new int[3][];
            a[0] = new int[10];
            a[1] = new int[2];
            a[2] = new int[111];
            a[3] = new int[20];
            // a[3][0] = 1; // Index was outside the bounds of the array.
            // WriteLine(a[3][0]);

        }
   
        /**
         * 
         */
        public static void test_interface_demo()
        {
            EditBox editBox = new EditBox();
            IControl control = (IControl)editBox;
            IDataBound dataBound = (IDataBound)editBox;

            editBox.Paint();
            control.Paint();
            dataBound.Bind(null);


        }
        
        public static void test_enum_demo()
        {
            Color c = Color.Red;
            ColorTest.PrintColor(c);
            ColorTest.PrintColor(Color.Red);

            WriteLine((int)Color.Red);
            WriteLine((int)Color.Green);
            WriteLine((int)Color.Blue);




        }
    

        /**
         * An instance of the Function delegate type can reference any method that takes a double argument and returns a double value.
         * A delegate can reference either a static method or an instance method,
         * 
         * Delegate does not know or care about the class of the method it reference;all that matters is that the referenced method
         * has the same parameters and return types as the delegate,  the input and output , look the funciton as a black box or pure funciton.
         */
        public static void test_delegate_demo()
        {
            double[] a = {0.0,0.1,0.5,1.5};
            double[] squares = DelegateTest.Apply(a, DelegateTest.Square);
            double[] sines = DelegateTest.Apply(a, Math.Sin);
            Multiplier m = new Multiplier(2.0);
            double[] doubles = DelegateTest.Apply(a, m.Multiply);

            WriteLine($"squares:{squares.Length}/n sines:{sines.Length} doubles:{doubles.Length}");

            double[] demos = DelegateTest.Apply(a, (double x) => x * 100);
            foreach(var i in demos)
            {
                WriteLine(i);
            }
        }
    
        /**
         * 
         * 
         * 
         */
        public static void test_attribute_demo()
        {
            AttributeTest.ShowHelp(typeof(Widget));
        }
    }

    public class Pair<TFirst, TSecond>
    {
        public TFirst First;
        public TSecond Second;
    }

    public class Point
    {
        public int x, y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point(int x)
        {
            this.y = 100;
            this.x = x;
        }
    }

    /**
     * struct are value types and do not require heap allocation
     * 
     */
    public struct Point_Struct
    {
        public int x, y;
        public Point_Struct(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Point_Struct(int x)
        {
            this.y = 100;
            this.x = x;
        }
    }


    public class Point3D : Point
    {
        public int z;
        public Point3D(int x, int y, int z) : base(x)
        {
            this.z = z;
        }
    }

    /**
     * one class has only one class copy, and the static method fully follow the one class one copy.
     * however the instance of the class which was called the object instance of the class has allocated at heap
     */
    public class Entity
    {
        static int nextSerialNo;
        int serialNo;

        public Entity()
        {
            serialNo = nextSerialNo++;
        }

        public int GetSerialNo()
        {
            WriteLine($"at this time nextSerialNo is:{nextSerialNo}");
            return serialNo;
        }
        public static int GetNextSerialNo()
        {
            return nextSerialNo;
        }
        public static void SetNextSerialNo(int value)
        {
            nextSerialNo = value;
        }
    }

    // this is for demo virtual abstract methods:

    public abstract class Expression
    {
        public abstract double Evaluate(Hashtable vars);

    }

    public class Constant : Expression
    {
        double value;
        public Constant(double value)
        {
            this.value = value;
        }
        public override double Evaluate(Hashtable vars)
        {
            return value;
        }
    }

    public class VariableReference : Expression
    {
        string name;
        public VariableReference(string name)
        {
            this.name = name;
        }
        public override double Evaluate(Hashtable vars)
        {
            object value = vars[name];
            if (value == null)
            {
                throw new Exception("Unknown variable:" + name);
            }
            return Convert.ToDouble(value);
        }
    }

    public class Operation : Expression
    {
        Expression left;
        Expression right;
        char op;

        public Operation(Expression left, char op, Expression right)
        {
            this.left = left;
            this.right = right;
            this.op = op;
        }

        public override double Evaluate(Hashtable vars)
        {
            double x = left.Evaluate(vars);
            double y = right.Evaluate(vars);
            switch (op)
            {
                case '+':
                    return x + y;

                case '-':
                    return x - y;

                case '*':
                    return x * y;

                case '/':
                    return x / y;

            }
            throw new Exception("Unknown operator");
        }
    }
    class MethodOverload
    {
        static void F()
        {
            WriteLine("F()");
        }
        static void F(object x)
        {
            WriteLine("F(object)");
        }
        static void F(int x)
        {
            WriteLine("F(int)");
        }
        static void F(double x)
        {
            WriteLine("F(double)");
        }
        public static void F<T>(T x)
        {
            WriteLine("F<T>(T)");
        }
        static void F(double x, double y)
        {
            WriteLine("F(double,double)");
        }

   
    }

    // function members of a class, constructors, properties, indexers, events,operators,and destructors
    public class List<T>
    {
        //constant..
        const int defaultCapacity = 4;

        //Fields...  need locations in memory
        T[] items;
        int count;

        //Constructors...

        static List()
        {
            WriteLine($"This is called when the class itself when it is first loaded");
        }

        public List()
        {
            WriteLine("I am in list construactor with zore param");
        }

        public List(int capacity = defaultCapacity)
        {
            items = new T[capacity];
            WriteLine("I am in list construactor with one param");
        }

        // Properties...  do not need locations in memory. this is just the functions which can get or set value relative with the class or instance
        public int Count { get { return count; } }

        public int Capacity
        {
            get
            {
                return items.Length;
            }
            set
            {
                if (value < count) value = count;
                if (value != items.Length)
                {
                    T[] newItems = new T[value];
                    Array.Copy(items, newItems, count);
                    items = newItems;
                }
            }
        }

        //Indexer... like the Properties, do not need locations in memory... Some syntaxs this[] get and set style is array style.
        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
                onChanged();
            }
        }

        //Methods...
        public void Add(T item)
        {
            if (count == Capacity) Capacity = count * 2; // will trigger the Capacity's set method,
            items[count] = item;
            count++;
            onChanged();
        }
      // here, invoking the delegate represented by the event
      // virtual funciton like pointer function and can be used as hook functions.
        protected virtual void onChanged()
        {
            if (Changed != null) Changed(this, EventArgs.Empty);
        }

        public override bool Equals(object other)
        {
            return Equals(this, other as List<T>);
        }

        static bool Equals(List<T> a, List<T> b)
        {
            if (a is null) return b is null;
            if (b is null || a.count != b.count) return false;
            for (int i = 0; i < a.count; i++)
            {
                if (!object.Equals(a.items[i], b.items[i]))
                {
                    return false;
                }
            }
            return true;
        }


        /**
         * 0: class consit of state and acitons, the state can be chance for sure, so the class should have a ability to provide the 
         * notifications to reflect the state's changes.
         * 1: how to implement?
         *   1.1: event member which is delegate type and using event keyword.
         *   1.2: the event should be raised by another virtual method
         *   summary: the notion of raising an event is precisely equivalent to invoking the delegate represented by the event
         *   thus,there are no special language constructs for raising events
         * 2: how to use it on the side of clients.
         *   Clients react to events through event handlers, Event handlers are attached using the += operator
         *   and removed using -= operator.
         */

        //Event...
        public event EventHandler Changed;


        //Operators...
        public static bool operator ==(List<T> a, List<T> b)
        {
            return Equals(a, b);
        }
        public static bool operator !=(List<T> a, List<T> b)
        {
            return !Equals(a, b);
        }

    }

    // Interfaces : can contain methods,properties,events,and indexers.

    interface IControl
    {
        void Paint();
    }
    interface ITextBox : IControl
    {
        void SetText(string text);
    }
    interface IListBox : IControl
    {
        void SetItems(string[] items);
    }
    interface IComboBox : ITextBox, IListBox
    {

    }
    public class Binder { }

    interface IDataBound
    {
        void Bind(Binder b);
    }

    public class EditBox:IControl, IDataBound
    {
        public void Paint() { WriteLine("EditBox Paint Method"); }
        public void Bind() { WriteLine("EditBox Bind Method"); }
        void IControl.Paint() { WriteLine("EditBox implement IControl's Method"); }
        void IDataBound.Bind(Binder b) { WriteLine("EditBox implement IDataBound's Method"); }
    }

    // Enum

    public enum Color
    {
        Red,
        Green,
        Blue
    }
    public class ColorTest
    {
        public static void PrintColor(Color color)
        {
            switch (color)
            {
                case Color.Red:
                    WriteLine("Red");
                    break;
                case Color.Green:
                    WriteLine("Green");
                    break;
                case Color.Blue:
                    WriteLine("Blue");
                    break;
                default:
                    WriteLine("Unknown color");
                    break;
            }
        }
    }


    //Delegatas  object-oriented and type-safe function pointers
   public delegate double Function(double x);


    public class Multiplier
    {
        double factor;
        public Multiplier(double factor)
        {
            this.factor = factor;
        }
        public double Multiply(double x)
        {
            return x * factor;
        }
        public void testThis()
        {
            WriteLine("this is test for this work or not");
        }
    }

    public class DelegateTest
    {
       public  static double Square(double x)
        {
            return x * x;
        }

        public static double[] Apply(double[]a, Function f)
        {
            double[] result = new double[a.Length];
            for(int i = 0; i < a.Length; i++)
            {
                result[i] = f(a[i]);
            }
            
            return result;
        }

       
    }

    // Attributes why Types,members,and other entities in a C# program support modifiers that control certain aspects of their behavior.

    /**
     * Think about the view of the compiler's point, and follow the convention-over-configuration rules
     * https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/february/patterns-in-practice-convention-over-configuration
     * 
     */
    public class HelpMeAttribute : Attribute
    {
        string url;
        string topic;
        public HelpMeAttribute(string url,string topic)
        {
            this.url = url;
            this.topic = topic;
        }
        public string Url { get { return url; } }
        public string Topic
        {
            get { return topic; }
            set { topic = value; }
        }
    }

    [HelpMe(url:"http://www.google.com",topic:"General")]
    public class Widget
    {
        [HelpMe(url:"https://zetcode.com/lang/csharp/delegates/",topic:"General")]
        public void Display(string text) { }
    }

    public class AttributeTest
    {
        public static void ShowHelp(MemberInfo member)
        {
            HelpMeAttribute a = Attribute.GetCustomAttribute(member, typeof(HelpMeAttribute)) as HelpMeAttribute;

            if(a is null)
            {
                WriteLine("No help for {0}", member);
            }
            else
            {
                WriteLine(" help for {0}", member);
                WriteLine("Url: {0}, Topic: {1}", a.Url,a.Topic);
            }
        }
    }
}
