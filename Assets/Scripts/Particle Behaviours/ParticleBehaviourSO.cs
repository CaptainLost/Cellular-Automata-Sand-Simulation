using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle Behaviour", menuName = "CptLost/Particle Behaviour/Particle Behaviour")]
public class ParticleBehaviourSO : ScriptableObject
{
    [SerializeField] private List<ParticleCommandSO> _moveCommands;

    public virtual bool DoStep(ParticleData particleData, ParticleStorage particleStorage)
    {
        foreach (ParticleCommandSO command in _moveCommands)
        {
            if (command == null)
                continue;

            bool hasBeenProcessed = command.Execute(particleData, particleStorage);

            if (hasBeenProcessed)
                return true;
        }

        return false;
    }
}
