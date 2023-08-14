using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] ballPrefabs; // Drag the 4 ball GameObject prefabs here.
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
        string ballColor = currentBall.name.Replace("(Clone)", "").Trim(); // Gets the name of the prefab, removing the "(Clone)" suffix that Unity adds

        if (ballColor.StartsWith(platformColor))
        {
            // Correct color passed
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
            // Wrong color passed
            multiplier = 1;
            correctPasses = 0;
        }

        ChangeBallColor();
    }

    void ChangeBallColor()
    {
        int randomIndex = Random.Range(0, ballPrefabs.Length);

        // Add a slight vertical offset to ensure the new ball is instantiated above the platform
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
