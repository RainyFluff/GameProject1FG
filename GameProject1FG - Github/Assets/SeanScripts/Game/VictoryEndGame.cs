using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryEndGame : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _replayBtn;
    [SerializeField] private Button _MainMenuBtn;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _endGameText;

    private GameObject _player;
    private GameObject[] _enemies;

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

        _endGameText.text = "Victory";

        replay.onClick.AddListener(ReplayGame);
        MainMenu.onClick.AddListener(ReturnToMainMenu);
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<ReworkedMovement>().enabled = false;
        _player.GetComponent<GhostFreeze>().enabled = false;    
        _player.GetComponent<DecoyAbility>().enabled = false;
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in _enemies)
        {
            enemy.SetActive(false);
        }
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
