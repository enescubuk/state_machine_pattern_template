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

    public void AddState<T>() where T : MonoBehaviour, IStateCommand
    {
        T newState = GetComponent<T>();
        if (newState == null)
        {
            Debug.LogWarning("Tried to add a state that doesn't exist on the GameObject");
            return;
        }

        if (!activeStates.Contains(newState))
        {
            newState.Enter();
            activeStates.Add(newState);
        }
    }

    public void RemoveState<T>() where T : MonoBehaviour, IStateCommand
    {
        T state = GetComponent<T>();
        if (state != null && activeStates.Contains(state))
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
