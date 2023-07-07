using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitePotentialWell2D : MonoBehaviour
{
    [Header("������Ʈ")]
    [SerializeField] Transform[] transforms_target;

    [Header("������Ʈ�� ������ �� �ִ� ����")]
    [SerializeField] Vector2 minPosition;
    [SerializeField] Vector2 maxPosition;

    [Header("�̷�")]
    [SerializeField] MazeEllerAlgorithm mazeEllerAlgorithm;

    [Header("Ȯ�ο�")]
    [SerializeField] Vector2[] grids_maze;
    [SerializeField] float[] probability_grids;
    [SerializeField] float Lx;
    [SerializeField] float Lz;
    [SerializeField] int nx;
    [SerializeField] int nz;
    [SerializeField] int targetCount;

    #region �ʱ�ȭ
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// class�� �ʱ�ȭ�Ѵ�.
    /// </summary>
    void Initialize()
    {
        if (maxPosition == Vector2.zero && minPosition == Vector2.zero)
        {
            minPosition = new(-mazeEllerAlgorithm.scale_wall.x * (mazeEllerAlgorithm.Number_oneSideGrid / 2 + 0.5f), -mazeEllerAlgorithm.scale_wall.x * (mazeEllerAlgorithm.Number_oneSideGrid / 2 + 0.5f));
            maxPosition = new(mazeEllerAlgorithm.scale_wall.x * (mazeEllerAlgorithm.Number_oneSideGrid / 2 + 0.5f), mazeEllerAlgorithm.scale_wall.x * (mazeEllerAlgorithm.Number_oneSideGrid / 2 + 0.5f));

        }
        Lx = maxPosition.x - minPosition.x;
        Lz = maxPosition.y - minPosition.y;
        nx = 1;
        nz = 1;
        targetCount = 0;
    }

    #endregion



    #region target�� ��ġ�� ���մϴ�.

    public void NextTargetPosition()
    {
        if (grids_maze.Length == 0)
        {
            GetMazeGridsOnce();
        }

        float TotalProbability = 0;
        probability_grids = new float[grids_maze.Length];
        
        for (int i = 0; i < grids_maze.Length; i++)
        {
            float probability = CalculateProbability(grids_maze[i].x, grids_maze[i].y);
            Debug.Log(i + "��° Ȯ�� : " + probability);
            TotalProbability += probability;
            probability_grids[i] = TotalProbability;
        }

        float selected = Random.Range(0, TotalProbability);

        for (int i = 0; i < grids_maze.Length; i++)
        {
            if (selected < probability_grids[i])
            {
                transforms_target[targetCount].position = new(grids_maze[i].x, transforms_target[targetCount].position.y, grids_maze[i].y);
                targetCount++;
                NextQuantumNumber();
                return;
            }
        }
    }

    /// <summary>
    /// �׸��带 �����ɴϴ�.
    /// </summary>
    void GetMazeGridsOnce()
    {
        grids_maze = new Vector2[mazeEllerAlgorithm.grids.Length];

        for (int i = 0; i < mazeEllerAlgorithm.grids.Length; i++)
        {
            grids_maze[i] = new((mazeEllerAlgorithm.grids[i].grid_x - (mazeEllerAlgorithm.Number_oneSideGrid - 1) * 0.5f) * mazeEllerAlgorithm.scale_wall.x,
                (mazeEllerAlgorithm.grids[i].grid_z - (mazeEllerAlgorithm.Number_oneSideGrid - 1) * 0.5f) * mazeEllerAlgorithm.scale_wall.x);
        }
    }

    /// <summary>
    /// �� ��ġ�� ������ Ȯ���� �����մϴ�.
    /// </summary>
    /// <param name="x"> ��ġ�� x ��ǥ </param>
    /// <param name="z"> ��ġ�� y ��ǥ </param>
    /// <returns></returns>
    float CalculateProbability(float x, float z)
    {
        float psi = Mathf.Sqrt(4f / (Lx * Lz)) * Mathf.Sin(nx * Mathf.PI * (x + mazeEllerAlgorithm.Number_oneSideGrid * 0.5f * mazeEllerAlgorithm.scale_wall.x) / Lx) * Mathf.Sin(nz * Mathf.PI * (z + mazeEllerAlgorithm.Number_oneSideGrid * 0.5f * mazeEllerAlgorithm.scale_wall.x) / Lz);
        return psi * psi;
    }

    /// <summary>
    /// ���� ������ ������ ���ڼ��� �����մϴ�.
    /// </summary>
    void NextQuantumNumber()
    {
        float term1 = Mathf.Pow((nx + 1) / Lx, 2) + Mathf.Pow(nz / Lz, 2);
        float term2 = Mathf.Pow(nx / Lx, 2) + Mathf.Pow((nz + 1) / Lz, 2);

        if (term1 < term2)
        {
            nx++;
        }
        else
        {
            nz++;
        }
    }
    #endregion
}
