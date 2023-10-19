using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu buttons")]
    [SerializeField] private Button _play_btn;
    [SerializeField] private Button _options_btn;
    [SerializeField] private Button _credits_btn;
    [SerializeField] private Button _quit_btn;

    [Header("Menus")]
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _creditsMenu;

    void Start()
    {
        Button play = _play_btn.GetComponent<Button>();
        Button quit = _quit_btn.GetComponent<Button>();
        Button options = _options_btn.GetComponent<Button>();
        Button credits = _credits_btn.GetComponent<Button>();
        
        if (play == null ) 
        {
            Debug.LogError("Play button is NULL");
        }
        if ( quit == null )
        {
            Debug.LogError("Quit button is NULL");
        }
        if (options == null )
        {
            Debug.LogError("Controls button is NULL");
        }

        play.onClick.AddListener(PlayGame);
        quit.onClick.AddListener(QuitGame);
        options.onClick.AddListener(Options);
        credits.onClick.AddListener(Credits);
    }

    void PlayGame()
    {
        SceneManager.LoadScene(1); // Load level 1
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void Options()
    {
        _optionsMenu.SetActive(true);
    }

    void Credits()
    {
        _creditsMenu.SetActive(true);
    }
}
