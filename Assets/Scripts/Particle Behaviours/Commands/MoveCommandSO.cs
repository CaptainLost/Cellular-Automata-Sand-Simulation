using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MoveCommand
{
    public Vector2Int MovePosition;
    public bool HasToBeFree;
}

[CreateAssetMenu(fileName = "Move Command", menuName = "CptLost/Particle Behaviour/Commands/Move Command")]
public class MoveCommandSO : ParticleCommandSO
{
    [SerializeField] private MoveCommand[] _moveCommands;
    [SerializeField] private bool _randomizePick;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        List<MoveCommand> moveCommands = _moveCommands.ToList();

        if (_randomizePick)
            moveCommands.Shuffle();

        for (int i = 0; i < moveCommands.Count; i++)
        {
            MoveCommand moveCommand = moveCommands[i];

            Vector2Int targetPos = particleData.ParticlePosition + moveCommand.MovePosition;
            ParticleData targetData = particleStorage.GetParticleFromPosition(targetPos);

            if (targetData == null)
                continue;

            if (moveCommand.HasToBeFree && !targetData.IsEmpty())
                continue;

            particleStorage.SwapParticles(particleData, targetData);

            return true;
        }

        return false;
    }
}
