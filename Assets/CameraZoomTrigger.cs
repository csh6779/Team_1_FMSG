using UnityEngine;
public class CameraZoomTrigger : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomedInSize = 3f;
    public float zoomedOutSize = 5f;
    public float zoomSpeed = 2f;

    private float targetSize;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        targetSize = mainCamera.orthographicSize; //orthographicSize = ���� ������ �ǹ��� 2D �信�� ���� ����
    }

    void Update()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            targetSize = zoomedInSize;  // ������ �۰�
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            targetSize = zoomedOutSize; // ������ �ٽ� ũ��
    }
}
