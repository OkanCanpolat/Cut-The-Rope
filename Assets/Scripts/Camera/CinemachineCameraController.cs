using Cinemachine;
using UnityEngine;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera characterVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera candyVirtualCamera;

    private void Start()
    {
        characterVirtualCamera.gameObject.SetActive(false);
        candyVirtualCamera.gameObject.SetActive(true);
    }
}
