using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReplaceCommand
{
    public Vector2Int ReplacePosition;
    public ParticleCategory ReplaceCategory;
}

[CreateAssetMenu(fileName = "Replace Command", menuName = "CptLost/Particle Behaviour/Commands/Replace Command")]
public class ReplaceCategoryCommandSO : ParticleCommandSO
{
    [SerializeField] private ReplaceCommand[] _replaceCommands;
    [SerializeField] private bool _randomizePick;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        List<ReplaceCommand> replaceCommands = _replaceCommands.ToList();

        if (_randomizePick)
            replaceCommands.Shuffle();

        for (int i = 0; i < replaceCommands.Count; i++)
        {
            ReplaceCommand replaceCommand = replaceCommands[i];

            Vector2Int targetPos = particleData.ParticlePosition + replaceCommand.ReplacePosition;
            ParticleData targetData = particleStorage.GetParticleFromPosition(targetPos);

            if (targetData == null)
                continue;

            if (targetData.IsEmpty() || targetData.ParticleType.ParticleCategory != replaceCommand.ReplaceCategory)
                continue;

            particleStorage.SwapParticles(particleData, targetData);

            return true;
        }

        return false;
    }
}
