using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter2 : MonoBehaviour
{

    public int playerID=0;
    private List<GameObject> bullet;
    private List<bool> fire;
    private List<float> CT;
    private List<int> bulletCount ;
    private List<int> bulletCountLimit;

    public void bulletCountAdd(int skillNum,int bulletNum) {
        bulletCount[skillNum]+=bulletNum;
    }
    public void bulletCountSub(int skillNum,int bulletNum) {
        bulletCount[skillNum]-=bulletNum;
    }

    public void Shoot() {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerID == 1) {
            fire[0] = Input.GetButtonDown("Fire1");
            fire[1] = Input.GetButtonDown("Fire2");
            fire[2] = Input.GetButtonDown("Fire3");
            fire[3] = Input.GetButtonDown("Fire4");
            fire[4] = Input.GetButtonDown("CharacterAction");
            fire[5] = Input.GetButtonDown("EX");
        }
        else if(playerID == 2) {
            fire[0] = Input.GetButtonDown("Fire12");
            fire[1] = Input.GetButtonDown("Fire22");
            fire[2] = Input.GetButtonDown("Fire32");
            fire[3] = Input.GetButtonDown("Fire42");
            fire[4] = Input.GetButtonDown("CharacterAction2");
            fire[5] = Input.GetButtonDown("EX2");
        }
    }
}
