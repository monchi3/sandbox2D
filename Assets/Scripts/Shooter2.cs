using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter2 : MonoBehaviour
{
    private enum skillType {
        LowShoot,
        MiddleShoot,
        Shoot,
        HighShoot,
        CharacterAction,
        ExShoot
    }
    public int playerID=0;
    private skillType type;
    private GameObject bullet;
    private bool fire=false;
    private float CT=0;
    private int bulletCount = 0;
    private int bulletCountLimit = 0;

    public void bulletCountAdd() {
        bulletCount++;
    }
    public void bulletCountSub() {
        bulletCount--;
    }

    public void Shoot() {
        if(playerID == 1) {
            if(type == skillType.LowShoot) {
                if (Input.GetButtonDown("Fire1")) {

                }
            }
            else if(type == skillType.MiddleShoot) {
                if (Input.GetButtonDown("Fire2")) {

                }
            }
            else if(type == skillType.Shoot) {
                if (Input.GetButtonDown("Fire3")) {

                }
            }
            else if(type == skillType.HighShoot) {
                if (Input.GetButtonDown("Fire4")) {
                    
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
