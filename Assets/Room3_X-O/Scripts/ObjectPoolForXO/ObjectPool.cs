using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public int poolSize = 5;
    public PooledObjectXO prefabX;
    public PooledObjectXO prefabO;
    private Stack<PooledObjectXO> poolXO;
   

    void Start()
    {
        SetUpPool();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetUpPool()
    {

    }
}
