using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk : MonoBehaviour
{
    public GameObject targetMap;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MapContoller.Instance.currentChunk = targetMap;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (MapContoller.Instance.currentChunk == targetMap)
            {
                MapContoller.Instance.currentChunk = null;
            }
        }
    }
}
