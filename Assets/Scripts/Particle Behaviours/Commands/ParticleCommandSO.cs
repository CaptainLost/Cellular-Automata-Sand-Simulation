using UnityEngine;

public abstract class ParticleCommandSO : ScriptableObject
{
    public abstract bool Execute(ParticleData particleData, ParticleStorage particleStorage);
}
