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

        targetSize = mainCamera.orthographicSize; //orthographicSize = 직교 투영을 의미함 2D 뷰에서 자주 사용됨
    }

    void Update()
    {
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            targetSize = zoomedInSize;  // 들어오면 작게
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            targetSize = zoomedOutSize; // 나가면 다시 크게
    }
}
