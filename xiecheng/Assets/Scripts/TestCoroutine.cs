using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyCoroutine
{
    private IEnumerator enumerator;

    public IEnumerator Enumerator
    {
        get
        {
            return enumerator;
        }
    }

    public MyCoroutine(IEnumerator enumerator)
    {
        this.enumerator = enumerator;
        this.enumerator.MoveNext();
    }

    private int delay_Time = 0;
    //判断条件 是否能往下执行
    public bool IsCanNext()
    {
        if(enumerator.Current == null)
        {
            if(delay_Time == 0)
            {
                delay_Time = 1;
                return false;
            }
            else
            {
                delay_Time = 0;
                return true;
            }
        }
        return true;
    }

    public bool MoveNext()
    {
        bool b = this.Enumerator.MoveNext();
        if(IsCanNext())
        {
            return MoveNext();
        }
        return b;
    }
}

public class TestCoroutine : MonoBehaviour
{
    //// 一Start is called before the first frame update
    //void Start()
    //{
    //    //开启协程
    //    //StartCoroutine();
    //    //结束协程
    //    //StopCoroutine();
    //    //结束所有协程
    //    //StopAllCoroutines();

    //    StartCoroutine(this.Test1());
    //}

    //public IEnumerator Test1()
    //{
    //    while (true)
    //    {
    //        //yield return null;
    //        //yield return new WaitForSeconds(2);
    //        yield return StartCoroutine(this.Test2());//挂起
    //        Debug.Log("Test1");
    //    }
    //}

    //public IEnumerator Test2()
    //{
    //    yield return new WaitForSeconds(3);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Debug.Log("update");
    //}

    List<MyCoroutine> myCoroutines = new List<MyCoroutine>();

    public MyCoroutine StartMyCoroutine(IEnumerator enumerator)
    {
        MyCoroutine myCoroutine = new MyCoroutine(enumerator);
        myCoroutines.Add(myCoroutine);
        return myCoroutine;
    }

    public void StopMyCoroutine(MyCoroutine myCoroutine)
    {
        myCoroutines.Remove(myCoroutine);
    }


    private void CheckMyCoroutine()
    {
        for (int i = 0; i < myCoroutines.Count; i++)
        {
            if (myCoroutines[i].IsCanNext())
            {
                if (myCoroutines[i].MoveNext() == false)
                {
                    StopMyCoroutine(myCoroutines[i]);
                }
            }
        }
    }

    private void Start()
    {
        //StartCoroutine(this.Test1());
        StartMyCoroutine(this.Test1());
    }
    private void Update()
    {
        Debug.Log("Update");
        CheckMyCoroutine();
    }

    public IEnumerator Test1()
    {
        yield return null;
        Debug.Log("test1");
        yield return null;
        Debug.Log("test2");
        yield return null;
        Debug.Log("test3");
    }
}

public class Test1 : IEnumerator
{
    public object[] objs = new object[3];
    private int position = -1;
    public object Current
    {
        get
        {
            return objs[position];
        }
    }

    public bool MoveNext()
    {
        position++;
        if (position > 0)
        {
            Debug.Log("Test" + position);
        }
        if (position < objs.Length) { return true; } else { return false; };

    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }
}