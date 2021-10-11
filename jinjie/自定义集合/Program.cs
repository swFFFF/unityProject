using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 自定义集合迭代器
{
    public class MySet<T> : IEnumerable
    {
        #region 字段
        private int defaultSize = 4;
        private T[] items;
        private int _size;
        private T[] emptyArr = new T[0];
        #endregion

        #region 属性
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public T this[int index]
        {
            get
            {
                if (items == null)
                {
                    return default(T);
                }

                if (index < 0 || index >= items.Length)
                {
                    throw new Exception("数组下标异常");
                }

                return items[index];
            }

            set
            {
                if (items == null)
                {
                    return;
                }

                if (index < 0 || index >= items.Length)
                {
                    throw new Exception("数组下标异常");
                }

                items[index] = value;
            }
        }
        #endregion

        #region 方法
        public MySet()
        {
            items = emptyArr;
        }

        public MySet(int size)
        {
            items = new T[size];
        }

        //添加数据
        public void Add(T item)
        {
            if (item == null)
            {
                throw new Exception("添加元素不能为空");
            }

            if (_size == items.Length)
            {
                int num = (items.Length == 0) ? defaultSize : items.Length * 2;
                SetCapacity(num);
            }
            items[_size] = item;
            _size++;
        }

        //添加数据
        public bool Remove(T item)
        {
            if (item == null)
            {
                throw new Exception("移除的元素不能为空");
            }

            // 找到移除元素下标
            int index = IndexOf(item);
            //后面元素往前移动
            if (index >= 0)
            {
                Array.Copy(items, index + 1, items, index, _size - index - 1);
                //最后元素设置为空
                items[_size - 1] = default(T);

                //长度--
                _size--;
                return true;
            }
            return false;
        }

        //清空数据
        public void Clear()
        {
            if (_size > 0)
            {
                Array.Clear(items, 0, _size);
                _size = 0;
            }
        }

        public void SetCapacity(int lenghth)
        {
            if (lenghth <= _size)
            {
                throw new Exception("扩容长度不能比当前长度小");
            }
            T[] array = new T[lenghth];
            if (_size > 0)
            {
                Array.Copy(items, array, _size);
            }
            items = array;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _size; i++)
            {
                if (items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public IEnumerator GetEnumerator()
        {
            //return new MySetIEnumerator<T>(this);
            for (int i = 0; i < _size; i++)
            {
                yield return items[i];
            }
        }

        #endregion
    }

    public class MySetIEnumerator<T> : IEnumerator
    {

        private int position;
        private MySet<T> mySet;

        public MySetIEnumerator(MySet<T> mySet)
        {
            position = -1;
            this.mySet = mySet;
        }

        public object Current
        {
            get
            {
                return mySet[position];
            }
        }

        public bool MoveNext()//迭代器会先调用一次
        {
            position++;
            if (position < mySet.Count)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            position = -1;
        }
    }
}