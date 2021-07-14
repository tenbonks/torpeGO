using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadingDelay;

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into a FRIENDLY object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            // case "Fuel":
            //     Debug.Log("Refuel");
            //     break;
            default:
                StartCrashSequence();
                break;

        }   
    }

    void StartCrashSequence()   // Disable movement and reload the level
    {
        //Add sfx
        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadingDelay);  // String method name ref's are passed to Invoke, which is a bit loose bhole brah
    }

    void StartSuccessSequence()
    {
        //Add sfx
        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadingDelay);        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)  
        {
            // If they are on the last level, restart it from the first level (index 0)
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex); 
    }
}
