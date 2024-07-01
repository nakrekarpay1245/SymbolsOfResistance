using Leaf._helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<ParticleData> ParticleDataList = new List<ParticleData>();
    public List<ParticleSystem> ParticleList = new List<ParticleSystem>();

    private void Awake()
    {
        GenerateParticles();
        StopAndDeactivateAllParticles();
    }

    public void GenerateParticles()
    {
        for (int i = 0; i < ParticleDataList.Count; i++)
        {
            ParticleData data = ParticleDataList[i];

            for (int j = 0; j < data.ParticleCount; j++)
            {
                ParticleSystem particleInstance = Instantiate(data.ParticleSystem);
                particleInstance.Stop();
                particleInstance.name = data.ParticleName;
                particleInstance.transform.parent = transform;
                ParticleList.Add(particleInstance);
            }
        }
    }

    public void StopAndDeactivateAllParticles()
    {
        for (int i = 0; i < ParticleDataList.Count; i++)
        {
            ParticleData data = ParticleDataList[i];
            for (int j = 0; j < ParticleList.Count; j++)
            {
                ParticleSystem particle = ParticleList[j];
                if (particle.name == data.ParticleName)
                {
                    ParticleSystem particlesystem = particle;
                    particlesystem.Stop();
                    particlesystem.gameObject.SetActive(false);
                    particlesystem.transform.parent = transform;
                }
            }
        }

    }

    public void PlayParticleAtPoint(string particleName, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        for (int i = 0; i < ParticleDataList.Count; i++)
        {
            ParticleData data = ParticleDataList[i];
            if (data.ParticleName == particleName)
            {
                for (int j = 0; j < ParticleList.Count; j++)
                {
                    ParticleSystem particle = ParticleList[j];
                    if (particle.name == particleName && !particle.isPlaying)
                    {
                        ParticleSystem particleSystem = particle;
                        particleSystem.transform.parent = parent;
                        particleSystem.transform.position = position;
                        particleSystem.transform.rotation = rotation;
                        particleSystem.gameObject.SetActive(true);
                        particleSystem.Play();

                        if (!particleSystem.main.loop)
                        {
                            Debug.Log(particle.name + " is not looping");
                            StartCoroutine(DeactivateAfterTime(particle, particle.main.duration));
                        }
                        else
                        {
                            Debug.Log(particle.name + " looping");
                        }

                        return;
                    }
                }
            }
        }
    }

    public void PlayParticleAtPoint(string particleName, Vector3 position, Transform parent = null)
    {
        for (int i = 0; i < ParticleDataList.Count; i++)
        {
            ParticleData data = ParticleDataList[i];
            if (data.ParticleName == particleName)
            {
                for (int j = 0; j < ParticleList.Count; j++)
                {
                    ParticleSystem particle = ParticleList[j];
                    if (particle.name == particleName && !particle.isPlaying)
                    {
                        ParticleSystem particlesystem = particle;
                        particlesystem.transform.parent = parent;
                        particlesystem.transform.position = position;
                        particlesystem.gameObject.SetActive(true);
                        particlesystem.Play();

                        StartCoroutine(DeactivateAfterTime(particle, particle.main.duration));
                        return;
                    }
                }
            }
        }
    }

    private IEnumerator DeactivateAfterTime(ParticleSystem particle, float time)
    {
        yield return new WaitForSeconds(time);
        particle.gameObject.SetActive(false);
    }
}