using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Maze_grid
{
    public int grid_x;
    public int grid_z;
    public int number;

    public Maze_grid(int grid_x, int grid_z, int number)
    {
        this.grid_x = grid_x;
        this.grid_z = grid_z;
        this.number = number;
    }
}

public class EllerAlgorithm : MonoBehaviour
{
    [Header("미로")]
    [SerializeField] GameObject gameObject_field;
    [SerializeField] GameObject gameObject_wallPrefebs;
    [Tooltip("항상 홀수개만 넣을 것")]
    [SerializeField] int Number_oneSideGrid;
    [SerializeField] Vector3 scale_wall;
    [Range(0, 1)]
    [SerializeField] float probability_wallSpawn;

    [Header("확인용")]
    public Maze_grid[] grids;
    [SerializeField] GameObject[] walls_forword;
    [SerializeField] GameObject[] walls_right;

    List<Maze_grid> maze_GridList;
    int curNumber;

    private void Awake()
    {
        if (Number_oneSideGrid % 2 == 0)
        {
            Debug.Log("반드시 홀수개만 넣어주세요.");
            return;
        }
        Initialize();
        GenerateMaze();
    }

    /// <summary>
    /// 변수를 초기화합니다.
    /// </summary>
    void Initialize()
    {
        gameObject_field.transform.localPosition = new Vector3(0, -0.5f - scale_wall.y * 0.5f, 0);
        gameObject_field.transform.localScale = new Vector3(Number_oneSideGrid * scale_wall.x, 1f, Number_oneSideGrid * scale_wall.x);

        grids = new Maze_grid[Number_oneSideGrid * Number_oneSideGrid];
        for (int i = 0; i < Number_oneSideGrid; i++)
        {
            for (int j = 0; j < Number_oneSideGrid; j++)
            {
                grids[i * Number_oneSideGrid + j].grid_x = j;
                grids[i * Number_oneSideGrid + j].grid_z = i;
                grids[i * Number_oneSideGrid + j].number = i * Number_oneSideGrid + j;
            }
        }

        walls_forword = new GameObject[(Number_oneSideGrid + 1) * Number_oneSideGrid];
        walls_right = new GameObject[(Number_oneSideGrid + 1) * Number_oneSideGrid];

    }

