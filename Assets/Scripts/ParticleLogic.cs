using System;
using System.Collections;
using UnityEngine;

public class ParticleLogic : MonoBehaviour
{
    [field: SerializeField] public ParticleRenderer ParticleRenderer { get; private set; }

    [SerializeField] private float _updateTime;
    public bool IsSimulationRunning;

    public ParticleStorage ParticleStorage { get; private set; }

    public Action<ParticleData> OnParticleUpdate;
    public Action<int> OnTick;

    private IEnumerator _updateCoroutine;

    private void Start()
    {
        ParticleStorage = new ParticleStorage(ParticleRenderer.SimulationSize.x, ParticleRenderer.SimulationSize.y);
        ParticleStorage.OnParticleUpdate += OnParticleUpdate;

        _updateCoroutine = UpdateData();
        StartCoroutine(_updateCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(_updateCoroutine);
    }

    private IEnumerator UpdateData()
    {
        while (true)
        {
            yield return new WaitForSeconds(_updateTime);

            if (IsSimulationRunning)
            {
                ParticleStorage.IncrementTick();

                ApplyLogic();

                OnTick?.Invoke(ParticleStorage.CurrentTick);
            }
        }
    }

    private void ApplyLogic()
    {
        ParticleStorage.LoopThrough((particleData) =>
        {
            if (particleData.IsEmpty() || ParticleStorage.ParticleWasUpdateCurrentTick(particleData))
                return;

            ParticleSO particleType = particleData.ParticleType;
            ParticleBehaviourSO particleBehaviour = particleType.ParticleBehaviour;

            if (particleBehaviour == null)
                return;

            particleBehaviour.DoStep(particleData, ParticleStorage);
        });
    }
}
