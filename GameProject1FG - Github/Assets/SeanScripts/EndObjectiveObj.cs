using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndObjectiveObj : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _player;

    [Header("Main camera")]
    [SerializeField] private GameObject _camera;

    [Header("Interact text")]
    [SerializeField] private TextMeshPro _interactText;

    [Header("End objective settings")]
    [SerializeField] private float _requiredSouls;

    [Header("Victory window")]
    [SerializeField] private GameObject _victory;

    private float distanceToPlayer;

    void Start()
    {
        if (_player == null)
        {
            Debug.LogError("Player is NULL");
        }
        if(_camera == null)
        {
            Debug.LogError("Main camera is NULL");
        }
        if (_interactText == null)
        {
            Debug.LogError("Interact text is NULL");
        }
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        
         if (distanceToPlayer <= 4f) 
         {
             if (_player.GetComponent<GhostEating>().ghostsEaten >= _requiredSouls)
             {
                _interactText.gameObject.SetActive(true);
                _interactText.text = "Q"; 
                _interactText.transform.LookAt(_interactText.transform.position - _camera.transform.position);
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    _interactText.GetComponent<MeshRenderer>().enabled = false;
                    Time.timeScale = 0;
                    StartCoroutine(EndInVictory());
                }
             }
            else
            {
                _interactText.gameObject.SetActive(true);
                _interactText.text = "Not enough souls";
                _interactText.transform.LookAt(_interactText.transform.position - _camera.transform.position);

            }
        }
         else
         {
            _interactText.gameObject.SetActive(false);
         }
    }

    IEnumerator EndInVictory()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        _victory.SetActive(true);
    }
}
