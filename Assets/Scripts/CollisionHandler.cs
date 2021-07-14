using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadingDelay;

    [SerializeField] AudioClip winNoise;
    [SerializeField] AudioClip crashNoise;

    AudioSource myAudioSource;

    void Start() {
        myAudioSource = GetComponent<AudioSource>();
    }

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
        myAudioSource.PlayOneShot(crashNoise);
        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadingDelay);  // String method name ref's are passed to Invoke, which is a bit loose bhole brah
    }

    void StartSuccessSequence() // Sequence to run when player reaches landing pad
    {
        myAudioSource.PlayOneShot(winNoise);
        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadingDelay);        
    }

    void ReloadLevel()  // Reloads the current level
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // Grab the current scenes index
        SceneManager.LoadScene(currentSceneIndex);  // Load scene with the index grabbed above
    }

    void LoadNextLevel()    // Loads the next level in the build manager
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
