using System;
using System.Collections.Generic;
/// <summary>
/// Macchina a Stati Finiti
/// </summary>
/// <typeparam name="T">Enumeratore</typeparam>
public class GenericStateMachine<T> where T : Enum
{
    private Dictionary<T, State> _allStates = new Dictionary<T, State>();
    private State _currentState;

    // per debug usiamo queste proprietà
    public T PreviousStateType; // stato dal quale sto uscendo
    public T CurrentStateType; // stato attuale, in generale il nuovo stato in cui sto entrando

    public void RegisterState(T stateType, State state)
    {
        if (_allStates.ContainsKey(stateType))
        {
            throw new InvalidOperationException($"Esiste già uno stato {stateType}");
        }

        _allStates.Add(stateType, state);
    }

    public void SetState(T stateType)
    {
        if (!_allStates.ContainsKey(stateType))
        {
            throw new InvalidOperationException($"Non esiste alcuno stato {stateType}");
        }

        PreviousStateType = CurrentStateType; // per debug

        _currentState?.OnExitState(); // chiamo la funzione che mi fa uscire dallo stato in cui sono, se esiste. aka onEnd

        CurrentStateType = stateType; // per debug

        _currentState = _allStates[stateType]; // seleziono lo stato che ho passato come parametro

        _currentState.OnEnterState(); // entro nel nuovo stato, aka start state
    }

    public void OnUpdate() => _currentState?.OnUpdate();
    public void OnFixedUpdate() => _currentState?.OnFixedUpdate();
    public void OnTriggerEnter() => _currentState?.OnTriggerEnter();
    public void OnTriggerExit() => _currentState?.OnTriggerExit();
    public void OnCollisionEnter() => _currentState?.OnCollisionEnter();
    public void OnCollisionExit() => _currentState?.OnCollisionExit();
}
