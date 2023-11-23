using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("Pause menu")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("Brightness overlay")]
    [SerializeField] private Image _darkOverlay;

    [Header("Scriptable Object (SO)")]
    [SerializeField] private GameSettings _gameSettings;

    [Header("Endgame windows")]
    [SerializeField] private GameObject _deathWindow;
    [SerializeField] private GameObject _victoryWindow;

    void Start()
    {
        if (_pauseMenu == null)
        {
            Debug.LogError("Pause menu is NULL");
        }
        if (_darkOverlay == null)
        {
            Debug.LogError("Dark overlay is NULL");
        }
        if (_gameSettings == null)
        {
            Debug.LogError("Game settings SO is NULL");
        }
    }

    void Update()
    {
        if (_deathWindow.activeSelf == false && _victoryWindow.activeSelf == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                AudioListener.pause = true;
            }
        }
        DarkOverlay();
    }

    private void DarkOverlay()
    {
        var tempColor = _darkOverlay.color;
        tempColor.a = _gameSettings.Brightness;
        _darkOverlay.color = tempColor;
    }
}
