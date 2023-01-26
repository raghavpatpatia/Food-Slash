using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;

    public TextMeshProUGUI scoreText;
    private int score;

    public TextMeshProUGUI gameOverText;

    public bool isGameActive = true;

    public Button restartButton;

    public TextMeshProUGUI livesText;
    private int liveNum;

    public GameObject titleScreen;

    public GameObject pauseScreen;
    private bool isPaused;

    public GameObject gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        titleScreen.gameObject.SetActive(true);
        gameObjects.SetActive(false);
    }

    public void OnPauseButtonClick()
    {
        Paused();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            if(!isGameActive)
                break;
        }
    }

    public void UpdatedScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdatedLives(int livesToAdd)
    {
        liveNum += livesToAdd;
        livesText.text = "Lives: " + liveNum;
        if (liveNum <= 0)
        {
            GameOver();
        }
    }

    public void StartGame(int difficulty, int lives)
    {
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdatedScore(0);
        UpdatedLives(lives);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
        gameObjects.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Paused()
    {
        if (!isPaused)
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
