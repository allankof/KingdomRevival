using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    Transform player, mainCamera;
    Vector3 offset;

    public GameObject bornPoint;
    public Tile[] GetTile;
    public Tilemap myTilemap;

    private Vector3Int bornLocation;

    // Start is called before the first frame update
    void Start()
    {
        TilemapBuild();

        player = GameObject.Find("精靈王").transform;
        mainCamera = GameObject.Find("Main Camera").transform;
        offset = player.position - mainCamera.position;

    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.position = player.position - offset;

    }

    private void TilemapBuild()
    {
        bornLocation = myTilemap.WorldToCell(transform.position);
        myTilemap.SetTile(bornLocation, GetTile[0]);

        for (int i = 0; i < 10; i++)
        {
            bornLocation.x += 5;
            myTilemap.SetTile(bornLocation, GetTile[0]);
        }
        for (int i = 0; i < 10; i++)
        {
            bornLocation.x += 9;
            myTilemap.SetTile(bornLocation, GetTile[1]);
        }

    }


}
