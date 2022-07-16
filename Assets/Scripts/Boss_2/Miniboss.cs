using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miniboss : MonoBehaviour
{
    GameObject miniboss;
    Animator anim;
    float timeAttacked;
    bool isAttacked=false;
    float timeDie;
    bool isDie = false;
    [SerializeField] GameObject boss;
    [Header("Attack VFX")]
    [SerializeField] ParticleSystem fireball;
    [SerializeField] ParticleSystem energyIncreasing;
    [Header("HealthBar")]
    [SerializeField] Slider healthBar;
    bool isStart = false;
    float lastTimeAttack;
    float lastTimeShoot;
    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        miniboss = gameObject;
        anim = gameObject.GetComponent<Animator>();
        energyIncreasing.Stop();
        fireball.Stop();
        anim.Play("Miniboss_Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (miniboss.activeInHierarchy&&!isStart)
        {
            isStart = true;
            lastTimeShoot = Time.time;
        }
        if (healthBar.value>0)
        {
            if (Time.time >= lastTimeShoot + 5 && !isAttack)
            {
                anim.Play("Miniboss_Shoot");
                isAttack = true;
                energyIncreasing.Play();
                lastTimeAttack = Time.time;
            }
            if (isAttack && Time.time >= lastTimeAttack + 2)
            {
                anim.Play("Miniboss_Idle");
                isAttack = false;
                energyIncreasing.Stop();
                fireball.Play();
                lastTimeShoot = Time.time;
            }
            if (isAttacked)
            {
                miniboss.GetComponent<SpriteRenderer>().color = new Color(1, (Time.time - timeAttacked) / 0.64f, (Time.time - timeAttacked) / 0.64f, 1);
                if (Time.time >= timeAttacked + 1) isAttacked = false;
            }
        }
        if (healthBar.value == 0) Die();
    }
    public void Attacked()
    {
        healthBar.value -= 0.25f;
        timeAttacked = Time.time;
        isAttacked = true;
        miniboss.GetComponent<SpriteRenderer>().color = new Color(1, 0.36f, 0.36f, 1);
        
    }
    void Die()
    {
       if (!isDie)
       {
            fireball.Stop();
            energyIncreasing.Stop();
            miniboss.GetComponent<BoxCollider2D>().isTrigger = true;
            timeDie = Time.time;
            boss.GetComponent<Boss_2>().Attacked();
            isDie = true;
       }
        miniboss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - (Time.time - timeDie) / 2);
    }
    private void OnMouseDown()
    {
        Attacked();
    }
}
