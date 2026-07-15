using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    private Vector3 knockbackVelocity = Vector3.zero; ///* codigo nuestro
    [SerializeField]    ///* codigo nuestro
    private float knockbackDamping = 8f;    

    public void AddKnockback(Vector3 direction, float force) ///* codigo nuestro
    {
        direction.y = 0f;   ///* codigo nuestro
        direction.Normalize();      ///* codigo nuestro
        knockbackVelocity += direction * force; ///* codigo nuestro
    }
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));                        ///* codigo nuestro
        Vector3 movementVelocity = transform.rotation * new Vector3(input.x, 0, input.y) * targetMovingSpeed;       ///* codigo nuestro
        Vector3 finalVelocity = movementVelocity + knockbackVelocity;                                               ///* codigo nuestro
        finalVelocity.y = rigidbody.linearVelocity.y;                                                               ///* codigo nuestro
        rigidbody.linearVelocity = finalVelocity;                                                                   ///* codigo nuestro
        knockbackVelocity = Vector3.Lerp(knockbackVelocity, Vector3.zero, knockbackDamping * Time.fixedDeltaTime); ///* codigo nuestro

        // Get targetVelocity from input.
        ///* Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        ///* rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);
    }
}