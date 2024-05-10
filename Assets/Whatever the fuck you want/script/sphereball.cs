using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
public class sphereball : MonoBehaviour
{  
    private CanGame cangame;
    private bool thrown = false;
    private float lifeSpan = 2;
    private float timepassed = 0;
    // Start is called before the first frame update
    void Start(){
        cangame = CanGame.Instance;
        cangame.ballSpawned = true;
    }

    // Update is called once per frame
    void Update(){
        if(thrown){
            timepassed += Time.deltaTime;
            if(timepassed > lifeSpan){
                destroyBall();
            }
            
        }
    }

    public void destroyBall(){
        gameObject.SetActive(false);
        cangame.ballSpawned = false;

    }

    public void throwBalls(){
        thrown = true;
    }
}
