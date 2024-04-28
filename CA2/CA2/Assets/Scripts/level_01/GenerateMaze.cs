using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeRandom : MonoBehaviour
{
	const int N = 1, S = 2, E = 3, W = 4;
	int[,] grid;
	[SerializeField]
	[Range(5, 100)]
	int width, heigth;

	[SerializeField]
	[Range(5, 100)]
	int wallSize;


	public GameObject verticalWall, horizontalWall;

	GameObject[,] gridObjectsH, gridObjectsV;
	GameObject[] allObjectsInScene;

	float wallHeight;

	public GameObject target, npc; 

    // Start is called before the first frame update
    void Start()
    {
            Init();
            GenerateMazeBinary();
    		DisplayGrid();
    		AddTarget();
    		AddNPC();


    }

    private void Init()
	{
	    heigth = width;
	    wallHeight = 4;
	    verticalWall.transform.localScale = new Vector3(.1f, wallHeight, wallSize);
	    horizontalWall.transform.localScale = new Vector3(wallSize, wallHeight, .1f);

	    grid = new int[width, heigth];
		gridObjectsV = new GameObject[width + 1, heigth + 1];
		gridObjectsH = new GameObject[width + 1, heigth + 1];
		drawFullgrid();

		GameObject.Find("ground").transform.localScale = new Vector3((width + 1) * wallSize, 1, (heigth + 1) * wallSize);
		//GameObject.Find("ceiling").transform.localScale = new Vector3((width + 1) * wallSize, 1, (heigth + 1) * wallSize);
		//GameObject.Find("ceiling").transform.position = new Vector3(GameObject.Find("ceiling").transform.position.x, wallSize-1, GameObject.Find("ceiling").transform.position.z);


	}
	void drawFullgrid()
	{
		print("Drawing the grid with height ="+heigth+"width="+width);
	    for (int i = 0; i <= heigth; i++)
	    {
	        for (int j = 0; j <= width; j++)
	        {       
	        	if (i < heigth)
				{
				    float vWallSize = verticalWall.transform.localScale.z;
				    float xOffset, zOffset;//so that maze centered around (0,0)
				    xOffset = -(width * vWallSize) / 2;
				    zOffset = -(heigth * vWallSize) / 2;

				    gridObjectsV[j, i] = (GameObject)(Instantiate(verticalWall, new Vector3(-vWallSize / 2 + j * vWallSize + xOffset, wallSize/2, i * vWallSize + zOffset), Quaternion.identity));

				    gridObjectsV[j, i].active = true;
				    gridObjectsV[j, i].name = "v" + i + j;
				    gridObjectsV[j, i].tag = "wall";
				}
				if (j < width)
				{
				    float hWallSize = horizontalWall.transform.localScale.x;
				    float xOffset, zOffset;
				    xOffset = -(width * hWallSize) / 2;
				    zOffset = -(heigth * hWallSize) / 2;
				    gridObjectsH[j, i] = (GameObject)(Instantiate(horizontalWall, new Vector3(j * hWallSize + xOffset, wallSize/2, -(hWallSize / 2) + i * hWallSize + zOffset), Quaternion.identity));//PF 

				    gridObjectsH[j, i].active = true;
				    gridObjectsH[j, i].name = "h" + i + j;
				    gridObjectsH[j, i].tag = "wall";
				}


	        }
	    }

	}
	void GenerateMazeBinary()
	{
		for (int row = 0; row < heigth; row++)
		{
			for (int cell = 0; cell < width; cell++)
			{
					float randomNumber = Random.Range(0, 100);
					int carvingDirection;
					if (randomNumber > 30) carvingDirection = N; else carvingDirection = E;
					if (cell == width - 1)
					{
					    if (row < heigth - 1) carvingDirection = N; else carvingDirection = W;
					}
					else if (row == heigth - 1)
					{
					    if (cell < width - 1) carvingDirection = E;
					    else carvingDirection = -1;
					}
					grid[cell, row] = carvingDirection;

			}

		}
	}
	void DisplayGrid()
	{
	    for (int row = 0; row < heigth; row++)
	    {
	        for (int cell = 0; cell < width; cell++)
	        {
	            if (grid[cell, row] == N) gridObjectsH[cell, row + 1].active = false;
	            if (grid[cell, row] == E) gridObjectsV[cell + 1, row].active = false;
	        }
	    }

	}
	void AddNPC()
	{
	    float xOffset = -(width * wallSize) / 2;
	    float zOffset = -(heigth * wallSize) / 2;
	    GameObject p = (GameObject)(Instantiate(npc, new Vector3(xOffset, 1.0f, zOffset), Quaternion.identity));
	    p.name = "NPC"; 
	}
	void AddTarget()
	{
	    float xOffset = -(width * wallSize) / 2;
	    float zOffset = -(heigth * wallSize) / 2;
	    GameObject p = (GameObject)(Instantiate(npc, new Vector3(xOffset, 1.0f, zOffset), Quaternion.identity));
	    p.name = "target"; 
	}


}
