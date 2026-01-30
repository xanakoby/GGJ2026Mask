using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Data", menuName = "ScriptableObjects/PlayerMovementData")]
public class PlayerMovementData : ScriptableObject
{
    public string MovementName;
    public float Speed;
    public float JumpForce;
    public float GravityAdded;
}
