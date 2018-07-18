using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This script was written as a youtube tutorial by  Sebastian Lague https://www.youtube.com/watch?v=rQG9aUWarwE
[CustomEditor(typeof (SIGHT.Sight))]
public class SightEditor : Editor {

    void OnSceneGUI()
    {
        SIGHT.Sight fov = (SIGHT.Sight)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
