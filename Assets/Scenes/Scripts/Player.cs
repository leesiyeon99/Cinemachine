using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    [SerializeField] GameObject miniMap;

    private void Start()
    {
        miniMap.gameObject.SetActive(false);
    }
    private void Update()
    {
        MiniMap();
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

    private void MiniMap()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("¹Ì´Ï¸Ê");
            miniMap.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            miniMap.gameObject.SetActive(false);
        }
    }
}
