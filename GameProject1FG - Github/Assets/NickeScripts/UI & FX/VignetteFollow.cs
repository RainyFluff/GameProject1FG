using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingProgram : MonoBehaviour
{
    [Header("General")]
    public Camera cam;
    public GameObject player;
    private Volume _volume;
    private GhostEating _ghostEating;
    private VolumeProfile _volumeProfile;
    
    [Header("Vignette")]
    public float _vignetteIntensityWhenHunting = 0.2f;
    public float _vignetteIntensityNormal = 0.65f;
    public float _vignetteIntensityLow = 0.70f;
    private Vignette _vignette;
    private float posX;
    private float posY;
    private Vector2 playerPos;

    [Header("Color Adjustment (VALUES USED DURING HUNT)")] 
    public float postExposure;
    public float contrast;
    public Color colorFilter;
    public float hueShift;
    public float saturation;
    private ColorAdjustments _colorAdjustments;
    private float regularExposure;
    private float regularContrast;
    private Color regularColor;
    private float regularHue;
    private float regularSaturation;
    
    
    [Header("Bools")]
    public bool wantVignetteFollow;
    public bool wantColorAdjustmentOnHunt;
    private EndObjectiveObj _endObjectiveObj;

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
        _volume.profile.TryGet(out _vignette);
        _volume.profile.TryGet(out _colorAdjustments);
        _ghostEating = player.GetComponent<GhostEating>();

        regularExposure = _colorAdjustments.postExposure.value;
        regularContrast = _colorAdjustments.contrast.value;
        regularColor = _colorAdjustments.colorFilter.value;
        regularHue = _colorAdjustments.hueShift.value;
        regularSaturation = _colorAdjustments.saturation.value;

        _endObjectiveObj = GameObject.Find("mainpiece").GetComponent<EndObjectiveObj>();

        if (_endObjectiveObj == null )
        {
            Debug.LogError("EndObjectiveObj is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
       VignetteFollow();
       ColorAdjustmentOnHunt();
    }

    void VignetteFollow()
    {
        if (wantVignetteFollow)
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

    void ColorAdjustmentOnHunt()
    {
        if (wantColorAdjustmentOnHunt)
        {
            if (_ghostEating.isHunting)
            {
                _colorAdjustments.postExposure.value = postExposure;
                _colorAdjustments.contrast.value = contrast;
                _colorAdjustments.colorFilter.value = colorFilter;
                _colorAdjustments.hueShift.value = hueShift;
                _colorAdjustments.saturation.value = saturation;
            }
            else
            {
                _colorAdjustments.postExposure.value = regularExposure;
                _colorAdjustments.contrast.value = regularContrast;
                _colorAdjustments.colorFilter.value = regularColor;
                _colorAdjustments.hueShift.value = regularHue;
                _colorAdjustments.saturation.value = regularSaturation;
            }
        }
    }
}
