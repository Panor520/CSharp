using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSharpAdvanced.Attribute;

namespace CSharpAdvanced.Attribute
{
    public static class InvokeCenter
    {
        /// <summary>
        /// 泛型扩展方法，所有的实例都可以调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public static void Management<T>(this T t)
        {
            Type type = t.GetType();
            DefineAttribute classAttribute = type.GetCustomAttribute(typeof(DefineAttribute), true) as DefineAttribute;

            foreach (MethodInfo method in type.GetMethods())
            {
                DefineAttribute methodAttribute = method.GetCustomAttribute(typeof(DefineAttribute), true) as DefineAttribute;
                methodAttribute.Show();
            }

            foreach (FieldInfo field in type.GetFields())
            {
                DefineAttribute fieldAttribute = field.GetCustomAttribute(typeof(DefineAttribute), true) as DefineAttribute;
                fieldAttribute.Show();
            }

            foreach (PropertyInfo property in type.GetProperties())
            {
                DefineAttribute propertyAttribute = property.GetCustomAttribute(typeof(DefineAttribute), true) as DefineAttribute;
                propertyAttribute.Show();
            }

            foreach (MemberInfo memeber in type.GetMembers())
            {
                
            }
        }
    }
}
