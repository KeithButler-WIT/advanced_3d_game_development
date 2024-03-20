// next move NPC towards tager, after adding both procedurally
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateMaze : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wall;
    public GameObject smallerWall;
    public GameObject smallWater;
    public GameObject tree;
    public GameObject NPC;
    public GameObject target;
    Color[,] colorOfPixel;
    public Texture2D outlineImage;
    GameObject t;

    private int[,] worldMap = new int[,]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 0, 0, 1 },
        { 1, 0, 1, 0, 0, 2, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 1, 1, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 0, 1, 0, 1, 1, 1, 1 },
        { 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
        { 1, 3, 1, 0, 0, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
    };

    void Start()
    {
        GenerateFromArray();
        //GenerateFromFile();
        //GenerateFromImage();
    }

    void GenerateFromFile()
    {
        TextAsset t1 = (TextAsset)Resources.Load("maze", typeof(TextAsset));
        string s = t1.text;
        int i;
        s = s.Replace(System.Environment.NewLine, "");
        for (i = 0; i < s.Length; i++)
        {
            int column,
                row;
            column = i % 10;
            row = i / 10;
            if (s[i] == '1')
            {
                t = (GameObject)(
                    Instantiate(
                        wall,
                        new Vector3(50 - column * 10, 1.5f, 50 - row * 10),
                        Quaternion.identity
                    )
                );
            }
            else if (s[i] == '2')
                t = (GameObject)(
                    Instantiate(
                        NPC,
                        new Vector3(50 - column * 10, 1.5f, 50 - row * 10),
                        Quaternion.identity
                    )
                );
            else if (s[i] == '3')
            {
                t = (GameObject)(
                    Instantiate(
                        target,
                        new Vector3(50 - column * 10, 1.5f, 50 - row * 10),
                        Quaternion.identity
                    )
                );
                t.name = "target";
            }
        }
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
                    t.name = "target";
                }
            }
        }
    }
}
