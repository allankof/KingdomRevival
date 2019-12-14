using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_01 : MonoBehaviour
{
    public float moveSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 進軍
    /// </summary>
    private void GoForward()
    {
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

    }

    private void Attack()
    {

    }

    private void Hit()
    {

    }

    private void Dead()
    {

    }
}
