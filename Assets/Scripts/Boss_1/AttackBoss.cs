using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;
    private void OnParticleCollision(GameObject other)
    {
        
        if (other.CompareTag("Boss"))
        {
            boss.GetComponent<Boss_1>().Attacked();
        }
    }
}
