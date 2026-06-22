using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isO = false;
    public bool isX = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("O") && !Cam.Instance.IsGrabbed)
        {
            isO = true;
            HoldingtheSymbol(other.gameObject);
            Debug.Log("O is in the cell");

        }
        else if (other.CompareTag("X") && !Cam.Instance.IsGrabbed)
        {
            isX = true;
            HoldingtheSymbol(other.gameObject);
            Debug.Log("X is in the cell");  
        }
    }

    public void HoldingtheSymbol(GameObject other)
    {
        other.gameObject.transform.SetParent(transform);
        other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.transform.localPosition = new Vector3(0, 0, -0.5f);
        other.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;


    }
}
