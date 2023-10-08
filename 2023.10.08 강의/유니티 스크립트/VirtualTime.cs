using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualTime : MonoBehaviour
{
    public static int year = 2023; //����
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
            yield return new WaitForSeconds(1); //15�� ��� ���� ���� �� ���� ����
            day++; // day �Ϸ� ������ 
            //day = day+1;
            //day++

            if(month == 2) //2���� ��쿡 �����
            {
                if (day > 28)
                {
                    day = 1;
                    month++;
                }
            }
            else if (month == 4 || month ==6 || month == 9 || month == 11)  //2���� �ƴ����� 4,6,9,11���� ��� ����
            {
                if(day > 30)
                {
                    day = 1;
                    month++;
                }
            }
            else if ( day>31 ) //2���� 4,6,9,11���� �ƴ� ��� ����
            {
                day = 1;
                month++;
            }

            if (month > 12) //13���� �� ��� ����
            {
                month = 1;
                year++;
            }

            timeText.text = year + "��" + month + "��" + day + "��";
        }
    }
}
