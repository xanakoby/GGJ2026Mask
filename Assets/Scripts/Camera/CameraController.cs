using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera cinemachineCam;

    private void Start()
    {
        cinemachineCam.Follow = GameManager.Instance.player.clawAttackPoint;
    }
}
