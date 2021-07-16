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

    // Particle Vars
    [SerializeField] ParticleSystem boosterParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

    
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
            startThrusting();   // Apply force, play main thrust audio, play particle FX
        }
        else    // Stop the audio and dont apply any force, as thrust is no longer being held down
        {
            stopThrusting();    // Stop all the thrusting audio and FX, no force will be applied 
        }

    }

    void ProcessRotation()
    {
                    
        if(Input.GetKey(inputLeft) && Input.GetKey(inputRight) == false)
        {
            RotateLeft();   // This will handle movement and particles
        }
        else if (Input.GetKey(inputRight) && Input.GetKey(inputLeft) == false)
        {
            RotateRight();  // ^ Ditto ^
        }
        else
        {
            StopRotation();
        }

    }

    private void startThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); // Vector3.up shorthand for 0, 1, 0

        if (!audioPlaying)  // Play audio if it is not currently playing
        {
            myAudioSource.PlayOneShot(mainThrusterFX);  // Play Audio
            audioPlaying = true;
        }

        if (!boosterParticles.isPlaying) // Visual booster FX
        {
            boosterParticles.Play();
        }
    }

    private void stopThrusting()    
    {
        myAudioSource.Stop();
        audioPlaying = false;

        boosterParticles.Stop();
    }

    private void RotateLeft()
    {
        ApplyRotation(sideThrust); // rotate left

        // Right thruster on to turn left
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-sideThrust); // rotate right

        // Left thruster on to turn right
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    private void StopRotation()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   // Freeze rotation, the physics system would cause a bug with rotation when using transform
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  // Unfreeze rotation, let the physics system have a say on the situation again
    }
}
