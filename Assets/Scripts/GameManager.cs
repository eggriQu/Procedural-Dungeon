using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private List<GameObject> ammoIcons;
    [SerializeField] private List<GameObject> healthIcons;
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

    // Update is called once per frame
    void Update()
    {
        if (player.shotsLeft == 2)
        {
            ammoIcons[0].SetActive(true);
            ammoIcons[1].SetActive(true);
        }
        else if (player.shotsLeft == 1)
        {
            ammoIcons[0].SetActive(true);
            ammoIcons[1].SetActive(false);
        }
        else if (player.shotsLeft <= 0)
        {
            ammoIcons[0].SetActive(false);
            ammoIcons[1].SetActive(false);
        }

        if (player.isReloading)
        {
            ammoIcons[2].SetActive(true);
        }
        else
        {
            ammoIcons[2].SetActive(false);
        }

        if (player.health == 5)
        {
            healthIcons[0].SetActive(true);
            healthIcons[1].SetActive(true);
            healthIcons[2].SetActive(true);
            healthIcons[3].SetActive(true);
            healthIcons[4].SetActive(true);
        }
        else if (player.health == 4)
        {
            healthIcons[0].SetActive(true);
            healthIcons[1].SetActive(true);
            healthIcons[2].SetActive(true);
            healthIcons[3].SetActive(true);
            healthIcons[4].SetActive(false);
        }
        else if (player.health == 3)
        {
            healthIcons[0].SetActive(true);
            healthIcons[1].SetActive(true);
            healthIcons[2].SetActive(true);
            healthIcons[3].SetActive(false);
            healthIcons[4].SetActive(false);
        }
        else if (player.health == 2)
        {
            healthIcons[0].SetActive(true);
            healthIcons[1].SetActive(true);
            healthIcons[2].SetActive(false);
            healthIcons[3].SetActive(false);
            healthIcons[4].SetActive(false);
        }
        else if (player.health == 1)
        {
            healthIcons[0].SetActive(true);
            healthIcons[1].SetActive(false);
            healthIcons[2].SetActive(false);
            healthIcons[3].SetActive(false);
            healthIcons[4].SetActive(false);
        }
        else if (player.health == 0)
        {
            healthIcons[0].SetActive(false);
            healthIcons[1].SetActive(false);
            healthIcons[2].SetActive(false);
            healthIcons[3].SetActive(false);
            healthIcons[4].SetActive(false);
        }
    }
}