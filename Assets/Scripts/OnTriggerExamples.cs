using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerExamples : MonoBehaviour
{
    public GameObject[] spheres;

    void OnTriggerEnter()
    {
        print("Entered trigger!");
        foreach(GameObject sphere in spheres)
        {
            sphere.GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    void OnTriggerStay()
    {
        print("Staying inside the trigger");
    }

    void OnTriggerExit()
    {
        print("Just existed the trigger");
    }
}
