using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelStateController : MonoBehaviour
{
    private List<IStateCommand> activeStates;
    private bool inTransition;

    public static ParallelStateController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            activeStates = new List<IStateCommand>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddState(IStateCommand newState)
    {
        if (!activeStates.Contains(newState))
        {
            newState.Enter();
            activeStates.Add(newState);
        }
    }

    public void RemoveState(IStateCommand state)
    {
        if (activeStates.Contains(state))
        {
            state.Exit();
            activeStates.Remove(state);
        }
    }

    private void Update()
    {
        if (!inTransition)
        {
            foreach (var state in activeStates)
            {
                state.Tick();
            }
        }
    }
}
