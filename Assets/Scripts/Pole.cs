using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    /*
     * ����� ������ ������ ������ �� �� ���� ������ ���̰� �ϴµ�
     * ������ ������ GameController.Awake() �޼ҵ忡�� ����
     * -> ������ �������� ������ ������ Start() �޼ҵ忡�� �߰� ����
     * -> Unity �̺�Ʈ �Լ� ���� ����
     */
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }
}