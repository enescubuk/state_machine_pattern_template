using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateController : MonoBehaviour
{
    public IStateCommand CurrentState;
    private bool InTransition;
    public static StateController Instance;
    private void Awake()
    {// Eğer bir örnek yoksa, bu sınıfı singleton olarak ayarlar.
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start() 
    {// Oyun başladığında varsayılan durumu ayarlamak için kullanılır.
        
    }

    public void ChangeState<T>() where T : IStateCommand
    {// Belirtilen türdeki yeni duruma geçişi sağlar.
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.Log("tried to change to null state");
        }
        InititeNewState(targetState);
    }

    void InititeNewState(IStateCommand targetState)
    {// Hedef duruma geçişi başlatır, eğer farklıysa ve geçiş yapmıyorsa.
        if (CurrentState != targetState && !InTransition)
        {
            CallNewState(targetState);
        }
    }

    void CallNewState(IStateCommand newState)
    {// Yeni duruma geçiş yapar ve ilgili durum metotlarını çağırır.
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
