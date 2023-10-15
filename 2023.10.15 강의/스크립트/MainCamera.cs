using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject Target; // �÷��̾ ������. 

    public float distance = 5.0f; //�÷��̾���� �Ÿ�
    public float height = 2.0f; // �÷��̾�� ���� ����
    public float CameraSpeed = 10.0f;
    Vector3 TargetPos;

    void FixedUpdate()
    {
        //�÷��̾� ȸ���� �����ɴϴ�. Rotation �� �� ������
        Quaternion playerRotation = Target.transform.rotation;

        // �÷��̾��� ���� ��ġ�� ����մϴ�.
        Vector3 offset = playerRotation * 
            new Vector3(0, height, -distance);

        TargetPos = Target.transform.position + offset;
        
        //ī�޶��� ȸ���� �÷��̾��� ȸ������ �����մϴ�.
        transform.rotation = playerRotation;

        //ī�޶��� ��ġ�� Lerp�� ����Ͽ� �ε巴�� �̵���ŵ�ϴ�.
        transform.position = Vector3.Lerp(transform.position, TargetPos, 
            Time.deltaTime * CameraSpeed);
    }
}
