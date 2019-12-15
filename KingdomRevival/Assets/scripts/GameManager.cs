using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [Header("設施生成點")]
    public Transform bornStructure;
    [Header("植物生成點")]
    public Transform bornFood;
    [Header("設施")]
    public GameObject[] objPrefabs;
    [Header("偵測防禦設施射線")]
    public Transform defendRayPoint;
    [Header("偵測騎士射線")]
    public Transform knightRayPoint;

    [Header("更換遠景")]
    public GameObject background;
    [Header("更換中景")]
    public GameObject backgroundM;
    [Header("要使用的圖塊")]
    public Tile[] usedTilemap;
    [Header("目標Tilemap")]
    public Tilemap myTilemap;
    [Header("素材")]
    public Sprite treeSpriteDay01;
    public Sprite treeSpriteDay02;
    public Sprite treeSpriteDay03;
    public Sprite groundSpriteDay;
    public Sprite treeSpriteSs01;
    public Sprite treeSpriteSs02;
    public Sprite treeSpriteSs03;
    public Sprite groundSpriteSs;
    public Sprite treeSpriteN01;
    public Sprite treeSpriteN02;
    public Sprite treeSpriteN03;
    public Sprite groundSpriteNight;
    [Header("素材"), HideInInspector]
    public float dayTime;

    private GameObject objChild;

    // 玩家與相機座標
    private Transform player, mainCamera;
    // 座標距離
    private Vector3 offset;

    private List<Vector3> availablePlaces;

    //public Sprite treeSprite02;

    public enum DayNight
    {
        day, sunSet, night
    }

    public DayNight _dayNight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("精靈王").transform;
        mainCamera = GameObject.Find("Main Camera").transform;
        offset = player.position - mainCamera.position;

        //getObjComponent();

        StartBuild();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.position = player.position - offset;

        TimeCycle();
        StartCoroutine(ChangeTile());
    }


    public float TimeCycle()
    {
        if (dayTime >= 12)
        {
            dayTime = 0;
        }
        else
        {
            dayTime += Time.deltaTime;
        }
        //print(dayTime);
        return dayTime;
    }

    /// <summary>
    /// Tilemap材質轉換
    /// </summary>
    /// <returns></returns>
    private IEnumerator ChangeTile()
    {
        //dayTime = (int)Time.realtimeSinceStartup;
        if (dayTime <= 5)
        {
            _dayNight = DayNight.day;
        }
        else if (dayTime >= 7)
        {
            _dayNight = DayNight.night;
        }
        else
        {
            _dayNight = DayNight.sunSet;
        }
        switch (_dayNight)
        {
            case DayNight.day:
                usedTilemap[0].sprite = treeSpriteDay01;
                usedTilemap[1].sprite = treeSpriteDay02;
                usedTilemap[2].sprite = treeSpriteDay03;
                usedTilemap[3].sprite = groundSpriteDay;
                background.transform.GetChild(0).gameObject.SetActive(true);
                background.transform.GetChild(1).gameObject.SetActive(false);
                background.transform.GetChild(2).gameObject.SetActive(false);
                myTilemap.RefreshAllTiles();
                break;
            case DayNight.sunSet:
                usedTilemap[0].sprite = treeSpriteSs01;
                usedTilemap[1].sprite = treeSpriteSs02;
                usedTilemap[2].sprite = treeSpriteSs03;
                usedTilemap[3].sprite = groundSpriteSs;
                background.transform.GetChild(0).gameObject.SetActive(false);
                background.transform.GetChild(1).gameObject.SetActive(true);
                background.transform.GetChild(2).gameObject.SetActive(false);
                myTilemap.RefreshAllTiles();
                break;
            case DayNight.night:
                usedTilemap[0].sprite = treeSpriteN01;
                usedTilemap[1].sprite = treeSpriteN02;
                usedTilemap[2].sprite = treeSpriteN03;
                usedTilemap[3].sprite = groundSpriteNight;
                background.transform.GetChild(0).gameObject.SetActive(false);
                background.transform.GetChild(1).gameObject.SetActive(false);
                background.transform.GetChild(2).gameObject.SetActive(true);
                myTilemap.RefreshAllTiles();
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.3f);
    }

    /// <summary>
    /// 建立使用的設施陣列
    /// </summary>
    private void StartBuild()
    {
        ArrayList myBuildings_1 = new ArrayList(){ 0, 0, 0, 1, 1, 1, 1, 1, 2, 2, 3, 3 };
        ArrayList myBuildings_2 = new ArrayList() { 0, 1, 1, 1, 1, 2, 3, 3, 4 };
        //ArrayList myBuildings_3 = new ArrayList() { 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2 };

        AddBuildings(myBuildings_1, 4, myBuildings_2, 3);
    }

    /// <summary>
    /// 傳入要產生的設施並建立
    /// </summary>
    /// <param name="ary_1">第一次要使用的設施陣列</param>
    /// <param name="ary_2">第二次要使用的設施陣列</param>
    /// <param name="a">第一次要建立設施的數量</param>
    /// <param name="b">第二次要建立設施的數量</param>
    private void AddBuildings(ArrayList ary_1, int a, ArrayList ary_2, int b)
    {
        float posX = 0;                                             // x軸增量

        // 建立一個基礎設施
        Instantiate(objPrefabs[0], bornStructure.position + new Vector3(posX, 0, 0), Quaternion.identity);
        posX += 14;

        ArrayList numList = RandomNum(ary_1, a);                    // 取得隨機設施索引值
        posX = InstanceBuildings(numList, posX);                       // 建立設施，取得回傳的x軸增量

        // 如果沒有農地建立一個
        if (numList.IndexOf(2) == -1)
        {
            Instantiate(objPrefabs[2], bornStructure.position + new Vector3(posX, 0, 0), Quaternion.identity);
            posX += 22;
        }

        // 建立敵人基地
        posX += 14;
        Instantiate(objPrefabs[8], bornStructure.position + new Vector3(posX, 0, 0), Quaternion.identity);
        posX += 10;

        numList = RandomNum(ary_2, b);

        numList.Insert(Random.Range(0, numList.Count), 5);                                       // 隨機位置加入設施[5]
        numList.Insert(Random.Range(0, numList.Count), 6);                                       // 隨機位置加入設施[6]

        posX = InstanceBuildings(numList, posX);

        // 建立敵人基地
        posX += 14;
        Instantiate(objPrefabs[9], bornStructure.position + new Vector3(posX, 0, 0), Quaternion.identity);

        //objChild = objPrefabs[0].transform.GetChild(0).gameObject;
        //objChild.SetActive(true);
    }

    /// <summary>
    /// 產生隨機設施的陣列
    /// </summary>
    /// <param name="aryList">傳入要使用的陣列</param>
    /// <param name="n">陣列個數</param>
    /// <returns></returns>
    private ArrayList RandomNum(ArrayList aryList, int n)
    {
        ArrayList numList = new ArrayList();

        for (int i = 0; i < n; i++)
        {
            int num = Random.Range(0, aryList.Count);
            //print(num);
            numList.Add(aryList[num]);
            aryList.RemoveAt(num);                                  // 移除已選過的內容
        }
        return numList;
    }

    /// <summary>
    /// 建立設施的方法
    /// </summary>
    /// <param name="list">隨機設施的陣列</param>
    /// <param name="posX">x軸的值</param>
    /// <returns></returns>
    private float InstanceBuildings(ArrayList list, float posX)
    {
        foreach (int item in list)
        {
            posX += 8;

            Instantiate(objPrefabs[item], bornStructure.position + new Vector3(posX, 0, 0), Quaternion.identity);
            switch (item)
            {
                case 0: posX += 14; break;
                case 1: posX += 7; break;
                case 2: posX += 20; break;
                case 3: posX += 10; break;
                case 4: posX += 10; break;
                case 5: posX += 12; break;
                case 6: posX += 12; break;
                default: break;
            }
        }
        return posX;
    }

    /// <summary>
    /// 偵測防禦設施
    /// </summary>
    /// <returns></returns>
    public Collider2D RayDefend()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(defendRayPoint.position + Vector3.up, -transform.right, 300, 1 << 12);
        //Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.right) * 300, Color.yellow);
        //print(hit2D.collider);
        return hit2D.collider;
    }
    /// <summary>
    /// 偵測敵人
    /// </summary>
    /// <returns></returns>
    public Collider2D RayKnight()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(knightRayPoint.position + Vector3.up, -transform.right, 300, 1 << 13);
        //Debug.DrawRay(transform.position, -transform.TransformDirection(Vector2.right) * 300, Color.yellow);
        //print(hit2D.collider);
        return hit2D.collider;
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

    /*
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
        position.x += 3;
        do
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

            //TreeRandom(tilemap, position, tiles);
            //Debug.Log(position.x);
        } while (position.x < 120);

            yield return new WaitForSeconds(0.2f);
    }
    */
}
