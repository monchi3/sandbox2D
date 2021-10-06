using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController2 : MonoBehaviour {
    //状態遷移変数
    public enum state {
        stand,
        run,
        inAir,
        shoot,
        guard,
        block,
        lean,
        down
    }
    public enum subState {
        none,
        notAction,
        damage
    }

    private state currentState;
    private subState currentSubState;


    private Rigidbody2D rb;

    private float xSpeed = 0, ySpeed = 0;

    //能力値

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
}