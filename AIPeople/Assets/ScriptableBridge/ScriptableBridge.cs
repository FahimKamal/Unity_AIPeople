using UnityEngine;

public class ScriptableBridge : MonoBehaviour
{
    [SerializeField] private BridgeSO bridge;
    [SerializeField] private GameObject target;
    private void OnEnable () { bridge.SetGameObject = target; }
}
