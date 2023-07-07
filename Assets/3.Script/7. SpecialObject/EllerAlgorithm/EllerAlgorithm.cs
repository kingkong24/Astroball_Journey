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
    [Header("�̷�")]
    [SerializeField] GameObject gameObject_field;
    [SerializeField] GameObject gameObject_wallPrefebs;
    [Tooltip("�׻� Ȧ������ ���� ��")]
    [SerializeField] int Number_oneSideGrid;
    [SerializeField] Vector3 scale_wall;
    [Range(0, 1)]
    [SerializeField] float probability_wallSpawn;

    [Header("Ȯ�ο�")]
    public Maze_grid[] grids;
    [SerializeField] GameObject[] walls_forword;
    [SerializeField] GameObject[] walls_right;

    List<Maze_grid> maze_GridList;
    int curNumber;

    private void Awake()
    {
        if (Number_oneSideGrid % 2 == 0)
        {
            Debug.Log("�ݵ�� Ȧ������ �־��ּ���.");
            return;
        }
        Initialize();
        GenerateMaze();
    }

    /// <summary>
    /// ������ �ʱ�ȭ�մϴ�.
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
    /// �̷θ� ����ϴ�. Number_oneSideGrid + 1 * Number_oneSideGrid���� �� �迭 ����.
    /// </summary>
    private void GenerateMaze()
    {
        // ��� ���� �����մϴ�.
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

        for (int i = 0; i < Number_oneSideGrid - 1; i++) // ������ �ѹ��� ���� (�ϼ��Ǹ� Number_oneSideGrid - 1 ��������)
        {
            // �ش� ���� ������ �� ��Ȱ��ȭ�ϰ�, �� �� �׸����� number ���� �����
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

            // �ش� ���� ���� ���տ� ���� ĭ�� �� ��� �ϳ��� ���� ��θ� �����, ���� ���տ� �ֱ�.
            for (int j = 0; j < Number_oneSideGrid; j++)
            {
                // 0��° number�� ����
                if (j == 0)
                {
                    curNumber = grids[i * Number_oneSideGrid + j].number;
                    maze_GridList.Add(grids[i * Number_oneSideGrid + j]);
                    continue;
                }

                if (grids[i * Number_oneSideGrid + j].number == curNumber) // ���� ����
                {
                    maze_GridList.Add(grids[i * Number_oneSideGrid + j]);
                }
                else // �ٸ� ����
                {
                    // ����Ʈ���� �ϳ� �̻��� �׸��带 �̱�
                    int num_random = Random.Range(1, maze_GridList.Count + 1);
                    List<Maze_grid> selectedGrids = new();
                    for (int k = 0; k < num_random; k++)
                    {
                        int randomIndex = Random.Range(0, maze_GridList.Count);
                        selectedGrids.Add(maze_GridList[randomIndex]);
                    }
                    // ���� �׸����� ���� ���� ��Ȱ��ȭ �� ���� �׸��� ����� ���� �����.
                    for (int k = 0; k < selectedGrids.Count; k++)
                    {
                        walls_forword[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].SetActive(false);
                        grids[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].number = curNumber;
                    }
                    maze_GridList.Clear();

                    // �ٸ� �׸��� ����
                    maze_GridList.Add(grids[i * (Number_oneSideGrid) + j]);
                    curNumber = grids[i * (Number_oneSideGrid) + j].number;
                }

                if (j == Number_oneSideGrid - 1) // ������ �׸���
                {
                    // ����Ʈ���� �ϳ� �̻��� �׸��带 �̱�
                    int num_random = Random.Range(1, maze_GridList.Count + 1);
                    List<Maze_grid> selectedGrids = new();
                    for (int k = 0; k < num_random; k++)
                    {
                        int randomIndex = Random.Range(0, maze_GridList.Count);
                        selectedGrids.Add(maze_GridList[randomIndex]);
                    }

                    // ���� �׸����� ���� ���� ��Ȱ��ȭ �� ���� �׸��� ����� ���� �����.
                    for (int k = 0; k < selectedGrids.Count; k++)
                    {
                        walls_forword[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].SetActive(false);
                        grids[selectedGrids[k].grid_x + (selectedGrids[k].grid_z + 1) * Number_oneSideGrid].number = curNumber;
                    }

                    maze_GridList.Clear();
                }
            }
        }

        // ������ ���� �����մϴ�.
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
    /// ���� ��ġ�� �����մϴ�.
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