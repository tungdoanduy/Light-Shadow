using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallButton : MonoBehaviour
{
    GameObject button;
    
    
    [Header("Player Checker")]
    [SerializeField] Transform checkPlayer;
    [SerializeField] float checkPlayerRadius;
    [SerializeField] LayerMask player;
    bool playerIn;
    public bool PlayerIn
    {
        get { return playerIn; }
    }
    // Start is called before the first frame update
    void Start()
    {
        button = gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerIn();
        if (playerIn) button.transform.position = new Vector3(checkPlayer.position.x, checkPlayer.position.y - 0.4f, 0);
        if (!playerIn) button.transform.position = new Vector3(checkPlayer.position.x, checkPlayer.position.y, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkPlayer.position, checkPlayerRadius);
    }
    void CheckPlayerIn()
    {
        Collider2D[] cols1 = Physics2D.OverlapCircleAll(checkPlayer.position, checkPlayerRadius, player);
        playerIn = cols1.Length >0;
    }
}
