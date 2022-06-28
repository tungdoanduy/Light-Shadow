using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isOpening;
    bool hasSetTime;
    float timeOpening;
    [Header("Transition")]
    [SerializeField] ParticleSystem transition;
    [SerializeField] string sceneName;
    float transitionTime;
    [Header("Sprites")]
    //[SerializeField] Sprite gate_1;
    [SerializeField] Sprite gate_2;
    [SerializeField] Sprite gate_3;
    [SerializeField] Sprite gate_4;
    [Header("Player Checker")]
    [SerializeField] Transform checkPlayer;
    [SerializeField] float checkPlayerRadius;
    [SerializeField] LayerMask player;
    bool playerIn;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isOpening = false;
        hasSetTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerIn();
        if (playerIn)
        {
            isOpening = true;
            
        }
        if (isOpening)
        {
            OpenGate();
        }
        if (spriteRenderer.sprite == gate_4)
        {
            transition.Play();
            if (Time.time >= transitionTime + 2) SceneManager.LoadScene(sceneName);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPlayer.position, checkPlayerRadius);
    }
    void CheckPlayerIn()
    {
        Collider2D[] cols1 = Physics2D.OverlapCircleAll(checkPlayer.position, checkPlayerRadius, player);
        playerIn = cols1.Length == 2;
    }
    void OpenGate()
    {
        if (hasSetTime)
        {
            timeOpening = Time.time;
            transitionTime = Time.time + 0.6f;
            hasSetTime = false;
        }
        if (Time.time >= timeOpening + 0.2f && Time.time < timeOpening + 0.4f) spriteRenderer.sprite = gate_2;
        if (Time.time >= timeOpening + 0.4f && Time.time < timeOpening + 0.6f) spriteRenderer.sprite = gate_3;
        if (Time.time >= timeOpening + 0.6f) spriteRenderer.sprite = gate_4;
    }
    //void LoadScene(string sceneName)
    //{
    //    SceneManager.LoadScene(sceneName);
    //}
}
