using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GameManager : MonoBehaviour
{
    // 玩家與相機座標
    Transform player, mainCamera;
    // 座標距離
    Vector3 offset;

    [Header("植物生成點")]
    public GameObject bornPoint;
    [Header("要使用的圖塊")]
    public Tile[] GetTile;
    [Header("目標Tilemap")]
    public Tilemap myTilemap;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("精靈王").transform;
        mainCamera = GameObject.Find("Main Camera").transform;
        offset = player.position - mainCamera.position;
        
        TilemapBorn();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.position = player.position - offset;

    }

    /// <summary>
    /// 生成背景植物
    /// </summary>
    private void TilemapBorn()
    {
        Vector3Int bornLocation = myTilemap.WorldToCell(bornPoint.transform.position);
        
        List<Tile> myTiles = new List<Tile>();

        for (int i = 0; i < GetTile.Length; i++)
        {
            myTiles.Add(GetTile[i]);
        }
        
        StartCoroutine(AddTilemap(myTilemap, bornLocation, myTiles));
    }

    /// <summary>
    /// 協同程序
    /// </summary>
    /// <param name="tilemap"></param>
    /// <param name="position"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    private IEnumerator AddTilemap(Tilemap tilemap, Vector3Int position, List<Tile> tiles)
    {
        TreeRandom(tilemap, position, tiles);
        do
        {
            position.x += Random.Range(2, 6);
            TreeRandom(tilemap, position, tiles);
            position.x += Random.Range(2, 12);
            //Debug.Log(position.x);
        } while (position.x < 120);

            yield return new WaitForSeconds(0.2f);
    }

    /// <summary>
    /// 隨機選擇植物
    /// </summary>
    /// <param name="tilemap"></param>
    /// <param name="position"></param>
    /// <param name="tiles"></param>
    private void TreeRandom(Tilemap tilemap, Vector3Int position, List<Tile> tiles)
    {
        int n = Random.Range(0, 3);
        if (n == 0)
        {
            position.y -= 3;
            tilemap.SetTile(position, tiles[n]);
        }
        else if (n == 1)
        {
            tilemap.SetTile(position, tiles[n]);
        }
        else
        {
            position.y -= 4;
            tilemap.SetTile(position, tiles[n]);
        }
    }

}
