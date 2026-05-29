using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isO = false;
    public bool isX = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("O"))
        {
            isO = true;
            HoldingtheSymbol(other.gameObject);
        }
        else if (other.CompareTag("X"))
        {
            isX = true;
            HoldingtheSymbol(other.gameObject);
        }
    }

    public void HoldingtheSymbol(GameObject other)
    {
        other.gameObject.transform.SetParent(transform);
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
}
