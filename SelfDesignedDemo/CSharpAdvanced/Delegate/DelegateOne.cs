using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAdvanced.Delegate
{
    public class DelegateOne
    {
        public static void BubbleSort1(int[] arr)
        {
            int temp;//临时变量
            bool flag;//是否交换的标志
            for (int i = 0; i < arr.Length - 1; i++)
            {   //表示趟数，一共 arr.length-1 次
                // 每次遍历标志位都要先置为false，才能判断后面的元素是否发生了交换
                flag = false;

                for (int j = arr.Length - 1; j > i; j--)
                { //选出该趟排序的最大值往后移动
                    if (arr[j] < arr[j - 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = temp;
                        flag = true;    //只要有发生了交换，flag就置为true
                    }
                }
                // 判断标志位是否为false，如果为false，说明后面的元素已经有序，就直接return
                if (!flag) break;
            }
        }

        public DelegateOne(int[] array)
        {//4123
            string total = "";
            foreach (int item in array)
            {
                total += item + ",";
            }
            Console.WriteLine($"{total}");
            int i, j, temp;
            for (i = 0; i < array.Length - 1; i++)
            {
                for (j = 0; j < array.Length - 1; j++)
                {
                    if (array[j + 1] < array[j])//asc  > desc
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            total = "";
            foreach (int item in array)
            {
                total += item + ",";
            }
            Console.WriteLine($"{total}");
        }

        public class BubbleSort2
        {
            public BubbleSort2(int[] array)
            {
                string total = "";
                foreach (int item in array)
                {
                    total += $"{item},";
                }
                Console.WriteLine($"冒泡排序前：\n {total}");
                int i, j, temp;
                for ( i = 0 ; i < array.Length-1; i++)
                {
                    for (j = 0; j < array.Length - 1; j++)//循环把最大的放到最后面
                    {
                        //if (array[j] > array[j + 1])
                        //if (BubbleSort2.GreaterThan(array[j], array[j + 1]))
                        if (BubbleSort2.Lessthan(array[j], array[j + 1]))
                        {
                            temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }

                    total = "";
                    foreach (int item in array)
                    {
                        total += $"{item},";
                    }
                    Console.WriteLine($"冒泡排序中：\n {total}");
                }
                total = "";
                foreach (int item in array)
                {
                    total += $"{item},";
                }
                Console.WriteLine($"冒泡排序后：\n {total}");
            }

            public static bool GreaterThan(int A, int B)//大于
            {
                bool flag = false;
                if (A>B)
                {
                    flag = true;
                }
                return flag;
            }

            public static bool Lessthan(int A, int B)//小于
            {
                bool flag = false;
                if (A < B)
                {
                    flag = true;
                }
                return flag;
            }
        }

        public delegate bool Compare(int A,int B);
    }
}
