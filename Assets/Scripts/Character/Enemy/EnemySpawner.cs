using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI killCountText;
    static EnemySpawner instance;
    List<GameObject> enemyList = new List<GameObject>(500);
    const float maxX = 10;
    const float maxY = 16;
    const float DecreseSpawnDelayTime = 0.8f;
    float spawnDelay;
    int stage;
    int killCount;
    int spawnCount=0;

    private EnemySpawner() { }
    
    enum Direction
    {
        North,
        South,
        West,
        East
    }

    void Awake()
    {
        Initialize();
    }

    void Start()
    {
    }

    void Initialize()
    {
        instance = this;
        spawnDelay = 0.5f;
        killCount = 0;
    }

    public void ChangeStage()
    {
        if (stage==5)
        {
            stage = 0;
        }
        stage++;
        StartCoroutine(SpawnEnemy());
        StartCoroutine(listChecker());
    }
    IEnumerator SpawnEnemy()
    {
        GameObject newEnemy;

     
            switch (stage)
            {
                default:
                case 1:
                   
                while (spawnCount<5)
                {
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                    spawnCount++;
                    newEnemy.transform.position = RandomPosition();
                    newEnemy.SetActive(true);
                    enemyList.Add(newEnemy);
                }
                spawnCount = 0;
                break;

                case 2:
                   
                while (spawnCount < 3)
                {
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Goblin);
                    spawnCount++;
                    newEnemy.transform.position = RandomPosition();
                    newEnemy.SetActive(true);
                    enemyList.Add(newEnemy);
                }
                spawnCount = 0;
                break;
                case 3:
                    
                while (spawnCount < 2)
                {
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Mushroom);
                    spawnCount++;
                    newEnemy.transform.position = RandomPosition();
                    newEnemy.SetActive(true);
                    enemyList.Add(newEnemy);
                }
                spawnCount = 0;
                break;
                case 4:
                newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                newEnemy.transform.position = RandomPosition();
                newEnemy.SetActive(true);
                enemyList.Add(newEnemy);
                break;
            case 5:
                    newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.Skeleton);
                newEnemy.transform.position = RandomPosition();
                newEnemy.SetActive(true);
                enemyList.Add(newEnemy);
                break;
         
            

            if(stage == 5)
            {
                newEnemy = ObjectPooling.GetObject(CharacterData.CharacterType.FlyingEye);
                newEnemy.transform.position = RandomPosition();
                newEnemy.SetActive(true);
                enemyList.Add(newEnemy);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3();

        Direction direction = (Direction)Random.Range(0, 4);

        switch (direction)
        {
            case Direction.North:
                pos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
                pos.y = player.transform.position.y + 10f;
                break;
            case Direction.South:
                pos.x = Random.Range(player.transform.position.x - 5, player.transform.position.x + 5);
                pos.y = player.transform.position.y - 10f;
                break;
            case Direction.West:
                pos.x = player.transform.position.x - 16f;
                pos.y = Random.Range(player.transform.position.y - 8, player.transform.position.y + 8);
                break;
            case Direction.East:
                pos.x = player.transform.position.x + 15f;
                pos.y = Random.Range(player.transform.position.y - 8, player.transform.position.y + 8);
                break;
        }

        return pos;
    }

    public Vector2 GetNearestEnemyPosition()
    {
        float[] min = {0, int.MaxValue};

        for(int i = 0; i < enemyList.Count; i++) { 
            if(min[1] > (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude)
            {
                min[0] = i;
                min[1] = (enemyList[i].transform.position - Player.GetInstance().GetPosition()).sqrMagnitude;
            }
        }

        return enemyList[(int)min[0]].transform.position;
    }

    public Vector2 GetRandomEnemyPosition()
    {
        int random = Random.Range(0, enemyList.Count);

        return enemyList[random].transform.position;
    }

    IEnumerator listChecker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            for(int i=0; i<enemyList.Count; i++)
            {
                if(!enemyList[i].activeSelf)
                    enemyList.RemoveAt(i);
            }
        }
    }


    
    public void IncreaseKillCount()
    {
        ++killCount;

        killCountText.text = killCount.ToString();
    }

    public static EnemySpawner GetInstance()
    {
        return instance;
    }

    public int GetListCount()
    {
        return enemyList.Count;
    }
}
