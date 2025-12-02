using UnityEngine;

public class MikanProjectileVR : MonoBehaviour
{
    [Header("ê›íË")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float minVelocity = 2.0f; 
    [SerializeField] private float lifeTime = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (rb.velocity.magnitude < minVelocity) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            BearController bear = collision.gameObject.GetComponent<BearController>();
            if (bear != null)
            {
                bear.TakeDamage(damage);
                
            }
            Destroy(gameObject);
        }
    }
}