    /// <summary>
    /// 미로를 만듭니다. Number_oneSideGrid + 1 * Number_oneSideGrid개의 벽 배열 생성.
    /// </summary>
    private void GenerateMaze()
    {
        // 모든 벽을 생성합니다.
        for (int i = 0; i <= Number_oneSideGrid; i++)
        {
            for (int j = 0; j < Number_oneSideGrid; j++)
            {
                GameObject wall = Instantiate(gameObject_wallPrefebs, gameObject.transform);
                CreateWall(j, i, 0, wall);
                walls_forword[i * Number_oneSideGrid + j] = wall;
            }
        }

        for (int i = 0; i < Number_oneSideGrid; i++)
        {
            for (int j = 0; j <= Number_oneSideGrid; j++)
            {
                GameObject wall = Instantiate(gameObject_wallPrefebs, gameObject.transform);
                CreateWall(j, i, 1, wall);
                walls_right[i * (Number_oneSideGrid + 1) + j] = wall;
            }
        }

        for (int i = 0; i < Number_oneSideGrid - 1; i++) // 열마다 한번씩 수행 (완성되면 Number_oneSideGrid - 1 넣으세요)
        {
            // 해당 열의 임의의 벽 비활성화하고, 양 옆 그리드의 number 같게 만들기
            for (int j = 1; j < Number_oneSideGrid; j++)
            {
                if (Random.Range(0f, 1.0f) >= probability_wallSpawn)
                {
                    walls_right[i * (Number_oneSideGrid + 1) + j].SetActive(false);
                    int smallerNumber = Mathf.Min(grids[i * (Number_oneSideGrid) + j - 1].number, grids[i * (Number_oneSideGrid) + j].number);
                    grids[i * (Number_oneSideGrid) + j - 1].number = smallerNumber;
                    grids[i * (Number_oneSideGrid) + j].number = smallerNumber;
                }
            }

            maze_GridList = new List<Maze_grid>();

            // 해당 열의 같은 집합에 속한 칸들 중 적어도 하나에 수직 경로를 만들고, 같은 집합에 넣기.
            for (int j = 0; j < Number_oneSideGrid; j++)
            {
                // 0번째 number를 저장
                if (j == 0)
                {
                    curNumber = grids[i * Number_oneSideGrid + j].number;
                    maze_GridList.Add(grids[i * Number_oneSideGrid + j]);
                    continue;
                }

                if (grids[i * Number_oneSideGrid + j].number == curNumber) // 같은 집합
                {
                    maze_GridList.Add(grids[i * Number_oneSideGrid + j]);
                }
                else // 다른 집합
                {
                    // 리스트에서 하나 이상의 그리드를 뽑기
                    int num_random = Random.Range(1, maze_GridList.Count + 1);
                    List<Maze_grid> selectedGrids = new();
                    for (int k = 0; k < num_random; k++)
                    {
                        int randomIndex = Random.Range(0, maze_GridList.Count);
                        selectedGrids.Add(maze_GridList[randomIndex]);
                    }
                    // 뽑힌 그리드의 위쪽 벽을 비활성화 및 위쪽 그리드 밸류를 같게 만들기.
                    for (int k = 0; k < selectedGrids.Count; k++)
                    {
                        walls_forword[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].SetActive(false);
                        grids[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].number = curNumber;
                    }
                    maze_GridList.Clear();

                    // 다른 그리드 저장
                    maze_GridList.Add(grids[i * (Number_oneSideGrid) + j]);
                    curNumber = grids[i * (Number_oneSideGrid) + j].number;
                }

                if (j == Number_oneSideGrid - 1) // 마지막 그리드
                {
                    // 리스트에서 하나 이상의 그리드를 뽑기
                    int num_random = Random.Range(1, maze_GridList.Count + 1);
                    List<Maze_grid> selectedGrids = new();
                    for (int k = 0; k < num_random; k++)
                    {
                        int randomIndex = Random.Range(0, maze_GridList.Count);
                        selectedGrids.Add(maze_GridList[randomIndex]);
                    }

                    // 뽑힌 그리드의 위쪽 벽을 비활성화 및 위쪽 그리드 밸류를 같게 만들기.
                    for (int k = 0; k < selectedGrids.Count; k++)
                    {
                        walls_forword[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].SetActive(false);
                        grids[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].number = curNumber;
                    }

                    maze_GridList.Clear();
                }
            }
        }

        // 마지막 행을 정리합니다.
        for (int i = 0; i < Number_oneSideGrid - 1; i++)
        {
            if (grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i].number != grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i + 1].number)
            {
                walls_right[(Number_oneSideGrid + 1) * (Number_oneSideGrid - 1) + i + 1].SetActive(false);
                int smallerNumber = Mathf.Min(grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i].number, grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i + 1].number);
                grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i].number = smallerNumber;
                grids[Number_oneSideGrid * (Number_oneSideGrid - 1) + i + 1].number = smallerNumber;
            }
        }
    }

    /// <summary>
    /// 벽을 위치에 생성합니다.
    /// </summary>
    /// <param name="x"> grid_x </param>
    /// <param name="z"> grid_z </param>
    /// <param name="dir"> 0: forward, 1: right </param>
    private void CreateWall(int x, int z, int dir, GameObject wall)
    {
        float posX = (x - (Number_oneSideGrid - 1) * 0.5f) * scale_wall.x;
        float posZ = (z - (Number_oneSideGrid - 1) * 0.5f) * scale_wall.x;

        if (dir == 0)
        {
            posZ -= scale_wall.x * 0.5f;
        }
        else if (dir == 1)
        {
            posX -= scale_wall.x * 0.5f;
        }

        wall.transform.localPosition = new Vector3(posX, 0, posZ);

        Quaternion rotation = Quaternion.Euler(0f, dir * 90f, 0f);
        wall.transform.localRotation = rotation;
        wall.transform.localScale = scale_wall;
    }
}