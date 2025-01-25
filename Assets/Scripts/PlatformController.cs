using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PlatformPartController[] parts;
    [SerializeField] private float removeDuration = 1;
    public bool IsCollision { private set; get; } = false;

    // Debug Test..
    //private void Start()
    //{
    //    // Invoke the method methodsName in time seconds;
    //    // y��ġ�� 0���� -0.5������ ��ġ
    //    // -> 0.5�� ������ ���ư��� �˴ϴ�
    //    Invoke(nameof(BreakAllParts), Mathf.Abs(transform.position.y));
    //}

    public void BreakAllParts()
    {
        // ��ø ȣ��Ǵ� ���� ���� ���� ù ȣ�� �� IsCollision true
        if (IsCollision == false) IsCollision = true;

        // �÷��� �������� �θ�� �Բ� ȸ���ϱ� ������ �θ� = null�� ����
        if (transform.parent != null) transform.parent = null;

        // PlatformPartController[] parts = transform.GetComponentsInChildren<PlatformPartController>();

        // �÷����� ���Ե� �������� BreakingPart() �޼ҵ� ȣ��
        foreach(PlatformPartController part in parts)
        {
            part.BreakingPart();
        }

        StartCoroutine(nameof(RemoveParts));
    }

    private IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(removeDuration);
        gameObject.SetActive(false);
    }
}
