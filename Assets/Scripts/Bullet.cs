using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    private int myCount;
    private Rigidbody2D rb;
    private Shooter parentShooter;
    private int aliveCount;

    public playerStatus ps;

    public int bulletID; //�e��ID
    public int playerID; //�e�̎������ID
    public int hitEffectID; //�����Ԃ�ID
    public int hitRecovery; //�̂�����d��
    public int guradRecovery; //�K�[�h���d��

    [Header("�e��")]        public float bulletSpeed=5.0f;
    [Header("�e�̎�������")] public int bulletTime = 100;
    [Header("�_���[�W")]     public int damage = 0;
    [Header("�ђ�")]         public int guardPenetration =0;

    public void DeleteThis() {
        switch (bulletID) {
            case 1:
                parentShooter.LowBulletCountSub();
                Destroy(this.gameObject);
                break;
            case 2:
                parentShooter.highBulletCountSub();
                Destroy(this.gameObject);
                break;

        }
    }

    public int GetDamage() {
        return damage;
    }
    public int GetPenetration() {
        return guardPenetration;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        parent = transform.parent.gameObject;
        parentShooter = parent.GetComponent<Shooter>();
        playerID = parentShooter.playerID;
        ps = transform.parent.parent.GetComponent<playerStatus>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        aliveCount++;

        if (playerID == 1) {
            rb.velocity = new Vector2(bulletSpeed, 0);
        }
        else if(playerID == 2) {
            rb.velocity = new Vector2(-bulletSpeed, 0);
        }

        if (aliveCount > bulletTime) {
            DeleteThis();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }

}
