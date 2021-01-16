using UnityEngine;
using UnityEngine.Events;

public class TargetCollision : MonoBehaviour
{
    public UnityEvent onCollisionEvent;

    public string colliderTag;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(colliderTag))
        {
            if (onCollisionEvent != null)
            {
                onCollisionEvent.Invoke();
            }
        }
    }
}
