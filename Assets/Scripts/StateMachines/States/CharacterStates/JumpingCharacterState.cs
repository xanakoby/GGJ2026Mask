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

    }

    public override void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnterState()
    {
        if(_owner.debug)
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
        //controllo se sta salendo o scendendo
        //if(_owner.GetComponent<Rigidbody2D>().velocity.y < 0)
        //{
        //    //cambio stato in falling
        //    //_owner.SetState(ECharacterState.Falling);
        //}
    }
}
