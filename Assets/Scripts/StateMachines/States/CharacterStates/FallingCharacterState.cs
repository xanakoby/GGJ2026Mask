using UnityEngine;

public class FallingCharacterState : State
{
    public FallingCharacterState(Player player)
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
        if (_owner.debug)
            Debug.Log("Sto entrando in Falling");
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
        //controllo se ha toccato terra
        //if(_owner.GetComponent<Rigidbody2D>().velocity.y == 0)
        //{
        //    //cambio stato in idle o walking
        //    if(_owner.isWalking)
        //    {
        //        _owner.SetState(ECharacterState.Walking);
        //    }
        //    else
        //    {
        //        _owner.SetState(ECharacterState.Idle);
        //    }
        //}
    }
}
