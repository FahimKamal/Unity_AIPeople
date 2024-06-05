using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class AIPeopleStateManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    private NPCBaseState _presentState;
}
