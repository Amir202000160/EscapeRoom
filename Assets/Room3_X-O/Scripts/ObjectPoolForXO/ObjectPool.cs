using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 5;
    public PooledObjectXO prefabX;
    public Transform Xlocation;
    public PooledObjectXO prefabO;
     public Transform Olocation;
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
            PooledObjectXO objX = Instantiate(prefabX,Xlocation.position , prefabX.transform.rotation);
            objX.ObjectPool = this;
            objX.gameObject.SetActive(false);
            poolX.Push(objX);

            PooledObjectXO objO = Instantiate(prefabO, Olocation.position, prefabO.transform.rotation);
            objO.ObjectPool = this;
            objO.gameObject.SetActive(false);
            poolO.Push(objO);
        }

    }

    public PooledObjectXO GetPooledObjectXO(bool isXtype)
    {
        if (isXtype)
        {
            if (poolX.Count > 0)
            {
                PooledObjectXO obj = poolX.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }
        }
        else
        {
            if (poolO.Count > 0)
            {
                PooledObjectXO obj = poolO.Pop();
                obj.gameObject.SetActive(true);
                return obj;
            }
        }
        return null; // No available objects in the pool
    }

    public void ReturnToPool(PooledObjectXO obj, bool isXType)
    {
        obj.gameObject.SetActive(false);

        if (isXType)
        {
            obj.transform.position = prefabX.transform.position;
            obj.transform.rotation = prefabX.transform.rotation;
            poolX.Push(obj);
        }
        else
        {
            obj.transform.position = prefabO.transform.position;
            obj.transform.rotation = prefabO.transform.rotation;
            poolO.Push(obj);
        }

        // Reset Rigidbody physics settings if component exists
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Re-enable collider if it was disabled
        BoxCollider bc = obj.GetComponent<BoxCollider>();
        if (bc != null)
        {
            bc.enabled = true;
        }
    }
}
