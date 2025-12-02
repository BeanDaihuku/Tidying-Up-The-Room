using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; 

public class MikanBox : MonoBehaviour
{
    [SerializeField] private GameObject mikanPrefab; 
    [SerializeField] private float respawnTime = 0.5f;

    private GameObject currentMikan;

    void Start()
    {
        SpawnMikan();
    }

    void SpawnMikan()
    {
        currentMikan = Instantiate(mikanPrefab, transform.position, transform.rotation);

        var grabInteractable = currentMikan.GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnMikanGrabbed);
        }
    }

    private void OnMikanGrabbed(SelectEnterEventArgs args)
    {
        var grabInteractable = currentMikan.GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.RemoveListener(OnMikanGrabbed);

        StartCoroutine(WaitAndSpawn());
    }

    System.Collections.IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnMikan();
    }
}