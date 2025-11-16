using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<GameObject> Coins; // A list of the coins in the map.
    public string nextScene; // The next scene to load in.

    // Update is called once per frame
    void Update()
    {
        if (Coins.Count <= 0) // If all of the coins have been destroyed, start the next level.
        {
            Pause(); // Pause the game for a short second.
            SceneManager.LoadScene(nextScene); // Load in the next scene.
        }
        foreach (GameObject coin in Coins) // Constantly check to see if a coin has been destroyed and remove it from the list if it has.
        {
            if (coin.IsUnityNull())
            {
                Coins.Remove(coin);
            }
        }
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
    }
}
