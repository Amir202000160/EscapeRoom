using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Cam : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float mouseSenstivity ;
   [SerializeField] LayerMask Enemylayer;
    InputAction LookAction;
    InputAction fireAction;
    [HideInInspector]
    public float angle;
    float xRotate;
    Ray ray;
    private Transform currentlyGrabbedObject = null;
    

    
    void Start()
    {
        LookAction = InputSystem.actions.FindAction("Mouse");
        fireAction = InputSystem.actions.FindAction("Attack");
        UnityEngine.Cursor.visible =false;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 LookValue = LookAction.ReadValue<Vector2>() *mouseSenstivity * Time.deltaTime;
        float xRotate = -LookValue.y;   
        //xRotate = Mathf.Clamp(xRotate, -900f, 900f);

        transform.rotation *= Quaternion.Euler(xRotate, 0, 0);
        //transform.Rotate(Vector3.right * xRotate);
        //transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
        Player.transform.Rotate(Vector3.up * LookValue.x);


        //transform.position = Player.transform.position;
        angle = Mathf.Atan2(LookValue.y, LookValue.x) * Mathf.Rad2Deg;
       // Debug.Log(angle);
        raycast();

    }

    public void raycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);   
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 10f, Enemylayer))
        {
           if(fireAction.IsPressed() && hitInfo.collider.tag == hitInfo.collider.name)
            {
                Debug.Log("Hit" + hitInfo.collider.gameObject.name);
                currentlyGrabbedObject = hitInfo.transform;
                Vector3 targetPosition = ray.GetPoint(2.1f);
                currentlyGrabbedObject.position = targetPosition;
                currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else
            {
                currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            }
       
        
        }
       
        void onDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray);
        }

    }
    
}