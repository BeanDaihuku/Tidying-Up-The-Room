using UnityEngine;
using UnityEngine.AI; 

public class BearController : MonoBehaviour
{
    [Header("ê›íË")]
    [SerializeField] private float hp = 30f; 
    [SerializeField] private string targetTag = "Nabe"; 

    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject nabeObj = GameObject.FindGameObjectWithTag(targetTag);
        if (nabeObj != null)
        {
            target = nabeObj.transform;
            agent.SetDestination(target.position); 
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("ÉNÉ} écÇËHP: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}