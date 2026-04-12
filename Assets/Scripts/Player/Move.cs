using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    Rigidbody RB;
    InputAction moveAction;
    [SerializeField] Transform cam;
    [SerializeField] float moveSpeed = 50f;
    [SerializeField] float maxVelocity = 100f;

    void Start()
    {
          moveAction = InputSystem.actions.FindAction("Move");
          RB = GetComponent<Rigidbody>();   
        RB.linearDamping = 5f; // Adjust the linear damping value as needed
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
        //Debug.Log("Current Velocity: " + RB.linearVelocity.magnitude);
    }

     private void move()
    {

        Vector3 moveValue = moveAction.ReadValue<Vector3>() ;
       // Debug.Log("Move Value: " + moveValue);
        //Vector3 modifiedValue= moveAction.ReadValue<ModifiableContactPair>() * Time.deltaTime;
        Vector3 cameraForward = cam.forward;
        Vector3 cameraRight = cam.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create movement direction relative to camera
        Vector3 moveDirection = (cameraForward * moveValue.z + cameraRight * moveValue.x);
        if(RB.linearVelocity.magnitude < maxVelocity)
        {
           // RB.linearVelocity = RB.linearVelocity.normalized * maxVelocity;
            RB.AddForce(moveDirection * moveSpeed *Time.deltaTime, ForceMode.VelocityChange);
            Debug.Log("Current Velocity: " + RB.linearVelocity.magnitude);
            
        }
        
       

    }
}
