using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 10.0f; //�����̴� �ӵ��� �����մϴ�.
    //public�� �ٿ��� ����Ƽ������ ������ �����մϴ�.
    float h, v;

    public float rotateSpeed = 10.0f;

    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal"); // -1, 0 ,1
        v = Input.GetAxis("Vertical"); // -1, 0 ,1

        //ī�޶��� ���⿡�� Y�� ȸ������ ����մϴ�.
        Vector3 cameraForward = new Vector3(Camera.main.transform.forward.x,
            0, Camera.main.transform.forward.z).normalized;
        Vector3 cameraRight = new Vector3(Camera.main.transform.right.x,
            0, Camera.main.transform.right.z).normalized;

        //�������� ī�޶��� ������ �������� �մϴ�.
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
