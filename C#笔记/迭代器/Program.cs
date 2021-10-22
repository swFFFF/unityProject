using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 迭代器
{
    #region 迭代器是什么
    //迭代器 又称光标（cursor）
    //是程序设计的软件设计模式
    //迭代器模式提供一个方法顺序访问一个聚合对象中的各个元素
    //而又不暴露其内部的标识

    //在表现效果上看
    //是可以再容器对象（例如链表或数组）上遍历访问的接口
    //设计人员无需关系容器对象的内存分配的实现细节
    //可以用foreach遍历的类，都是实现了迭代器的
    #endregion

    #region 标准迭代器的实现方法
    //关键接口：IEnumerator,IEnumerable
    //命名空间：using System.Collections;
    //可以通过同时继承IEnumerable和IEnumerator实现其中的方法

    class CustomList:IEnumerable,IEnumerator
    {
        private int[] list;
        //从-1开始的光标 用于表示 数据得到了哪个位置
        private int position = -1;
        public CustomList()
        {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }
        #endregion

        public object Current 
        {
            get
            {
                return list[position];
            }
        }

        public bool MoveNext()
        {
            //移动光标
            ++position;
            //是否溢出 溢出就不合法
            return position < list.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }

    #endregion

    #region 用yield return 语法糖实现迭代器
    //yield return 是c#提供给我们的语法糖
    //主要作用是把复杂逻辑简化，增加程序可读性
    //减少代码出错

    //关键接口：IEnumerable
    //命名空间：using System.Collections;
    //让想要通过foreach遍历的自定义类实现接口中的方法GetEnumerator即可

    class CustomList2 : IEnumerable
    {
        private int[] list;

        public CustomList2()
        {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < list.Length; i++)
            {
                //yield关键字配合迭代器使用
                //可理解为暂时返回 保留当前的状态
                //可以再回来
                //c#的语法糖
                yield return list[i];
            }
            //yield return list[0];
            //yield return list[1];
            //yield return list[2];
            //yield return list[3];
            //yield return list[4];
            //yield return list[5];
            //yield return list[6];
            //yield return list[7];
        }
    }
    #endregion

    #region 用yield return 语法糖为泛型类实现迭代器
    class CustomList<T> : IEnumerable
    {
        private T[] array;

        //参数不确定时用params传递参数
        public CustomList(params T[] array)
        {
            this.array = array;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++) 
            {
                yield return array[i];
            }
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            CustomList list = new CustomList();

            //foreach本质
            //1.先获取in后面这个对象的IEnumerator
            //会调用其中的GetEnumerator方法 来获取（可以不继承IEnumerable）
            //2.执行得到这个IEnumerator对象中给的MoveNext方法
            //3.只要MoveNext方法返回值为true后得到Current属性后赋值给item

            //foreach (int item in list)
            //{
            //    Console.WriteLine(item);
            //}

            CustomList<string> list2 = new CustomList<string>("1234","223");
            foreach(string item in list2)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}

//总结：
//迭代器是可以让我们在外部直接通过foreach遍历对象中元素而不需要了解其结构
//主要两种方式
//1.传统方式 继承两个接口 实现里面的方法
//2.用语法糖yield return 去返回内容 只需要继承一个接口即可
