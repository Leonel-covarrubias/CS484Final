using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
public class WhackaMole : MonoBehaviour
{
    public List<MoleScript> moleScripts;
    private bool test = false;
    private int randomIndex;
    private bool anyMoleDown = false;

    public int playerPoints = 0;

    public TMP_Text score;
    public static WhackaMole Instance;
    public AudioSource bonk;
    public AudioSource fire;
    public int currentPoint;
    public float timer = 35f;
    public float timepassed = 0;
    public float postGameTime = 10f;
    private float postGameTimer = 0;
    public List<GameObject> fireworks;
    private float prematchTimer = 0;
    private float prematchTime = 5f;
    private bool start = false;
    private bool start1 = false;
    private bool start2 = true;

    void Start()
    {
        StartCoroutine(countDown());
        currentPoint = playerPoints;
    }

    void Update(){
        if(start){
            StartCoroutine(RandomMoleMovementCoroutine());
            start1  = !start1;
            start = !start;
        }
        if(start1){
            score.text = playerPoints.ToString();
            if(currentPoint != playerPoints){
                bonk.Play();
                currentPoint = playerPoints;
            }
            timepassed += Time.deltaTime;
            if(timepassed > timer && playerPoints >= 300){
                SceneManager.LoadScene(0);
            }
            if(playerPoints >= 300){
                if(start2){
                    fire.Play();
                }
                start2 = false;
                foreach(GameObject t in fireworks){
                    t.gameObject.SetActive(true);
                }
                postGameTimer += Time.deltaTime;
                if(postGameTimer > postGameTime){
                    SceneManager.LoadScene(0);
                }
            }
        }       
    }
    // Coroutine to call StartRandomMoleMovement every 3 seconds
    IEnumerator RandomMoleMovementCoroutine()
    {
        while (start2)
        {
            float waitTime = Random.Range(0, 3);
            yield return new WaitForSeconds(waitTime);
            StartRandomMoleMovement();
        }
    }

    // Method to start movement of a random mole
    public void StartRandomMoleMovement()
    {
        // Check if any mole is down
        foreach (MoleScript moleScript in moleScripts)
        {
            if (!moleScript.isUp)
            {
                anyMoleDown = true;
                break;
            }
        }

        // If at least one mole is down, proceed with starting movement
        if (anyMoleDown)
        {
            randomIndex = Random.Range(0, moleScripts.Count);

            // Ensure the selected mole is currently down
            while (moleScripts[randomIndex].isUp)
            {
                randomIndex = Random.Range(0, moleScripts.Count);
            }

            // Start movement of the selected mole
            moleScripts[randomIndex].StartMovement();
        }
        else
        {
            Debug.LogWarning("All moles are currently up. Wait until at least one mole is down.");
        }
    }

    IEnumerator countDown(){
        for(int i = 5; i>0; i--){
            Debug.Log(i);
            score.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        start = true;
    }
}
