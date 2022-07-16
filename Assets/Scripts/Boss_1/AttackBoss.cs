using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("Boss"))
        {
            if(other.name == "Boss_1") other.GetComponent<Boss_1>().Attacked();
            if (other.name == "Boss_2") other.GetComponent<Boss_2>().Attacked();
            if (other.name == "Miniboss")
            {
                other.GetComponent<Miniboss>().Attacked();
            }
        }
    }
}
