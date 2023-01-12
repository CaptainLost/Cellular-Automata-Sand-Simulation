using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Particle", menuName = "CptLost/Particles/Particle")]
public class ParticleSO : ScriptableObject
{
    [field: SerializeField] public string ParticleName { get; private set; }
    [field: SerializeField] public ParticleCategory ParticleCategory { get; private set; }
    [field: SerializeField] public List<Color> ParticleColor { get; private set; }
    [field: SerializeField] public ParticleBehaviourSO ParticleBehaviour { get; private set; }
}
