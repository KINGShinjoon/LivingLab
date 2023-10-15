using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10.0f; //움직이는 속도를 조절합니다.
    //public을 붙여서 유니티에서도 조작이 가능합니다.
    float h, v;

    public float rotateSpeed = 10.0f;

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal"); // -1, 0 ,1
        v = Input.GetAxis("Vertical"); // -1, 0 ,1

        //카메라의 방향에서 Y축 회전만을 사용합니다.
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x,
            0, Camera.main.transform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(Camera.main.transform.right.x,
            0, Camera.main.transform.right.z).normalized;

        //움직임은 카메라의 방향을 기준으로 합니다.
        Vector3 moveDirection = cameraForward * v + cameraRight * h;

        if(moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * Speed * Time.deltaTime;
            if( v>=0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                    Quaternion.LookRotation(moveDirection),
                    Time.deltaTime * rotateSpeed);
            }
        }
    }
}
