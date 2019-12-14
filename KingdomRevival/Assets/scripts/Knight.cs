using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public GameManager gm;

    public float hp = 300;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    public float defendRange = 16;
    public float attackRange = 2;

    private float timerAttack;
    private float attackCD = 2f;
    private bool isGround;
    public bool isAttackMode;
    private float moveSpeed;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

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

    public void SetAttackMode(bool b)
    {
        isAttackMode = b;
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
        Collider2D collider2D = gm.RayDefend();                         // 城門collider
        float posX = Random.Range(4, 6);

        if (collider2D != null)
        {
            float dis = Vector3.Distance(transform.position, collider2D.transform.position);
            if (isGround && dis > posX)
            {
                ani.SetBool("走路開關", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(collider2D.transform.position.x - posX, transform.position.y, 0), moveSpeed * Time.deltaTime);
            }
            else
            {
                Idle();
            }
        }
        else
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
    }
    private void Track()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, attackRange, 1 << 11);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * attackRange, Color.red);

        if (hit2D.collider)
        {
            moveSpeed = 0;
            Attack();
        }
    }

    private void Attack()
    {
        if (isGround)
        {
            //print(isGround);
            if (timerAttack >= attackCD)
            {
                ani.SetTrigger("攻擊觸發");
                timerAttack = 0;
            }
            else
            {
                timerAttack += Time.deltaTime;
            }
        }
    }

    private void HitByEnemy(float damage)
    {
        ani.SetTrigger("觸發受傷");
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

    private void Idle()
    {
        ani.SetBool("走路開關", false);
        ani.SetBool("跑步開關", false);
    }
}
