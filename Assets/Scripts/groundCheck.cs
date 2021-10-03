using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{

    [Header("エフェクトがついた床を判定するか")] public bool checkPlatformGroud = true;
    private bool isGround=false;
    private bool onPlatform = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    private string groundTag = "Ground";
    private string platformTag = "GroundPlatform";
    private Collider2D col;

    private void Update() {
        
    }

    public bool IsGround() {
        if (isGroundEnter || isGroundStay) {
            isGround = true;
        }else if (isGroundExit) {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;
    }

    public bool IsOnPlatform() {
        return onPlatform;
    }
    public Collider2D GetFloor() {
        return col;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == groundTag) {
            isGroundEnter = true;
        }
        else if (checkPlatformGroud && collision.tag == platformTag) {
            isGroundEnter = true;
            onPlatform = true;
            col = collision;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == groundTag) {
            isGroundStay = true;
        }
        else if (checkPlatformGroud && collision.tag == platformTag) {
            isGroundStay = true;
            onPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == groundTag) {
            isGroundExit = true;
        }
        else if (checkPlatformGroud && collision.tag == platformTag) {
            isGroundExit = true;
            onPlatform = false;
        }else if(!checkPlatformGroud && collision.tag == platformTag) {
            collision.isTrigger = false;
        }
    }
}
