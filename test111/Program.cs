using System;
using System.Reflection;

namespace test111
{
    class test
    {
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

            #endregion

            #region Activator
            //用于快速实例化对象的类
            //
            #endregion
            Console.ReadLine();
        }
    }
}
