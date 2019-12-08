using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // 玩家與相機座標
    Transform player, mainCamera;
    // 座標距離
    Vector3 offset;
    Vector3Int iTilePosition = new Vector3Int(-1, -1, 0);

    [Header("植物生成點")]
    public GameObject bornPoint;
    [Header("要使用的圖塊")]
    public Tile[] myTreeTile;

    //public ITilemap myITilemap;
    [Header("目標Tilemap")]
    public Tilemap myTilemap;
    [Header("素材")]
    public Sprite treeSprite01;
    public Sprite treeSprite02;

    private int timeCircle;
    private List<Vector3> availablePlaces;
    
    //public Sprite treeSprite02;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("精靈王").transform;
        mainCamera = GameObject.Find("Main Camera").transform;
        offset = player.position - mainCamera.position;

        //TilemapBorn();

    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.position = player.position - offset;

        ChangeTile();
    }

    
    /// <summary>
    /// 生成背景植物
    /// </summary>
    private void TilemapBorn()
    {
        Vector3Int bornLocation = myTilemap.WorldToCell(bornPoint.transform.position);
        
        List<Tile> myTiles = new List<Tile>();

        for (int i = 0; i < myTreeTile.Length; i++)
        {
            myTiles.Add(myTreeTile[i]);
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
        do
        {
            TreeRandom(tilemap, position, tiles);
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
        for (int i = 0; i < Random.Range(3, 9); i++)
        {
            position.x += Random.Range(3, 9);
            tilemap.SetTile(position, tiles[Random.Range(1, 3)]);
        }

        for (int i = 0; i < Random.Range(1, 2); i++)
        {
            position.x += Random.Range(3, 8);
            tilemap.SetTile(position, tiles[0]);
            position.x += Random.Range(3, 8);
        }

    }
    
    private void GetAllTileLocale()
    {
        availablePlaces = new List<Vector3>();

        foreach (var position in myTilemap.cellBounds.allPositionsWithin)
        {
            if (!myTilemap.HasTile(position)) continue;
            availablePlaces.Add(position);
        }
    }

    private void ChangeTile()
    {
        timeCircle = (int)Time.realtimeSinceStartup;
        if (timeCircle > 10)
        {
            //myTile.sprite = treeSprite01;
            //myTilemap.RefreshAllTiles();
        }

    }

}
