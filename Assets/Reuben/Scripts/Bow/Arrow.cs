using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Rigidbody rb;

    public LayerMask layerMask;

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if (rb != null)
        {
            pointInMovementDirection();            
        }
    }

    void pointInMovementDirection()
    {
        transform.LookAt(transform.position + rb.linearVelocity, Vector3.up);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            Debug.Log("Collision with " + collision.gameObject.name);
            rb.constraints = RigidbodyConstraints.FreezeAll; 
        }
    }
}

    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         Physics.IgnoreCollision(other, GetComponent<Collider>());
    //     }
    //     else
    //     {
    //         Destroy(rb);
    //     }
    // }
