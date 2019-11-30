using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    public GameObject player;

    private float moveSpeed = 10.0f;
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
        MoveTo();
        
    }

    /// <summary>
    /// 玩家移動
    /// </summary>
    private void MoveTo()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ani.SetBool("走路開關", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ani.SetBool("走路開關", true);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb2.isKinematic = false;
            rb2.AddForce(transform.right * thrust, ForceMode2D.Impulse);
            rb2.AddForce(transform.up * thrust, ForceMode2D.Impulse);

            //rb2.AddRelativeForce(transform.position, ForceMode2D.Impulse);
        }
    }


    private void Buildings()
    {
        //Instantiate()
    }
}
