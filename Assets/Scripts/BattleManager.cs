using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private int GameProcess=0;
    private GameObject exBall;
    private Coroutine SEB;
    private CharacterManager cm;
    private Text text;
    [SerializeField]private playerController pc1;
    [SerializeField]private playerController pc2;
    [SerializeField] private playerStatus ps1;
    [SerializeField] private playerStatus ps2;

    public GameObject[] spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        exBall = Resources.Load("ExBall") as GameObject;
        StartCoroutine("FirstSpawnExBall");
        cm = GameObject.Find("Center").GetComponent<CharacterManager>();
        text = GameObject.Find("START").GetComponent<Text>();
        StartCoroutine("BattleStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameProcess == 0 ) {
            pc1.SetIsAction(true);
            pc2.SetIsAction(true);
        }
        else if (GameProcess == 1) {
            if (ps1.CurrentHPGet() <= 0 && ps2.CurrentHPGet() > 0) {
                GameFinish(2);
            }
            else if (ps2.CurrentHPGet() <= 0 && ps1.CurrentHPGet() > 0) {
                GameFinish(1);
            }
            else if (ps1.CurrentHPGet() <= 0 && ps2.CurrentHPGet() <= 0) {
                GameFinish(3);
            }
        }
        else if(GameProcess == 2) {
            pc1.SetIsAction(true);
            pc2.SetIsAction(true);
        }
    }

    private void GameFinish( int i ) {
        GameProcess = 2;

        StartCoroutine("BattleFinish", i);
    }
    IEnumerator BattleFinish(int i) {
        yield return null;
        if(i == 1) {
            text.text = "1P WIN!";
        }else if (i == 2) {
            text.text = "2P WIN" ;
        }else if(i == 3) {
            text.text = "DOUBLE K.O";
        }
        
    }

    IEnumerator BattleStart() {
        if (GameProcess == 0) {
            text.text = "3";
            yield return new WaitForSeconds(1);
            text.text = "2";
            yield return new WaitForSeconds(1);
            text.text = "1";
            yield return new WaitForSeconds(1);
            text.text = "FIGHT!!";
            yield return new WaitForSeconds(1);
            text.text = "";
            GameProcess = 1;
            pc1.SetIsAction(false);
            pc2.SetIsAction(false);
        }
    }

    IEnumerator SpawnExBall() {
        int i = 0;
        Random.InitState(System.DateTime.Now.Millisecond);
        yield return new WaitForSeconds(5f);
        i = Random.Range(0, 2);
        Instantiate(exBall, spawnPoint[i].transform).GetComponent<ExBall>();
        SEB=StartCoroutine("SpawnExBall");
    }
    IEnumerator FirstSpawnExBall() {
        int i = 0;
        Random.InitState(System.DateTime.Now.Millisecond);
        yield return new WaitForSeconds(5f);
        i = Random.Range(0, 2);
        if (cm.GetState()==0) {
            Instantiate(exBall, spawnPoint[i].transform).GetComponent<ExBall>();
            SEB = StartCoroutine("SpawnExBall");
        }
    }
}
