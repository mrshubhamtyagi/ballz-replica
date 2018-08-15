using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    private new Rigidbody2D rigidbody2D;

    BallLauncher ballLauncher;

    void Start()
    {
        ballLauncher = FindObjectOfType<BallLauncher>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        ballLauncher.ReturnBall();
        gameObject.SetActive(false);
    }
}
