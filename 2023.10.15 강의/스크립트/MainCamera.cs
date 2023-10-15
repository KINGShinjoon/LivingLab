using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Target; // 플레이어를 가져옴. 

    public float distance = 5.0f; //플레이어와의 거리
    public float height = 2.0f; // 플레이어로 부터 높이
    public float CameraSpeed = 10.0f;
    Vector3 TargetPos;

    void FixedUpdate()
    {
        //플레이어 회전을 가져옵니다. Rotation 값 을 가져옴
        Quaternion playerRotation = Target.transform.rotation;

        // 플레이어의 뒤쪽 위치를 계산합니다.
        Vector3 offset = playerRotation * 
            new Vector3(0, height, -distance);

        TargetPos = Target.transform.position + offset;
        
        //카메라의 회전을 플레이어의 회전으로 설정합니다.
        transform.rotation = playerRotation;

        //카메라의 위치를 Lerp를 사용하여 부드럽게 이동시킵니다.
        transform.position = Vector3.Lerp(transform.position, TargetPos, 
            Time.deltaTime * CameraSpeed);
    }
}
