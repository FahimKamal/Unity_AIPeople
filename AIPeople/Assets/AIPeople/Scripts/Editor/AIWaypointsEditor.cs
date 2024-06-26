using UnityEngine;
using UnityEditor;
using UnityEngine.Splines;

[CustomEditor(typeof(AIWaypoints))]
public class AIWaypointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get the target object
        AIWaypoints aiWaypoints = (AIWaypoints)target;

        // Iterate through the wayPointKnots list
        for (int i = 0; i < aiWaypoints.wayPointKnots.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            // Display the default property field for the WayPointKnot
            EditorGUILayout.LabelField($"Waypoint {i}:", GUILayout.Width(70));
            aiWaypoints.wayPointKnots[i].aiAction = (AIAction)EditorGUILayout.EnumPopup(aiWaypoints.wayPointKnots[i].aiAction, GUILayout.Width(100));
            
            // Add a button next to each item
            if (GUILayout.Button("Show in Scene", GUILayout.Width(100)))
            {
                // Perform some action when the button is clicked
                // Debug.Log($"Button clicked for waypoint {i}");
                aiWaypoints.selectedIndex = i;
                // Add your custom action here
            }

            EditorGUILayout.EndHorizontal();
        }

        // Ensure changes are saved back to the object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}