using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement keys set in unity inspector
    [SerializeField] KeyCode inputThrust;
    [SerializeField] KeyCode inputLeft;
    [SerializeField] KeyCode inputRight;

    [SerializeField] float mainThrust = 10f;
    [SerializeField] float sideThrust = 0.3f;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Cache a reference of the rigidbody
    }

    // Update is called once per frame
    void Update()
    {

        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if(Input.GetKey(inputThrust))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Vector3.up shorthand for 0, 1, 0
        }

    }

    void ProcessRotation()
    {

        if(Input.GetKey(inputLeft) && Input.GetKey(inputRight) == false)
        {
            ApplyRotation(sideThrust); // rotate left
        }

        if (Input.GetKey(inputRight) && Input.GetKey(inputLeft) == false)
        {
            ApplyRotation(- sideThrust); // rotate right
        }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // Freeze rotation, so manual input doesn't conflict with physics
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreeze rotation, let the physics system have a say on the situation again
    }
}
