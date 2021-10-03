using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    #region//�C���X�y�N�^�[
    [Header("�v���C���[ID")] public int playerID;
    [Header("�ړ����x")] public float walkSpeed;
    [Header("�d��")] public float gravity;
    [Header("�W�����v���x")] public float jumpSpeed;
    [Header("�W�����v���x")] public float jumpHeight;
    [Header("�W�����v����")] public float jumpLimitTime;
    [Header("���W�����v����")] public float shortJumpLimitTime;
    [Header("�ڒn����")] public groundCheck ground;
    [Header("�����Ԃ�������")] public groundCheck head;
    [Header("�W�����v�̕\���Ȑ�")] public AnimationCurve jumpCurve;
    [Header("�U���X�L��")] public List<Skill> atkSkillList = null;
    [Header("�h��X�L��")] public List<Skill> defSkillList = null;
    

    #endregion
    #region//�v���C�x�[�g�ϐ�
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
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>Y���̑���</returns>
    private float GetYSpeed() {

        float verticalKey = Input.GetAxisRaw("Vertical");
        float ySpeed = -gravity;

        if (isGround) {
            if (verticalKey > 0) {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
            }
            else {
                isJump = false;
            }

        }
        else if (isJump) {
            bool pushUpKey = verticalKey > 0;
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead) {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else {
                isJump = false;
                jumpTime = 0f;
            }
        }

        if (isJump) {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }

        return (ySpeed * isInvasion);

    }
    /// <summary>
    /// X�����ŕK�v�Ȍv�Z�����A���x��Ԃ��B
    /// </summary>
    /// <returns>X���̑���</returns>
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
            xSpeed = walkSpeed;

        }
        else if (horizontalKey < 0 && !isAction && !isGuard) {
            if (isTouchingWall && playerID == 1) {
                backSpeed = 0;
            }
            transform.localScale = new Vector3(3, 3, 1);
            isRun = true;
            xSpeed = backSpeed;
        }
        else {
            isRun = false;
            xSpeed = 0f;

        }
        return (xSpeed*isInvasion);

    }
    /// <summary>
    /// ����̔��������
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
    /// �K�[�h��Ԃ̐^�U���Ǘ�����
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
        }
        else {
            isGuard = false;
        }

    }
    /// <summary>
    /// �A�j���[�V������ݒ肷��
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
    #endregion

    #region //�ϐ��ݒ�擾�֐�
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
    #endregion

    // Update is called once per frame
    void Update()
    {
        isGround = ground.IsGround();
        isHead = head.IsGround();
        isOnPlatform = ground.IsOnPlatform();
        GetOnFloor = ground.GetFloor();
        
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
