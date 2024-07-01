using UnityEngine;

[CreateAssetMenu(fileName = "BridgeSO", menuName = "ScriptableObjects/ScriptableBridge")]
public class BridgeSO : ScriptableObject
{
    [SerializeField] private GameObject gameObject;
    public GameObject SetGameObject {
        set => gameObject = value;
    }
    public GameObject GetGameObject => gameObject;
}
