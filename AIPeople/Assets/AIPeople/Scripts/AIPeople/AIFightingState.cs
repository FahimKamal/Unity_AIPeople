using System.Collections.Generic;
using QuickEye.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIFightingState : NPCBaseState
{
    [SerializeField] private UnityDictionary<string, List<string>> fightingAnimNames;
    [SerializeField] private int minPracticeTime = 40;
    [SerializeField] private int maxPracticeTime = 40;
    
    private float _timer;
    private float _timerStopDuration;

    private bool _warmUp = false;
    private bool _fireBall = false;
    private bool _singleStyle = false;
    
    private List<string> _randomAnimNames;
    private int _randomAnimNum = 0;
    
    private string _lastSelectedAnim = "";
    private string _nextAnimName = "";
    
    private AIPeopleStateManager _aiPeopleStateManager;

    private const string WarmUpKey = "Warming Up";
    private const string FireballKey = "Fireball";
    private const string SingleStyle = "SingleStyle";

    public override void EnterState(AIPeopleStateManager aiState)
    {
        // aiState.animator.applyRootMotion = true;
        aiState.DebugLog("Entering shopping State");
        _aiPeopleStateManager = aiState;
        _timerStopDuration = Random.Range(minPracticeTime, maxPracticeTime);
        aiState.presentAIPeopleAction = AIPeopleActions.Fighting;
        _randomAnimNames = fightingAnimNames[SingleStyle];
        
        WarmUp();
    }

    private void WarmUp()
    {
        _aiPeopleStateManager.DebugLog("Warm up start.");
        var temp = fightingAnimNames[WarmUpKey];
        _nextAnimName = temp[1];
        _aiPeopleStateManager.PlayAnimation(temp[0]);
    }
    
    private void FireBall()
    {
        _aiPeopleStateManager.DebugLog("Fireball start.");
        
        var temp = fightingAnimNames[FireballKey];
        
        _aiPeopleStateManager.DebugLog(temp[0]);
        _aiPeopleStateManager.DebugLog(temp[1]);
        _nextAnimName = temp[1];
        _aiPeopleStateManager.PlayAnimation(temp[0]);
    }

    public override void UpdateState(AIPeopleStateManager aiState)
    {
        _timer += Time.deltaTime;
        
        // aiState.DebugLog($"Time: {_timer}");
    }

    public override void ExitState(AIPeopleStateManager aiState)
    {
        // aiState.animator.applyRootMotion = false;
        _timer = 0;
        _timerStopDuration = 0;
        
        _warmUp = false;
        _fireBall = false;
        _singleStyle = false;
        
        _randomAnimNum = 0;
        _lastSelectedAnim = "";
        _nextAnimName = "";
        aiState.DebugLog("Exiting Fighting State");
    }

    public override void OnStateTriggerEnter(AIPeopleStateManager aiState, Collider aiCollider)
    {
        base.OnStateTriggerEnter(aiState, aiCollider);
    }

    /// <summary>
    /// Animation Event.
    /// Very important Don't delete.  
    /// </summary>
    public void AnimationOver()
    {
        if (_timer > _timerStopDuration)
        {
            _aiPeopleStateManager.SwitchState(_aiPeopleStateManager.walkingState);
            return;
        }
        
        if (!_warmUp)
        {
            _warmUp = true;
            _aiPeopleStateManager.PlayAnimation(_nextAnimName);
            
            return;
        }

        if (!_singleStyle)
        {
            if (_randomAnimNum < 3)
            {
                _aiPeopleStateManager.DebugLog($"Random Fight Animation: {_randomAnimNum}");
                _randomAnimNum++;
                _aiPeopleStateManager.PlayAnimation(GetRandomItem());
                return;
            }
            _singleStyle = true;
            FireBall();
        }

        if (!_fireBall)
        {
            _fireBall = true;
            _aiPeopleStateManager.PlayAnimation(_nextAnimName);
            
            return;
        }
        _aiPeopleStateManager.SwitchState(_aiPeopleStateManager.walkingState);
    }
    
    private string GetRandomItem()
    {
        if (_randomAnimNames.Count <= 1)
        {
            Debug.LogWarning("List must contain at least two items to ensure a different item is selected.");
            return _randomAnimNames.Count == 1 ? _randomAnimNames[0] : "";
        }

        string selectedItem;
        do
        {
            var randomIndex = Random.Range(0, _randomAnimNames.Count);
            selectedItem = _randomAnimNames[randomIndex];
        } while (selectedItem == _lastSelectedAnim);

        _lastSelectedAnim = selectedItem;
        return selectedItem;
    }
}
