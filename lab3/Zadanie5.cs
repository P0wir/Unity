using UnityEngine;
using System.Collections.Generic;

public class Zadanie5 : MonoBehaviour
{
    public GameObject cubePrefab; 
    public int CubeNumber = 10; 
    public float planeSize = 10.0f; 

    private List<Vector3> usedPositions = new List<Vector3>(); 

    void Start()
    {
        GenerateCubes(); 
    }

    void GenerateCubes()
    {
        for (int i = 0; i < CubeNumber; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(cubePrefab, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        bool positionTaken;

        do
        {
            float randomX = Random.Range(-planeSize / 2, planeSize / 2);
            float randomZ = Random.Range(-planeSize / 2, planeSize / 2);
            randomPosition = new Vector3(randomX, 0.5f, randomZ);
            positionTaken = CheckPositionTaken(randomPosition); 
        } while (positionTaken); 

        usedPositions.Add(randomPosition);

        return randomPosition;
    }

    bool CheckPositionTaken(Vector3 position)
    {
        foreach (Vector3 usedPosition in usedPositions)
        {
            if (Vector3.Distance(usedPosition, position) < 1f)
            {
                return true;
            }
        }
        return false;
    }
}
