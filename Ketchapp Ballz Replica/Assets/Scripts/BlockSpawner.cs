using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    private Block blockPrefab;

    private int playWidth = 5;
    private float distanceBtwBlocks = 1f;
    private int rowsSpawned;

    private List<Block> blockSpawned = new List<Block>();

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnRowOfBlocks();
        }
    }

    public void SpawnRowOfBlocks()
    {
        foreach (var block in blockSpawned)
        {
            if (block != null)
            {
                block.transform.position += Vector3.down * distanceBtwBlocks;
            }
        }

        for (int i = 0; i < playWidth; i++)
        {
            if (UnityEngine.Random.Range(0, 100) < 30)
            {
                Block block = Instantiate(blockPrefab, GetPosition(i), Quaternion.identity);
                int hits = UnityEngine.Random.Range(1, 3) + rowsSpawned;
                block.SetHits(hits);
                blockSpawned.Add(block);
            }
        }
        rowsSpawned++;
    }

    private Vector3 GetPosition(int i)
    {
        Vector3 position = transform.position;
        position += Vector3.right * i * distanceBtwBlocks;
        return position;
    }
}
