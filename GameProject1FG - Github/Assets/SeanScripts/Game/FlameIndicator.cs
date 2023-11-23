using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameIndicator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _indicator;

    private GhostEating _ghostEating;

    void Start()
    {
        _ghostEating = _player.GetComponent<GhostEating>();
        _indicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_ghostEating.isHunting)
        {
            _indicator.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftControl) && !_ghostEating.isHunting) 
        {
            _indicator.SetActive(true);
            GameObject flame = GameObject.FindGameObjectWithTag("Flame");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(flame.transform.position - transform.position),
                _rotationSpeed * Time.deltaTime);

            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + 0.04f, _player.transform.position.z);
        }
        else
        {
            _indicator.SetActive(false);
        }
    }
}
