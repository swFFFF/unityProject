using System;

namespace 值和引用
{
    class test
    {

    }
    struct TestStruct
    {
        public test t;
        public int i;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("值和引用");

            #region 值和引用类型
            //值类型
            //无符号：byte,ushort,uint,ulong
            //有符号：sbyte,short,int,long
            //浮点数：float,double,decimal
            //特殊：char,bool
            //枚举：enum
            //结构体：struct

            //引用类型
            //string
            //数组
            //class
            //interface
            //委托

            //值类型和引用类型的本质区别
            //值存在栈内存上
            //引用存在堆内存上

            #endregion

            #region 判断值类型和引用类型
            //F12进入类型内部查看
            //class是引用
            //struct是值
            int i = 12;
            string str = "123";
            #endregion

            #region 语句块
            //命名空间
            //  ↓
            //类、接口、结构体
            //  ↓
            //函数、属性、索引器、运算符重载等（类、接口、结构体）
            //  ↓
            //条件分支、循环

            //上层语句块：类、结构体
            //中层语句块：函数
            //底层的语句块：条件分支 循环等

            //我们的逻辑代码写再函数、条件分支、循环-中层语句块中

            //我们的变量可以声明在上、中、底层中
            //上层语句块：成员变量
            //中、底层语句块：临时变量
            #endregion

            #region 变量的生命周期
            //中低层声明的临时变量
            //语句块执行结束
            //没有被记录的对象将被回收或变成垃圾
            //值类型：被系统自动回收
            //引用类型：栈上用于存地址的房间被系统自动回收，栈中具体内容变成垃圾，等待下次GC回收

            //想要不被回收或者不变成垃圾
            //必须记录下来
            //在更高层记录或者使用静态全局变量
            #endregion

            #region 结构体中的值和引用
            //结构体是值类型
            //前提是该结构体没有作为其他类的成员
            //结构体中的值，栈中存储值具体的内容
            //结构体中的引用，堆中存储引用具体的内容

            //引用类型始终存储在堆中
            //真正通过结构体使用其中引用类型时是顺藤摸瓜
            TestStruct ts = new TestStruct();
            #endregion

            #region 类中的值和引用
            //类本身是引用类型
            //在类中的值，堆中存储具体的值
            //在类中的引用，堆中存储具体的值

            //值类型跟着大哥走，引用类型一根筋
            #endregion

            #region 数组中的存储规则
            //数组本身是引用类型
            //值类型数组，堆中房间存具体内容
            //引用类型数组，堆中房间存地址

            test t = new test();
            #endregion

            #region 结构体继承接口
            //利用里氏替换原则，用接口 容器装在结构图题存在装箱拆箱
            TestStruct1 obj1 = new TestStruct1();
            obj1.Value = 1;
            Console.WriteLine(obj1.Value);
            TestStruct1 obj2 = obj1;
            Console.WriteLine(obj1.Value);
            Console.WriteLine(obj2.Value);

            ITest iObj1 = obj1;//装箱 value 1
            ITest iObj2 = iObj1;
            iObj2.Value = 99;
            Console.WriteLine(iObj1.Value);
            Console.WriteLine(iObj2.Value);

            TestStruct1 obj3 = (TestStruct1)iObj1;  //拆箱
            #endregion
        }
    }

    interface ITest
    {
        int Value
        {
            get;
            set;
        }
    }

    struct TestStruct1 : ITest
    {
        int value;
        public int Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
