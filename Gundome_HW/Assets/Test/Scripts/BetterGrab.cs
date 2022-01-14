using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BetterGrab : XRGrabInteractable
{
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    private bool isHeld;

    public GameObject fakeHand;
    public GameObject glowPads;

    public Transform originPosition;
    public Rigidbody ownRigidbody;

    void Start()
    {
        isHeld = false;
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        GetComponent<Rigidbody>().isKinematic = false;
        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    private void Update()
    {
        if (isHeld) return;
        transform.position = originPosition.position;
        transform.rotation = originPosition.rotation;

    }

    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
            GetComponent<MeshRenderer>().enabled = false;
            isHeld = true;
            glowPads.SetActive(false);
            fakeHand.SetActive(true);
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }
        base.OnSelectEntering(interactor);
    }
    
    protected override void OnSelectExiting(XRBaseInteractor interactor)
    {
        if (interactor is XRDirectInteractor)
        {
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;
            GetComponent<MeshRenderer>().enabled = true;
            isHeld = false;
            glowPads.SetActive(true);
            fakeHand.SetActive(false);
        }
        else
        {
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }
        transform.position = originPosition.position;
        transform.rotation = originPosition.rotation;
        base.OnSelectExiting(interactor);
    }
}
