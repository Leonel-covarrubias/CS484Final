using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WhackaMole : MonoBehaviour
{
    public List<MoleScript> moleScripts;
    private bool test = false;
    private int randomIndex;
    private bool anyMoleDown = false;

    public int playerPoints = 0;

    public static WhackaMole instance;

    void Start()
    {
        // Start the coroutine to call StartRandomMoleMovement every 3 seconds
        StartCoroutine(RandomMoleMovementCoroutine());
    }

    // Coroutine to call StartRandomMoleMovement every 3 seconds
    IEnumerator RandomMoleMovementCoroutine()
    {
        while (true)
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
}
