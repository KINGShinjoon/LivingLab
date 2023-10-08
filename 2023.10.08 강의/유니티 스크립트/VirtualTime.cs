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
   
    void Start() //시작되자마자 실행되는 코드
    {
        timeText.text = year + "년" + month + "월" + day + "일";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
