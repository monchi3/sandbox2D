using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private GameObject myself;
    private GameObject lowBullet;
    private GameObject NeutralBullet;
    private GameObject highBullet;
    private GameObject chargeCircle;
    private playerController pc;
    private playerStatus ps;
    private SpriteRenderer anten;
    private Coroutine co;
    

    public int playerID;
    private int lowBulletCount=0;
    private int highBulletCount = 0;

    private bool fire1, fire2, fire3,Ex;
    public int lowBulletCountLimit = 0;
    public int highBulletCountLimit = 0;

    private List<GameObject> bullet;
    private List<bool> fire;
    private List<float> CT;
    private List<int> bulletCount;
    private List<int> bulletCountLimit;


    public void LowBulletCountAdd() {
        lowBulletCount++;
    }
    public void LowBulletCountSub() {
        lowBulletCount--;
    }

    public void highBulletCountAdd() {
        highBulletCount++;       
    }
    public void highBulletCountSub() {
        highBulletCount--;
    }
    
    // Start is called before the first frame update
    public void Shoot() {

        if (fire1 && lowBulletCount<lowBulletCountLimit && !pc.GetIsAction()) {
            lowBulletCount++;
            co=StartCoroutine("LowShoot");
         }
        if (fire2 && highBulletCount < highBulletCountLimit && !pc.GetIsAction()) {
            highBulletCount++;
            co=StartCoroutine("HighShoot");
        }
        if(Ex&& !pc.GetIsAction() && !anten.enabled && ps.canExShoot()) {
            co=StartCoroutine("ExShoot");
            ps.useExShoot();
        }
    }

   
    
    void Start()
    {
        lowBullet = Resources.Load("bulletLow") as GameObject;
        highBullet = Resources.Load("highBullet") as GameObject;
        myself = transform.parent.gameObject;
        pc=myself.GetComponent<playerController>();
        ps = myself.GetComponent<playerStatus>();
        playerID = pc.playerID;
        anten = GameObject.Find("Anten").GetComponent<SpriteRenderer>();
        chargeCircle = Resources.Load("chargeParticle") as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerID == 1) {
            fire1 = Input.GetButtonDown("Fire1");
            fire2 = Input.GetButtonDown("Fire2");
            fire3 = Input.GetButtonDown("Fire3");
            Ex = Input.GetButtonDown("EX");
        }
        else if (playerID == 2) {
            fire1 = Input.GetButtonDown("Fire12");
            fire2 = Input.GetButtonDown("Fire22");
            fire3 = Input.GetButtonDown("Fire32");
            Ex = Input.GetButtonDown("EX2");
        }
    }

    IEnumerator LowShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 3; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        Instantiate(lowBullet, this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        for (i = 0; i < 15; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);
    }
    IEnumerator HighShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 10; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        Instantiate(highBullet, this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        for (i = 0; i < 50; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);

    }
    IEnumerator ExShoot() {
        int i = 0;
        pc.SetIsAction(true);
        #region //à√ì]
        anten.enabled = true;
        myself.GetComponent<SpriteRenderer>().sortingLayerID = 1;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(5 / 60f);
        Instantiate(chargeCircle, myself.transform);
        yield return new WaitForSecondsRealtime(40 / 60f);
        Time.timeScale = 1;
        myself.GetComponent<SpriteRenderer>().sortingLayerID = 0;

        anten.enabled = false;

        #endregion
        #region //î≠ê∂
        for (i = 0; i < 13; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        #endregion
        #region //éùë±
        for (i = 0; i < 90; i++) {
            if (i % 30 == 0) {
                Instantiate(highBullet, this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
            }
            else {
                yield return new WaitForFixedUpdate();
                if (pc.GetIsDamaged()) {
                    yield break;
                }
            }

        }
        #endregion
        #region//çdíº
        for (i = 0; i < 30; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        #endregion
        pc.SetIsAction(false);
    }

}
