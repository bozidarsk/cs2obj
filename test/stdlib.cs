namespace System
{
    using System.Collections;
    using System.Collections.Generic;

    public abstract class MulticastDelegate : Delegate 
    {

    }

    public struct RuntimeFieldHandle 
    {
        
    }

    public struct RuntimeTypeHandle 
    {
        
    }

    public class ParamArrayAttribute : Attribute 
    {

    }

    public struct Void 
    {
    }

    public delegate void Action();

    public class ArgumentException : Exception
    {
        public ArgumentException(){}
        public ArgumentException(string message){}
        public ArgumentException(string message, Exception innerException){}
    }

    public class ArithmeticException : Exception
    {
        public ArithmeticException(){}
        public ArithmeticException(string message){}
        public ArithmeticException(string message, Exception innerException){}
    }

    public abstract class Array : IList, ICollection, IEnumerable
    {
        public int Length { get; }
        public int Rank { get; }
        public int GetLength(int dimension) => 0;

        public bool IsFixedSize { get => true; }
        public bool IsReadOnly { get => false; }

        public object this[int index] 
        {
            set {}
            get => null;
        }

        public int Add(object value) => 0;
        public void Clear(){}
        public bool Contains(object value) => false;
        public int IndexOf(object value) => 0;
        public void Insert(int index, object value){}
        public void Remove(object value){}
        public void RemoveAt(int index){}

        public int Count { get => this.Length; }
        public bool IsSynchronized { get => true; }
        public object SyncRoot { get => null; }
        public void CopyTo(Array array, int index){}

        public IEnumerator GetEnumerator() => null;
    }

    public class ArrayTypeMismatchException : Exception
    {
        public ArrayTypeMismatchException(){}
        public ArrayTypeMismatchException(string message){}
        public ArrayTypeMismatchException(string message,
            Exception innerException){}
    }

    [AttributeUsageAttribute(AttributeTargets.All, Inherited = true,
        AllowMultiple = false)]
    public abstract class Attribute
    {
        protected Attribute(){}
    }

    public enum AttributeTargets
    {
        Assembly = 0x1,
        Module = 0x2,
        Class = 0x4,
        Struct = 0x8,
        Enum = 0x10,
        Constructor = 0x20,
        Method = 0x40,
        Property = 0x80,
        Field = 0x100,
        Event = 0x200,
        Interface = 0x400,
        Parameter = 0x800,
        Delegate = 0x1000,
        ReturnValue = 0x2000,
        GenericParameter = 0x4000,
        All = 0x7FFF
    }

    [AttributeUsageAttribute(AttributeTargets.Class, Inherited = true)]
    public sealed class AttributeUsageAttribute : Attribute
    {
        public AttributeUsageAttribute(AttributeTargets validOn){}
        public bool AllowMultiple { get; set; }
        public bool Inherited { get; set; }
        public AttributeTargets ValidOn { get; }
    }

    public /*readonly*/ struct Boolean { }
    public /*readonly*/ struct Byte { }
    public /*readonly*/ struct Char { }
    public /*readonly*/ struct Decimal { }
    public abstract class Delegate { }

    public class DivideByZeroException : ArithmeticException
    {
        public DivideByZeroException(){}
        public DivideByZeroException(string message){}
        public DivideByZeroException(string message, Exception innerException){}
    }

    public /*readonly*/ struct Double { }

    public abstract class Enum : ValueType
    {
        protected Enum(){}
    }

    public class Exception
    {
        public Exception(){}
        public Exception(string message){}
        public Exception(string message, Exception innerException){}
        public /*sealed*/ Exception InnerException { get; }
        public virtual string Message { get; }
    }

    public class GC { }

    public interface IDisposable
    {
        void Dispose();
    }

    public interface IFormattable { }

    public sealed class IndexOutOfRangeException : Exception
    {
        public IndexOutOfRangeException(){}
        public IndexOutOfRangeException(string message){}
        public IndexOutOfRangeException(string message,
            Exception innerException){}
    }

    public /*readonly*/ struct Int16 { }
    public /*readonly*/ struct Int32 { }
    public /*readonly*/ struct Int64 { }
    public /*readonly*/ struct IntPtr { }

    public class InvalidCastException : Exception
    {
        public InvalidCastException(){}
        public InvalidCastException(string message){}
        public InvalidCastException(string message, Exception innerException){}
    }

    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(){}
        public InvalidOperationException(string message){}
        public InvalidOperationException(string message,
            Exception innerException){}
    }

    public class NotSupportedException : Exception
    {
        public NotSupportedException(){}
        public NotSupportedException(string message){}
        public NotSupportedException(string message, Exception innerException){}
    }

    public struct Nullable<T>
    {
        public bool HasValue { get; }
        public T Value { get; }
    }

    public class NullReferenceException : Exception
    {
        public NullReferenceException(){}
        public NullReferenceException(string message){}
        public NullReferenceException(string message, Exception innerException){}
    }

    public class Object
    {
        public Object(){}
        ~Object(){}
        public virtual bool Equals(object obj) => false;
        public virtual int GetHashCode() => 0;
        public Type GetType() => null;
        public virtual string ToString() => null;
    }

    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Struct |
        AttributeTargets.Enum | AttributeTargets.Interface |
        AttributeTargets.Constructor | AttributeTargets.Method |
        AttributeTargets.Property | AttributeTargets.Field |
        AttributeTargets.Event | AttributeTargets.Delegate, Inherited = false)]
    public sealed class ObsoleteAttribute : Attribute
    {
        public ObsoleteAttribute(){}
        public ObsoleteAttribute(string message){}
        public ObsoleteAttribute(string message, bool error){}
        public bool IsError { get; }
        public string Message { get; }
    }

    public class OutOfMemoryException : Exception
    {
        public OutOfMemoryException(){}
        public OutOfMemoryException(string message){}
        public OutOfMemoryException(string message, Exception innerException){}
    }

    public class OverflowException : ArithmeticException
    {
        public OverflowException(){}
        public OverflowException(string message){}
        public OverflowException(string message, Exception innerException){}
    }

    public /*readonly*/ struct SByte { }
    public /*readonly*/ struct Single { }

    public sealed class StackOverflowException : Exception
    {
        public StackOverflowException(){}
        public StackOverflowException(string message){}
        public StackOverflowException(string message, Exception innerException){}
    }

    public sealed class String : IEnumerable<char>
    {
        public int Length { get; }
        public char this [int index] { get => (char)0; }
        public static string Format(string format, params object[] args) => null;

        public IEnumerator<char> GetEnumerator() => null;
        // public IEnumerator GetEnumerator() => null;
        public char Current { get; }
    }

    public abstract class Type : System.Reflection.MemberInfo { }

    public sealed class TypeInitializationException : Exception
    {
        public TypeInitializationException(string fullTypeName,
            Exception innerException){}
    }

    public /*readonly*/ struct UInt16 { }
    public /*readonly*/ struct UInt32 { }
    public /*readonly*/ struct UInt64 { }
    public /*readonly*/ struct UIntPtr { }

    public struct ValueTuple<T1>
    {
        public T1 Item1;
        public ValueTuple(T1 item1) 
        {
            this.Item1 = item1;
        }
    }
    public struct ValueTuple<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;
        public ValueTuple(T1 item1, T2 item2) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }
    }
    public struct ValueTuple<T1, T2, T3>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public ValueTuple(T1 item1, T2 item2, T3 item3) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }
    }
    public struct ValueTuple<T1, T2, T3, T4>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }
    }
    public struct ValueTuple<T1, T2, T3, T4, T5>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
        }
    }
    public struct ValueTuple<T1, T2, T3, T4, T5, T6>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
        }
    }
    public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public T7 Item7;
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) 
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
            this.Item7 = item7;
        }
    }
    public struct ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
        public T6 Item6;
        public T7 Item7;
        public TRest Rest;
        public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest) 
        {
            this.Item1 = item1;
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
            this.Item5 = item5;
            this.Item6 = item6;
            this.Item7 = item7;
            this.Rest = rest;
        }
    }

    public abstract class ValueType
    {
        protected ValueType(){}
    }
}

