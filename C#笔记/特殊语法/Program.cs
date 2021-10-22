using System;
using System.Collections.Generic;

namespace 特殊语法
{
    class Program
    {
        class test
        {

            public bool sex;
            public string Name
            {
                set => sex = false;
                get => "1123";
            }

            public int Age
            {
                get;
                set;
            }
            public test(bool s)
            {
                this.sex = s;
            }

            public int Add(int x, int y) => x + y;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("特殊语法");
            #region var隐式类型
            //var是一种特殊的变量类型
            //它可以用来表示任意类型的变量
            //注意：
            //1.var不能作为类的成员 只能用于临时变量声明
            //也就是一般写在函数语句块中
            //2.var必须初始化
            var i = 5;
            var s = "123";
            var array = new int[] { 1, 2, 3 };
            var list = new List<int>();
            #endregion

            #region 设置对象初始值
            //声明对象时
            //可以通过直接写大括号的形式初始化公共成员变量的属性
            test t = new test(true){ sex = true, Age = 18, Name = "1123"};
            #endregion

            #region 设置集合初始值
            //声明集合对象时
            //也可以通过大括号 直接初始化内部属性
            int[] array2 = new int[] { 1, 2, 3, 4, 5 };
            List<int> listInt = new List<int>() { 1, 2, 3, 4, 5 };
            List<test> listTest = new List<test>() { 
                new test(true){ sex = true, Name = "123"},
            };

            Dictionary<int, string> dic = new Dictionary<int, string>()
            {
                { 1,"123"},
                { 2, "222"}
            };
            #endregion

            #region 匿名类型
            //var 变量可以声明为自定义的匿名类型
            var v = new { age = 10, money = 11, name = "222" };
            Console.WriteLine(v.age);
            #endregion

            #region 可空类型
            //1.值类型不能赋值为空
            //int c = null;
            //2.声明时 在值类型后面加 ？可以赋值为空
            int? c = null;
            //3.判断是否为空
            if(c.HasValue)
            {
                Console.WriteLine(c);
                Console.WriteLine(c.Value);
            }

            int? value = null;
            //如果为空 默认返回 值类型的默认值
            Console.WriteLine(value.GetValueOrDefault());
            //可以指默认值
            Console.WriteLine(value.GetValueOrDefault(1));

            object o = null;
            if(o != null)
            {
                Console.WriteLine(o.ToString());
            }
            //是一种语法糖 能自动判断o是否为空
            //如果是null就不会执行toString也不会报错
            Console.WriteLine(o?.ToString());

            int[] arrayInt = null;
            Console.WriteLine(arrayInt?[0]);

            Action action = null;
            action?.Invoke();
            #endregion

            #region 空合并操作符
            //空合并操作符
            //左边值？？右边值
            //如果左边值为null 就返回右边值 否则返回左边值
            //只要是可以为null的类型都能用
            int? intV = null;
            int intI = intV == null ? 100 : intV.Value;
            intI = intV ?? 100;
            #endregion

            #region 内插字符串
            //关键符号：$
            //用$来构造支付穿，让字符串中可以拼接变量
            string name = "1123";
            int age = 18;
            Console.WriteLine($"name -- {name},age -- {age}");
            #endregion

            #region 单句逻辑简略写法
            //当语句中只有一句代码可以省略大括号
            if (true)
                Console.WriteLine("11");

            for(int j = 0; j < 10; j++)
                Console.WriteLine("11");
            #endregion
        }
    }
}
