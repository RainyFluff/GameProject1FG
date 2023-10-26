using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFollow : MonoBehaviour
{
    public Camera cam;
    private Vector2 playerPos;
    public GameObject player;
    private float posX;

    private float posY;

    private Volume _volume;
    private VolumeProfile _volumeProfile;
    private Vignette _vignette;
    [SerializeField] private GhostEating _ghostEating;
    public float _vignetteIntensityWhenHunting = 0.2f;

    public float _vignetteIntensityNormal = 0.65f;
    public float _vignetteIntensityLow = 0.70f;

    private EndObjectiveObj _endObjectiveObj;

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);

        _endObjectiveObj = GameObject.Find("mainpiece").GetComponent<EndObjectiveObj>();

        if (_endObjectiveObj == null )
        {
            Debug.LogError("EndObjectiveObj is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
       playerPos = cam.WorldToScreenPoint(player.transform.position);
       //Debug.Log(posX);
       //Debug.Log(posY);
       posX = playerPos.x / Screen.currentResolution.width;
       posY = playerPos.y / Screen.currentResolution.height;
       Vector2 vignettePos = new Vector2(posX, posY);
       _vignette.center.value = vignettePos;
       if (_ghostEating.isHunting)
       {
           _vignette.intensity.value = _vignetteIntensityWhenHunting;
       }
       else if (_ghostEating.ghostsEaten < _endObjectiveObj.GetComponent<EndObjectiveObj>().GetRequiredSoulsValue())
       {
           _vignette.intensity.value = _vignetteIntensityNormal;
       }
       else if(_ghostEating.ghostsEaten >= _endObjectiveObj.GetComponent<EndObjectiveObj>().GetRequiredSoulsValue())
       {
            _vignette.intensity.value = _vignetteIntensityLow;
       }
    }
}
