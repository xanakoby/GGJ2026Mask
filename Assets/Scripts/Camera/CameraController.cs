using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;
    public CinemachineConfiner2D confiner;

    private void Start()
    {
        cinemachineCam.Follow = GameManager.Instance.player.clawAttackPoint;
    }
    public void SetBounding(Collider2D newBounds)
    {
        confiner.BoundingShape2D = newBounds;
    }
}
