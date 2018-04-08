using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnder : MonoBehaviour
{
    public ParticleSystem explodeParticle;

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void CreateParticle()
    {
        Instantiate(explodeParticle, transform.position, transform.rotation);
    }
}
