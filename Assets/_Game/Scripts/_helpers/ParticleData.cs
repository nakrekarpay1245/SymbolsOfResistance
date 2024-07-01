using UnityEngine;

[System.Serializable]
public class ParticleData
{
    public ParticleSystem ParticleSystem;
    public int ParticleCount;
    public string ParticleName;

    public ParticleData(ParticleSystem ps, int count, string name)
    {
        ParticleSystem = ps;
        ParticleCount = count;
        ParticleName = name;
    }
}