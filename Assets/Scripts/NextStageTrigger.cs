using UnityEngine;

public class NextStageTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null && gm.CanGoNext())
            {
                gm.LoadNextStage();
            }
        }
    }
}
