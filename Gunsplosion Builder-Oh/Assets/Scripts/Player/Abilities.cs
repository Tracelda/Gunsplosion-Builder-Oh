﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public enum abilityType
    {
        NONE, JETPACK, SHIELD, SPEEDBOOST
    };

    public abilityType type;
}
