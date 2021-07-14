using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into a FRIENDLY object");
                break;
            case "Fuel":
                Debug.Log("Refuel");
                break;
            case "Finish":
                Debug.Log("You've reached the FINISH, yay!");
                break;
            default:
                Debug.Log("You hit an obstacle, BOOOOM!");
                break;

        }   
    }
}
