using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    /*
     * 기둥은 임의의 설정된 색상을 좀 더 옅은 색으로 보이게 하는데
     * 임의의 색상을 GameController.Awake() 메소드에서 설정
     * -> 임의의 색상으로 변경한 이후인 Start() 메소드에서 추가 설정
     * -> Unity 이벤트 함수 실행 순서
     */
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }
}