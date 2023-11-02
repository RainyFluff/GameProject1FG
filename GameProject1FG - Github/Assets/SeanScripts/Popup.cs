using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private GameObject _popupObject;
    [SerializeField] private TextMeshProUGUI _popupText;

    void Start()
    {
        if (_popupObject == null)
        {
            Debug.LogError("Popup object is NULL");
        }
        if (_popupText == null)
        {
            Debug.LogError("Popup text is NULL");
        }
    }

    public void ShowPopup(string text)
    {
        _popupObject.SetActive(true);
        _popupText.text = text;
    }

    public void HidePopup()
    {
        _popupObject.SetActive(false);
    }
}
