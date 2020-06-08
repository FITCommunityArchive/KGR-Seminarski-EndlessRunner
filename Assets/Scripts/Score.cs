using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private float difficultyLevel = 0.1f;
    private float startScore = 1.0f;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;
    private bool isDeath = false;
#pragma warning disable 0649
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private DeathMenu deathMenu;
#pragma warning restore 0649

    void Update()
    {
        if (isDeath)
            return;

        else  if (score >= scoreToNextLevel)
            LevelUp();

        if(startScore>difficultyLevel)
            score += Time.deltaTime * startScore;
        else
            score += Time.deltaTime*difficultyLevel;
        scoreText.text = ((int)score).ToString();
    }

    private void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMovement>().SetSpeed(difficultyLevel);
    }
    public void OnDeath()
    {
        isDeath = true;
        if(PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore", score);

        //deathMenu.ToggleEndMenu(score);
        StartCoroutine(ShowDeathScreen());
    }

    IEnumerator ShowDeathScreen()
    {
        yield return new WaitForSeconds(2.5f);
        deathMenu.ToggleEndMenu(score);

    }
}
