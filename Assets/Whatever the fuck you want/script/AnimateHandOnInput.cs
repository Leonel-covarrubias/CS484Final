using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public Animator handAnimator;
    public bool triggerHold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        if(triggerValue > 0.5){
            triggerHold = true;
        }
        else{
            triggerHold = false;
        }
        handAnimator.SetFloat("Trigger", triggerValue);
    }
}
