using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * z * moveSpeed * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.up, x * rotateSpeed * Time.deltaTime);
        }
    }
}
