using Cinemachine;
using UnityEngine;

public enum LockAxis
{
    X, Y, Z
}

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]
public class CinemachineXLock : CinemachineExtension
{
    public LockAxis LockAxis;
    public float LockPosition;
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;

            switch (LockAxis)
            {
                case LockAxis.X:
                    pos.x = LockPosition;
                    break;
                case LockAxis.Y:
                    pos.y = LockPosition;
                    break;
                case LockAxis.Z:
                    pos.z = LockPosition;
                    break;
            }

            state.RawPosition = pos;
        }
    }

   
}
