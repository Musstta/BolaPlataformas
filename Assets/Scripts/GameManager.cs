using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ballPrefabs; // Los prefabs de colores para la bola
    private GameObject currentBall;

    public int score = 0;
    public int multiplier = 1;
    public int correctPasses = 0;

    private void Start()
    {
        currentBall = GameObject.FindGameObjectWithTag("ColorChanger");
    }

    public void BallPassedPlatform(string platformColor)
    {
        string ballColor = currentBall.name.Replace("(Clone)", "").Trim(); // Elimina el "(Clone)" del nombre del objeto nuevo que Unity crea al cambiar el color de la bola

        if (ballColor.StartsWith(platformColor))
        {
            // color correcto
            score += multiplier;
            correctPasses++;

            if (correctPasses == 5)
            {
                multiplier++;
                correctPasses = 0;
            }
        }
        else
        {
            // color erroneo
            multiplier = 1;
            correctPasses = 0;
        }

        ChangeBallColor();
    }

    void ChangeBallColor()
    {
        int randomIndex = Random.Range(0, ballPrefabs.Length);

        // Spawnea el color nuevo por encima de la plataforma siguiente
        Vector3 currentBallPosition = currentBall.transform.position + new Vector3(0, 0.25f, 0);
        Quaternion currentBallRotation = currentBall.transform.rotation;

        Destroy(currentBall);
        currentBall = Instantiate(ballPrefabs[randomIndex], currentBallPosition, currentBallRotation);
        currentBall.tag = "ColorChanger";

        CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
        if (cameraFollow != null)
        {
            cameraFollow.SetBallTransform(currentBall.transform);
        }
    }
}
