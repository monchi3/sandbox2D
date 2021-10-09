using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBall : MonoBehaviour
{
    [Header("ëùâ¡ExÉQÅ[ÉW")]public int increaseEx;
    [Header("ëÃóÕ")] public int hp;

    private Rigidbody2D rb;
    private int DeleteTimer=0;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        

        if (gameObject.transform.position.y > 0) {
            rb.velocity = new Vector2(0, -4);
        }
        else {
            rb.velocity = new Vector2(0, 4);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (DeleteTimer < 3000) {
            DeleteTimer++;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (collision.tag == "bullet" && bullet.skillNum == 0) {
            if (bullet.playerID == 1) {
                bullet.ps.CurrentExAdd(increaseEx);
                Destroy(this.gameObject);
            }
            else if (bullet.playerID == 2) {
                bullet.ps.CurrentExAdd(increaseEx);
                Destroy(this.gameObject);
            }
        }
    }
}
