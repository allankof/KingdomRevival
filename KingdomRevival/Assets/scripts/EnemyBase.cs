using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject orc;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("Born", 0, 8);
    }

    private void Born()
    {
        Instantiate(orc, transform.position, Quaternion.identity);
    }
}
