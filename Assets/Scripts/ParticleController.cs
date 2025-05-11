using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject targetObject;
    public float speed = 5f;
    public float updateInterval = 0.1f; // Update every 0.1 seconds

    private ParticleSystem.Particle[] particles;
    private Coroutine particleUpdateCoroutine;
    public Vector3 targetPosition;
    private float _particlesLifetime;

    void Start()
    {
        if (particleSystem == null)
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        if (particleSystem == null)
        {
            Debug.LogError("Particle System not found!");
            return;
        }

        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        Vector3 meterPos = targetObject.transform.position;
        if (Camera.main != null)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(meterPos);
            targetPosition = newPos;
        }
        _particlesLifetime = particleSystem.main.startLifetime.constant;
    }
    
    public void PlayParticles()
    {
        particleSystem.Play();
    }

    IEnumerator UpdateParticles()
    {
        while (particleSystem.isEmitting)
        {
            int numParticlesAlive = particleSystem.GetParticles(particles);
            print(numParticlesAlive);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                particles[i].position = Vector3.Lerp(particles[i].position, targetPosition, 0.5f);
                
            }

            particleSystem.SetParticles(particles, numParticlesAlive);

            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void LateUpdate()
    {
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // particles[i].position = Vector3.Lerp(particles[i].position, targetPosition, _particlesLifetime);
            particles[i].position = Vector3.MoveTowards(particles[i].position, targetPosition, speed * Time.deltaTime);
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
