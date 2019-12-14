using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    Vector3 offset;

    private Rigidbody2D rb2d;
    private Collider2D cc2dHit;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ShootRotate();
    }

    // Update is called once per frame
    void Update()
    {
        StopArrow();
    }

    private void ShootRotate()
    {
        if (transform.GetComponent<Rigidbody2D>() != null)
        {
            if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                Vector3 vel = GetComponent<Rigidbody2D>().velocity;                 // 取得箭矢速度
                float angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;           // 計算向量角度
                transform.eulerAngles = new Vector3(0, 0, angleZ);                  // 改變箭矢角度
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            cc2dHit = collision;

            //print(collision.gameObject.name);
            rb2d.velocity = new Vector2(0, 0);
            rb2d.gravityScale = 0;
            //rb2d.Sleep();
            offset = collision.transform.position - rb2d.transform.position;                    // 維持位置
            //print(offset);
            Invoke("DestoryArrow", 1.5f);
        }
        if (collision.tag == "Ground")
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.gravityScale = 0;
            Invoke("DestoryArrow", 0.15f);
        }
    }

    private void DestoryArrow()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 停止箭矢移動
    /// </summary>
    private void StopArrow()
    {
        if (cc2dHit !=null && cc2dHit.tag == "Enemy")
        {
            rb2d.transform.position = cc2dHit.transform.position - offset;
        }
    }

    /// <summary>
    /// 回收箭矢
    /// </summary>
    private void RetrieveArrow()
    {
        cc2dHit = null;
        rb2d.MovePosition(new Vector3(-25, -25, 0));
    }
}
