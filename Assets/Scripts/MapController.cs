using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapContoller : Singleton<MapContoller>
{
    [SerializeField] GameObject[] _terrainChunk;
    [SerializeField] float _checkerRadius;
    [SerializeField] LayerMask _terrainMask;
    [SerializeField] float maxDistanceFromChunk;

    public GameObject currentChunk;

    Vector3[] _noTerrainPosition;
    Vector3 playerMoveDirection;

    List<GameObject> _activeChunks = new();
    GameObject latestChunk1;
    GameObject latestChunk2;
    GameObject latestChunk3;

    float distanceFromChunk;


    private void Start()
    {
        _noTerrainPosition = new Vector3[3];

        SpawnChunk2();
        InvokeRepeating(nameof(ChunkOptimizer), 1f, 1f);
    }


    private void Update()
    {
        playerMoveDirection = PlayerController.Instance.MoveDirection;
        ChunkChecker();
    }


    private void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

      //  int xCheckCoord = playerMoveDirection.x == 0 ? 0 : playerMoveDirection.x > 0 ? 24 : -24;
      //  int yCheckCoord = playerMoveDirection.y == 0 ? 0 : playerMoveDirection.y > 0 ? 24 : -24;

       

    int[] xCheckCoord = new int[3]; // = playerMoveDirection.x == 0 ? 0 : playerMoveDirection.x > 0 ? 24 : -24;
    int[] yCheckCoord = new int[3]; // = playerMoveDirection.y == 0 ? 0 : playerMoveDirection.y > 0 ? 24 : -24;

    if (playerMoveDirection.x == 0)
    {
        xCheckCoord[0] = -24;
        xCheckCoord[1] = 0;
        xCheckCoord[2] = 24;
    }
    else if (playerMoveDirection.x > 0 && playerMoveDirection.y == 0)
    {
        xCheckCoord[0] = xCheckCoord[1] = xCheckCoord[2] = 24;
    }
    else if (playerMoveDirection.x < 0 && playerMoveDirection.y == 0)
    {
        xCheckCoord[0] = xCheckCoord[1] = xCheckCoord[2] = -24;
    }
    else if (playerMoveDirection.x > 0 && playerMoveDirection.y >0)
    {
        xCheckCoord[0] = 0;
        xCheckCoord[1] = xCheckCoord[2] = 24;
            yCheckCoord[0] = yCheckCoord[1] = 24;
            yCheckCoord[2] = 0;
    }
    else if (playerMoveDirection.x < 0 && playerMoveDirection.y >0)
    {
        xCheckCoord[0] = 0;
        xCheckCoord[1] = xCheckCoord[2] = -24;
            yCheckCoord[0] = yCheckCoord[1] = 24;
            yCheckCoord[2] = 0;
    }
    if (playerMoveDirection.y == 0)
    {
        yCheckCoord[0] = -24;
        yCheckCoord[1] = 0;
        yCheckCoord[2] = 24;
    }
    else if (playerMoveDirection.y > 0 && playerMoveDirection.x == 0)
    {
        yCheckCoord[0] = yCheckCoord[1] = yCheckCoord[2] = 24;
    }
    else if (playerMoveDirection.y < 0 && playerMoveDirection.x == 0)
    {
        yCheckCoord[0] = yCheckCoord[1] = yCheckCoord[2] = -24;
    }
    else if (playerMoveDirection.x > 0 && playerMoveDirection.y <0)
    {
        xCheckCoord[0] = 0;
        xCheckCoord[1] = xCheckCoord[2] = 24;
            yCheckCoord[0] = yCheckCoord[1] = -24;
            yCheckCoord[2] = 0;
    }
    else if (playerMoveDirection.x < 0 && playerMoveDirection.y <0)
    {
        xCheckCoord[0] = 0;
        xCheckCoord[1] = xCheckCoord[2] = -24;
            yCheckCoord[0] = yCheckCoord[1] = -24;
            yCheckCoord[2] = 0;
    }


        _noTerrainPosition[0] = currentChunk.transform.position + new Vector3(xCheckCoord[0], yCheckCoord[0], 0);
        _noTerrainPosition[1] = currentChunk.transform.position + new Vector3(xCheckCoord[1], yCheckCoord[1], 0);
        _noTerrainPosition[2] = currentChunk.transform.position + new Vector3(xCheckCoord[2], yCheckCoord[2], 0);

        var chunk1 = Physics2D.OverlapCircle(_noTerrainPosition[0], _checkerRadius, _terrainMask);
        var chunk2 = Physics2D.OverlapCircle(_noTerrainPosition[1], _checkerRadius, _terrainMask);
        var chunk3 = Physics2D.OverlapCircle(_noTerrainPosition[2], _checkerRadius, _terrainMask);


        if (!chunk1)
            {
                SpawnChunk1();
            }

        if (!chunk2)
        {
            SpawnChunk2();
        }

        if (!chunk3)
        {
            SpawnChunk3();
        }


        // _noTerrainPosition = currentChunk.transform.position + new Vector3(xCheckCoord, yCheckCoord, 0);

        // var chunk = Physics2D.OverlapCircle(_noTerrainPosition, _checkerRadius, _terrainMask);

        // if (!chunk)
        // {
        //    SpawnChunk();
        // }

    }

    private void SpawnChunk1()
    {
        latestChunk1 = Instantiate(_terrainChunk[Random.Range(0, _terrainChunk.Length)], _noTerrainPosition[0], Quaternion.identity);

        latestChunk1.transform.SetParent(transform);

        _activeChunks.Add(latestChunk1);
    }
    private void SpawnChunk2()
    {
        latestChunk2 = Instantiate(_terrainChunk[Random.Range(0, _terrainChunk.Length)], _noTerrainPosition[1], Quaternion.identity);

        latestChunk2.transform.SetParent(transform);

        _activeChunks.Add(latestChunk2);
    }
    private void SpawnChunk3()
    {
        latestChunk3 = Instantiate(_terrainChunk[Random.Range(0, _terrainChunk.Length)], _noTerrainPosition[2], Quaternion.identity);

        latestChunk3.transform.SetParent(transform);

        _activeChunks.Add(latestChunk3);
    }

    private void ChunkOptimizer()
    {
        foreach (var chunk in _activeChunks)
        {
            distanceFromChunk = Vector3.Distance(PlayerController.Instance.transform.position, chunk.transform.position);
            chunk.SetActive(distanceFromChunk < maxDistanceFromChunk);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        
        for (int i = 0; i<=2; i++)
        {
            Gizmos.DrawWireSphere(_noTerrainPosition[i], _checkerRadius);

        }

    }
}
