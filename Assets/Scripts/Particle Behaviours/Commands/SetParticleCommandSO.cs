using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SetParticleCommand
{
    public Vector2Int ReplacePosition;
    public ParticleSO Particle;
    [Range(0f, 100f)] public float SetChance;
    public bool IfFlammable;
    public bool IfFreeSpace;
}

[CreateAssetMenu(fileName = "Set Particle Command", menuName = "CptLost/Particle Behaviour/Commands/Set Particle Command")]
public class SetParticleCommandSO : ParticleCommandSO
{
    [SerializeField] private SetParticleCommand[] _setCommand;
    [SerializeField] private bool _randomizePick;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        List<SetParticleCommand> replaceCommands = _setCommand.ToList();

        if (_randomizePick)
            replaceCommands.Shuffle();

        for (int i = 0; i < replaceCommands.Count; i++)
        {
            SetParticleCommand replaceCommand = replaceCommands[i];

            Vector2Int targetPos = particleData.ParticlePosition + replaceCommand.ReplacePosition;
            ParticleData targetData = particleStorage.GetParticleFromPosition(targetPos);

            if (targetData == null)
                continue;

            if (replaceCommand.Particle == null || (!targetData.IsEmpty() && replaceCommand.Particle == targetData.ParticleType))
                continue;

            if (replaceCommand.IfFreeSpace && !targetData.IsEmpty())
                continue;

            if (replaceCommand.IfFlammable && (targetData.IsEmpty() || !targetData.ParticleType.IsFlammable))
                continue;

            float randomPick = UnityEngine.Random.Range(0f, 100f);

            if (randomPick > replaceCommand.SetChance)
                continue;

            particleStorage.CreateParticleAtPosition(targetPos.x, targetPos.y, replaceCommand.Particle);

            return true;
        }

        return false;
    }
}
