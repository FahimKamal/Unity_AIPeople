using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

#region Serializables

public enum AIAction
{
    None, Idle, Eat, Sit
}
[Serializable]
public class WayPointKnot
{
    public bool isKnotSelected;
    public int knotIndex;
    public AIAction aiAction;
}

#endregion

public class AnimalWaypoints : MonoBehaviour
{
    [SerializeField] private SplineContainer wayPoints;
    [SerializeField] public List<WayPointKnot> wayPointKnots;

    [HideInInspector] public int selectedIndex = -1;

    [ContextMenu("Get Waypoints")]
    public void GetAllWaypoints()
    {
        wayPointKnots.Clear();
        var wayPointCount = wayPoints[0].Count;
        for (var i = 0; i < wayPointCount; i++)
        {
            wayPointKnots.Add(new WayPointKnot() { knotIndex = i, aiAction = AIAction.None });
        }
    }

    public (float3 positionValue, quaternion rotationValue, AIAction action, WayPointKnot selectedWaypointKnot) GetRandomWayPoint()
    {
        var index = Random.Range(0, wayPointKnots.Count);
        if (wayPointKnots[index].isKnotSelected)
        {
            return GetRandomWayPoint();
        }
        else
        {
            wayPointKnots[index].isKnotSelected = true;
            var returnWayPoint = wayPoints[0][index];
            return (
                (float3)wayPoints.transform.position + returnWayPoint.Position, 
                wayPoints.transform.rotation * returnWayPoint.Rotation, 
                wayPointKnots[index].aiAction, 
                wayPointKnots[index]);
        }
    }
    
    public void ResetWayPoint(WayPointKnot wayPointKnot)
    {
        wayPointKnot.isKnotSelected = false;
    }

    private void OnDrawGizmosSelected()
    {
        Draw();
    }

    private void OnDrawGizmos()
    {
        // Draw();
    }

    private void Draw()
    {
        for (var i = 0; i < wayPointKnots.Count; i++)
        {
            var defaultColor = GUI.color;
            if (i == selectedIndex)
            {
                var style = new GUIStyle();
                style.normal.textColor = Color.red; // Set the text color
                style.fontSize = 28;
                // GUI.color = Color.red;
                UnityEditor.Handles.Label(
                    (float3)wayPoints.transform.position + wayPoints[0][i].Position, 
                    $"A:{ Enum.GetName(typeof(AIAction), wayPointKnots[i].aiAction)}", style
                    );
                // GUI.color = defaultColor;
                continue;
            }
            UnityEditor.Handles.Label(
                (float3)wayPoints.transform.position + wayPoints[0][i].Position, 
                $"A:{ Enum.GetName(typeof(AIAction), wayPointKnots[i].aiAction)}");
        }
        
    }
}


