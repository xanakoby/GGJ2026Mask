using UnityEngine;

public class JumpingCharacterState : State
{
    public JumpingCharacterState(Player player)
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
        Debug.Log("Sto uscendo da Jumping");
    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState()
    {
        Debug.Log("Sto entrando in Jumping");
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
        Debug.Log("Sono nell'update di Jumping");
    }
}
