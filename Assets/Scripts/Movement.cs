using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement keys set in unity inspector
    [SerializeField] KeyCode inputThrust;
    [SerializeField] KeyCode inputLeft;
    [SerializeField] KeyCode inputRight;

    // Thrust values set in unity inspector
    [SerializeField] float mainThrust = 10f;    // Default value, not really needed
    [SerializeField] float sideThrust = 0.3f;

    //Audio variables set in unity
    [SerializeField] AudioClip mainThrusterFX;
    [SerializeField] AudioClip sideThrusterFX;  // Not figured out how to implement this sound... sound glitchy when on first attempt

    Rigidbody rb;

    AudioSource myAudioSource;
    bool audioPlaying;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Cache a reference of the rigidbody
        myAudioSource = GetComponent<AudioSource>();
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
            
            if (!audioPlaying)  // Only play the audio if it is not playing
            {
                myAudioSource.PlayOneShot(mainThrusterFX);
                audioPlaying = true;
            }
        }
        else    // Stop the audio and dont apply any force, as thrust is no longer being held down
        {
            myAudioSource.Stop();
            audioPlaying = false;
        }

    }

    void ProcessRotation()
    {

            if(Input.GetKey(inputLeft) && Input.GetKey(inputRight) == false)
            {
                ApplyRotation(sideThrust); // rotate left
                //myAudioSource.PlayOneShot(sideThrusterFX);
            }

            if (Input.GetKey(inputRight) && Input.GetKey(inputLeft) == false)
            {
                ApplyRotation(- sideThrust); // rotate right
                //myAudioSource.PlayOneShot(sideThrusterFX);
            }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // Freeze rotation, the physics system would cause a bug with rotation when using transform
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreeze rotation, let the physics system have a say on the situation again
    }
}
