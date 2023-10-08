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
   
    private void Start() //���۵��ڸ��� ����Ǵ� �ڵ�
    {
        timeText.text = year + "��" + month + "��" + day + "��";
        StartCoroutine(StartTime());
    }

   private IEnumerator StartTime()
    {
        while(true) // while �ش��ϴ� ���� ������ �� ���������� ����
        {
            yield return new WaitForSeconds(15); //15�� ��� ���� ���� �� ���� ����
            day++; // day �Ϸ� ������ 
            //day = day+1;
            //day++

            if(day>30)
            {
                day = 1;
                month++;
            }
            timeText.text = year + "��" + month + "��" + day + "��";
        }
    }
}
