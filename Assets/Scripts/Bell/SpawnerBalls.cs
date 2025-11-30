using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnerBalls : MonoBehaviour
{
    public GameObject ballPrefab;
    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.selectEntered.AddListener(OnGrab);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        GameObject clone = Instantiate(ballPrefab, transform.position, transform.rotation);

        var interactor = args.interactorObject;

        var cloneGrab = clone.GetComponent<XRGrabInteractable>();
        args.manager.SelectEnter(interactor, cloneGrab);

        args.manager.SelectExit(interactor, grab);
    }
}
