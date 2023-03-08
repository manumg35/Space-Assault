using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log(this.name + " KABOOM collisionated with "+ collision.gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {

        Debug.Log(this.name + " Meh triggered with "+ other.gameObject.name);
        SceneManager.LoadScene(0);
    }
    
}
