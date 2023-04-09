using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyCrashParticles;
    [SerializeField] Transform parentForSpawnAtRuntime;
    [Tooltip("Points Player gets on hitting the Enemy with Bullet")]
    [SerializeField] int pointsPerHit = 1;
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void KillEnemy()
    {
        var vfxObject = Instantiate(enemyCrashParticles, transform.position, Quaternion.identity);
        vfxObject.transform.parent = parentForSpawnAtRuntime;
        Destroy(gameObject);
    }

    void ProcessHit()
    {
        scoreBoard.IncreaseScore(pointsPerHit);
    }
}
