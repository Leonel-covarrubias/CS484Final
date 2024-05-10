using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGame : MonoBehaviour
{
    public int Score = 0;
    public bool ballSpawned = false;
    public Transform spawnlocation;
    public GameObject sphereball;
    public static CanGame Instance;
    public List<canmovement> canlist;
    public List<GameObject> fireworks;
    public AudioSource audios;
    public float postGameTime = 10;
    private float timepassed = 0;
    // Start is called before the first frame update
    void Start(){
        
    }
    void Awake() {
    if (Instance == null) {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    } else {
        Destroy(gameObject);
    }
}
    // Update is called once per frame
    void Update(){
        if (!ballSpawned)
        {
            Instantiate(sphereball, spawnlocation.position, Quaternion.identity);
        }
        if(canlist.Count == Score && !audios.isPlaying){
            audios.Play();
            foreach(GameObject t in fireworks){
                t.gameObject.SetActive(true);
            }
            timepassed += Time.deltaTime;
            // if(timepassed > postGameTime){
                
            // }
        }
    }
    
}
