using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [Header("Menu buttons")]
    [SerializeField] private Button _back_btn;

    void Start()
    {
        Button back = _back_btn.GetComponent<Button>();

        if (back == null)
        {
            Debug.LogError("Close button is NULL");
        }

        back.onClick.AddListener(CloseWindow);
    }

    void CloseWindow()
    {
        gameObject.SetActive(false);
    }
}
