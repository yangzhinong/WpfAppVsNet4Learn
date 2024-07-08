using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Text;


namespace DemoThread;

internal class Program
{
    static Dictionary<int, string> dic = new();
    static List<Task> tasks = new();
    private readonly static object _locker = new object();
    static ProgressStatus _status;
    static void Main(string[] args)
    {
        var i = 5;
        var j = 6;
        DoWorkProxy();
        i++;
        Console.WriteLine(i);

        var p = Process.GetCurrentProcess();
        foreach (var item in p.Threads)
        {
            
        }
    }

    [DebuggerStepThrough, DebuggerHidden]
    static void DoWorkProxy()
    {
        Console.WriteLine("Hello");

    }

    private static WeakReference GetWeakRef()
    {
        return new WeakReference(new StringBuilder("weak"));
    }

    private static void Test()
    {
        
        //Span<int> a = stackalloc int[1000];
        //for (int i = 0; i < a.Length; i++)
        //{

        //    Console.WriteLine(a[i]);
        //}

        unsafe
        {
            delegate*<string, int> functionPointer = &GetLength;

            int length = functionPointer("Hello world");
            Console.WriteLine((IntPtr)functionPointer);
            static int GetLength(string s) => s.Length;
        }

    }

    /// <summary>
    /// Test Foo Document <br/>
    /// 详见<see cref="Test"/>
    /// <code>
    ///     var i=0;
    ///     
    /// </code>
    /// <list type="bullet">
    /// <listheader>
    ///     <term>Hello</term>
    ///     <description>Hello sample</description>
    /// </listheader>
    /// <item>
    ///     <term>dd</term>
    ///     <description>dd desc</description>
    /// </item>
    /// <item>
    ///     <term>aa</term>
    ///     <description>aa desc</description>
    ///     <code>dd</code>
    /// </item>
    /// </list>
    /// </summary>
    /// 
    /// <example>
    /// <c>this</c>
    /// </example>
    static void Foo()
    {
        //int local;
        //int* ptr = &local;
        //Console.WriteLine(*ptr);
        //Console.WriteLine("-----------");
        //int* a = stackalloc int[100];
        //for(int i=0; i<100; i++)
        //{
        //    Console.WriteLine(a[i]);
        //}
        System.Numerics.BitOperations.IsPow2(5);
        Span<int> a = stackalloc int[100];
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(a[i]);
        }
    }

    public struct SqlBoolean
    {
        public static bool operator true(SqlBoolean x) => x.m_value == True.m_value;
        public static bool operator false(SqlBoolean x) => x.m_value == False.m_value;

        public static readonly SqlBoolean Null = new SqlBoolean(0);
        public static readonly SqlBoolean False = new SqlBoolean(1);
        public static readonly SqlBoolean True = new SqlBoolean(2);


        private SqlBoolean(byte value) { m_value = value; }
        private byte m_value;
    }

    public struct Note : IEquatable<Note>, IComparable<Note>
    {
        int value;
        public Note(int semitonesFromA) { value = semitonesFromA; }

        public static Note operator +(Note a, Note b)
        {
            return new Note(a.value + b.value);
        }

        public static Note operator +(Note a, int semitones)
        {
            return new Note(a.value + semitones);
        }




        // override object.Equals
        //public override readonly bool Equals(object obj)
        //{

        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }

        //    return obj is Note n && n.value == value;
        //}

        // override object.GetHashCode
        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        bool IEquatable<Note>.Equals(Note other)
        {
            return other.value == value;
        }

        int IComparable<Note>.CompareTo(Note other)
        {
            throw new NotImplementedException();
        }

        



    }
}





    record Student(string ID, string LastName, string GivenName)
    {
        public string ID { get; } = ID;
    }

    record struct Student2(string ID)
    {
        public string ID = ID;
    }

    record Student3(string ID, string LastName, string FirstName)
    {
        public string ID { get; } = ID;
        readonly int _enrollmentYear = int.Parse(ID.Substring(0, 4));
    }


    public class ProgressStatus:IEnumerator<string>
    {
        volatile ProgressStatus _expensive;
        public readonly int PercentComplete;
        public readonly string StatusMessage;
        [ThreadStatic]
        static int _x;
        readonly ThreadLocal<Random> _y = new(() => new Random(Guid.NewGuid().GetHashCode()));

        LocalDataStoreSlot _secSol = Thread.GetNamedDataSlot("dd");
        
        // This class might have many more fields...

        public ProgressStatus(int percentComplete, string statusMessage)
        {
            PercentComplete = percentComplete;
            StatusMessage = statusMessage;
            var ddd = new System.Threading.Timer(dd, null, 200, Timeout.Infinite);
        }

        private void dd(object? state)
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ProgressStatus Expensive
        {
            get
            {
                if (_expensive == null)
                {
                    var instace = new ProgressStatus(0, "Success");
                    Interlocked.CompareExchange(ref _expensive, instace, null);
                    
                }
                return _expensive;
            }
        }

        public string Current { get; }
        object IEnumerator.Current { get => Current; }
    }



