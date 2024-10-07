using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapContoller : Singleton<MapContoller>
{
    [SerializeField] GameObject _terrainChunk;
    [SerializeField] float _checkerRadius;
    [SerializeField] LayerMask _terrainMask;
    [SerializeField] float maxDistanceFromChunk;

    public GameObject currentChunk;

    Vector3[] _noTerrainPosition;
    Vector3 playerMoveDirection;

    List<GameObject> _activeChunks = new();
    GameObject latestChunk;

    float distanceFromChunk;


    private void Start()
    {
        _noTerrainPosition = new Vector3[3];
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

        int[] xCheckCoord = new int[3]; // = playerMoveDirection.x == 0 ? 0 : playerMoveDirection.x > 0 ? 24 : -24;
        int[] yCheckCoord = new int[3]; // = playerMoveDirection.y == 0 ? 0 : playerMoveDirection.y > 0 ? 24 : -24;

        if (playerMoveDirection.x == 0)
        {
            xCheckCoord[0] = -24;
            xCheckCoord[1] = 0;
            xCheckCoord[2] = 24;
        }
        else if (playerMoveDirection.x > 0)
        {
            xCheckCoord[0] = xCheckCoord[1] = xCheckCoord[2] = 24;
        }
        else
        {
            xCheckCoord[0] = xCheckCoord[1] = xCheckCoord[2] = -24;
        }

        if (playerMoveDirection.y == 0)
        {
            yCheckCoord[0] = -24;
            yCheckCoord[1] = 0;
            yCheckCoord[2] = 24;
        }
        else if (playerMoveDirection.y > 0)
        {
            yCheckCoord[0] = yCheckCoord[1] = yCheckCoord[2] = 24;
        }
        else
        {
            yCheckCoord[0] = yCheckCoord[1] = yCheckCoord[2] = -24;
        }

        _noTerrainPosition[0] = currentChunk.transform.position + new Vector3(xCheckCoord[0], yCheckCoord[0], 0);
        _noTerrainPosition[1] = currentChunk.transform.position + new Vector3(xCheckCoord[1], yCheckCoord[1], 0);
        _noTerrainPosition[2] = currentChunk.transform.position + new Vector3(xCheckCoord[2], yCheckCoord[2], 0);


        for (int i = 0; i<=2; i++)
        {
            var chunk = Physics2D.OverlapCircle(_noTerrainPosition[i], _checkerRadius, _terrainMask);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i <= 2; i++)
        {
            Gizmos.DrawWireSphere(_noTerrainPosition[i], _checkerRadius);
        }
    }
}
