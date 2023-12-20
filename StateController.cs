using UnityEngine;
using UnityEngine.UIElements;

public class StateController : MonoBehaviour
{
    public IStateCommand CurrentState;
    private bool InTransition;
    public static StateController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start() 
    {
        //default
    }

    public void ChangeState<T>() where T : IStateCommand
    {
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.Log("tried to change to null state");
        }
        InititeNewState(targetState);
    }

    void InititeNewState(IStateCommand targetState)
    {
        if (CurrentState != targetState && !InTransition)
        {
            CallNewState(targetState);
        }
    }

    void CallNewState(IStateCommand newState)
    {
        InTransition = true;
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        InTransition = false;
    }

    private void Update()
    {
        if (CurrentState != null && !InTransition)
        {
            CurrentState.Tick();
        }
    }
}