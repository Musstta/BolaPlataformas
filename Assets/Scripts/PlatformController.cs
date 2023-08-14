using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject[] platforms; // Las plataformas de colores que se spawnearan
    public float spawnYDistance = 2.0f;  // distancia vertical entre plataformas
    public float laneWidth = 2.0f;  // tamano horizontal de las "lineas" de plataformas
    private Vector3 nextSpawnPosition = Vector3.zero;

    void Update()
    {
        // checkea la distancia de la camara al prozimo punto de spawneo para las plataformas
        if (Camera.main.WorldToViewportPoint(nextSpawnPosition).y < 0.75f)
        {
            SpawnPlatformRow();
        }
    }

    void SpawnPlatformRow()
    {
        List<int> indices = new List<int> { 0, 1, 2, 3 };  // indices para los colores de plataforma
        Shuffle(indices);

        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnPosition = new Vector3(-1.5f * laneWidth + i * laneWidth, nextSpawnPosition.y, 0);  // posicion de spawneo de la plataforma
            Instantiate(platforms[indices[i]], spawnPosition, Quaternion.identity);
        }

        nextSpawnPosition += new Vector3(0, spawnYDistance, 0);
    }

    // color aleatorio para spawn de plataforma
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
