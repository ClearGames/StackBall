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
    //    // y위치는 0부터 -0.5단위로 배치
    //    // -> 0.5초 단위로 날아가게 됩니다
    //    Invoke(nameof(BreakAllParts), Mathf.Abs(transform.position.y));
    //}

    public void BreakAllParts()
    {
        // 중첩 호출되는 것을 막기 위해 첫 호출 때 IsCollision true
        if (IsCollision == false) IsCollision = true;

        // 플랫폼 조각들이 부모와 함께 회전하기 때문에 부모 = null로 설정
        if (transform.parent != null) transform.parent = null;

        // PlatformPartController[] parts = transform.GetComponentsInChildren<PlatformPartController>();

        // 플랫폼에 포함된 조각들의 BreakingPart() 메소드 호출
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
