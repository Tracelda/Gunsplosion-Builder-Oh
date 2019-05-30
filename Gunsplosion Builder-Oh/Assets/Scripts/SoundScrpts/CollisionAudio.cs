using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioClip clip;
    public int maxImpacts;
    private int impacts = 0;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (clip && impacts < maxImpacts)
        {
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            int i = 0;

            while (i < numCollisionEvents)
            {
                if (rb)
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    AudioSource.PlayClipAtPoint(clip, pos);
                    impacts++;
                }
                i++;
            }
        }
    }
}
