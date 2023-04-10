using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyCrashParticles;
    [SerializeField] Transform parentForSpawnAtRuntime;
    [Tooltip("Enemy Damage per bullet out of 100")]
    [SerializeField] int pointsPerHit = 25;
    int Health = 100;
    ScoreBoard scoreBoard;

    void Start()
    {
        AddRigidBody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentForSpawnAtRuntime = GameObject.FindWithTag("SpawnAtRunTime").transform;
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (Health < 0)
        {
            KillEnemy();
        }
    }

    void KillEnemy()
    {
        var vfxObject = Instantiate(enemyCrashParticles, transform.position, Quaternion.identity);
        vfxObject.transform.parent = parentForSpawnAtRuntime;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        HitAnimation();
        Health -= pointsPerHit;
        scoreBoard.IncreaseScore(pointsPerHit);
    }

    void HitAnimation()
    {
        var material = gameObject.GetComponent<Renderer>().material;

        for (float t = 0; t < 1.0f; t += 0.1f)
        {
            material.color = Color.LerpUnclamped(Color.red, Color.white, t);
        }
    }
}
