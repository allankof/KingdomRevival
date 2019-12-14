using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    public GameObject player;

    public float moveSpeed = 10.0f;
    [Header("金幣")]
    public float money = 0;

    private bool playerDirection;
    private float thrust = 1.0f;

    private Animator ani;
    private Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UseFlag();
    }

    /// <summary>
    /// 玩家移動
    /// </summary>
    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ani.SetBool("走路開關", true);
            playerDirection = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ani.SetBool("走路開關", true);
            playerDirection = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb2.isKinematic = false;
            rb2.AddForce(transform.right * thrust, ForceMode2D.Impulse);
            rb2.AddForce(transform.up * thrust, ForceMode2D.Impulse);

            //rb2.AddRelativeForce(transform.position, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// 放置戰旗
    /// </summary>
    private void UseFlag()
    {
        float flagX = transform.position.x + 3;

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!playerDirection) flagX = transform.position.x - 3;

            GameObject.Find("戰旗").transform.position = new Vector3(flagX, transform.position.y + 0.6f);
        }
    }

    private void Buildings()
    {
        //Instantiate()
    }
}
