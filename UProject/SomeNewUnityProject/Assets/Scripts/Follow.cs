using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{


    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        offset = transform.position - target.position;
        


    }


    // здесь использую свой метод вместо Monobehavior Update или LateUpdate, вызываю из корабля после смешения ригидбади 
    // как выяснилось, это минимизирует тряску камеры, если мы фоловим ригидбади-контроллер

    public void UpdatePosition()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 desiredPosition = target.position + target.TransformDirection(target.forward + offset);
        transform.position = Vector3.SmoothDamp(transform.localPosition, desiredPosition, ref velocity, smoothTime);
        transform.LookAt(target, target.up);
    }
}
