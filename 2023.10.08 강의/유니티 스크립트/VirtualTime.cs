using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualTime : MonoBehaviour
{
    public static string year = "2023"; //����
    public int month = 1; //��
    public int day = 1; //�ϼ�

    public Text timeText; //�ð��� ǥ���ϱ� ���ؼ� �ʿ�
   
    void Start() //���۵��ڸ��� ����Ǵ� �ڵ�
    {
        timeText.text = year + "��" + month + "��" + day + "��";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
