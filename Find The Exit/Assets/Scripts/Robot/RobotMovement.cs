using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    private Vector3 currentDirection = Vector3.forward;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private Vector3 originalRotation;
    private Vector3 targetRotation;

    private bool isMoving = false;
    [SerializeField] public float timeToMove = 1.0f;
    [SerializeField] private float stepSize = 4.0f;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask floorLayerMask;

    private void Update()
    {
        Vector3 targetDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
            targetDirection = Vector3.back;
        if (Input.GetKey(KeyCode.RightArrow))
            targetDirection = Vector3.forward;
        if (Input.GetKey(KeyCode.UpArrow))
            targetDirection = Vector3.left;
        if (Input.GetKey(KeyCode.DownArrow))
            targetDirection = Vector3.right;

        if (targetDirection != Vector3.zero && !isMoving)
        {
            if (targetDirection == currentDirection)
            {
                if (CheckNextPosition())
                    StartCoroutine(Move(currentDirection));
            }
            else
            {
                StartCoroutine(Rotate(Vector3.SignedAngle(currentDirection, targetDirection, Vector3.up)));
                currentDirection = targetDirection;
            }
        }
        
    }

    private bool CheckNextPosition()
    {
        Collider[] wallColliders = Physics.OverlapBox(gameObject.transform.position + currentDirection* stepSize + Vector3.up * 2 , new Vector3(1, 1, 1), Quaternion.identity, wallLayerMask);
        Collider[] floorColliders = Physics.OverlapBox(gameObject.transform.position + currentDirection* stepSize + Vector3.down / 2 , new Vector3(1, 1, 1), Quaternion.identity, floorLayerMask);
        return wallColliders.Length == 0 && floorColliders.Length > 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(gameObject.transform.position + currentDirection * stepSize + Vector3.up * 2, new Vector3(1, 1, 1));
        Gizmos.DrawWireCube(gameObject.transform.position + currentDirection * stepSize + Vector3.down/2, new Vector3(1, 1, 1));
    }

    private IEnumerator Rotate(float angle)
    {
        isMoving = true;

        float elapsedTime = 0;
        originalRotation = transform.localEulerAngles;
        targetRotation = transform.localEulerAngles + new Vector3(0, angle, 0);

        float timeToRotate = timeToMove;
        if (Mathf.Abs(angle) == 180)
            timeToRotate *= 2;
        GetComponent<RobotTracksController>().direction = RobotTracksController.Direction.Right;

        while (elapsedTime < timeToRotate)
        {
            transform.eulerAngles = Vector3.Lerp(originalRotation, targetRotation, (elapsedTime / timeToRotate));
            GetComponent<RobotTracksController>().rotation += (((2*Mathf.PI*1.810792f)/360)* angle / timeToRotate) * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = targetRotation;
        isMoving = false;
    }

    private IEnumerator Move(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;
        originalPosition = transform.position;
        targetPosition = originalPosition + direction * stepSize;
        GetComponent<RobotTracksController>().direction = RobotTracksController.Direction.Forward;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (elapsedTime / timeToMove));
            GetComponent<RobotTracksController>().rotation += (stepSize / timeToMove) * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        isMoving = false;
    }
}
