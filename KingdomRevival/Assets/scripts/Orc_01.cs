using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_01 : MonoBehaviour
{
    public float hp = 200;
    public float attack = 50;
    public float attackRange = 3;
    public float moveSpeed = 5;
    private float timerAttack;
    private float attackCD = 1.5f;

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GoForward();
        Track();
    }

    /// <summary>
    /// 進軍
    /// </summary>
    private void GoForward()
    {
        ani.SetBool("走路開關", true);
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }
    /// <summary>
    /// 撤退
    /// </summary>
    private void Retreat()
    {

    }

    private void Track()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.right, attackRange, 1 << 12);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * attackRange, Color.yellow);

        if (hit2D.collider)
        {
            moveSpeed = 0;
            Attack();
        }
    }

    private void Attack()
    {
        if (timerAttack >= attackCD)
        {
            ani.SetTrigger("攻擊觸發");
            ani.SetBool("走路開關", false);
            timerAttack = 0;
        }
        else
        {
            timerAttack += Time.deltaTime;
            ani.SetBool("走路開關", true);
        }
    }

    private void Hit(float damage)
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
        moveSpeed = 0;
        Destroy(this.gameObject);
    }
}
