using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CallDebugKey();

       
    }

    void CallDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            loadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe - Touched the launch pad");
                break;
            case "Finish":
                Debug.Log("Victory!");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel boost!");
                break;
            default:
                Debug.Log("Boom, you're dead!");
                StartCrashSequence();
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

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadLevel", 1);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", 1);
    }

}
