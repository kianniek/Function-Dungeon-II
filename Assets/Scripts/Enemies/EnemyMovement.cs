using UnityEngine;
using WorldGrid;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private PathData pathData;
    [SerializeField] private int movementSpeed;

    private int currentTargetIndex = 1;

    private void Start()
    {
        transform.position = pathData.PathCoordinates[0];
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = pathData.PathCoordinates[currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, pathData.PathCoordinates[currentTargetIndex], movementSpeed * Time.deltaTime);
        Debug.Log(targetPosition);
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            if (currentTargetIndex != pathData.PathCoordinates.Count - 1)
            {
                currentTargetIndex = currentTargetIndex + 1;
            }
        }
    }

}
