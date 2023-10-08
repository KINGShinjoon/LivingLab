using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualTime : MonoBehaviour
{
    public static string year = "2023"; //연도
    public int month = 1; //달
    public int day = 1; //일수

    public Text timeText; //시간을 표시하기 위해서 필요
   
    private void Start() //시작되자마자 실행되는 코드
    {
        timeText.text = year + "년" + month + "월" + day + "일";
        StartCoroutine(StartTime());
    }

   private IEnumerator StartTime()
    {
        while(true) // while 해당하는 값이 충족할 때 지속적으로 실행
        {
            yield return new WaitForSeconds(15); //15초 대기 이후 실행 ㅡ 이후 변경
            day++; // day 하루 증가함 
            //day = day+1;
            //day++

            if(day>30)
            {
                day = 1;
                month++;
            }
            timeText.text = year + "년" + month + "월" + day + "일";
        }
    }
}
