using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionExamples1 : MonoBehaviour
{

    // Entering Collision 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "Floor")
        {
            print("Object collding with Ethan is: " + collision.collider.name.ToString());
            Destroy(collision.collider.gameObject);
        }
    }

    // Staying in collsion
    private void OnCollisionStay(Collision collision)
    {
        print("I am colliding with: " + collision.collider.name);
    }

    // Exit the collsion
    private void OnCollisionExit(Collision collision)
    {
        print("Just exited the collsion!");
    }
}
