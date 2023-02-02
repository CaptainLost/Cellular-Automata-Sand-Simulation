using UnityEngine;

[CreateAssetMenu(fileName = "Die Command", menuName = "CptLost/Particle Behaviour/Commands/Die Command")]
public class DieCommandSO : ParticleCommandSO
{
    [SerializeField] private int _dieTime;
    [Range(0f, 100f)] [SerializeField] private float _dieChance;

    public override bool Execute(ParticleData particleData, ParticleStorage particleStorage)
    {
        if (particleData.CreationTick + _dieTime <= particleStorage.CurrentTick)
        {
            float randomPick = Random.Range(0f, 100f);

            if (randomPick > _dieChance)
            {
                return false;
            }

            particleStorage.ClearParticle(particleData);

            return true;
        }

        return false;
    }
}
