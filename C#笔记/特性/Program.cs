#define Fun
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace 特性
{
    #region 特性描述
    //特性是一种允许我们向程序集添加元数据的语言结构
    //是用于保存程序结构信息的某种特殊类型的类

    //特性提供功能强大的方法以将声明信息与C# 代码（类型、方法、属性等）相关联
    //特性与程序实体关联后，即可在运行时使用反射查询特性信息

    //特性的目的是告诉编译器把程序结构的某组元数据嵌入程序集中
    //它可以放置在几乎所有的声明中（类、变量、函数等等声明）

    //简
    //特性本质是个类
    //可以利用特性类为元数据添加额外信息
    //比如一个类、成员变量、成员方法等等为他们添加更多的额外信息
    //之后可以通过反射来或者这些额外信息
    #endregion

    #region 自定义特性
    //继承特性基类 Attribute
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    class MyCustomAttribute : Attribute
    {
        //特性中的成员 
        public string info;
        public MyCustomAttribute(string info)
        {
            Console.WriteLine("MyCustomAttribute-" + info);
            this.info = info;
        }

        public void TestFun()
        {
            Console.WriteLine("特性的方法");
        }
    }
    #endregion

    #region 特性使用
    //基本语法
    //[特性名(参数列表)]
    //本质上就是在调用特性类的构造函数
    //写在类、函数、变量的上一行，表示它们具有该特性信息
    [MyCustom("这是个用于计算的类")]
    [MyCustom("这是个用于计算的类")]
    class MyClass
    {
        [MyCustom("这是个成员变量")]
        public int value;
        //[MyCustom("这是个用于计算的类")]
        //public void TestFun([MyCustom("这是个成员变量")]int a)
        //{
           
        //}

        public void TestFun(int a)
        {

        }
        public MyClass(int i) { Console.WriteLine(i); }
    }
    #endregion

    #region 限制自定义特性的使用范围 
    //通过为特性类 加特性 限制其使用范围
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    //参数一：AttributeTargets -- 特性能够用在哪些地方
    //参数二：AllowMutiple -- 是否允许多个特性实例用在同一个目标上
    //参数三：Inherited -- 特性是否能被派生类和重写成员继承
    public class MyCustom2Attribute : Attribute
    {

    }
    #endregion

    #region 系统自带特性---过时特性
    //过时特性
    //Obsolete
    //用于提示用户 使用的方法等成员已经过时 建议使用新方法
    //一般加在函数前的特性

    class TestClass
    {
        //参数一：调用过时方法时 提示的内容
        //参数二：true-使用 该方法时会报错 false-使用该方法时直接警告
        [Obsolete("OldSpeak()已经过时，请使用Speak()", false)]
        public void OldSpeak(string str)
        {

        }

        public void Speak()
        {

        }

        //可以加在try catch 捕获异常信息
        public void SpeakCaller(string str, [CallerFilePath]string fileName = "", 
            [CallerLineNumber]int line = 0, [CallerMemberName]string target = "")
        {
            Console.WriteLine(str);
            Console.WriteLine(fileName);
            Console.WriteLine(line);
            Console.WriteLine(target);
        }
    }

    #endregion

    #region 系统自带特性---调用者信息特性
    //哪个文件调用？
    //CallerFilePath特性
    //哪一行调用？
    //CallerLineNumber特性
    //哪个函数调用？
    //CallerMemberName特性

    //需要引用命名空间 using System.Runtime.CompilerServices;
    //一般作为函数参数的特性
    #endregion

    #region 系统自带特性 -- 条件编译特性
    //条件变异特性
    //Conditional
    //它会和预处理指令 #define配合使用

    //需要引用命名空间using System.Diagnostics;
    //主要可以用在一些调试代码上
    //有时想执行有时不想执行
    #endregion

    #region 系统自带特性 -- 外部DLL包函数特性
    //DLLImport

    //用来标记非.Net(C#)的函数,表明该函数在一个外部的DLL中定义。
    //一般用来调用C或者C++的DLL包写好的方法
    //需要引用命名空间 using System.Runtime.InteropServices
    #endregion
    class Program
    {
        [DllImport("Test.dll")]
        public static extern int Add(int a, int b);

        [Conditional("Fun")]
        static void Fun()
        {
            Console.WriteLine("Fun执行");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("特性");
            MyClass mc = new MyClass(1);
            Type t = mc.GetType();
            //判断类是否使用了某个特性
                //特性类名 是否查找父类有该特性
                //参数一：特性的类型
                //参数二：代表是否搜索继承链（属性和事件忽略此参数）
            if(t.IsDefined(typeof(MyCustomAttribute), false))
            {
                Console.WriteLine("该类型应用了MyCustom特性");
            }

            //获取Type元数据中的所有特性
            object[] array = t.GetCustomAttributes(true);
            for (int i = 0; i < array.Length; i++) 
            {
                if(array[i] is MyCustomAttribute)
                {
                    Console.WriteLine((array[i] as MyCustomAttribute).info);
                    (array[i] as MyCustomAttribute).TestFun();
                }
            }

            TestClass tc = new TestClass();
            tc.OldSpeak("123");

            tc.SpeakCaller("12312312");

            Fun();
        }
    }
    //总结
    //特性是用于 为元数据再添加更多的额外信息（变量、方法等等）
    //我们可以通过反射获取这些额外的数据 进行一些特殊的处理
    //自定义特性 -- 继承Attribute类

    //系统自带特性：过时特性

}
