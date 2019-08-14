using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField]
    int moveSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }
    }
}