using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_1 : MonoBehaviour
{
    GameObject boss;
    Animator anim;
    [SerializeField]bool isFullHealth;
    float deadTime = 0;
    bool hasdeadTime = false;
    bool disappearPhase4 = false;
    bool appearPhase4 = false;
    bool endChangeToPhase4 = false;
    float timeToPhase4;
    float timeAttacked;
    bool isAttacked = false;
    [Header("Fire")]
    [SerializeField] ParticleSystem[] fire;
    //int numberOfFire;
    float timePickFire;
    [Header("Standing Block")]
    [SerializeField] GameObject group_1;
    [SerializeField] GameObject group_2;
    [SerializeField] GameObject group_3;
    [SerializeField] GameObject group_4; // moving platform
    [Header("Cannon")]
    [SerializeField] GameObject cannon_phase_3;
    [SerializeField] GameObject cannon_phase_4;
    [Header("Health")]
    [SerializeField] Slider healthBar;
    public Slider HealthBar
    { get { return healthBar; } }
    [SerializeField] Gradient healthColor;
    [SerializeField] Image fill;
    [Header("Transition")]
    [SerializeField] GameObject heartOfTheMountain;
    [SerializeField] ParticleSystem startTransition;
    [SerializeField] ParticleSystem endTransition;
    [Header("DialogueSystem")]
    [SerializeField] Story storyPrepare; bool startStoryPrepare = false;
    [SerializeField] Story storyPhase4; bool startStoryPhase4 = false;
    [SerializeField] Story storyDie; bool startStoryDie = false;
    [SerializeField] DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject;
        anim = boss.GetComponent<Animator>();
        healthBar.value = 0;
        isFullHealth = false;
        timePickFire = -2;
        cannon_phase_3.SetActive(false);
        cannon_phase_4.SetActive(false);
        group_4.SetActive(false);
        group_1.SetActive(true);
        group_2.SetActive(true);
        group_3.SetActive(true);
        heartOfTheMountain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransition.isStopped&&endTransition.isStopped)
        {
            if (!isFullHealth)
            {
                if (!startStoryPrepare) 
                {
                    startStoryPrepare = true;
                    dialogueManager.IsEndDialogue = false;
                    storyPrepare.TriggerDialogue();
                }
                if (startStoryPrepare&& dialogueManager.IsEndDialogue) Preparing();
            }
            if (isFullHealth)
            {
                if (healthBar.value >= 0.8f) Phase1();
                if (healthBar.value >= 0.5f && healthBar.value < 0.8f) Phase2();
                if (healthBar.value >= 0.2f && healthBar.value < 0.5f) Phase3();
                if (healthBar.value < 0.2f && healthBar.value > 0)
                {
                    if (!startStoryPhase4)
                    {
                        startStoryPhase4 = true;
                        dialogueManager.IsEndDialogue = false;
                        storyPhase4.TriggerDialogue();
                    }
                    if (storyPhase4&&dialogueManager.IsEndDialogue) Phase4();
                }
                if (healthBar.value == 0)
                {
                    if (!startStoryDie)
                    {
                        startStoryDie = true;
                        dialogueManager.IsEndDialogue = false;
                        storyDie.TriggerDialogue();
                    }
                    if (storyDie && dialogueManager.IsEndDialogue) Die();
                }
                if (isAttacked&&healthBar.value!=0.2f&&healthBar.value!=0)
                {
                    if (Time.time<timeAttacked+1) boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - (Time.time - timeAttacked));
                    if (Time.time>=timeAttacked+1&&Time.time<timeAttacked+2) boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Time.time - timeAttacked -1);
                    if (Time.time >= timeAttacked + 2) isAttacked = false;
                }
            }
        }
       
    }
    void Preparing()
    {
        if (healthBar.value <= 1 )
        {
            healthBar.value += 0.005f;
            fill.color = healthColor.Evaluate(healthBar.value);
            fill.GetComponent<CanvasGroup>().alpha = healthBar.value;
            if (healthBar.value == 1) isFullHealth = true;
        }
    }
    void Phase1()
    {
        int pickFire = Random.Range(0, 9);
        if (Time.time>=timePickFire+1&& fire[pickFire].isStopped)
        {
            timePickFire = Time.time;
            fire[pickFire].Play();
        }
    }
    void Phase2()
    {
        group_1.SetActive(false);
        int pickFire = Random.Range(0, 9);
        if (Time.time >= timePickFire + 1 && fire[pickFire].isStopped)
        {
            timePickFire = Time.time;
            fire[pickFire].Play();
        }
    }
    void Phase3()
    {
        group_2.SetActive(false);
        cannon_phase_3.SetActive(true);
        int pickFire = Random.Range(0, 9);
        if (Time.time >= timePickFire + 1 && fire[pickFire].isStopped)
        {
            timePickFire = Time.time;
            fire[pickFire].Play();
        }
    }
    void ChangeAnimationToPhase4()
    {
        if (!disappearPhase4)
        {
            boss.GetComponent<BoxCollider2D>().isTrigger = true;
            timeToPhase4 = Time.time;
            disappearPhase4 = true;
        }
        if (!anim.GetBool("Chaos")) boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - (Time.time - timeToPhase4) / 2);
        if (boss.GetComponent<SpriteRenderer>().color.a <= 0 && !appearPhase4)
        {
            anim.SetBool("Chaos", true);
            timeToPhase4 = Time.time;
            appearPhase4 = true;
        }
        if (anim.GetBool("Chaos"))
        {
            boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (Time.time - timeToPhase4) / 2);
            if (Time.time>=timeToPhase4+1) endChangeToPhase4 = true;
        }
    }
    void Phase4()
    {  
        ChangeAnimationToPhase4();
        if (endChangeToPhase4)
        {
            boss.GetComponent<BoxCollider2D>().isTrigger = false;
            group_3.SetActive(false);
            group_4.SetActive(true);
            cannon_phase_4.SetActive(true);
            int pickFire = Random.Range(0, 9);
            if (Time.time >= timePickFire + 2 && fire[pickFire].isStopped)
            {
                timePickFire = Time.time;
                fire[pickFire].Play();
            }
        }  
    }
    void Die()
    {
        if (!hasdeadTime)
        {
            boss.GetComponent<BoxCollider2D>().isTrigger = true;
            deadTime = Time.time;
            hasdeadTime = true;
        }
        boss.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1- (Time.time - deadTime) / 2);
        cannon_phase_3.SetActive(false);
        cannon_phase_4.SetActive(false);
        if (Time.time>=deadTime+2) heartOfTheMountain.SetActive(true);
    }
    
    public void Attacked()
    {
        
        healthBar.value -= 0.1f;
        fill.color = healthColor.Evaluate(healthBar.value);
        timeAttacked = Time.time;
        isAttacked = true;
    }
}
