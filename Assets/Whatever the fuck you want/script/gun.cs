using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public AnimateHandOnInput triggerHold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        if(other.CompareTag("hand")){
            if(triggerHold.triggerHold){
                Debug.Log("hi");
            }
        }
    }
}
