using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Velocity Move Command", menuName = "CptLost/Particle Behaviour/Commands/Velocity Move Command")]
public class VelocityMoveCommandSO : ParticleCommandSO
{
    [SerializeField] private Vector2Int _testVelocity;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        Vector2Int targetPos = particleData.ParticlePosition + _testVelocity;

        if (targetPos == particleData.ParticlePosition)
            return false;

        int xMoveAbs = Mathf.Abs(_testVelocity.x);
        int yMoveAbs = Mathf.Abs(_testVelocity.y);

        float slope = yMoveAbs / (float)xMoveAbs;
        //Debug.Log($"Slope {slope}");

        ParticleData finalData = null;

        if (xMoveAbs >= yMoveAbs)
        {
            for (int x = 1; x <= xMoveAbs; x++)
            {
                int y = Mathf.RoundToInt(slope * x);

                ParticleData targetData = particleStorage.GetParticleFromPosition(particleData.ParticlePosition.x + x, particleData.ParticlePosition.y + y);

                if (targetData == null || !targetData.IsEmpty())
                    break;

                finalData = targetData;

                //Debug.Log($"New pos {x} {y}");
            }
        }

        if (finalData != null)
        {
            particleStorage.SwapParticles(particleData, finalData);
        }

        return true;
    }
}
