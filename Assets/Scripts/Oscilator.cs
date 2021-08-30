using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{

    Vector3 startingPostion;
    [SerializeField] Vector3 movementVector;    // Ill leave this as serialize field, as can be used on multiple objects

    float movementFactor;     // Range will in square brackets will display a slider in unity inspector
    [SerializeField] float period = 2f;


    // Upon running, initalise the startingPostion with the current position of the transform
    void Start()
    {
        startingPostion = transform.position;
    }

    // Update is called once per frame
    void Update() // Calculate how much the object should move and apply it to transform
    {

        // Dangerous to check floats if they == 0, use Epsilon function instead, an epsilon is a very small 
        if (period <= Mathf.Epsilon) {return; } // This was the instructed method of protecting against nan, (just stop the code causing error to run)

        // Time.time is pretty much the current uptime of the running game, this resets when stopped
        float cycles = Time.time / period;  // After "period" amount of seconds, a full cycle has taken place, will continually grow
        const float tau = Mathf.PI * 2;   // Constant value of 6.823
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f)  / 2f;   // recalculate the rawSinOutput to range from 0 to 1

        // If rawSinWave is not recalucalted the starting point would now be a mid point, the range of movement would be twice as long 

        //Debug.Log(rawSineWave); //Outputs between -1 to 1 : Successful sine wave

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPostion + offset;
    }
}
