using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo
{
    /// <summary>
    /// 协变 covariant 和 逆变 contravariant
    /// 协变和逆变能够实现数组类型、委托类型和泛型类型参数的 隐式引用转换。 协变保留分配兼容性，逆变则与之相反
    /// </summary>
    /// 
    #region covariant
    class Covariant
    {
        //协变允许方法具有的返回类型比接口的泛型类型参数定义的返回类型的派生程度更大。
        //IEnumerable<String> 接口不继承 IEnumerable<Object> 接口。 但是，String 类型会继承 Object 类型
        //例子如下：
        static void ExampleCovariant()
        {
            IEnumerable<String> strings = new List<String>();
            IEnumerable<Object> objects = strings;// string -> object 存在隐式转换

            //只有引用类型才支持使用泛型接口中的变体。 值类型不支持变体.
            IEnumerable<int> integers = new List<int>();
            // The following statement generates a compiler error,
            // because int is a value type.
            // IEnumerable<Object> objects = integers;

            //实现变体接口的类仍是固定类,固定类是没有变体的
            // The following line generates a compiler error
            // because classes are invariant.
            // List<Object> list = new List<String>();

            // You can use the interface object instead.
            IEnumerable<Object> listObjects = new List<String>();
        }


    }
    #endregion

    #region contravariant
    // Simple hierarchy of classes.
    class BaseClass { }
    class DerivedClass : BaseClass { }

    // Comparer class.
    class BaseComparer : IEqualityComparer<BaseClass>
    {
        public int GetHashCode(BaseClass baseInstance)
        {
            return baseInstance.GetHashCode();
        }
        public bool Equals(BaseClass x, BaseClass y)
        {
            return x == y;
        }
    }
    class Program
    {
        /// <summary>
        /// //逆变允许方法具有的实参类型比接口的泛型形参定义的类型的派生程度更小
        /// IEqualityComparer<DerivedClass> 接口不继承 IEqualityComparer<BaseClass> 接口。 但是，DerivedClass 类型继承 BaseClass 类型
        /// </summary>
        static void Test()
        {
            IEqualityComparer<BaseClass> baseComparer = new BaseComparer();

            // Implicit conversion of IEqualityComparer<BaseClass> to
            // IEqualityComparer<DerivedClass>.
            IEqualityComparer<DerivedClass> childComparer = baseComparer;//BaseClass ->DerivedClass 存在隐式转换
        }
    }
    #endregion
}
