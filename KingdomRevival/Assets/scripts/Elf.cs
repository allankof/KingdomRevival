using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    private void MoveTo()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-10 * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(-10 * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Buildings()
    {
        //Instantiate()
    }
}
