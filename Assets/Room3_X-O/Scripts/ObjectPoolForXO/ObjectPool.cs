using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 5;
    public PooledObjectXO prefabX;
    public PooledObjectXO prefabO;
    private Stack<PooledObjectXO> poolO;
    private Stack<PooledObjectXO> poolX;


    void Start()
    {
        SetUpPool();
    }


    public void SetUpPool()
    {
        poolO = new Stack<PooledObjectXO>();
        poolX = new Stack<PooledObjectXO>();
        for (int i = 0; i < poolSize; i++)
        {
            PooledObjectXO objX = Instantiate(prefabX);
            objX.gameObject.SetActive(false);
            poolX.Push(objX);
            PooledObjectXO objO = Instantiate(prefabO);
            objO.gameObject.SetActive(false);
            poolO.Push(objO);
        }

    }

    //public PooledObjectXO GetPooledObjectXO(bool isXtype)
    //{
    //    if (isXtype)
    //    {
    //        if (poolX.Count > 0)
    //        {
    //            PooledObjectXO obj = poolX.Pop();
    //            obj.gameObject.SetActive(true);
    //            return obj;
    //        }
    //    }
    //    else
    //    {
    //        if (poolO.Count > 0)
    //        {
    //            PooledObjectXO obj = poolO.Pop();
    //            obj.gameObject.SetActive(true);
    //            return obj;
    //        }
    //    }
    //    return null; // No available objects in the pool
    //}

    public void ReturnToPool(PooledObjectXO obj, bool isXType)
    {
        
        obj.gameObject.SetActive(false);

        if (isXType)
        {
            poolX.Push(obj);
        }
        else
        {
            poolO.Push(obj);
        }
    }
}
