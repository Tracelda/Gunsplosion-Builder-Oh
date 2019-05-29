using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public enum ParticleTypes { StraightShot, BounceShot, HomingShot};
    public GameObject straightShot, bounceShot, homingShot;

    public static EffectManager instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    public void AttachParticle(Transform target, ParticleTypes type) {
        if (GetPrefab(type)) {
            AttachEffect particle = Instantiate(GetPrefab(type), null).GetComponent<AttachEffect>();
            particle.target = target;
        }
    }

    private GameObject GetPrefab(ParticleTypes type) {
        switch (type) {
            case ParticleTypes.StraightShot:
                return straightShot;
            case ParticleTypes.BounceShot:
                return bounceShot;
            case ParticleTypes.HomingShot:
                return homingShot;
        }
        return null;
    }
}
