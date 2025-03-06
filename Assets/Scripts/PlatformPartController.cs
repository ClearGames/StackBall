using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPartController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private MeshRenderer meshRenderer;
    private new Rigidbody rigidbody;
    private new Collider collider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    // Debug Test..
    //private void Start()
    //{
    //    // Invoke the method methodsName in time seconds;
    //    // y위치는 0부터 -0.5단위로 배치
    //    // -> 0.5초 단위로 날아가게 됩니다
    //    Invoke(nameof(BreakingPart), Mathf.Abs(transform.position.y));
    //}

    public void BreakingPart()
    {
        rigidbody.isKinematic   = false;    // 중력, 물리를 받도록 설정
        collider.enabled        = false;    // 충돌이 되지 않도록 Collider 비활성화

        Vector3 forcePoint      = transform.parent.position;
        float parentXPosiiton   = transform.parent.position.x;  // 부모 오브젝트(플랫폼)의 x 위치
        float xPosition         = meshRenderer.bounds.center.x; // 조각 플랫폼 매쉬의 중앙 x 위치

        Vector3 direction       = ((parentXPosiiton - xPosition) < 0) ? Vector3.right : Vector3.left;
        direction               = (Vector3.up * moveSpeed + direction).normalized;

        float force             = Random.Range(20, 40);
        float torque            = Random.Range(110, 180);

        rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
        rigidbody.velocity = Vector3.down;
    }
}
