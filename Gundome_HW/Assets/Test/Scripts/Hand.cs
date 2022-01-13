using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{
    public float speed;
    
    private Animator animator;
    private float gripTarget;
    private float pointerTarget;
    private float gripCurrent;
    private float pointerCurrent;

    private string animatorGripName = "Grip";
    private string animatorPointerName = "Pointer";
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }
    
    internal void SetPointer(float v)
    {
        pointerTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget)
        {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripName, gripCurrent);
        }
        if (pointerCurrent != pointerTarget)
        {
            pointerCurrent = Mathf.MoveTowards(pointerCurrent, pointerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorPointerName, pointerCurrent);
        }
    }
}
