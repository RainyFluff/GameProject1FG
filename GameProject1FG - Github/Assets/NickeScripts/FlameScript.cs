using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    private bool _hasBeenCollected = false;

    private void Update()
    {
        if (_hasBeenCollected)
        {
            Destroy(gameObject);
        }
    }

    public bool GetHasBeenCollected()
    {
        return _hasBeenCollected;
    }

    public void SetHasBeenCollected(bool collected)
    {
        _hasBeenCollected = collected;
    }
}
