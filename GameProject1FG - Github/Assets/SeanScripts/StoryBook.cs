using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryBook : MonoBehaviour
{
    [Header("Book")]
    [SerializeField] private GameObject _bookObj;
    [SerializeField] private TextMeshPro _interactText;

    [Header("Book UI")]
    [SerializeField] private GameObject _bookUI;
    [SerializeField] private Button _closeBtn;

    [Header("Player")]
    [SerializeField] private GameObject _player;

    [Header("Main camera")]
    [SerializeField] private GameObject _camera;

    private float distanceToPlayer;

    void Start()
    {
        Button close = _closeBtn.GetComponent<Button>();
        close.onClick.AddListener(Close);

        if (close == null) 
        {
            Debug.LogError("Back button is NULL");
        }
        if (_bookObj == null)
        {
            Debug.LogError("Book is NULL");
        }
        if (_interactText == null)
        {
            Debug.LogError("Interact text is NULL");
        }
        if (_bookUI == null)
        {
            Debug.LogError("Book UI is NULL");
        }
        if (_player == null) 
        {
            Debug.LogError("Player is NULL");
        }
        if (_camera == null)
        {
            Debug.LogError("Main camera is NULL");
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= 2f)
        {
            _interactText.gameObject.SetActive(true);
            _interactText.text = "Q";
            _interactText.transform.rotation = Quaternion.LookRotation((_interactText.transform.position - Camera.main.transform.position).normalized);
            if (Input.GetKeyUp(KeyCode.Q))
            {
                _bookObj.GetComponent<MeshRenderer>().enabled = false;
                _bookUI.SetActive(true);
            }
        }
        else
        {
            _interactText.gameObject.SetActive(false);
        }
    }

    void Close()
    {
        StartCoroutine(CloseDelay());
    }

    IEnumerator CloseDelay()
    {
        ButtonClick close = _closeBtn.GetComponent<ButtonClick>();
        float _sound = close._clickSound.length;
        yield return new WaitForSecondsRealtime(_sound - 0.1f);
        _bookUI.SetActive(false);
    }
}
