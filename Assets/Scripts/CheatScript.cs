using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour
{

    [SerializeField] KeyCode skipLevelKey;
    [SerializeField] KeyCode godModeKey;

    void Update() 
    {
        SkipToNextLevel();    
    }

    void SkipToNextLevel()
    {
        if (Input.GetKey(skipLevelKey))
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

}
