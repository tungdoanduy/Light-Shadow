using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameObject player;
    [Header("Player Status")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    bool isOnGround = false;
    [Header("Left Wall Check")]
    [SerializeField] Transform leftWallCheck;
    [SerializeField] float leftWallCheckRadius;
    bool isLeftWall = false;
    [Header("Right Wall Check")]
    [SerializeField] Transform rightWallCheck;
    [SerializeField] float rightWallCheckRadius;
    bool isRightWall = false;
    [Header("VFX")]
    [SerializeField] ParticleSystem vfx;
    [SerializeField] ParticleSystem startTransition;
    [SerializeField] ParticleSystem endTransition;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        jumpSpeed = 12f;
        moveSpeed = 3f;
        vfx.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Checker();
        Move();
        vfx.transform.position = player.transform.position;
        //if (isLeftWall && player.GetComponent<Rigidbody2D>().velocity.x < 0) player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
        //if (isRightWall && player.GetComponent<Rigidbody2D>().velocity.x > 0) player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.GetComponent<Rigidbody2D>().velocity.y);
    }
    void Move()
    {
        if (!startTransition.isPlaying || !endTransition.isPlaying)
        {
            if (player.name == "White")
            {
                if (Input.GetKey(KeyCode.A))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * moveSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                }
                if (Input.GetKey(KeyCode.D))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * moveSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                }
                if (Input.GetKey(KeyCode.W) && (isOnGround||isRightWall||isLeftWall))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
                }
            }
            if (player.name == "Black")
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(-2 * moveSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(2 * moveSpeed, player.GetComponent<Rigidbody2D>().velocity.y);
                }
                if (Input.GetKey(KeyCode.UpArrow) && (isOnGround || isRightWall || isLeftWall))
                {

                    player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, jumpSpeed);
                }
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireSphere(leftWallCheck.position, leftWallCheckRadius);
        Gizmos.DrawWireSphere(rightWallCheck.position, rightWallCheckRadius);
    }
    void Checker()
    {
        Collider2D[] cols1 = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        isOnGround = cols1.Length > 0;
        Collider2D[] cols2 = Physics2D.OverlapCircleAll(leftWallCheck.position, leftWallCheckRadius, groundLayer);
        isLeftWall = cols2.Length > 0;
        Collider2D[] cols3 = Physics2D.OverlapCircleAll(rightWallCheck.position, rightWallCheckRadius, groundLayer);
        isRightWall = cols3.Length > 0;
    }
    private void OnDestroy()
    {
        vfx.Play();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Platform"))
        {
            player.transform.parent = collision.transform;
           if (player.GetComponent<Rigidbody2D>().velocity.y<0) player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x,0);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        player.transform.parent = null;
    }
}
