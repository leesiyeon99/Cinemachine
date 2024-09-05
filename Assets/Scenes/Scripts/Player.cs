using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; //플레이어 움직이는 속도
    [SerializeField] float rotateSpeed = 2f; //플레이어 회전속도
    [SerializeField] CinemachineVirtualCamera virtualCamera; //플레이어의 시점 카메라
    [SerializeField] GameObject zoomCamera; // 줌 카메라
    [SerializeField] GameObject miniMap; // 미니맵 게임 오브젝트

    [SerializeField] float xRotation = 0f; // 카메라 상하회전 각도
    [SerializeField] float yRotation = 0f; // 카메라 좌우회전 각도

    private void Start()
    {
        zoomCamera.gameObject.SetActive(false);
        miniMap.SetActive(false); //시작할 때 미니맵 비활성화
        Cursor.visible = false; //커서 화면에 안보이게
        Cursor.lockState = CursorLockMode.Locked; //커서 화면 중앙에 고정
    }

    private void Update()
    {
        MiniMap();
        Move();
        Zoom();
        Look();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * z + transform.right * x; //플레이어 움직이는 방향에 따라
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World); //moveSpeed만큼 이동
    }

    private void Look()
    {
        //플레이어가 마우스가 보는 좌우각도로 회전하도록
        yRotation += Input.GetAxis("Mouse X") * rotateSpeed; //마우스의 좌우 움직임에 따라 각도를 더해줌
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f); //플레이어가 마우스의 움직임 만큼 좌우로 회전

        // 마우스의 상하회전
        xRotation -= Input.GetAxis("Mouse Y") * rotateSpeed; //마우스의 상하 움직임에 따라 각도를 빼줌, 더해주면 상하좌우의 움직임이 반대로 보임
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); // 상하각도는 45도로 제한
        virtualCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //visualcamera는 상하로만 회전하도록 해줌, 좌우회전도 하게 되면 플레이어의 각도가 바뀌어서 기울어지거나 위로 날아감
    
        //플레이어쪽에서는 좌우각도만 다루고 virtualcamera에서는 상하각도만 다루도록 해야 좌우회전 할 때 플레이어가 같이 회전하고 상하회전 할 때 플레이어가 같이 회전 안하게 할 수 있음
    }

    private void MiniMap()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            miniMap.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            miniMap.SetActive(false);
        }
    }

    private void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            zoomCamera.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            zoomCamera.gameObject.SetActive(false);
        }
    }
}
