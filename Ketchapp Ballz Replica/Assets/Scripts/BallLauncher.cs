using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [SerializeField]
    private Transform ballContainer;
    [SerializeField]
    private Ball ballPrefab;
    [SerializeField]
    private TextMeshPro ballsCount;

    private Vector3 worldPosition;
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;

    private LaunchPreview launchPreview;

    private List<Ball> balls = new List<Ball>();
    private int ballsReady;

    private BlockSpawner blockSpawner;

    private bool readyToLaunch = true;

    private void Start()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        launchPreview = GetComponent<LaunchPreview>();
        CreateBall();
    }

    private void CreateBall()
    {
        Ball ball = Instantiate(ballPrefab);
        ball.gameObject.SetActive(false);
        balls.Add(ball);
        ballsReady++;
        ballsCount.text = ballsReady.ToString();
    }

    void Update()
    {
        // getting world position and adding -10 to set a distance from the camera
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;

        if (Input.GetMouseButtonDown(0) && readyToLaunch)
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0) && readyToLaunch)
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 _direction = endDragPosition - startDragPosition;
        _direction.Normalize();

        if (Mathf.Abs(_direction.x) > 0.1f || Mathf.Abs(_direction.y) > 0.1f)
        {
            foreach (var ball in balls)
            {
                if (ball != null)
                {
                    ball.gameObject.SetActive(true);
                    ball.transform.position = transform.position;
                    ball.transform.SetParent(ballContainer);
                    ball.GetComponent<Rigidbody2D>().AddForce(-_direction);

                    yield return new WaitForSeconds(0.1f);
                }
            }
            ballsReady = 0;
            readyToLaunch = false;
            launchPreview.SetStartPoint(transform.position);
            launchPreview.SetEndPoint(transform.position);
        }
    }

    private void EndDrag()
    {
        if (readyToLaunch)
            StartCoroutine(LaunchBalls());
    }

    private void ContinueDrag(Vector3 _worldPosition)
    {
        endDragPosition = _worldPosition;

        Vector3 _direction = endDragPosition - startDragPosition;
        launchPreview.SetEndPoint(transform.position - _direction);
    }

    private void StartDrag(Vector3 _worldPosition)
    {
        startDragPosition = _worldPosition;
        launchPreview.SetStartPoint(transform.position);
    }


    internal void ReturnBall()
    {
        ballsReady++;
        if (ballsReady == balls.Count)
        {
            print("ReturnBall");
            blockSpawner.SpawnRowOfBlocks();
            CreateBall();
            readyToLaunch = true;
        }
    }
}
