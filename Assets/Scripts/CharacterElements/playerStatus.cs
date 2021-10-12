using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStatus : MonoBehaviour
{
    #region//インスペクター
    [Header("最大HP")]            public int maxhp;
    [Header("現在HP")]            public int currenthp;
    [Header("最大ガード耐久値")]   public int maxGuard;
    [Header("現在ガード耐久値")]   public int currentGuard;

    #endregion
    #region //プライベート変数
    private bool canHit=true;
    private bool isGuard;
    private bool isBlocking;
    private bool isGuardBreak;
    private int playerID;
    private int hitRecovery;
    private int guardRecovery;
    private int guardFixTimer;
    private int maxEx=100;
    private int currentEx = 0;
    private Coroutine blockCo;
    private playerController pc;
    private int i = 0;
    #endregion
    #region //変数設定取得関数
    public int CurrentHPGet() {
        return currenthp;
    }
    public int MaxHPGet() {
        return maxhp;
    }
    public int CurrentGuardGet() {
        return currentGuard;
    }
    public int MaxGuardGet() {
        return maxGuard;
    }
    public int CurrentExGet() {
        return currentEx;
    }
    public bool isGuardBreakGet() {
        return isGuardBreak;
    }

    public bool canExShoot() {
        if (currentEx >= 50) {
            return true;
        }
        else return false;
    }
    public void useExShoot() {
        currentEx -= 50;
    }
    #endregion
    #region //変数操作関数
    public void CurrentExAdd(int i) {
        currentEx = currentEx + i;
    }
    public void CurrentExSub(int i) {
        currentEx -= i;
    }
    public void SetIsGuardBreak(bool i) {
        isGuardBreak = i;
    }
    #endregion

    /// <summary>
    /// ガード耐久値を管理する
    /// </summary>
    private void GuardFixer() {
        if (guardFixTimer == 0) {
            i = 0;
        }
        if (!isGuardBreak) {
            if (guardFixTimer < 1000) {
                guardFixTimer++;
            }

            if (guardFixTimer == 1000 && currentGuard < maxGuard) {
                i++;
                if (i % 10 == 0) {
                    currentGuard++;
                }
            }
        }
    }
    /// <summary>
    /// ガードする関数
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="hitEffectID"></param>
    /// 
    private void Defence(Collider2D collision,int hitEffectID) {
        if (collision.tag == "bullet" && collision.GetComponent<Bullet>().playerID != playerID && canHit) {

            if (!isGuard) {

                currenthp -= collision.gameObject.GetComponent<Bullet>().GetDamage();
                if (hitEffectID == 0) {
                    StartCoroutine("TakeDamage");
                }
                else if (hitEffectID == 1) {
                    StartCoroutine("TakeDown");
                }
                collision.GetComponent<Bullet>().DeleteThis();

            }
            else if (isGuard) {
                if (!isBlocking) {

                    guardFixTimer = 0;
                    currentGuard -= collision.gameObject.GetComponent<Bullet>().GetPenetration();

                    if (currentGuard < 0) {
                        isGuardBreak = true;
                        blockCo = StartCoroutine("GuardBreak");
                    }
                    else {
                        blockCo = StartCoroutine("Block");
                    }
                    collision.GetComponent<Bullet>().DeleteThis();

                }
                else if (isBlocking) {

                    guardFixTimer = 0;
                    currentGuard -= collision.gameObject.GetComponent<Bullet>().GetPenetration();

                    if (currentGuard < 0) {
                        isGuardBreak = true;
                        StopCoroutine(blockCo);
                        blockCo = StartCoroutine("GuardBreak");
                    }
                    else {
                        blockCo = StartCoroutine("Block");
                    }
                    collision.GetComponent<Bullet>().DeleteThis();

                }
            }

        }
    }
    public void SetCanHit(bool i) {
        canHit = i;
    }

    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxhp;
        currentGuard = maxGuard;
        pc = gameObject.GetComponent<playerController>();
        playerID = pc.playerID;
        currentEx = 0;

    }

    // Update is called once per frame
    void Update()
    {
        isGuard = pc.GetIsGuard();
        isBlocking = pc.GetIsBlocking();
        GuardFixer();

        if (currentEx > maxEx) {
            currentEx = maxEx;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "bullet") {
            hitRecovery = collision.GetComponent<Bullet>().hitRecovery;
            guardRecovery = collision.GetComponent<Bullet>().guradRecovery;

            int hitEffetctID = collision.GetComponent<Bullet>().hitEffectID;
            Defence(collision, hitEffetctID);
        }
    }

    IEnumerator TakeDamage() { //のけぞり
        int i;
        pc.SetIsAction(true);
        pc.SetIsDamaged(true);
        for (i = 0; i < hitRecovery; i++) {
            yield return null;
        }
        pc.SetIsAction(false);
        pc.SetIsDamaged(false);
    }
    IEnumerator GuardBreak() {
        int i;
        pc.SetIsAction(true);
        pc.SetIsDamaged(true);
        for (i = 0; i < 600; i++) {
            yield return null;
        }
        pc.SetIsAction(false);
        pc.SetIsDamaged(false);
    }
    IEnumerator TakeDown() {
        int i;
        pc.SetIsAction(true);
        pc.SetIsDown(true);
        canHit = false;
        for (i = 0; i < 480; i++) {
            yield return null;
        }       
        pc.SetIsAction(false);
        pc.SetIsDown(false);
        canHit = true;
    }
    IEnumerator Block() {
        int i;
        pc.SetIsAction(true);
        pc.SetIsBlocking(true);
        for (i = 0; i < guardRecovery; i++) {
            yield return null;
            guardFixTimer = 0;
        }
        pc.SetIsAction(false);
        pc.SetIsBlocking(false);

    }
}

