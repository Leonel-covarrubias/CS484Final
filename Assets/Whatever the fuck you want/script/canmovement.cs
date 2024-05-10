using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canmovement : MonoBehaviour
{
    public AudioSource canFall;
    public CanGame cangame;
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 1.0f;
    private float fraction = 0;
    public bool blueCan;
    public bool reverse;
    // Start is called before the first frame update
    void Start(){
        canFall.volume = 1;
        if(blueCan){
            speed = Random.Range(4,6);
        }else{
            speed = Random.Range(1,3);
        }
    }
    void Update()
    {
        if (!reverse)
        {
            // Move right
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // Move left
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            reverse = !reverse; // Toggle direction on hitting a wall
        }
        if (other.CompareTag("ball"))
        {
            StartCoroutine(DeactivateAfterSound());
            cangame.Score += 1; // Increase score
            sphereball Sphereball = other.gameObject.GetComponent<sphereball>();
            Sphereball.destroyBall();
        }
    }
    private IEnumerator DeactivateAfterSound(){
        canFall.Play();
        yield return new WaitForSeconds(canFall.clip.length); // Wait for the length of the clip
        gameObject.SetActive(false);
    }
}
