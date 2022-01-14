using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class startButtonScript : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    private SphereCollider leftCollider;
    private SphereCollider rightCollider;
    private BoxCollider ownCollider;
    private ActionBasedController leftController;
    private ActionBasedController rightController;
    void Start()
    {
        leftCollider = leftHand.GetComponent<SphereCollider>();
        rightCollider = rightHand.GetComponent<SphereCollider>();
        ownCollider = GetComponent<BoxCollider>();

        leftController = leftHand.GetComponent<ActionBasedController>();
        rightController = rightHand.GetComponent<ActionBasedController>();

    }
    void Update()
    {
        if (ownCollider.bounds.Contains(leftCollider.bounds.center)
            || ownCollider.bounds.Contains(rightCollider.bounds.center))
            if (rightController.activateAction.action.ReadValue<float>() >= 0.2 ||
                leftController.activateAction.action.ReadValue<float>() >= 0.2)
                SceneManager.LoadScene("SampleScene");
    }
}
