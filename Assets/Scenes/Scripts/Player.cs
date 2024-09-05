using Cinemachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; //�÷��̾� �����̴� �ӵ�
    [SerializeField] float rotateSpeed = 2f; //�÷��̾� ȸ���ӵ�
    [SerializeField] CinemachineVirtualCamera virtualCamera; //�÷��̾��� ���� ī�޶�
    [SerializeField] GameObject zoomCamera; // �� ī�޶�
    [SerializeField] GameObject miniMap; // �̴ϸ� ���� ������Ʈ

    [SerializeField] float xRotation = 0f; // ī�޶� ����ȸ�� ����
    [SerializeField] float yRotation = 0f; // ī�޶� �¿�ȸ�� ����

    private void Start()
    {
        zoomCamera.gameObject.SetActive(false);
        miniMap.SetActive(false); //������ �� �̴ϸ� ��Ȱ��ȭ
        Cursor.visible = false; //Ŀ�� ȭ�鿡 �Ⱥ��̰�
        Cursor.lockState = CursorLockMode.Locked; //Ŀ�� ȭ�� �߾ӿ� ����
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

        Vector3 moveDirection = transform.forward * z + transform.right * x; //�÷��̾� �����̴� ���⿡ ����
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World); //moveSpeed��ŭ �̵�
    }

    private void Look()
    {
        //�÷��̾ ���콺�� ���� �¿찢���� ȸ���ϵ���
        yRotation += Input.GetAxis("Mouse X") * rotateSpeed; //���콺�� �¿� �����ӿ� ���� ������ ������
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f); //�÷��̾ ���콺�� ������ ��ŭ �¿�� ȸ��

        // ���콺�� ����ȸ��
        xRotation -= Input.GetAxis("Mouse Y") * rotateSpeed; //���콺�� ���� �����ӿ� ���� ������ ����, �����ָ� �����¿��� �������� �ݴ�� ����
        xRotation = Mathf.Clamp(xRotation, -45f, 45f); // ���ϰ����� 45���� ����
        virtualCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //visualcamera�� ���Ϸθ� ȸ���ϵ��� ����, �¿�ȸ���� �ϰ� �Ǹ� �÷��̾��� ������ �ٲ� �������ų� ���� ���ư�
    
        //�÷��̾��ʿ����� �¿찢���� �ٷ�� virtualcamera������ ���ϰ����� �ٷ絵�� �ؾ� �¿�ȸ�� �� �� �÷��̾ ���� ȸ���ϰ� ����ȸ�� �� �� �÷��̾ ���� ȸ�� ���ϰ� �� �� ����
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
