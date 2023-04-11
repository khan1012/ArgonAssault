using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem crashParticles;
    ScoreBoard scoreBoard;
    bool disableCrash;

    void Start()
    {
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
