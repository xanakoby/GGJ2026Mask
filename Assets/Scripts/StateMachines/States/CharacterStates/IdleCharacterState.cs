using UnityEngine;

public class IdleCharacterState : State 
{
    public IdleCharacterState(Player player)
    {
        _owner = player;
    }

    public Player _owner { get; }

    public override void OnCollisionEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState()
    {
        
    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState()
    {
        if(_owner.debug)
        Debug.Log("Sto entrando in Idle");
    }

    public override void OnTriggerEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //controllo se premo un tasto di movimento
        if (_owner.IsWalking)
        {
            _owner.SetState(ECharacterState.Walking);
        }
    }
}
