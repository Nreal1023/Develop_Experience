using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementUP : MonoBehaviour
{
    public float moveDistance = 10.0f;
    public float moveDuration = 0.5f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.forward);
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction * moveDistance;
        
        transform.DOKill();
        
        transform.DOMove(targetPosition, moveDuration);
        
        transform.DORotateQuaternion(Quaternion.LookRotation(direction), moveDuration);
    }
}