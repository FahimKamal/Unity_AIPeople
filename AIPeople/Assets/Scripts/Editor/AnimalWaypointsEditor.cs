using UnityEngine;
using UnityEditor;
using UnityEngine.Splines;

[CustomEditor(typeof(AnimalWaypoints))]
public class AnimalWaypointsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get the target object
        AnimalWaypoints animalWaypoints = (AnimalWaypoints)target;

        // Iterate through the wayPointKnots list
        for (int i = 0; i < animalWaypoints.wayPointKnots.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            
            // Display the default property field for the WayPointKnot
            EditorGUILayout.LabelField($"Waypoint {i}:", GUILayout.Width(70));
            animalWaypoints.wayPointKnots[i].aiAction = (AIAction)EditorGUILayout.EnumPopup(animalWaypoints.wayPointKnots[i].aiAction, GUILayout.Width(100));
            
            // Add a button next to each item
            if (GUILayout.Button("Show in Scene", GUILayout.Width(100)))
            {
                // Perform some action when the button is clicked
                // Debug.Log($"Button clicked for waypoint {i}");
                animalWaypoints.selectedIndex = i;
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