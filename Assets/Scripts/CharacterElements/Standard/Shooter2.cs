using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter2 : MonoBehaviour
{
    
    public int playerID=0;
    public List<ShootSkill> shootSkill;

    [SerializeField]private GameObject myself;
    [SerializeField] private playerController pc;
    [SerializeField] private playerStatus ps;

    [SerializeField] private List<string> skillName;
    [SerializeField] private List<GameObject> bullet;
    [SerializeField] private List<bool> fire;
    [SerializeField] private List<float> CT;
    [SerializeField] private List<int> bulletCountLimit;

    [SerializeField] private List<int> bulletCount;
    [SerializeField] private List<float> currentCT;


    public void bulletCountAdd(int skillNum,int bulletNum) {
        bulletCount[skillNum]+=bulletNum;
    }
    public void bulletCountSub(int skillNum,int bulletNum) {
        bulletCount[skillNum]-=bulletNum;
    }
    public void Shoot() {
        if (fire[0]) {
            StartCoroutine("lowShoot");
        }

        if (fire[1]) {
            StartCoroutine("middleShoot");
        }

        if (fire[2]) {
            StartCoroutine("highShoot");
        }

        if (fire[3]) {
            StartCoroutine("NeutralShoot");
        }

        if (fire[4]) {

        }

        if (fire[5]) {

        }
    }
    public void GetFireButton() {
        if (playerID == 1) {
            fire[0] = Input.GetButtonDown("Fire1") && !pc.GetIsAction() && bulletCount[0] < bulletCountLimit[0] && currentCT[0] == 0;
            fire[1] = Input.GetButtonDown("Fire2") && !pc.GetIsAction() && bulletCount[1] < bulletCountLimit[1] && currentCT[1] == 0;
            fire[2] = Input.GetButtonDown("Fire3") && !pc.GetIsAction() && bulletCount[2] < bulletCountLimit[2] && currentCT[2] == 0;
            fire[3] = Input.GetButtonDown("Fire4") && !pc.GetIsAction() && bulletCount[3] < bulletCountLimit[3] && currentCT[3] == 0;
            fire[4] = Input.GetButtonDown("CharacterAction") && !pc.GetIsAction() && bulletCount[4] < bulletCountLimit[4] && currentCT[4] == 0;
            fire[5] = Input.GetButtonDown("EX") && !pc.GetIsAction() && bulletCount[5] < bulletCountLimit[5] && currentCT[5] == 0;
        }
        else if (playerID == 2) {
            fire[0] = Input.GetButtonDown("Fire12") && !pc.GetIsAction() && bulletCount[0] < bulletCountLimit[0] && currentCT[0] == 0;
            fire[1] = Input.GetButtonDown("Fire22") && !pc.GetIsAction() && bulletCount[1] < bulletCountLimit[1] && currentCT[1] == 0;
            fire[2] = Input.GetButtonDown("Fire32") && !pc.GetIsAction() && bulletCount[2] < bulletCountLimit[2] && currentCT[2] == 0;
            fire[3] = Input.GetButtonDown("Fire42") && !pc.GetIsAction() && bulletCount[3] < bulletCountLimit[3] && currentCT[3] == 0;
            fire[4] = Input.GetButtonDown("CharacterAction2") && !pc.GetIsAction() && bulletCount[4] < bulletCountLimit[4] && currentCT[4] == 0;
            fire[5] = Input.GetButtonDown("EX2") && !pc.GetIsAction() && bulletCount[5] < bulletCountLimit[5] && currentCT[5] == 0;
        }
    }
    public void CoolDowner() {
        Debug.Log(currentCT[0]);

        int i;
        for (i = 0; i < 6; i++) {
            if(currentCT[i] > 0) {
                currentCT[i]--;
            }
        }

    }
    int i;
    // Start is called before the first frame update
    void Start()
    {
        myself = transform.parent.gameObject;
        pc = myself.GetComponent<playerController>();
        ps = myself.GetComponent<playerStatus>();

        for (i = 0; i < 6; i++) {
            skillName[i] = shootSkill[i].GetSkillName();
            bullet[i] = shootSkill[i].GetBullet();
            CT[i] = shootSkill[i].GetCT();
            bulletCountLimit[i] = shootSkill[i].GetBulletCountLimit();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        GetFireButton();
        Shoot();

    }
    private void FixedUpdate() {
        CoolDowner();
    }
    IEnumerator lowShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 3; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        Instantiate(bullet[0], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        bulletCountAdd(0, 1);
        currentCT[0] = CT[0];
        for (i = 0; i < 10; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);
    }
    IEnumerator middleShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 3; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        GameObject b3 = Instantiate(bullet[1], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        b3.GetComponent<Bullet>().SetSkillNum(1);
        GameObject b1 = Instantiate(bullet[1], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        b1.GetComponent<Bullet>().SetAngle(10);
        b1.GetComponent<Bullet>().SetSkillNum(1);
        GameObject b2 = Instantiate(bullet[1], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        b2.GetComponent<Bullet>().SetAngle(-10);
        b2.GetComponent<Bullet>().SetSkillNum(1);
        bulletCountAdd(1, 3);
        currentCT[1] = CT[1];
        for (i = 0; i < 15; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);
    }
    IEnumerator highShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 9; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        Instantiate(bullet[2], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        bulletCountAdd(2, 1);
        currentCT[2] = CT[2];
        for (i = 0; i < 20; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);
    }
    IEnumerator NeutralShoot() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 8; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        Instantiate(bullet[3], this.gameObject.transform.position, this.gameObject.transform.rotation, gameObject.transform);
        bulletCountAdd(3, 1);
        currentCT[3] = CT[3];
        for (i = 0; i < 22; i++) {
            yield return new WaitForFixedUpdate();
            if (pc.GetIsDamaged()) {
                yield break;
            }
        }
        pc.SetIsAction(false);
    }
    IEnumerator CharacterAction() {
        int i = 0;
        pc.SetIsAction(true);
        for (i = 0; i < 3; i++) {
            yield return new WaitForFixedUpdate();
        }

        pc.SetIsAction(false);

    }
}

