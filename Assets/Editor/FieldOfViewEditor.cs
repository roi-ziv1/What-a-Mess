using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView) target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        
        Vector3 viewAngleA = DirFromAngle(fov.transform.eulerAngles.y, -fov.viewAngle / 2);
        Vector3 viewAngleB = DirFromAngle(fov.transform.eulerAngles.y, fov.viewAngle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);
        
        if(fov.playerInSight)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }
    
    private Vector3 DirFromAngle(float eulorY, float angleInDegrees)
    {
        angleInDegrees += eulorY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}
