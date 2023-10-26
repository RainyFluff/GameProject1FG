using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _continue_btn;
    [SerializeField] private Button _options_btn;
    [SerializeField] private Button _exit_btn;

    [Header("Menus")]
    [SerializeField] private GameObject _optionsMenu;

    void Start()
    {
        Button continueBtn = _continue_btn.GetComponent<Button>();
        Button options = _options_btn.GetComponent<Button>();
        Button exit = _exit_btn.GetComponent<Button>();

        if (continueBtn == null )
        {
            Debug.LogError("Close button is NULL");
        }
        if (options == null)
        {
            Debug.LogError("Options button is NULL");
        }
        if (exit == null)
        {
            Debug.LogError("Exit button is NULL");
        }

        if (_optionsMenu == null)
        {
            Debug.LogError("Options menu is NULL");
        }

        continueBtn.onClick.AddListener(Continue);
        options.onClick.AddListener(Options);
        exit.onClick.AddListener(Exit);
    }

    void Continue()
    {
        StartCoroutine(ContinueGameDelay());
    }

    IEnumerator ContinueGameDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        if (_optionsMenu.activeSelf == true)
        {
            _optionsMenu.SetActive(false);
        }
        gameObject.SetActive(false);
        AudioListener.pause = false;
    }

    void Options()
    {
        _optionsMenu.SetActive(true);
    }

    void Exit()
    {
        StartCoroutine(ExitDelay());
    }

    IEnumerator ExitDelay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0); // Main menu
    }
}
