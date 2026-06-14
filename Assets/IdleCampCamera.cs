using UnityEngine;

[RequireComponent(typeof(Camera))]
public class IdleCampCamera : MonoBehaviour
{
    [Header("Frame these two points")]
    [SerializeField] private Transform housePoint;
    [SerializeField] private Transform treePoint;

    [Header("OreCraft-style view")]
    [SerializeField] private float height = 14f; // 12-18
    [SerializeField] private float distance = 12f; // 10-14
    [SerializeField] private float pitchDown = 42f; // 40-45
    [SerializeField] private float yaw = 45f; // 45 (corner view)

    [Header("Orthographic (recommended for idle)")]
    [SerializeField] private bool useOrthographic = true;
    [SerializeField] private float orthographicSize = 8f; //7-9

    private void Start()
    {
        if (housePoint == null || treePoint == null)
            return;

        Vector3 center = (housePoint.position + treePoint.position) * 0.5f;

        Quaternion rotation = Quaternion.Euler(pitchDown, yaw, 0f);
        Vector3 offset = rotation * Vector3.back * distance;
        offset.y = height;

        transform.position = center + offset;
        transform.rotation = rotation;

        Camera cam = GetComponent<Camera>();
        cam.orthographic = useOrthographic;
        if (useOrthographic)
            cam.orthographicSize = orthographicSize;
    }
}