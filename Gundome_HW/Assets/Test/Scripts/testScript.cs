using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class testScript : MonoBehaviour
{

    public GameObject PlayerMecha;
    public GameObject MechaGun;
    public GameObject Reticle;
    public Transform FirePoint;
    public GameObject TheBullet;
    public ParticleSystem ShootingEffect;

    public float FlySpeed;
    public float RotationSpeed;

    public Transform leftHandle;
    public Transform rightHandle;

    private Vector3 direction;
    private Rigidbody mechaBody;
    private Vector3 defaultGunRotation;
    private Vector3 defaultReticleRotation;
    private Vector3 defaultRerticlePosition;

    private bool hasShot;

    void Start()
    {
        hasShot = false;
        mechaBody = PlayerMecha.gameObject.GetComponent<Rigidbody>();
        defaultGunRotation = MechaGun.gameObject.transform.localRotation.eulerAngles;
        defaultReticleRotation = Vector3.zero;
        defaultRerticlePosition = Reticle.gameObject.transform.localPosition;
    }


    void Update()
    {
        
        if(rightHandle.transform.rotation.z >= 0.25) SlideBackwards();
        else if(rightHandle.transform.rotation.z <= -0.25) SlideForwards();
        
        if(rightHandle.transform.rotation.x >= 0.25) TurnRight();
        else if(rightHandle.transform.rotation.x <= -0.25) TurnLeft();
        
        if(leftHandle.transform.rotation.z >= 0.25) RotateBackwards();
        else if(leftHandle.transform.rotation.z <= -0.25) RotateForwards();
        
        if(leftHandle.transform.rotation.x >= 0.25) RollRight();
        else if (leftHandle.transform.rotation.x <= -0.25) RollLeft();

        List<InputDevice> handDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, handDevices);
        if (handDevices.Count == 1)
        {
            CheckController(handDevices[0]);
        }
    }

    private void CheckController(InputDevice rightInputs)            
    {
        Vector2 JoystickAxis;
        bool TriggerVar;

        rightInputs.TryGetFeatureValue(CommonUsages.triggerButton, out TriggerVar);
        rightInputs.TryGetFeatureValue(CommonUsages.primary2DAxis, out JoystickAxis);
        MechaGun.gameObject.transform.localRotation = Quaternion.Euler(defaultGunRotation.x - (30 * JoystickAxis.y), defaultGunRotation.y + (30 * JoystickAxis.x), defaultGunRotation.z);
        Reticle.gameObject.transform.localRotation = Quaternion.Euler(defaultReticleRotation.x, defaultReticleRotation.y + (24 * JoystickAxis.x), defaultReticleRotation.z + (24 * JoystickAxis.y));

        if (TriggerVar && !hasShot)
        {
            hasShot = true;
            ShootingEffect.Play();
            var obj = (GameObject)Instantiate(TheBullet, FirePoint.position, MechaGun.gameObject.transform.localRotation);
            obj.GetComponent<Rigidbody>().AddForce(FirePoint.forward * 6, ForceMode.Impulse);

            
        }
        if (!TriggerVar) hasShot = false;
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

    public void RollLeft() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(-RotationSpeed * Time.deltaTime, Vector3.right);
    public void RollRight() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.right);
    
    public void TurnLeft() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.up);

    public void TurnRight() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(-RotationSpeed * Time.deltaTime, Vector3.up);

    public void RotateForwards() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(-RotationSpeed * Time.deltaTime, Vector3.forward);
    
    public void RotateBackwards() =>
        PlayerMecha.gameObject.transform.localRotation *=
            Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.forward);

}
