using UnityEngine;

[CreateAssetMenu(fileName = "Change Command", menuName = "CptLost/Particle Behaviour/Commands/Change Command")]
public class ChangeChanceCommandSO : ParticleCommandSO
{
    [SerializeField] private ParticleSO _particleType;
    [SerializeField] private int _changeTime;
    [Range(0f, 100f)][SerializeField] private float _changeChance;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        if (particleData.CreationTick + _changeTime <= particleStorage.CurrentTick)
        {
            float randomPick = Random.Range(0f, 100f);

            if (randomPick > _changeChance)
            {
                return false;
            }

            particleStorage.CreateParticle(particleData, _particleType);

            return true;
        }

        return false;
    }
}
