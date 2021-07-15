using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadingDelay;

    [SerializeField] AudioClip winNoise;
    [SerializeField] AudioClip crashNoise;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    

    bool isTransitioning = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {

        if (isTransitioning) {return;} //  if the player is in a transitioning state, ignore collisions

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You've bumped into a FRIENDLY object");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;

        }   
    }

    void StartCrashSequence()   // Disable movement and reload the level
    {
        
        audioSource.Stop();
        audioSource.PlayOneShot(crashNoise);
        
        crashParticles.Play();

        isTransitioning = true;

        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadingDelay);  // String method name ref's are passed to Invoke, which is a bit loose bhole brah
    }

    void StartSuccessSequence() // Sequence to run when player reaches landing pad
    {

        audioSource.Stop();
        audioSource.PlayOneShot(winNoise);

        successParticles.Play();

        isTransitioning = true; // This defaults to first, so when the level is reloaded it will be set back

        //Add Particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", loadingDelay);        
    }

    void ReloadLevel()  // Reloads the current level
    {
        
        isTransitioning = false;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // Grab the current scenes index

        SceneManager.LoadScene(currentSceneIndex);  // Load scene with the index grabbed above
    }

    void LoadNextLevel()    // Loads the next level in the build manager
    {

        isTransitioning = false;

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
