using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform truck;
    private float leftLimit = -3f;
    private float offset = 0f;
    private Transform activeBuildingBox;
    private Camera cam;
    private float lerpSpeed = 5f;
    private float targetSize;
    private bool shouldResize = false;
    private float originalBottomEdge;

    void Start()
    {
        if (truck == null)
        {
            truck = GameObject.FindGameObjectWithTag("Truck").transform;
        }

        activeBuildingBox = GameObject.FindGameObjectWithTag("ActiveBuildingBox").transform;
        cam = GetComponent<Camera>();
        targetSize = cam.orthographicSize;
        originalBottomEdge = cam.transform.position.y - cam.orthographicSize;
    }

    void Update()
    {
        if (activeBuildingBox.tag != "ActiveBuildingBox")
        {
            activeBuildingBox = GameObject.FindGameObjectWithTag("ActiveBuildingBox").transform;
            targetSize += 2;
            shouldResize = true;
        }

        if (shouldResize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * lerpSpeed);

            float newY = originalBottomEdge + cam.orthographicSize;
            Vector3 newCamPos = new Vector3(cam.transform.position.x, newY+ 0.3f, cam.transform.position.z);
            cam.transform.position = Vector3.Lerp(cam.transform.position, newCamPos, Time.deltaTime * lerpSpeed);

            if (Mathf.Abs(cam.orthographicSize - targetSize) < 0.01f)
            {
                shouldResize = false;
                originalBottomEdge = cam.transform.position.y - cam.orthographicSize;
            }
        }

        float rightLimit = activeBuildingBox.position.x;
        float newX = Mathf.Clamp(truck.position.x, leftLimit, rightLimit);
        Vector3 targetCameraPosition = new Vector3(newX, transform.position.y + offset, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, Time.deltaTime * lerpSpeed);
    }
}
