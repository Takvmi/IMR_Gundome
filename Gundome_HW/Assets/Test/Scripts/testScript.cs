using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class testScript : MonoBehaviour
{

    public GameObject PlayerMecha;

    public float FlySpeed;

    public GameObject RightHandController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<InputDevice> handDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, handDevices);
        if (handDevices.Count == 1)
        {
            CheckController(handDevices[0]);
        }
        else Debug.Log("NO HANDS!");
    }

    private void CheckController(InputDevice d)
    {
        bool primaryButtonDown = false;
        bool secondaryButtonDown = false;
        Vector2 JoystickAxis;
        d.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonDown);
        d.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonDown);
        if(primaryButtonDown) MoveForward();
        if(secondaryButtonDown) MoveBackwards();

        d.TryGetFeatureValue(CommonUsages.secondary2DAxis, out JoystickAxis);
        if(JoystickAxis.x > 0.1) MoveForward();
        else if(JoystickAxis.x < -0.1) MoveBackwards();
    }

    public void MoveForward() => PlayerMecha.gameObject.transform.position = new Vector3(
        PlayerMecha.gameObject.transform.position.x + FlySpeed, PlayerMecha.gameObject.transform.position.y,
        PlayerMecha.gameObject.transform.position.z);
    
    public void MoveBackwards() => PlayerMecha.gameObject.transform.position = new Vector3(
        PlayerMecha.gameObject.transform.position.x - FlySpeed, PlayerMecha.gameObject.transform.position.y,
        PlayerMecha.gameObject.transform.position.z);
}
