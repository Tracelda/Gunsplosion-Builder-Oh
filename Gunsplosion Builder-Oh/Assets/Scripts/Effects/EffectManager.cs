using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public enum ParticleTypes { StraightShot, BounceShot, HomingShot, SmallExplosion, LargeExplosion, SmallDust, LargeDust};
    public GameObject straightShot, bounceShot, homingShot, smallExplosion, largeExplosion, smallDust, largeDust;

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

    public void PlaceParticle(Vector3 position, ParticleTypes type) {
        if (GetPrefab(type)) {
            Instantiate(GetPrefab(type), position, Quaternion.identity);
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
            case ParticleTypes.SmallExplosion:
                return smallExplosion;
            case ParticleTypes.LargeExplosion:
                return largeExplosion;
            case ParticleTypes.SmallDust:
                return smallDust;
            case ParticleTypes.LargeDust:
                return largeDust;
        }
        return null;
    }
}
