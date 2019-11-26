using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{

    private float moveSpeed = 10f;

    public Transform mainCemaraPosition;

    // Start is called before the first frame update
    void Start()
    {
        mainCemaraPosition = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
        MoveCamera();
    }

    private void MoveCamera()
    {
        //mainCemaraPosition.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        
    }

    private void MoveTo()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Buildings()
    {
        //Instantiate()
    }
}
