using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject bullet;
    float timeAiming;
    GameObject aiming;
    public GameObject Aiming
    {
        get { return aiming; }
        set { aiming = value; }
    }
    [SerializeField] float movingSpeed;
    [SerializeField] GameObject black;
    [SerializeField] ParticleSystem aimingOnBlack;
    [SerializeField] GameObject white;
    [SerializeField] ParticleSystem aimingOnWhite;
    // Start is called before the first frame update
    void Start()
    {
        white = GameObject.Find("White");
        black = GameObject.Find("Black");
        bullet = gameObject;
        int aim = Random.Range(0, 2);
        if (aim == 0)
        {
            aimingOnBlack.Play();
            aiming = black;
        }
        if (aim == 1)
        {
            aimingOnWhite.Play();
            aiming = white;
        }
        if (aiming == null) Destroy(bullet);
        if (aiming!=null)
        {
            timeAiming = Time.time;
            float angle = Vector3.Angle(aiming.transform.position - bullet.transform.position, Vector3.right);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed * Mathf.Cos(angle), movingSpeed * Mathf.Sin(angle));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiming == null) Destroy(bullet);
        if (black!=null) aimingOnBlack.transform.position = black.transform.position;
        if (white!= null) aimingOnWhite.transform.position = white.transform.position;
        if (Time.time >= timeAiming + 0.5f&&aiming!=null)
        {
            timeAiming = Time.time;
            float angle = Vector3.Angle(aiming.transform.position - bullet.transform.position, Vector3.right);
            if (aiming.transform.position.y < bullet.transform.position.y) angle = 0 - angle;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(movingSpeed * Mathf.Cos(Mathf.Deg2Rad*angle), movingSpeed * Mathf.Sin(Mathf.Deg2Rad * angle));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(bullet);
        }
    }
}
