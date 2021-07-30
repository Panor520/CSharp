using System;
using System.Collections.Generic;

namespace CSharpDemo
{
    /// <summary>
    /// 迭代器（Iterator） yield return 用法
    /// </summary>
    class Iterator
    {
        static void Main1(string[] args)
        {
            foreach (int number in EvenSequence(5, 18))
            {
                Console.Write(number.ToString() + " ");
            }
            Console.WriteLine();
        }

        //通过使用 foreach 语句调用迭代器。 foreach 循环的每次迭代都会调用迭代器。 迭代器中到达 yield return 语句时，会返回一个表达式，并保留当前在代码中的位置。 下次调用迭代器时，将从该位置重新开始执行。
        private static IEnumerable<int> EvenSequence(int firstNum,int lastNum)
        {
            for (int number = firstNum; number <= lastNum; number++)
            {
                if (number % 2 == 0)
                {
                    yield return number;
                }//外面调用完会继续跳到这执行
            }
        }
    }
}
