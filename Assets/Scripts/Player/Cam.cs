using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Cam : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float mouseSenstivity;
    InputAction LookAction;
    public InputAction fireAction;
    [HideInInspector]
    public float angle;
    public bool IsGrabbed = false;  
    public static Cam Instance { get; private set; }
    private Transform currentlyGrabbedObject = null;
    

    private void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LookAction = InputSystem.actions.FindAction("Mouse");
        fireAction = InputSystem.actions.FindAction("Attack");
        MousueLocked();
        fireAction.performed +=ButtonClicked; 
        fireAction.canceled += ButtonReleased;
    }
    private void OnDisable()
    {
        fireAction.performed -= ButtonClicked;
        fireAction.canceled -= ButtonReleased;
    }   

    public void ButtonClicked(InputAction.CallbackContext context)
    {
        OnHold();
    }
    
    public void ButtonReleased(InputAction.CallbackContext context)
    {
        OnRelease();
    }
   
    
    public void OnHold()
    {
        if(currentlyGrabbedObject != null)
            currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        IsGrabbed = true;
        
    }
    public void OnRelease()
    {
        if (currentlyGrabbedObject != null)
            currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
     IsGrabbed = false; 
     currentlyGrabbedObject = null;

    }
        // Update is called once per frame
        void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            UnMousueLocked();
        }
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            MousueLocked();
        }
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
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 10f))
        {
           if( fireAction.IsPressed() && hitInfo.collider.tag == hitInfo.collider.name)
            {
                Debug.Log("Hit" + hitInfo.collider.gameObject.name);
                currentlyGrabbedObject = hitInfo.transform;
                Vector3 targetPosition = ray.GetPoint(2.1f);
                currentlyGrabbedObject.position = targetPosition;
               // currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            //else
            //{
            //    if(currentlyGrabbedObject != null)
            //        currentlyGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            //}
       
        
        }
       
        void onDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray);
        }

    }
    public void MousueLocked()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        //UnityEngine.Cursor.visible = false;
    }
    public void UnMousueLocked()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        //UnityEngine.Cursor.visible = true;
    }
}