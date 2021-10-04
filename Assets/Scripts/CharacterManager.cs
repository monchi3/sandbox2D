using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject check1;
    [SerializeField] private GameObject check2;
    [SerializeField] private Image Text;
    [SerializeField] private List<Sprite> okisemeText;
    private bool isContinue = false;
    private bool isEnter1 = false;
    private bool isEnter2 = false;
    private bool okisemeLog = false;
    private int state = 0;
    private int action1 = -1, action2 = -1;
   
    private GameObject anten;
    private playerController pc1,pc2;
    private playerStatus atkps, defps;
    private playerStatus ps1, ps2;
    private Vector3 okisemePosition1 = new Vector3(-2, 0, 0);
    private Vector3 okisemePosition2 = new Vector3(2, 0, 0);
    private Vector3 neutralPosition1 = new Vector3(-8, 0, 0);
    private Vector3 neutralPosition2 = new Vector3(8, 0, 0);
    private Skill atkSkill;
    private Skill defSkill;

    #region//ââèoóp
    [SerializeField] private Flash flashPanel=null; 

    #endregion

    private int isOkiseme=0; //0=null 1=1p 2=2p

    private int OkisemeCheck(bool down1,bool down2,bool invation1,bool invation2) {
        if(!down1 && down2 && invation1 && !invation2) {
            return 1; //1P is Okiseme.
        }
        else if(down1 && !down2 && !invation1 && invation2) {
            return 2; //2P is Okiseme
        }
        else{
            return 0;
        }
    }
    private void Damage(int damage, playerStatus ps) {
        ps.currenthp -= damage;
    }

    public int GetState() {
        return state;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        anten = GameObject.Find("Anten") as GameObject;
        pc1 = player1.GetComponent<playerController>();
        pc2 = player2.GetComponent<playerController>();
        ps1 = player1.GetComponent<playerStatus>();
        

    }

    // Update is called once per frame
    void Update() {
        bool enterbutton = Input.GetKeyDown(KeyCode.Return);

        if(state == 0) { //ãNÇ´çUÇﬂÇ≈Ç»Ç¢Ç∆Ç´
            if (isOkiseme == 0) {
                isOkiseme = OkisemeCheck(player1.GetComponent<playerController>().GetIsDown(), player2.GetComponent<playerController>().GetIsDown(),
                                        player1.GetComponent<playerController>().GetIsInvasion(), player2.GetComponent<playerController>().GetIsInvasion());
            }
            else state = 1; //éüèÛë‘Ç÷ëJà⁄

            if (action1 != -1) action1 = -1;
            if (action2 != -1) action2 = -1;
        }
        else if (state == 1) { //ãNÇ´çUÇﬂì¸óÕë“Çø


            pc1.SetIsAction(true);
            pc2.SetIsAction(true);
            anten.GetComponent<SpriteRenderer>().enabled = true;
            player1.transform.DOMove(okisemePosition1, 1f);
            player2.transform.DOMove(okisemePosition2, 1f);
            pc1.gravity = 0;
            pc2.gravity = 0;

            if (!isEnter1) {
                check1.GetComponent<SpriteRenderer>().enabled = false;
                if (Input.GetButtonDown("Fire1")) action1 = 0;
                else if (Input.GetButtonDown("Fire2")) action1 = 1;
                else if (Input.GetButtonDown("Fire3")) action1 = 2;
                else if (Input.GetButtonDown("Guard")) action1 = 3;
            }
            if (!isEnter2) {
                check2.GetComponent<SpriteRenderer>().enabled = false;
                if (Input.GetButtonDown("Fire12")) action2 = 0;
                else if (Input.GetButtonDown("Fire22")) action2 = 1;
                else if (Input.GetButtonDown("Fire32")) action2 = 2;
                else if (Input.GetButtonDown("Guard2")) action2 = 3;
            }
            if (!isEnter1 && Input.GetButtonDown("Enter") && action1 != -1) {
                if (action1 == 3 && ps1.CurrentExGet() < 50) {
                    isEnter1 = false;
                }
                else {

                    isEnter1 = true;
                    check1.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (!isEnter2 && enterbutton && action2 != -1) {
                if (action2 == 3 && ps2.CurrentExGet() < 50) {

                }
                else {
                    isEnter2 = true;
                    check2.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (isEnter1 && isEnter2 && action1 != -1 && action2 != -1) {
                if (isOkiseme == 1) {
                    atkSkill = pc1.atkSkillList[action1];
                    defSkill = pc2.defSkillList[action2];
                    atkps = player1.GetComponent<playerStatus>();
                    defps = player2.GetComponent<playerStatus>();
                }
                else if (isOkiseme == 2) {
                    defSkill = pc1.defSkillList[action1];
                    atkSkill = pc2.atkSkillList[action2];
                    atkps = player2.GetComponent<playerStatus>();
                    defps = player1.GetComponent<playerStatus>();
                }
                check1.GetComponent<SpriteRenderer>().enabled = false;
                check2.GetComponent<SpriteRenderer>().enabled = false;
                isEnter1 = false;
                isEnter2 = false;
                flashPanel.DoFlash();
                state = 2;
            }


        } 
        else if(state == 2) { //ãZÇÃèàóù
            if (atkSkill.GetSkillType() == Skill.Type.strike && !okisemeLog) {

                if (defSkill.GetSkillType() == Skill.Type.parry) {

                    isContinue = false;
                    defps.CurrentExAdd(10);
                    Text.sprite = okisemeText[3];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.dodge) {

                    Damage(atkSkill.GetSkillDamage(), defps);
                    isContinue = true;

                    Text.sprite = okisemeText[0];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.Defguard) {

                    
                    isContinue = true;
                    Text.sprite = okisemeText[2];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.Invinsibility) {

                    Damage(defSkill.GetSkillDamage(), atkps);
                    ps1.CurrentExSub(50);
                    isContinue = false;
                    Text.sprite = okisemeText[5];
                    Text.enabled = true;

                }
            }
            else if (atkSkill.GetSkillType() == Skill.Type.grapple && !okisemeLog) {

                if (defSkill.GetSkillType() == Skill.Type.parry) {

                    Damage(atkSkill.GetSkillDamage(), defps);
                    isContinue = false;
                    Text.sprite = okisemeText[1];
                    Text.enabled = true;
                }
                else if (defSkill.GetSkillType() == Skill.Type.dodge) {
                    
                    isContinue = false;
                    Text.sprite = okisemeText[4];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.Defguard) {
                
                    Damage(atkSkill.GetSkillDamage(), defps);
                    isContinue = false;
                    Text.sprite = okisemeText[2];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.Invinsibility) {
                
                    Damage(defSkill.GetSkillDamage(), atkps);
                    ps1.CurrentExSub(50);
                    Text.sprite = okisemeText[5];
                    Text.enabled = true;
                }
            }
            else if (atkSkill.GetSkillType() == Skill.Type.Atkguard && !okisemeLog) {

                if (defSkill.GetSkillType() == Skill.Type.parry) {
                
                    isContinue = false;
                    Text.sprite = okisemeText[3];
                    Text.enabled = true;

                }
                else if (defSkill.GetSkillType() == Skill.Type.dodge) {
                
                    isContinue = false;
                    Text.sprite = okisemeText[4];
                    Text.enabled = true;
                }
                else if (defSkill.GetSkillType() == Skill.Type.Defguard) {
                
                    isContinue = false;
                    Text.sprite = okisemeText[2];
                    Text.enabled = true;


                }
                else if (defSkill.GetSkillType() == Skill.Type.Invinsibility) {
                
                    Damage(atkSkill.GetSkillDamage(), defps);
                    ps1.CurrentExSub(50);
                    isContinue = true;
                    Text.sprite = okisemeText[2];
                    Text.enabled = true;

                }
            }

            okisemeLog = true;
            StartCoroutine("Okiseme");

            

        }
        else if(state == 3) {
            player1.transform.DOMove(neutralPosition1, 1f);
            player2.transform.DOMove(neutralPosition2, 1f);
            isOkiseme = 0;
            StartCoroutine("OkisemeFinish");
        }

        
    }
    IEnumerator OkisemeFinish() {
        yield return new WaitForSeconds(1f);
        anten.GetComponent<SpriteRenderer>().enabled = false;
        pc1.gravity = 11;
        pc2.gravity = 11;
        pc1.SetIsAction(false);
        pc2.SetIsAction(false);
        atkps.currentGuard = 100;
        defps.currentGuard = 100;
        atkps.SetIsGuardBreak(false);
        defps.SetIsGuardBreak(false);
        
        state = 0;
    }

    IEnumerator Okiseme() {
        yield return new WaitForSeconds(1f);
        Text.enabled = false;
        okisemeLog = false;
        action1 = -1;
        action2 = -1;
        if (isContinue) {
            state = 1;
        }
        else {
            state = 3;
        }
    }
}
