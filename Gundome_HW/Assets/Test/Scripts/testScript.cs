using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class testScript : MonoBehaviour
{

    public GameObject PlayerMecha;

    public float FlySpeed;
    public float RotationSpeed;

    public Transform leftHandle;
    public Transform rightHandle;

    private Vector3 direction;
    private Rigidbody mechaBody;

    void Start()
    {
        mechaBody = PlayerMecha.gameObject.GetComponent<Rigidbody>();

    }


    void Update()
    {
        
        if(rightHandle.transform.rotation.z >= 0.2) SlideBackwards();
        else if(rightHandle.transform.rotation.z <= -0.2) SlideForwards();
        
        if(rightHandle.transform.rotation.x >= 0.2) RotateLeft();
        else if(rightHandle.transform.rotation.x <= -0.2) RotateRight();
        
        if(leftHandle.transform.rotation.x >= 0.2) RotateForwards();
        else if(leftHandle.transform.rotation.x <= -0.2) RotateBackwards();

        List<InputDevice> handDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, handDevices);
        if (handDevices.Count == 1)
        {
            CheckController(handDevices[0]);
        }
    }

    private void CheckController(InputDevice d)
    {
        bool primaryButtonDown = false;
        bool secondaryButtonDown = false;
        Vector2 JoystickAxis;
        d.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonDown);
        d.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonDown);
        if(primaryButtonDown) SlideBackwards();
        if(secondaryButtonDown) SlideBackwards();

        d.TryGetFeatureValue(CommonUsages.secondary2DAxis, out JoystickAxis);
        if(JoystickAxis.x > 0.1) SlideBackwards();
        else if(JoystickAxis.x < -0.1) SlideBackwards();
    }

    public void SlideForwards()
    {
        var localVelocity = PlayerMecha.transform.InverseTransformDirection(mechaBody.velocity);
        localVelocity.x = FlySpeed;
        mechaBody.velocity = PlayerMecha.transform.TransformDirection(localVelocity);
    }
    
    public void SlideBackwards()
    {
        var localVelocity = PlayerMecha.transform.InverseTransformDirection(mechaBody.velocity);
        localVelocity.x = -FlySpeed;
        mechaBody.velocity = PlayerMecha.transform.TransformDirection(localVelocity);
    }

    public void SlideLeft()
    {
        
    }

    public void SlideRight()
    {
        
    }

    public void RotateLeft()
    {
        var addRotation = new Vector3(1,0,0);
    }

    public void RotateRight()
    {
        var addRotation = new Vector3(-1,0,0);
        PlayerMecha.gameObject.transform.Rotate(addRotation);
    }

    public void RotateForwards()
    {
        var addRotation = new Vector3(0,0,1);
        PlayerMecha.gameObject.transform.Rotate(addRotation);
    }
    
    public void RotateBackwards()
    {
        var addRotation = new Vector3(0,0,-1);
        PlayerMecha.gameObject.transform.Rotate(addRotation);
    }

}
