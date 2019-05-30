using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachEffect : MonoBehaviour
{
    public Transform target;
    public List<ParticleSystem> particleEmitter = new List<ParticleSystem>();
    private bool active;
    private Animator anim;
    private AudioSource audioSource;
    public float muteSpeed = 10;
    public AudioClip particleCollideClip;

    private void Start() {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        active = true;
        StartCoroutine(DelayedStart());
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
            else if (!anim){
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
            else {
                active = false;
                float killTime = 1;
                if (audioSource && audioSource.clip.length > killTime)
                    killTime = audioSource.clip.length;
                Destroy(gameObject, killTime);
            }
        }
        else
        {
            if (audioSource)
            {
                audioSource.volume -= Time.deltaTime * muteSpeed;
            }
        }
    }

    IEnumerator DelayedStart() {
        yield return new WaitForEndOfFrame();
        foreach (ParticleSystem particle in particleEmitter) {
            particle.Play();
        }
    }
}
