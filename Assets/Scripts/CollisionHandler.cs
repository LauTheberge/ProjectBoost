using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe - Touched the launch pad");
                break;
            case "Finish":
                Debug.Log("Victory!");
                loadNextLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel boost!");
                break;
            default:
                Debug.Log("Boom, you're dead!");
                reloadLevel();
                break;
        }
    }

    void reloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevel);
    }

    void loadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = currentLevel+ 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else { SceneManager.LoadScene(nextLevel); }

    }
}
