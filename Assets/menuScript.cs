using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button exitButton;
    void Start()
    {
        startButton.onClick.AddListener(startGame);
        exitButton.onClick.AddListener(closeGame);
    }

    void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void closeGame()
    {
        Application.Quit();
    }
}