namespace System.Collections
{
    using System;
    using System.Collections.Generic;

    public interface ICollection : IEnumerable
    {
        int Count { get; }
        bool IsSynchronized { get; }
        object SyncRoot { get; }
        void CopyTo(Array array, int index);
    }

    public interface IEnumerable
    {
        IEnumerator GetEnumerator();
    }

    public interface IEnumerator
    {
        object Current { get; }
        bool MoveNext();
        void Reset();
    }

    public interface IList : ICollection, IEnumerable
    {
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }
        object this [int index] { get; set; }
        int Add(object value);
        void Clear();
        bool Contains(object value);
        int IndexOf(object value);
        void Insert(int index, object value);
        void Remove(object value);
        void RemoveAt(int index);
    }
}

namespace System.Collections.Generic
{
    using System;
    using System.Collections;

    public interface ICollection<T> : IEnumerable<T>
    {
        int Count { get; }
        bool IsReadOnly { get; }
        void Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, int arrayIndex);
        bool Remove(T item);
    }

    public interface IEnumerable<T> /*: IEnumerable*/
    {
        IEnumerator<T> GetEnumerator();
    }

    public interface IEnumerator<T> : IDisposable, IEnumerator
    {
        new T Current { get; }
    }

    public interface IList<T> : ICollection<T>
    {
        T this [int index] { get; set; }
        int IndexOf(T item);
        void Insert(int index, T item);
        void RemoveAt(int index);
    }

    public interface IReadOnlyCollection<T> : IEnumerable<T>
    {
        int Count { get; }
    }

    public interface IReadOnlyList<T> : IReadOnlyCollection<T>
    {
        T this [int index] { get; }
    }
}

namespace System.Diagnostics
{
    using System;

    [AttributeUsageAttribute(AttributeTargets.Method | AttributeTargets.Class,
                             AllowMultiple = true)]
    public sealed class ConditionalAttribute : Attribute
    {
        public ConditionalAttribute(string conditionString){}
        public string ConditionString { get; }
    }
}

namespace System.Reflection
{
    using System;

    public abstract class MemberInfo
    {
        protected MemberInfo(){}
    }
}

namespace System.Runtime.CompilerServices
{
    using System;

    public sealed class IndexerNameAttribute : Attribute
    {
        public IndexerNameAttribute(String indexerName){}
    }

    public static class Unsafe
    {
        public static ref T NullRef<T>() where T : new()
        {
            T t = default(T);
            return ref t;
        }
    }
}

namespace System.Runtime.InteropServices 
{
    using System;

    public class OutAttribute : Attribute 
    {

    }
}

namespace System.Threading
{
    using System;

    public static class Monitor
    {
        public static void Enter(object obj){}
        public static void Exit(object obj){}
    }
}