using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenu : MonoBehaviour
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
        StartCoroutine(CloseDelay());
    }

    IEnumerator CloseDelay()
    {
        ButtonClick click = _back_btn.GetComponent<ButtonClick>();
        float _sound = click._clickSound.length;
        yield return new WaitForSecondsRealtime(_sound - 0.1f);
        gameObject.SetActive(false);
    }
}
