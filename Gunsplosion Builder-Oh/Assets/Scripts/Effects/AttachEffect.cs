using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachEffect : MonoBehaviour
{
    public Transform target;
    public List<ParticleSystem> particleEmitter = new List<ParticleSystem>();
    private bool active;

    private void Start() {
        active = true;
    }

    void Update()
    {
        if (active) {
            if (target) {
                transform.position = target.position;
                transform.rotation = target.rotation;

                Bullet bullet = target.GetComponent<Bullet>();
                if (bullet && !bullet.active) {
                    target = null;
                }
            }
            else {
                active = false;
                float maxLifetime = 0;
                foreach (ParticleSystem particle in particleEmitter) {
                    ParticleSystem.EmissionModule emitter = particle.emission;
                    ParticleSystem.MainModule main = particle.main;
                    emitter.rateOverTime = 0;
                    if (particle.main.startLifetime.constantMax > maxLifetime) {
                        maxLifetime = particle.main.startLifetime.constantMax;
                    }
                }
                Destroy(gameObject, maxLifetime);
            }
        }
    }
}