public class Duck:DynamicObject
{
    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        Console.WriteLine(binder.Name + " method was called");
        result = null;
        return true; 
    }
}


public class WordyFormatProvider : IFormatProvider, ICustomFormatter
{
    static readonly string[] _numberWords = "zero one two three four five six sevent eight nine minus point".Split();

    IFormatProvider _parent;

    public WordyFormatProvider():this(CultureInfo.CurrentCulture)
    {
        
    }
    public WordyFormatProvider(IFormatProvider parent)
    {
        _parent= parent;
    }
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        if (arg == null || format != "W")
            return string.Format(_parent, "{0:" + format + "}", arg);


        StringBuilder result = new();
        string digitList = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
        foreach(char digit in digitList)
        {
            int i = "0123456789-.".IndexOf(digit);
            if (i == -1) continue;
            if (result.Length > 0) result.Append(' ');
            result.Append(_numberWords[i]);
        }
        return result.ToString();
    }

    public object? GetFormat(Type? formatType)
    {
        if (formatType == typeof(ICustomFormatter)) return this;
        return null;
    }

    
}

public enum EnuSex
{
    U = 0,
    F = 1,
    M = 2,
}

[Flags]
public enum EnuLeftRight
{
    Left =1,
    Right = 2,
}

public struct Area: IEquatable<Area>
{
    public readonly int Measure1;
    public readonly int Measure2;

    public Area(int m1, int m2)
    {
        Measure1 = Math.Min(m1,m2);
        Measure2 = Math.Max(m1, m2);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Area a && Equals(a);
    }

    public bool Equals(Area other)
    {
        return Measure1 == other.Measure1 && Measure2 == other.Measure2;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Measure1, Measure2);
    }

    public static bool operator == (Area a, Area b)=> a.Equals(b);

    public static bool operator !=(Area a, Area b) => !a.Equals(b);

}


public class MyGenCollection : IEnumerable<int>
{
    int[] data = { 1, 2, 3, 4 }; 
    public IEnumerator<int> GetEnumerator()
    {
        foreach(var i in data) yield return i;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
       var data=   Array.CreateInstance(typeof(int), 5);
        data.SetValue("hi", 0);
        LinkedList<int> dd = new LinkedList<int>();
        var bits = new BitArray(2);
        bits[1] = true;
        
        return GetEnumerator();
    }
}
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Animal
{
    string name;
    public string Name { get => name;
    
    set
        {
            Zoo?.Animals.NotifyNameChanged(this, value);
            name = value;
        }
    }

    public int Popularity;

    public Zoo Zoo { get; internal set; }

    public Animal(string name, int popularity)
    {
        Name = name;
        Popularity = popularity;
    }

}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class AnimalCollection:KeyedCollection<string, Animal>
{
    private readonly Zoo _zoo;

    public AnimalCollection(Zoo zoo)
    {
        _zoo = zoo;
    }

    internal void NotifyNameChanged(Animal a, string newName)
    {
        this.ChangeItemKey(a, newName); 
    }

    protected override string GetKeyForItem(Animal item)
    {
        return item.Name;
    }

    protected override void InsertItem(int index, Animal item)
    {
        item.Zoo = _zoo;
        base.InsertItem(index, item);
        
    }

    protected override void RemoveItem(int index)
    {
        this[index].Zoo = null;
        base.RemoveItem(index);
    }

    protected override void ClearItems()
    {
        foreach (Animal a in this) a.Zoo = null;
        base.ClearItems();
    }

    protected override void SetItem(int index, Animal item)
    {
        item.Zoo = _zoo;
        base.SetItem(index, item);
    }
}

public class Zoo
{
    public readonly AnimalCollection Animals;
    public Zoo() { Animals = new AnimalCollection(this);}
}