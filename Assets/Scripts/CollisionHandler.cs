using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticles;
    AudioSource crashAudioSource;
    ScoreBoard scoreBoard;
    bool disableCrash;
    bool isTransitioning;

    void Start()
    {
        crashAudioSource = GetComponent<AudioSource>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            disableCrash = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTransitioning)
        {
            return;
        }

        if (!disableCrash && scoreBoard.Health <= 0)
        {
            StartCrashSequence();
        }
        else
        {
            scoreBoard.DecreaseHealth(20);
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        crashAudioSource.Play();
        crashParticles.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", 1.5f);
    }

    void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
