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

        Vector3 dir = new Vector3(h, 0, v);
        if(!(h == 0 && v == 0))
        {
            transform.position += dir * Speed * Time.deltaTime;
            transform.rotation = 
                Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(dir),Time.deltaTime
                * rotateSpeed);
        }
        //transform.position += new Vector3(h, 0, v) * Speed * Time.deltaTime;
    }
}
