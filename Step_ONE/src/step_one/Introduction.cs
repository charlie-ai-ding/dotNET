using System;
using System.Collections;
using System.Collections.Generic;
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
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>(10);


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

        //Fields...
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

        public List(int capacity )
        {
           capacity = defaultCapacity;
            items = new T[capacity];
            WriteLine("I am in list construactor with one param");
        }

        // Properties...
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

        //Indexer...
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
            if (a == null) return b == null;
            if (b == null || a.count != b.count) return false;
            for (int i = 0; i < a.count; i++)
            {
                if (!object.Equals(a.items[i], b.items[i]))
                {
                    return false;
                }
            }
            return true;
        }




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

}
