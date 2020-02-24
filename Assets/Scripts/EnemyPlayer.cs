using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayer : MonoBehaviour
{

    public ParticleSystem bloodEffect;

    public void CreateBlood()
    {
        bloodEffect.Play();
    }
}
