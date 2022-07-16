using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCannon : MonoBehaviour
{
    [SerializeField] GameObject buttonShoot;
    [SerializeField] ParticleSystem fireball;
    [SerializeField] ParticleSystem energyIncreasing;
    [SerializeField] ParticleSystem energyMax;
    bool energyMaxOn = false;
    [SerializeField] Slider slider;
    [SerializeField] float cooldownTime;
    float lastTimeAttack;
    [SerializeField] GameObject boss;
    [SerializeField] DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        lastTimeAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = (Time.time - lastTimeAttack) / cooldownTime;
        if (slider.value>=1)
        {
            if (!energyMaxOn)
            {
                energyMax.Play();
                energyMaxOn = true;
            }
                energyIncreasing.Stop();
            if (buttonShoot.GetComponent<WallButton>().PlayerIn && boss.GetComponent<Boss_2>().HealthBar.value != 0 && dialogueManager.IsEndDialogue)
            {
                energyMax.Stop();
                fireball.Play();
                energyIncreasing.Play();
                lastTimeAttack = Time.time;
                energyMaxOn = false;
            }
        }
    }
}
