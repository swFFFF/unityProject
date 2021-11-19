using System;

namespace 排序进阶
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("排序");
            //TODO
            #region 希尔排序
            int[] arr = new int[] { 8, 7, 1, 5, 4, 2, 6, 3, 9 };
            //第一步：实现插入排序
            //第一层循环 使用来去除未排序区中的元素的
            //for(int i = 1; i < arr.Length; i++)
            //{
            //    //得出未排序区的元素
            //    int noSortNum = arr[i];
            //    //得出排序区中最后的一个元素索引
            //    int sortIndex = i - 1;
            //    //进入条件
            //    //首先排序区中还有可以比较的 >=0
            //    //排序区中的元素 满足交换条件 升序就是排序区中元素要大于未排序区的中元素
            //    while (sortIndex >= 0 && arr[sortIndex] > noSortNum) 
            //    {
            //        arr[sortIndex + 1] = arr[sortIndex];
            //        --sortIndex;
            //    }
            //    //找到位置后 真正的插入 值
            //    arr[sortIndex + 1] = noSortNum;
            //}

            //for(int i = 0; i < arr.Length; i++)
            //{
            //    Console.WriteLine(arr[i]);
            //}

            //第二步：确定步长
            //基本规则：每次步长变化都 /2
            //一开始步长就是数组长度的 /2
            //之后每次都在上一次步长基础上 /2
            //结束条件是步长 <=0
            //1.第一次的步长是数组长度/2 所以 int step = arr.length / 2
            //2.之后每次步长变化都是/2 所以 step /= 2
            //3.最小步长是1 所以：step > 0
            for (int step = arr.Length / 2; step > 0; step /= 2) 
            {
                //注意
                //每次得到步长后 会把该步长下所有序列都进行插入排序
                //第三步：执行插入排序
                //i=1代码 相当于 代表取出来的排序区的第一个元素
                //for(int i = 1; i < arr.Length; i++)
                //i=step 相当于代表取出来的排序区的第一个元素
                for (int i = step; i < arr.Length; i++)
                {
                    //得出未排序区的元素
                    int noSortNum = arr[i];
                    //得出排序区中最后的一个元素索引
                    //int sortIndex = i - 1;
                    //i - step 代表和子序列中 已排序区元素一一比较
                    int sortIndex = i - step;
                    //进入条件
                    //首先排序区中还有可以比较的 >=0
                    //排序区中的元素 满足交换条件 升序就是排序区中元素要大于未排序区的中元素
                    while (sortIndex >= 0 && arr[sortIndex] > noSortNum)
                    {
                        //arr[sortIndex + 1] = arr[sortIndex];
                        //代表移步长个位置 代表子序列中的下一个位置
                        arr[sortIndex + step] = arr[sortIndex];
                        //--sortIndex
                        //一个步长单位之间的比较
                        sortIndex -= step;
                    }
                    //找到位置后 真正的插入 值
                    //arr[sortIndex + 1] = noSortNum;
                    //现在是加步长个单位
                    arr[sortIndex + step] = noSortNum;
                }

                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            }
            #endregion
        }
    }
}
