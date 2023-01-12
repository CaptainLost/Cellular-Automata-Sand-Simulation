using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleWorldTest : MonoBehaviour
{
    [SerializeField] private ParticleLogic _particleLogic;
    [SerializeField] private ParticleRenderer _particleRenderer;

    [SerializeField] private ParticleSO _testParticleType;
    [SerializeField] private ParticleSO _testParticleType2;
    [SerializeField] private ParticleSO _testParticleType3;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int pixelPosition = _particleRenderer.GetPixelPositionFromWorldPosition(worldMouse);

        ParticleData targetData = _particleLogic.ParticleStorage.GetParticleFromPosition(pixelPosition);

        if (targetData == null || !targetData.IsEmpty())
            return;

        if (Input.GetMouseButton(0))
        {
            _particleLogic.ParticleStorage.CreateParticleAtPosition(pixelPosition.x, pixelPosition.y, _testParticleType);
        }
        else if (Input.GetMouseButton(1))
        {
            _particleLogic.ParticleStorage.CreateParticleAtPosition(pixelPosition.x, pixelPosition.y, _testParticleType2);
        }
        else if (Input.GetMouseButton(2))
        {
            _particleLogic.ParticleStorage.CreateParticleAtPosition(pixelPosition.x, pixelPosition.y, _testParticleType3);
        }
        else if (Input.GetMouseButton(3))
        {
            
        }
    }
}
