using UnityEngine;

public class ParticleData
{
    public ParticleData(int startPosX, int startPosY)
    {
        ParticlePosition = new Vector2Int(startPosX, startPosY);
    }

    public int LastUpdateTick { get; private set; } = 0;
    public int CreationTick { get; private set; } = 0;
    public Vector2Int ParticlePosition { get; private set; }
    public Vector2Int ParticleVelocity { get; private set; }

    public ParticleSO ParticleType = null;
    public Color ParticleColor = Color.white;

    public void OnUpdate(int updateTick)
    {
        LastUpdateTick = updateTick;
    }

    public void SetPosition(Vector2Int newPos)
    {
        ParticlePosition = newPos;
    }

    public void SetPosition(int newPosX, int newPosY)
    {
        SetPosition(new Vector2Int(newPosX, newPosY));
    }

    public bool IsEmpty()
    {
        return ParticleType == null;
    }

    public void CreateParticle(ParticleSO particleType, int updateTick)
    {
        ParticleType = particleType;
        ParticleColor = particleType.ParticleColor.Count > 0 ?
            particleType.ParticleColor[UnityEngine.Random.Range(0, particleType.ParticleColor.Count)] :
            Color.white;

        CreationTick = updateTick;
    }

    public void ClearParticle()
    {
        ParticleType = null;
    }
}
