using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuildings : MonoBehaviour
{
    public GameObject archer;

    private float timer;

    public float buildTime = 3;


    //private float timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum BuildingsType
    {
        buildings_0, buildings_1, buildings_2, buildings_3, buildings_4, buildings_5, buildings_6
    }

    BuildingsType _buildingsType;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
            //InvokeRepeating("Born", 0, 5);
        }
    }

    private void Born()
    {
        Instantiate(archer, transform.position, Quaternion.identity);
    }









    private IEnumerator BornArcher()
    {
        if (timer > 5)
        {
            Instantiate(archer, transform.position, Quaternion.identity);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        yield return new WaitForSeconds(1);
    }





    private IEnumerator ChangeBuildings()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(buildTime);
    }
}
