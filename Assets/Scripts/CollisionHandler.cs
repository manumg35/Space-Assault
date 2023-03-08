using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay=1f;
    [SerializeField] ParticleSystem explosionVFX;
    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(this.name + " KABOOM collisionated with "+ collision.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        explosionVFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }


}
