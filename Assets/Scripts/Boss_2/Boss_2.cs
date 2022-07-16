using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Boss_2 : MonoBehaviour
{
    GameObject boss;
    Animator anim;
    SpriteRenderer spriteRenderer;
    bool isFullHealth = false;
    float timeToPhase1;
    bool onPhase1 = false;
    float timeToPhase2;
    bool onPhase2 = false;
    float timeToPhase3;
    bool onPhase3 = false;
    bool appearPhase3 = false;
    bool endAnimationPhase3 = false;
    float timeToPhase4;
    bool onPhase4 = false;
    bool appearPhase4 = false;
    bool endAnimationPhase4 = false;
    float timeAttacked;
    bool isAttacked = false;
    float timeDie;
    bool isDead = false;
    [Header("Health")]
    [SerializeField] Slider healthBar;
    public Slider HealthBar
    { get { return healthBar; } }
    [SerializeField] Gradient healthColor;
    [SerializeField] Image fill;
    [Header("Transition")]
    [SerializeField] GameObject lunarDrop;
    [SerializeField] ParticleSystem startTransition;
    [SerializeField] ParticleSystem endTransition;
    [Header("Miniboss")]
    [SerializeField] GameObject minibossPhase1;
    bool appearAfterPhase1 = false;
    float timeAppearAfterPhase1;
    [SerializeField] GameObject minibossPhase2;
    bool appearAfterPhase2 = false;
    float timeAppearAfterPhase2;
    [Header("Spell")]
    [SerializeField] ParticleSystem circleOfFire;
    [SerializeField] ParticleSystem[] explosion;
    float timeExplosion;
    bool isExplode = false;
    [SerializeField] GameObject bulletPhase4;
    float timeBullet;
    [Header("Dialogue System")]
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] Story[] story;
    bool[] startStory;
    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject;
        anim = boss.GetComponent<Animator>();
        spriteRenderer = boss.GetComponent<SpriteRenderer>();
        boss.GetComponent<BoxCollider2D>().isTrigger = true;
        minibossPhase1.SetActive(false);
        minibossPhase2.SetActive(false);
        healthBar.value = 0;
        //bulletPhase4.SetActive(false);
        //lunarDrop.SetActive(false);
        startStory = new bool[story.Length];
        for (int i=0;i<story.Length;i++)
        {
            startStory[i] = false;
        }
        anim.Play("Boss_2_Phase_1");
    }

    // Update is called once per frame
    void Update()
    {
        if (startTransition.isStopped&&endTransition.isStopped)
        {
            if (!isFullHealth)
            {
                if (!startStory[0])
                {
                    startStory[0] = true;
                    dialogueManager.IsEndDialogue = false;
                    story[0].TriggerDialogue();
                }
                if (startStory[0] && dialogueManager.IsEndDialogue) Preparing();
            } 
            if (isFullHealth)
            {
                if (healthBar.value>0.8f)
                {
                    if (!startStory[1])
                    {
                        startStory[1] = true;
                        dialogueManager.IsEndDialogue = false;
                        story[1].TriggerDialogue();
                    }
                    if (startStory[1] && dialogueManager.IsEndDialogue) Phase1();
                }
                if (healthBar.value>0.5f&&healthBar.value<=0.8f)
                {
                    if (!appearAfterPhase1)
                    {
                        
                        appearAfterPhase1 = true;
                        timeAppearAfterPhase1 = Time.time; 
                    }
                    if (appearAfterPhase1) spriteRenderer.color = new Color(1, 1, 1, (Time.time - timeAppearAfterPhase1) / 2);
                    if (appearAfterPhase1&&Time.time>=timeAppearAfterPhase1+2)
                    {
                        if (!startStory[2])
                        {
                            startStory[2] = true;
                            dialogueManager.IsEndDialogue = false;
                            story[2].TriggerDialogue();
                        }
                        if (startStory[2] && dialogueManager.IsEndDialogue) Phase2();
                    }
                }
                if (healthBar.value>0.2f&&healthBar.value<=0.5f)
                {
                    if (!appearAfterPhase2)
                    {
                       
                        appearAfterPhase2 = true;
                        timeAppearAfterPhase2 = Time.time;
                    }
                    if (appearAfterPhase2) spriteRenderer.color = new Color(1, 1, 1, (Time.time - timeAppearAfterPhase2) / 2);
                    if (appearAfterPhase2 && Time.time >= timeAppearAfterPhase2 + 2)
                    {
                        if (!startStory[3])
                        {
                            startStory[3] = true;
                            dialogueManager.IsEndDialogue = false;
                            story[3].TriggerDialogue();
                        }
                        if (startStory[3] && dialogueManager.IsEndDialogue) Phase3();
                    }
                }
                if (healthBar.value>0&&healthBar.value<=0.2f)
                {
                    if (!startStory[4])
                    {
                        startStory[4] = true;
                        dialogueManager.IsEndDialogue = false;
                        story[4].TriggerDialogue();
                    }
                    if (startStory[4] && dialogueManager.IsEndDialogue) Phase4();
                }
                if (healthBar.value==0)
                {
                    if (!startStory[5])
                    {
                        startStory[5] = true;
                        dialogueManager.IsEndDialogue = false;
                        story[5].TriggerDialogue();
                    }
                    if (startStory[5] && dialogueManager.IsEndDialogue) Die();
                }
                if (isAttacked&&healthBar.value<0.5f&&healthBar.value!=0.2f&&healthBar.value!=0)
                {
                    spriteRenderer.color = new Color(1, (Time.time - timeAttacked) / 0.64f, (Time.time - timeAttacked) / 0.64f, 1);
                }
            }
        }
    }
    void Preparing()
    {
        if (healthBar.value <= 1)
        {
            healthBar.value += 0.005f;
            fill.color = healthColor.Evaluate(healthBar.value);
            fill.GetComponent<CanvasGroup>().alpha = healthBar.value;
            if (healthBar.value == 1)
            {
                isFullHealth = true;
                healthBar.value = 0.3f;
            }
        }
    }
    public void Attacked()
    {
        healthBar.value -= 0.05f;
        fill.color = healthColor.Evaluate(healthBar.value);
        timeAttacked = Time.time;
        isAttacked = true;
        if (healthBar.value<0.5f) spriteRenderer.color = new Color(1, 0.36f, 0.36f, 1);
    }
    void Phase1()
    {
        if (!onPhase1)
        {
            timeToPhase1 = Time.time;
            onPhase1 = true;
            minibossPhase1.SetActive(false);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1 - (Time.time - timeToPhase1)/2);
        if (Time.time>=timeToPhase1+2) minibossPhase1.SetActive(true);
    }
    void Phase2()
    {
        if (!onPhase2)
        {
            timeToPhase2 = Time.time;
            onPhase2 = true;
        }
            spriteRenderer.color = new Color(1, 1, 1, 1 - (Time.time - timeToPhase2) / 2);
            if (Time.time >= timeToPhase2 + 2) minibossPhase2.SetActive(true);
    }
    void AnimationToPhase3()
    {
        if (!onPhase3)
        {
            timeToPhase3 = Time.time;
            onPhase3 = true;
            minibossPhase2.SetActive(false);
        }
        if (!appearPhase3)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1 - (Time.time - timeToPhase3) / 2);
        }
        if (Time.time > timeToPhase3 + 2)
        {
            if (!appearPhase3)
            {
                appearPhase3 = true;
                timeToPhase3 = Time.time;
                anim.Play("Boss_2_Phase_2");
            }
        }
           if (appearPhase3)
           {
                spriteRenderer.color = new Color(1, 1, 1, (Time.time - timeToPhase3) / 2);
                if (spriteRenderer.color.a >= 1)
                {
                    circleOfFire.Play();
                    endAnimationPhase3 = true;
                }
           }
        
    }    
    void Phase3()
    {
        if (!endAnimationPhase3)
        {
            AnimationToPhase3();
            boss.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (!isExplode)
        {
            isExplode = true;
            timeExplosion = Time.time;
            explosion[Random.Range(0, explosion.Length)].Play();
        }
        if (Time.time >= timeExplosion + 4) isExplode = false;
    }
    void AnimationToPhase4()
    {
        if (!onPhase4)
        {
            timeToPhase4 = Time.time;
            onPhase4 = true;
            boss.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        if (!appearPhase4) spriteRenderer.color = new Color(1, 1, 1, 1 - (Time.time - timeToPhase4) / 2);
        if (Time.time > timeToPhase4 + 2)
        {
            if (!appearPhase4)
            {
                appearPhase4 = true;
                timeToPhase4 = Time.time;
                anim.Play("Boss_2_Phase_3");
            }
        }
        if (appearPhase4)
        {
            spriteRenderer.color = new Color(1, 1, 1, (Time.time - timeToPhase4) / 2);
            if (spriteRenderer.color.a >= 1)
            {
                endAnimationPhase4 = true;
            }
        }

    }
    void Phase4()
    {
        if (!endAnimationPhase4)
        {
            AnimationToPhase4();
            boss.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        if (Time.time>=timeBullet+5)
        {
            timeBullet = Time.time;
            Instantiate(bulletPhase4, new Vector3(-16, Random.Range(-8, 8), 0), Quaternion.identity);
        }
    }
    void Die()
    {
        if (!isDead)
        {
            isDead = true;
            timeDie = Time.time;
            boss.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        spriteRenderer.color = new Color(1, 1, 1, 1 - (Time.time - timeDie) / 2);
        circleOfFire.Stop();
        if (Time.time >= timeDie + 2) lunarDrop.SetActive(true);
    }
    private void OnMouseDown()
    {
        if (!boss.GetComponent<BoxCollider2D>().isTrigger) Attacked();
    }
}
