using DesignPatterns.Generics;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerInput playerInput;

    public override void Awake()
    {
        base.Awake();
    }
}
