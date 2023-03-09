using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    
    [SerializeField] int scorePerHit=15;
    [SerializeField] int healthPoints = 8;

    Rigidbody rb;
    ScoreBoard scoreBoard;
    GameObject parent;
    void Start()
    {
        AddRigidbody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnObjs");
    }

    private void AddRigidbody()
    {
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        
        if (healthPoints < 1)
        {
            KillEnemy();
        }
        
    }

    void ProcessHit(GameObject obj)
    {
        Vector3 LocationOfHit = transform.position;
        ParticleSystem laserParticles = obj.gameObject.GetComponent<ParticleSystem>();
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int numParticleCollisions = laserParticles.GetCollisionEvents(this.gameObject, collisionEvents);

        if (numParticleCollisions > 0)
        {
            LocationOfHit = collisionEvents[0].intersection;
        }


        GameObject vfx = Instantiate(hitVFX, LocationOfHit, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        healthPoints--;
        scoreBoard.UpdateScore(scorePerHit);
        
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }

    
}
