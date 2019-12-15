using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Archer : MonoBehaviour
{
    public GameObject arrow;
    public Rigidbody2D rb2dArcher;
    
    public float hp = 100;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float attackRange = 10;
    public float defendRange = 14;
    public float forceX = 12;
    public float forceY = 14;

    private Rigidbody2D rb2dArrow;
    private float timerAttack;
    private float attackCD = 1.5f;
    private bool isGround;
    private float enemyDistance;
    public bool isAttackMode = true;
    private float moveSpeed;

    private GameManager gm;

    private CapsuleCollider2D cc2d;
    private Animator ani;


    // Start is called before the first frame update
    void Start()
    {
        rb2dArcher = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CapsuleCollider2D>();
        ani = GetComponent<Animator>();

        gm = GameObject.Find("GameController").GetComponent<GameManager>();

        moveSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        if (isAttackMode == true || gm.RayDefend() != null)
        {
            Track();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMode();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void ChangeMode()
    {
        if (!isAttackMode)
        {
            DefendMode();
        }
        else
        {
            AttackMode();
        }
    }

    public void SetAttackMode(bool b)
    {
        isAttackMode = b;
    }

    /// <summary>
    /// 攻擊模式
    /// </summary>
    private void AttackMode()
    {
        moveSpeed = runSpeed;
        if (isGround)
        {
            ani.SetBool("跑步開關", true);
            transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
        }
    }

    /// <summary>
    /// 防禦模式
    /// </summary>
    private void DefendMode()
    {
        Collider2D c2DDoor = gm.RayDefend();                         // 城門collider
        Collider2D c2DKnight = gm.RayKnight();
        
        if (c2DDoor != null)
        {
            ActionMode01(c2DDoor);
        }
        else
        {
            if (c2DKnight != null && c2DDoor == null)
            {
                AttackMode();
            }
            else
            {
                ActionMode02();
            }
        }
    }
    /// <summary>
    /// 在城牆後面防禦
    /// </summary>
    /// <param name="c2DDoor"></param>
    /// <param name="posX"></param>
    private void ActionMode01(Collider2D c2DDoor)
    {
        float posX = Random.Range(1, 3);
        float dis = c2DDoor.transform.position.x - transform.position.x;

        //float dis = Vector3.Distance(transform.position, c2DDoor.transform.position);
        if (isGround && dis > posX)
        {
            ani.SetBool("走路開關", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(c2DDoor.transform.position.x - posX, transform.position.y, 0), moveSpeed * Time.deltaTime);
        }
        else
        {
            Idle();
        }
    }
    /// <summary>
    /// 回主城防禦
    /// </summary>
    private void ActionMode02()
    {
        Vector3 homePoint = new Vector3(5, transform.position.y, 0);
        if (transform.position != homePoint)
        {
            moveSpeed = runSpeed;
            ani.SetBool("走路開關", true);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(5, transform.position.y, 0), walkSpeed * Time.deltaTime);
        }
        else
        {
            Idle();
            Track();
        }
    }

    /// <summary>
    /// 攻擊範圍
    /// </summary>
    private void Track()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << 11);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * attackRange, Color.red);

        enemyDistance = hit2D.distance;

        if (hit2D.collider)
        {
            moveSpeed = 0;
            Attack();
        }
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (isGround)
        {
            //print(isGround);
            if (timerAttack >= attackCD)
            {
                ani.SetTrigger("攻擊觸發");
                Invoke("ShootArrow", 0.5f);
                timerAttack = 0;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 射箭,給箭矢瞬間作用力
    /// </summary>
    private void ShootArrow()
    {
        float vecX = forceX;
        float vecY = forceY * (enemyDistance / attackRange);

        rb2dArrow = Instantiate(arrow, transform.position - Vector3.up * 0.5f, Quaternion.identity).GetComponent<Rigidbody2D>();

        rb2dArrow.AddForce(new Vector2(vecX, vecY), ForceMode2D.Impulse);
        /*
        if (gm.RayDefend() != null)
        {
            rb2dArrow.AddForce(new Vector2(vecX / 1.2f, vecY * 1.2f), ForceMode2D.Impulse);
        }
        else
        {
            rb2dArrow.AddForce(new Vector2(vecX, vecY), ForceMode2D.Impulse);
        }
        */
    }

    private void Idle()
    {
        ani.SetBool("走路開關", false);
        ani.SetBool("跑步開關", false);
    }

    public void HitByEnemy(float damage)
    {
        ani.SetTrigger("觸發受傷");
        rb2dArcher.AddForce(new Vector2(2, 2), ForceMode2D.Impulse);
        hp -= damage;
        if (hp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        ani.SetBool("死亡開關", true);
        walkSpeed = 0;
        Destroy(this.gameObject);
    }

    private void DelayAttack()
    {

    }
}
