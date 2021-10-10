using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region//インスペクター
    [Header("プレイヤーID")] public int playerID;
    [Header("移動速度")] public float walkSpeed;
    [Header("重力")] public float gravity;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプ高度")] public float jumpHeight;
    [Header("ジャンプ時間")] public float jumpLimitTime;
    [Header("小ジャンプ時間")] public float shortJumpLimitTime;
    [Header("接地判定")] public groundCheck ground;
    [Header("頭をぶつけた判定")] public groundCheck head;
    [Header("ジャンプの表現曲線")] public AnimationCurve jumpCurve;
    [Header("攻撃スキル")] public List<Skill> atkSkillList = null;
    [Header("防御スキル")] public List<Skill> defSkillList = null;


    #endregion
    #region//プライベート変数
    private bool isJump = false;
    private bool isGround = false;
    private bool isHead = false;
    private bool isRun = false;
    private bool isDamaged = false;
    private bool isDown=false;
    private bool isOnPlatform = false;
    private bool isAction = false;
    private bool isGuard = false;
    private bool isBlocking = false;
    private bool isTouchingWall = false;
    private bool isOkiseme = false;
    private float isInvasion = 1;

    private animState currentAnimState;
    private animSubState currentAnimSubState;

    private float jumpPos = 0.0f;
    private float jumpTime = 0f;
    private float xSpeed;
    private float ySpeed;
    private float originGravity=0;

    private GameObject enemy;
    
    private Collider2D GetOnFloor=null;
    private Rigidbody2D rb = null;
    private Animator anim = null;
    private playerStatus ps = null;
    private Shooter shooter = null;


    #endregion
    #region//アニメーション状態遷移変数
    public enum animState {
        stand,
        run,
        inAir,
        shoot,
        guard,
        block,
        lean,
        down,
        inAirDamage
    }
    public enum animSubState {
        action,
        notAction,
        damage
    }

    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        anim=gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        shooter = gameObject.GetComponentInChildren<Shooter>();
        ps = gameObject.GetComponent<playerStatus>();
        originGravity = gravity;
    }

    #region//functions
    /// <summary>
    /// Y成分で必要な計算をし、速度を返す。
    /// </summary>
    /// <returns>Y軸の速さ</returns>
    private float Jumper() {
        float verticalKey=0;
        float ySpeed = -gravity;

        if (playerID == 1) verticalKey = Input.GetAxisRaw("Vertical");
        else if(playerID ==2) verticalKey = Input.GetAxisRaw("Vertical2");
        if (isGround) {
            if (verticalKey > 0 && !isJump && !isAction) {
                jumpPos = transform.position.y;
                isJump = true;
                
            }
        }

        if (isJump) {
            bool canTime = jumpLimitTime > jumpTime;
            if (!isHead && canTime) {
                jumpTime += Time.deltaTime;
                ySpeed = jumpSpeed * jumpCurve.Evaluate(jumpTime);
            }
            else {
                jumpTime = 0f;
                isJump = false;
            }
        } 
        

        return ySpeed;

    } 
    private float GetXSpeed() {
        float horizontalKey=0;
        float xSpeed = 0.0f;
        float backSpeed = -walkSpeed;
        int playerDirection = 0;


        if (playerID == 1)horizontalKey = Input.GetAxisRaw("Horizontal");
        else if(playerID ==2 ) horizontalKey = Input.GetAxisRaw("Horizontal2");

        

        if (playerID == 1) playerDirection = 1;
        else if(playerID == 2) playerDirection = 180;

        transform.rotation = Quaternion.Euler(0, playerDirection, 0);

        if (horizontalKey > 0 && !isAction && !isGuard) {
            if (isTouchingWall && playerID == 2) {
                backSpeed = 0;
            }

            transform.localScale = new Vector3(3, 3, 1);            
            isRun = true;
            if(isGround)currentAnimState = animState.run;
            xSpeed = walkSpeed;

        }
        else if (horizontalKey < 0 && !isAction && !isGuard) {
            if (isTouchingWall && playerID == 1) {
                backSpeed = 0;
            }
            transform.localScale = new Vector3(3, 3, 1);
            
            isRun = true;
            if (isGround) currentAnimState = animState.run;
            xSpeed = backSpeed;
        }
        else {
            isRun = false;
            xSpeed = 0f;

        }
        return (xSpeed*isInvasion);

    }
    /// <summary>
    /// 足場の判定を消す
    /// </summary>
    private void GetOff() {
        if (isOnPlatform && !isAction) {
            float verticalKey = 0;
            if (playerID == 1) verticalKey = Input.GetAxisRaw("Vertical");
            else if (playerID == 2) verticalKey = Input.GetAxisRaw("Vertical2");
            if (verticalKey < 0) {
                GetOnFloor.isTrigger = true;
            }
        }
    }
    /// <summary>
    /// ガード状態の真偽を管理する
    /// </summary>
    private void Guard() {
        bool guardKey=false;
        if (playerID == 1) {
            guardKey = Input.GetButton("Guard");
            
        }
        else if (playerID == 2) {
            guardKey = Input.GetButton("Guard2");
        }

        if (guardKey && !isAction && isGround && !ps.isGuardBreakGet()) {
            isGuard = true;
            currentAnimState = animState.guard;
        }
        else {
            isGuard = false;
        }

    }
    /// <summary>
    /// アニメーションを設定する
    /// </summary>
    private void SetAnimation() {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
        anim.SetBool("damage",isDamaged);
        anim.SetBool("down", isDown);
        anim.SetBool("guard",isGuard);
        anim.SetBool("blocking", isBlocking);

    }
    private void DitectStand() {
        if (playerID == 1) {
            if (currentAnimSubState == animSubState.notAction && isGround && Input.GetAxisRaw("Horizontal") == 0) {
                currentAnimState = animState.stand;
            }
        }
        else if (playerID == 2) {
            if (currentAnimSubState == animSubState.notAction && isGround && Input.GetAxisRaw("Horizontal2") == 0) {
                currentAnimState = animState.stand;
            }
        }
    }
    private void SetSubAnimState() {
        if (currentAnimState == animState.stand || currentAnimState == animState.run || currentAnimState == animState.inAir) {
            currentAnimSubState = animSubState.notAction;
        }
        else if (currentAnimState == animState.shoot || currentAnimState == animState.block || currentAnimState == animState.guard) {
            currentAnimSubState = animSubState.action;
        }
        else if(currentAnimState == animState.lean || currentAnimState == animState.down) {
            currentAnimSubState = animSubState.damage;
        }
    }
    
    #endregion

    #region //変数設定取得関数
    public void SetIsAction(bool x) {
        isAction = x;
    }
    public bool GetIsAction() {
        return isAction;
    }
    public void SetIsDamaged(bool x) {
        isDamaged = x;
    }
    public bool GetIsDamaged() {
        return isDamaged;
    }
    public void SetIsDown(bool x) {
        isDown = x;
    }
    public bool GetIsDown() {
        return isDown;
    }
    public bool GetIsGuard() {
        return isGuard;
    }
    public void SetIsBlocking(bool x) {
        isBlocking = x;
    }
    public bool GetIsBlocking() {
        return isBlocking;
    }
    public bool GetIsOkiseme() {
        return isOkiseme;
    }
    public bool GetIsInvasion() {
        if(isInvasion == 1) {
            return false;
        }
        else if(isInvasion == 0.5f) {
            return true;
        }
        else {
            return false;
        }
    }

    public animState GetAnimState() {
        return currentAnimState;
    }
    public animSubState GetAnimSubState() {
        return currentAnimSubState;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

        SetSubAnimState();
        DitectStand();
        isGround = ground.IsGround();
        isHead = head.IsGround();
        isOnPlatform = ground.IsOnPlatform();
        GetOnFloor = ground.GetFloor();

        if (!isGround) {
            currentAnimState = animState.inAir;
        }

        xSpeed = GetXSpeed();
        ySpeed = Jumper();
        
        SetAnimation();

        shooter.Shoot();
        GetOff();
        Guard();

        
        rb.velocity = new Vector2(xSpeed,ySpeed);
        //this.gameObject.transform.position =new Vector3(Mathf.Clamp(this.gameObject.transform.position.x, -8, 8), this.gameObject.transform.position.y, 0f);

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "wall") {
            isTouchingWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "wall") {
            isTouchingWall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Respawn" ) {
            if (playerID == 1) {
                isInvasion = 0.5f;
            }else if(playerID == 2) {
                isInvasion = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Respawn") {
            if(playerID == 1) {
                isInvasion = 1;
            }else if(playerID == 2) {
                isInvasion = 0.5f;
            }
        }
    }

}
