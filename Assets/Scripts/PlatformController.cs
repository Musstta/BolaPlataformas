using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] platforms; // Drag the 4 colored platforms here.
    public float spawnYDistance = 2.0f;  // The vertical distance between each row
    public float laneWidth = 2.0f;  // The width of each platform lane
    private Vector3 nextSpawnPosition = Vector3.zero;

    void Update()
    {
        // Check if the camera's view point is approaching the next spawn position
        if (Camera.main.WorldToViewportPoint(nextSpawnPosition).y < 0.75f)
        {
            SpawnPlatformRow();
        }
    }

    void SpawnPlatformRow()
    {
        List<int> indices = new List<int> { 0, 1, 2, 3 };  // indices for our 4 platform colors
        Shuffle(indices);

        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = new Vector3(-1.5f * laneWidth + i * laneWidth, nextSpawnPosition.y, 0);  // Adjust for 4 lanes
            Instantiate(platforms[indices[i]], spawnPosition, Quaternion.identity);
        }

        nextSpawnPosition += new Vector3(0, spawnYDistance, 0);
    }

    // Shuffle using Fisher-Yates algorithm
    void Shuffle(List<int> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            int tmp = list[i];
            list[i] = list[r];
            list[r] = tmp;
        }
    }
}
