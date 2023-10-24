using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathEndGame : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _replayBtn;
    [SerializeField] private Button _MainMenuBtn;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _endGameText;

    void Start()
    {
        Button replay = _replayBtn.GetComponent<Button>();
        Button MainMenu = _MainMenuBtn.GetComponent<Button>();

        if (replay == null)
        {
            Debug.LogError("Replay button is NULL");
        }
        if (MainMenu == null)
        {
            Debug.LogError("Main menu button is NULL");
        }

        _endGameText.text = "You Died";

        replay.onClick.AddListener(ReplayGame);
        MainMenu.onClick.AddListener(ReturnToMainMenu);
    }

    
    void ReplayGame()
    {
        StartCoroutine(ReplayGameDelay());
    }

    IEnumerator ReplayGameDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Replay current scene
    }

    void ReturnToMainMenu()
    {
        StartCoroutine(ReturnDelay());
    }

    IEnumerator ReturnDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // Main menu
    }
}
