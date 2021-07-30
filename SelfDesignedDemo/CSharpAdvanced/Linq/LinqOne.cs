using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;

namespace CSharpAdvanced.Linq
{
    public class LinqOne
    {
        public void Linq1()
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("OneTable");
            int i = new int();
            dataSet.Tables["OneTable"].Columns.Add("ID").DataType = i.GetType();//typeof(int);
            dataSet.Tables["OneTable"].Columns.Add("Name");

            dataSet.Tables["OneTable"].Rows.Add(1, "张三");
            dataSet.Tables["OneTable"].Rows.Add(2, "李四");
            dataSet.Tables["OneTable"].Rows.Add(3, "王五");
            dataSet.Tables["OneTable"].Rows.Add(4, "赵六");
            dataSet.Tables["OneTable"].Rows.Add(5, null);

            DataTable dataTable = dataSet.Tables["OneTable"];

            //var c=  dataTable.AsEnumerable().Select(p=>p.Field<int>("ID")==1);

            //这是linq的声明，但是此时是不执行的.
            var query = from c in dataTable.AsEnumerable()
                        where c.Field<int>("ID") == 2 || c.Field<int>("ID") == 3
                        select c;
            //这是原始写法，上面的var代表的其实就是IEnumerable<DataRow>
            IEnumerable<DataRow> dataTables = from c in dataTable.AsEnumerable()
                                              select c;
            //Query execution
            foreach (DataRow item in query)
            {
                Console.WriteLine(item.Field<string>("Name"));
            }

            //要强制立即执行任何查询并缓存其结果，可调用 ToList 或 ToArray 方法。
            var query1 = (from c in dataTable.AsEnumerable()
                          where c.Field<int>("ID") == 2 || c.Field<int>("ID") == 3
                          select c).ToList();

            //连接
            //var innerJoinQuery =from cust in customers
            //                    join dist in distributors on cust.City equals dist.City
            //                    select new { CustomerName = cust.Name, DistributorName = dist.Name };
        }

        public void Linq2()
        {
            // Create the first data source.
            List<Student> students = new List<Student>()
            {
                new Student { First="Svetlana",
                    Last="Omelchenko",
                    ID=111,
                    Street="123 Main Street",
                    City="Seattle",
                    Scores= new List<int> { 97, 92, 81, 60 } },
                new Student { First="Claire",
                    Last="O’Donnell",
                    ID=112,
                    Street="124 Main Street",
                    City="Redmond",
                    Scores= new List<int> { 75, 84, 91, 39 } },
                new Student { First="Sven",
                    Last="Mortensen",
                    ID=113,
                    Street="125 Main Street",
                    City="Lake City",
                    Scores= new List<int> { 88, 94, 65, 91 } },
            };

            // Create the second data source.
            List<Teacher> teachers = new List<Teacher>()
            {
                new Teacher { First="Ann", Last="Beebe", ID=945, City="Seattle" },
                new Teacher { First="Alex", Last="Robinson", ID=956, City="Redmond" },
                new Teacher { First="Michiyo", Last="Sato", ID=972, City="Tacoma" }
            };

            // Create the query.
            var peopleInSeattle = (from student in students
                                   where student.City == "Seattle"
                                   select student.Last)
                        .Concat(from teacher in teachers
                                where teacher.City == "Seattle"
                                select teacher.Last);
            //another create the query
            IEnumerable<string> peopleInSeattle1 = (from student in students
                                                    where student.City == "Seattle"
                                                    select student.Last)
                        .Concat(from teacher in teachers
                                where teacher.City == "Seattle"
                                select teacher.Last);

            //let Sample
            string[] strs ={ "A penny saved is a penny earned.",
            "The early bird catches the worm.",
            "The pen is mightier than the sword." };

            // Split the sentence into an array of words
            // and select those whose first letter is a vowel
            //let could set format
            var strsQuery = from cc in strs                 //还是按照strs的格式显示
                            let words = cc.Split(' ')       //在strs的基础上，再split
                            from word in words              //合成一个大数组
                            //let w = word.ToLower().TrimEnd('.')
                            //where w[0] == 'a' || w[0] == 'e'
                            //    || w[0] == 'i' || w[0] == 'o'
                            //    || w[0] == 'u'
                            select word;

            var studentToXML = new XElement("Root",
                from student in  students
                let score=string.Join(",",student.Scores)
                select new XElement("Student",new XElement("first",student.First),
                new XElement("Last",student.Last),new XElement("Score",score))
                );

            Console.WriteLine(studentToXML);

            double[] radii = { 1, 2, 3 };
            
            var output = radii.Select(r =>
            {
                return $"Area for a circle with a radius of '{r}' = {r * r * Math.PI:F2}";//:F2表示保留两位小数
            });
            foreach (string s in output)
            {
                Console.WriteLine(s);
            }
            int c = 2;
            testOutParameter(1,out c);
            Console.WriteLine(c);
        }

        protected void testOutParameter(int a ,out int  i)
        {
            i = a + 11;
        }
    }
    class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public List<int> Scores;
    }

    class Teacher
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public string City { get; set; }
    }

}
