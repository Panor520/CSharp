using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CSharpAdvanced.Attribute
{
    public abstract class ValidationAttribute :System.Attribute
    {
        public abstract bool IsValid(object value);
    }

    public class RequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value.ToString());
        }
    }

    public class StringLengthAttribute : ValidationAttribute
    {
        public StringLengthAttribute(int minLength, int maxLength)
        {
            this.MaxLength = maxLength;
            this.MinLength = minLength;
        }
        public int MaxLength { get; set;}
        public int MinLength { get; set;}
        public override bool IsValid(object value)
        {
            string value1 =Convert.ToString(value);
            int length = value1.ToString().Length;
            return !string.IsNullOrWhiteSpace(value1)                                                                                   
                && value1.ToString().Length>=MinLength
                && value1.ToString().Length <= MaxLength;
        }
    }


    public class UseAttributeAbstract
    {
        [StringLength(3, 6)]
        [Required]
        public int PassWord { get; set; }
        [StringLength(3, 5)]
        [Required]
        public string Name { get; set; }
    }

    public static class GetAttribute
    {
        public static void GetAttribute1<T>(this T t)
        {
            Type type = t.GetType();

            foreach (PropertyInfo item in type.GetProperties())
            {
                object value = item.GetValue(t);
                RequiredAttribute requiredAttribute = item.GetCustomAttribute(typeof(RequiredAttribute),true) as RequiredAttribute;
                StringLengthAttribute StringLengthAttribute = item.GetCustomAttribute(typeof(StringLengthAttribute), true) as StringLengthAttribute;
                if (!requiredAttribute.IsValid(value))
                {
                    Console.WriteLine("验证失败！");
                }
                else
                {
                    Console.WriteLine("验证成功！");
                }
                if (!StringLengthAttribute.IsValid(value))
                {
                    Console.WriteLine("验证失败！");
                }
                else
                {
                    Console.WriteLine("验证成功！");
                }
            }
            
        }
    }
}
