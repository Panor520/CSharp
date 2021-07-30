using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDemo
{
    /// <summary>
    /// 表达式树，要引入System.Linq.Expressions;
    /// </summary>
    class ExpressionTrees
    {
        static void Main1()
        {
            ExampleOne();
        }

        static void ExampleOne()
        {
            // Creating an expression tree.  
            Expression<Func<int, bool>> expr = num => num < 5;

            // Compiling the expression tree into a delegate.  
            Func<int, bool> result = expr.Compile();

            // Invoking the delegate and writing the result to the console.  
            Console.WriteLine(result(4));

            // Prints True.  

            // You can also use simplified syntax  
            // to compile and run an expression tree.  
            // The following line can replace two previous statements.  
            Console.WriteLine(expr.Compile()(4));

            // Also prints True.
        }

        static void ExampleTwo()
        {
            // The expression tree to execute.  
            //Power(a ,b) 就是 a的b次幂
            BinaryExpression be = Expression.Power(Expression.Constant(2D), Expression.Constant(3D));

            // Create a lambda expression.  
            Expression<Func<double>> le = Expression.Lambda<Func<double>>(be);

            // Compile the lambda expression.  
            Func<double> compiledExpression = le.Compile();

            // Execute the lambda expression.  
            double result = compiledExpression();

            // Display the result.  
            Console.WriteLine(result);

            // This code produces the following output:  
            // 8
        }
    }
}
