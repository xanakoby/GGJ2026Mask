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
        Debug.Log("Sto uscendo da Idle");
    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState()
    {
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
        Debug.Log("Sono nell'update di Idle");

        if (_owner.InputPressed == 'W')
        {
            _owner.SetState(ECharacterState.Jumping);
        }
    }
}
