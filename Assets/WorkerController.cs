using System.Collections;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    [Header("Locations")]
    [SerializeField] private Transform housePoint;
    [SerializeField] private Transform treePoint;

    [Header("References")]
    [SerializeField] private GameManager gameManager;

    [Header("Timings (seconds)")]
    [SerializeField] private float walkToTreeDuration = 5f;
    [SerializeField] private float chopDuration = 20f;
    [SerializeField] private float walkToHouseDuration = 5f;

    private void Start()
    {
        if (housePoint != null)
            transform.position = housePoint.position;

        StartCoroutine(WorkLoop());
    }

    private IEnumerator WorkLoop()
    {
        while (true)
        {
            // 5s: house → tree
            yield return MoveTo(treePoint.position, walkToTreeDuration);

            // 20s: chop at tree
            yield return ChopAtTree();

            // 5s: tree → house
            yield return MoveTo(housePoint.position, walkToHouseDuration);

            // Back at house (30s total) → +1 wood
            if (gameManager != null)
                ResourceManager.Instance.AddWood(1);
        }
    }

    private IEnumerator MoveTo(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(start, target, t);

            Vector3 dir = target - start;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
                transform.forward = dir.normalized;

            yield return null;
        }

        transform.position = target;
    }

    private IEnumerator ChopAtTree()
    {
        // Optional: trigger Animator "Chop" here
        yield return new WaitForSeconds(chopDuration);
        // Optional: stop chop animation here
    }
}