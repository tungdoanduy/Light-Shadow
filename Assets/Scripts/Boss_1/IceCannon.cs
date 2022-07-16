using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceCannon : MonoBehaviour
{
    [SerializeField] ParticleSystem iceAttack;
    [SerializeField] Slider slider;
    [SerializeField] float cooldownTime;
    float lastTimeAttack;
    [SerializeField] GameObject boss;
    [SerializeField] DialogueManager dialogueManager;
    private void Start()
    {
        lastTimeAttack = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = (Time.time - lastTimeAttack) / cooldownTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (slider.value>=1&&collision.CompareTag("Player")&&boss.GetComponent<Boss_1>().HealthBar.value != 0&& dialogueManager.IsEndDialogue)
        { 
            iceAttack.Play();
            lastTimeAttack = Time.time;
        }
    }
}
