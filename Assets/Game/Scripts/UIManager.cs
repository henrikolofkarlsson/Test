using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    // Array of images corresponding to lives, indexed by number of lives (0-3)
    public Sprite[] lives; 

    // using UnityEngine.UI this allows us to access the image component of
    // whatever we drop in the editor. 
    public Image livesImageDisplay;
    public Text scoreDisplay;
    public GameObject newGameDisplay;
    public int score;

    // Handles life bars
    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    // Handels score bar
    public void UpdateScore()
    {
        score += 10;
        scoreDisplay.text = "Score: " + score;
    }

    public void HideStartScreen()
    {
        newGameDisplay.SetActive(false);
        scoreDisplay.text = "Score: " + score;
    }

    public void ShowStartScreen()
    {
        newGameDisplay.SetActive(true);
    }

}
