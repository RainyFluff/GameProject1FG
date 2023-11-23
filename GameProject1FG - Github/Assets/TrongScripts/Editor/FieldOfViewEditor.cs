using System;
using System.Collections;
using System.Collections.Generic;
using PlasticGui.Gluon.WorkspaceWindow.Views.IncomingChanges;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

[CustomEditor(typeof(DetectPlayer))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        DetectPlayer detectPlayer = (DetectPlayer)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(detectPlayer.transform.position, Vector3.up, Vector3.forward, 360, detectPlayer.Radius);

        Vector3 viewAngle01 = DirectionFromAngle(detectPlayer.transform.eulerAngles.y, -detectPlayer.Angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(detectPlayer.transform.eulerAngles.y, detectPlayer.Angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(detectPlayer.transform.position, detectPlayer.transform.position + viewAngle01 * detectPlayer.Radius);
        Handles.DrawLine(detectPlayer.transform.position, detectPlayer.transform.position + viewAngle02 * detectPlayer.Radius);

        if (detectPlayer.CanSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(detectPlayer.transform.position, detectPlayer.DummyPlayer.transform.position);
        }       
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegree)
    {
        angleInDegree += eulerY;
        return new Vector3(Mathf.Sin(angleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegree * Mathf.Deg2Rad));
    }
}
