using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    private int myCount;
    private Rigidbody2D rb;
    private Shooter2 parentShooter;
    private int aliveCount;
    private float horizontalSpeed ;
    private float verticalSpeed ;

    public playerStatus ps;

    public int skillNum; //‚Ç‚ÌƒXƒLƒ‹‚Å¶¬‚³‚ê‚½‚©
    public int playerID; //’e‚Ì‚¿å‚ÌID
    public int hitEffectID; //‚â‚ç‚êó‘Ô‚ÌID
    public int hitRecovery; //‚Ì‚¯‚¼‚èd’¼
    public int guradRecovery; //ƒK[ƒhd’¼

    [Header("’e‘¬")]        public float bulletSpeed=5.0f;
    [Header("’e‚Ì‘±ŠÔ")] public int bulletTime = 100;
    [Header("ƒ_ƒ[ƒW")]     public int damage = 0;
    [Header("ŠÑ’Ê")]         public int guardPenetration =0;
    [Header("”­ËŠp“x")]    public float angle;
    
    public enum orbitType {
        straight
    }
    public orbitType type; 

    public void DeleteThis() {
        parentShooter.bulletCountSub(skillNum, 1);
        Destroy(this.gameObject);
    }

    public int GetDamage() {
        return damage;
    }
    public int GetPenetration() {
        return guardPenetration;
    }

    public void SetAngle(int i) {
        angle = i;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        parent = transform.parent.gameObject;
        parentShooter = parent.GetComponent<Shooter2>();
        playerID = parentShooter.playerID;
        ps = transform.parent.parent.GetComponent<playerStatus>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        aliveCount++;
        if (type == orbitType.straight) {
            horizontalSpeed = bulletSpeed * Mathf.Cos(angle);
            verticalSpeed = bulletSpeed * Mathf.Sin(angle);

            if (playerID == 1) {
                rb.velocity = new Vector2(horizontalSpeed, verticalSpeed);
            }
            else if (playerID == 2) {
                rb.velocity = new Vector2(-horizontalSpeed,verticalSpeed);
            }
        }

        if (aliveCount > bulletTime) {
            DeleteThis();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }

}
