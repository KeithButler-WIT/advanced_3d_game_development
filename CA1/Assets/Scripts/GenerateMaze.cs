// next move NPC towards tager, after adding both procedurally
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateMaze : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    public GameObject NPC;
    public GameObject target;
    public GameObject WP;
    public GameObject Player;

    Color[,] colorOfPixel;
    GameObject t;

    private int[,] worldMap = new int[,]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 4, 1, 0, 0, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 1, 0, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 2, 2, 0, 1, 1 },
        { 1, 0, 1, 1, 0, 1, 2, 2, 0, 1 },
        { 1, 0, 0, 0, 5, 0, 2, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 0, 1, 1, 1 },
        { 1, 4, 1, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 3, 1, 0, 0, 0, 0, 0, 4, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };

    void Start()
    {
        GenerateFromArray();
    }

    void GenerateFromArray()
    {
        print("Start Method");
        int i,
            j;
        for (i = 0; i < 10; i++)
        {
            for (j = 0; j < 10; j++)
            {
                GameObject t;
                if (worldMap[i, j] == 1)
                    t = (GameObject)(
                        Instantiate(
                            wall,
                            new Vector3(50 - i * 10, 1.5f, 50 - j * 10),
                            Quaternion.identity
                        )
                    );
                else if (worldMap[i, j] == 2)
                    t = (GameObject)(
                        Instantiate(
                            NPC,
                            new Vector3(50 - i * 10, 1.5f, 50 - j * 10),
                            Quaternion.identity
                        )
                    );
                else if (worldMap[i, j] == 3)
                {
                    t = (GameObject)(
                        Instantiate(
                            target,
                            new Vector3(50 - i * 10, 1.5f, 50 - j * 10),
                            Quaternion.identity
                        )
                    );
                    t.name = "teamLeader";
                }
                else if (worldMap[i, j] == 4)
                    t = (GameObject)(
                        Instantiate(
                            WP,
                            new Vector3(50 - i * 10, 1.5f, 50 - j * 10),
                            Quaternion.identity
                        )
                    );
                else if (worldMap[i, j] == 5)
                    t = (GameObject)(
                        Instantiate(
                            Player,
                            new Vector3(50 - i * 10, 1.5f, 50 - j * 10),
                            Quaternion.identity
                        )
                    );
            }
        }
    }
}
