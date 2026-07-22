using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject gameOverScreen;

    public void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        gameOverScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}