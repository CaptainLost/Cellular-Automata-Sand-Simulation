using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePixelUpdate : MonoBehaviour
{
    [SerializeField] private ParticleLogic _particleLogic;

    private List<ParticleData> _particlesToUpdate = new List<ParticleData>();

    private void OnEnable()
    {
        _particleLogic.OnParticleUpdate += OnParicleUpdate;
        _particleLogic.OnTick += OnTick;
    }

    private void OnDisable()
    {
        _particleLogic.OnParticleUpdate -= OnParicleUpdate;
        _particleLogic.OnTick -= OnTick;
    }

    private void OnParicleUpdate(ParticleData particleData)
    {
        _particlesToUpdate.Add(particleData);
    }

    private void UpdateParticle(ParticleData particleData)
    {
        if (particleData.IsEmpty())
        {
            _particleLogic.ParticleRenderer.SetEmptySpace(particleData.ParticlePosition);

            return;
        }

        _particleLogic.ParticleRenderer.SetPixelColor(particleData.ParticlePosition, particleData.ParticleColor);
    }

    private void OnTick()
    {
        if (_particlesToUpdate.Count <= 0)
            return;

        foreach (ParticleData particleData in _particlesToUpdate)
        {
            UpdateParticle(particleData);
        }

        _particleLogic.ParticleRenderer.ApplyPixelChanges();

        _particlesToUpdate.Clear();
    }
}
