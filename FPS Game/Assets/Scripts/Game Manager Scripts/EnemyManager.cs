using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;

    [SerializeField]
    private GameObject  tiger_Prefab, soldier_Prefab, rhino_Prefab, snow_Soldier_Prefab;

    public Transform[]  tiger_SpawnPoints, soldier_SpawnPoints, rhino_SpawnPoints, snow_Soldier_SpawnPoints;

    [SerializeField]
    private int  tiger_Enemy_Count, soldier_Enemy_Count, rhino_Enemy_Count, snow_Soldier_Enemy_Count;

    private int  initial_Tiger_Count, initial_Soldier_Count, initial_Rhino_Count, initial_Snow_Soldier_Count;

    public float wait_Before_Spawn_Enemies_Time = 10f;

    // Use this for initialization
    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
 
        initial_Tiger_Count = tiger_Enemy_Count;
        initial_Soldier_Count = soldier_Enemy_Count;
        initial_Rhino_Count = rhino_Enemy_Count;
        initial_Snow_Soldier_Count = snow_Soldier_Enemy_Count;

        SpawnEnemies();

        StartCoroutine("CheckToSpawnEnemies");
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void SpawnEnemies()
    {

        SpawnTigers();
        SpawnSoldiers();
        SpawnRhinos();
        SpawnSnowSoldiers();
    }




    void SpawnTigers()
    {

        int index = 0;

        for (int i = 0; i < tiger_Enemy_Count; i++)
        {

            if (index >= tiger_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(tiger_Prefab, tiger_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        tiger_Enemy_Count = 0;

    }

    void SpawnSoldiers()
    {

        int index = 0;

        for (int i = 0; i < soldier_Enemy_Count; i++)
        {

            if (index >= soldier_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(soldier_Prefab, soldier_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        soldier_Enemy_Count = 0;

    }

    void SpawnRhinos()
    {

        int index = 0;

        for (int i = 0; i < rhino_Enemy_Count; i++)
        {

            if (index >= rhino_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(rhino_Prefab, rhino_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

       rhino_Enemy_Count = 0;

    }

    void SpawnSnowSoldiers()
    {
        int index = 0;

        for (int i = 0; i < snow_Soldier_Enemy_Count; i++)
        {

            if (index >= snow_Soldier_SpawnPoints.Length)
            {
                index = 0;
            }

            Instantiate(snow_Soldier_Prefab, snow_Soldier_SpawnPoints[index].position, Quaternion.identity);

            index++;

        }

        snow_Soldier_Enemy_Count = 0;

    }

    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(wait_Before_Spawn_Enemies_Time);

        SpawnTigers();

        SpawnSoldiers();

        SpawnRhinos();

        SpawnSnowSoldiers();


        StartCoroutine("CheckToSpawnEnemies");

    }



    public void TigerDied(bool tiger)
    {
        if (tiger)
        {
            tiger_Enemy_Count++;
            if (tiger_Enemy_Count > initial_Tiger_Count)
            {
                tiger_Enemy_Count = initial_Tiger_Count;
            }
        }
        else
        {
            snow_Soldier_Enemy_Count++;

            if (snow_Soldier_Enemy_Count > initial_Snow_Soldier_Count)
            {
                snow_Soldier_Enemy_Count = initial_Snow_Soldier_Count;
            }
        }
    }

    public void RhinoDied(bool rhino)
    {
        if (rhino)
        {
            rhino_Enemy_Count++;
            if (rhino_Enemy_Count > initial_Rhino_Count)
            {
                rhino_Enemy_Count = initial_Rhino_Count;
            }
        }
        else
        {
            soldier_Enemy_Count++;

            if (soldier_Enemy_Count > initial_Soldier_Count)
            {
                soldier_Enemy_Count = initial_Soldier_Count;
            }
        }

    }

    public void StopSpawning()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }

} // class


































