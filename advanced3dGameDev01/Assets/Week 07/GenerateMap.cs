using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    float[,] map;

    [SerializeField]
    [Range(10, 100)]
    int mapHeight,
        mapWidth;

    [SerializeField]
    [Range(0, 100)]
    float blockSize,
        blockHeight,
        frequency,
        scale;

    public GameObject minecraftBlock;

    void InitArray()
    {
        map = new float[mapWidth, mapHeight];
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                // nx = i / mapWidth;
                // ny = j / mapHeight;
                map[i, j] = Mathf.PerlinNoise(
                    i * 1.0f / frequency * 0.1f,
                    j * 1.0f / frequency + 0.1f
                );
            }
        }
    }

    void DisplayArray()
    {
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                GameObject t = Instantiate(
                    minecraftBlock,
                    new Vector3(
                        i * blockSize,
                        Mathf.Round(map[i, j] * blockHeight * scale),
                        j * blockSize
                    ),
                    Quaternion.identity
                );
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new float[mapWidth, mapHeight];
        minecraftBlock.transform.localScale = new Vector3(blockSize, blockHeight, blockSize);
        InitArray();
        DisplayArray();
    }

    // Update is called once per frame
    void Update() { }
}
