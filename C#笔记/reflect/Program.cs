using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace 反射
{
    class test
    {
        public test() { }
        public test(int a) { }
        public test(int a, string b) { }
        private int i = 1;
        public int j = 0;
        public string str = "123";
    }



    class Program
    {
        static void Main(string[] args)
        {

            #region Type
            #region 获取Type
            //获取类
            int a = 1;
            Type type = a.GetType();
            Type type2 = typeof(int);
            Type type3 = Type.GetType("System.Int32");
            Console.WriteLine(type.Assembly);   //获取程序集
            #endregion

            #region 获取类中的所有公共成员
            //得到Type
            Type t = typeof(test);
            //得到公共成员
            MemberInfo[] infos = t.GetMembers();
            for (int i = 0; i < infos.Length; i++) 
            {
                Console.WriteLine(infos[i]);
            }
            #endregion

            #region 获取类的公共构造函数并调用
            //1.获取类的所有构造函数
            ConstructorInfo[] ctors = t.GetConstructors();
            for (int i = 0; i < ctors.Length; i++) 
            {
                Console.WriteLine(ctors[i]);
            }

            //2.获取其中一个构造函数 并执行
            //构造函数传入 Type数组 数组中内容按顺序是参数类型
            //执行构造函数传入 object数组 表示按照顺序传入的参数
            //2-1得到无参构造
            ConstructorInfo info = t.GetConstructor(new Type[0]);
            //执行无参构造 无参数传 null
            test obj = info.Invoke(null) as test;
            Console.WriteLine(obj.j);
            //2-2得到有参构造
            ConstructorInfo info2 = t.GetConstructor(new Type[] {typeof(int) });
            obj = info2.Invoke(new object[] { 2}) as test;
            Console.WriteLine(obj.str);

            ConstructorInfo info3 = t.GetConstructor(new Type[] { typeof(int), typeof(string)});
            obj = info3.Invoke(new object[] { 4, "222" }) as test;
            Console.WriteLine(obj.str);
            #endregion

            #region 获取类的公共成员变量
            //1.得到所有成员变量
            FieldInfo[] fieldInfo = t.GetFields();
            for (int i = 0; i < fieldInfo.Length; i++)
            {
                Console.WriteLine(fieldInfo[i]);
            }
            //2.得到指定名称的公共成员变量
            FieldInfo infoJ = t.GetField("j");
            Console.WriteLine(infoJ);
            //3.通过反射获取的设置对象的值
            test tt = new test();
            tt.j = 99;
            tt.str = "123333";
            //  3-1通过反射获取对象的某个变量的值
            Console.WriteLine(infoJ.GetValue(tt));
            //  3-2通过反射设置指定对象的某个变量的值
            infoJ.SetValue(tt, 100);
            Console.WriteLine(infoJ.GetValue(tt));

            #region 获得类中的公共成员方法
            //通过Type 类中的GetMethod 方法 得到类中的方法
            //MethodInfo是方法的反射信息
            Type strType = typeof(string);
            //1.如果存在方法重载 用Type 数组表示参数类型
            MethodInfo[] methods = strType.GetMethods();
            for(int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine(methods[i]);
            }
            MethodInfo subStr = strType.GetMethod("Substring", new Type[] {typeof(int), typeof(int) });
            //2.调用该方法
            //如果是静态方法 Invoke中第一个参数传null
            string str = "Helllo";
            object objj = subStr.Invoke(str, new object[] { 3, 2 });
            Console.WriteLine(objj);
            #endregion
            #endregion
            #endregion

            #region Assembly
            //程序集类
            //主要用来加载其他程序集，加载后才能用Type获取其他程序集信息
            //如果想使用不是自己程序集中的内容 需要先加载程序集
            //比如 dll文件
            //简单的把库文件看成一种代码仓库，它提供给使用着一些可以拿来使用的变量、函数或类

            //三种加载程序集的函数
            //一般用来加载在同一文件下的其它程序集
            //Assembly assembly2 = Assembly.Load("程序集名称");

            //一般用来加载不同一文件下的其它程序集
            //Assembly assemby = Assembly.LoadFrom("包含程序集清单的文件的名称或者路径");
            //Assembly assembly3 = Assembly.LoadFile("要加载的文件的完全限定路径");

            //1.先加载一个指定程序集 @去转义
            Console.WriteLine("---------");
            Assembly assembly = Assembly.LoadFrom(@"D:\unityProject\C#笔记\测试\bin\Debug\net5.0\测试");
            Type[] types = assembly.GetTypes();
            for(int i = 0; i< types.Length; i++)
            {
                Console.WriteLine(types[i]);
            }
            //2.再加载程序集中的一个类对象 之后才能用反射
            Type icon = assembly.GetType("测试.test");
            MemberInfo[] members = icon.GetMembers();
            for(int i =0; i < members.Length; i++)
            {
                Console.WriteLine(members[i]);
            }
            //通过反射 实例化一个 icon对象
            //首先得到枚举Type 来得到可传入的参数
            Type moveDir = assembly.GetType("测试.enumTest");
            FieldInfo right = moveDir.GetField("Right");
            //直接实例化对象
            object iconObj = Activator.CreateInstance(icon, right.GetValue(null));
            //通过反射得到对象中的方法
            MethodInfo testMove = icon.GetMethod("tsetMove");
            while(true)
            {
                Thread.Sleep(1000);
                testMove.Invoke(iconObj, null);
            }
            #endregion

            #region Activator
            //用于快速实例化对象的类
            //用于将Type对象快捷实例化为对象
            //先得到Type
            //然后快速实例化一个对象
            Type testType = typeof(test);
            //1.无参构造函数
            test testobj = Activator.CreateInstance(testType) as test;
            //2.有参构造
            testobj = Activator.CreateInstance(testType, 1) as test;
            #endregion

            #region 总结
            //反射
            //在程序在运行时，通过反射可以得到其他程序集或者自己的程序集代码的各种信息
            //类、函数、变量、对象等，实例化他们，执行他们，操作他们

            //关键类
            //Type
            //Assembly
            //Activator
            #endregion
            Console.ReadLine();
        }
    }
}
