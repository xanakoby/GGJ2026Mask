using DesignPatterns.Generics;

public class GameManager : Singleton<GameManager>
{
    public override void Awake()
    {
        base.Awake();
    }
}
