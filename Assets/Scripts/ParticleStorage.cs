using System;
using UnityEngine;

public class ParticleStorage
{
    public ParticleStorage(int storageSizeX, int storageSizeY)
    {
        StorageSize = new Vector2Int(storageSizeX, storageSizeY);

        _storage = new ParticleData[storageSizeX, storageSizeY];

        for (int i = 0; i < storageSizeX; i++)
        {
            for (int j = 0; j < storageSizeY; j++)
            {
                _storage[i, j] = new ParticleData(i, j);
            }
        }
    }

    public Vector2Int StorageSize { get; private set; }
    public int CurrentTick { get; private set; }
    public Action<ParticleData> OnParticleUpdate;

    private ParticleData[,] _storage;

    public void IncrementTick()
    {
        CurrentTick++;
    }

    public bool ParticleWasUpdateCurrentTick(ParticleData particleData)
    {
        return particleData.LastUpdateTick == CurrentTick;
    }

    public bool CheckPositionInBound(int posX, int posY)
    {
        if (posX >= StorageSize.x || posY >= StorageSize.y || posX < 0 || posY < 0)
            return false;

        return true;
    }

    public void LoopThrough(Action<ParticleData> actionToPerform)
    {
        for (int y = 0; y < _storage.GetLength(1); y++)
        {
            for (int x = 0; x < _storage.GetLength(0); x++)
            {
                actionToPerform(_storage[x, y]);
            }
        }
    }

    public ParticleData GetParticleFromPosition(int posX, int posY)
    {
        if (!CheckPositionInBound(posX, posY))
            return null;

        return _storage[posX, posY];
    }

    public ParticleData GetParticleFromPosition(Vector2Int pos)
    {
        return GetParticleFromPosition(pos.x, pos.y);
    }

    public ParticleData CreateParticle(ParticleData data, ParticleSO particleType)
    {
        if (data == null)
            return null;

        data.CreateParticle(particleType);
        data.OnUpdate(CurrentTick);

        OnParticleUpdate?.Invoke(data);

        return data;
    }

    public ParticleData CreateParticleAtPosition(int posX, int posY, ParticleSO particleType)
    {
        if (!CheckPositionInBound(posX, posY))
            return null;

        ParticleData data = _storage[posX, posY];

        return CreateParticle(data, particleType);
    }

    public ParticleData SwapParticles(ParticleData dataA, ParticleData dataB)
    {
        if (dataA == null || dataB == null)
            return null;

        _storage[dataA.ParticlePosition.x, dataA.ParticlePosition.y] = dataB;
        _storage[dataB.ParticlePosition.x, dataB.ParticlePosition.y] = dataA;

        Vector2Int oldBPos = dataB.ParticlePosition;

        dataB.SetPosition(dataA.ParticlePosition);
        dataA.SetPosition(oldBPos);

        dataA.OnUpdate(CurrentTick);
        OnParticleUpdate?.Invoke(dataA);

        dataB.OnUpdate(CurrentTick);
        OnParticleUpdate?.Invoke(dataB);

        return dataA;
    }

    public ParticleData SwapParticles(int posAX, int posAY, int posBX, int posBY)
    {
        if (!CheckPositionInBound(posAX, posAY) || !CheckPositionInBound(posBX, posBY))
            return null;

        ParticleData dataA = _storage[posAX, posAY];
        ParticleData dataB = _storage[posBX, posBY];

        return SwapParticles(dataA, dataB);
    }
}
