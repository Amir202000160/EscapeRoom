using UnityEngine;

public class PooledObjectXO : MonoBehaviour
{
    private ObjectPool objectPool;  
    public ObjectPool ObjectPool
    {
        get { return objectPool; }
        set { objectPool = value; }
    }
    
    public  void Release()
    {
        objectPool.ReturnToPool(this, this.gameObject.CompareTag("X"));
    }
}
