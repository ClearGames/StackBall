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
    //    // y��ġ�� 0���� -0.5������ ��ġ
    //    // -> 0.5�� ������ ���ư��� �˴ϴ�
    //    Invoke(nameof(BreakingPart), Mathf.Abs(transform.position.y));
    //}

    public void BreakingPart()
    {
        rigidbody.isKinematic   = false;    // �߷�, ������ �޵��� ����
        collider.enabled        = false;    // �浹�� ���� �ʵ��� Collider ��Ȱ��ȭ

        Vector3 forcePoint      = transform.parent.position;
        float parentXPosiiton   = transform.parent.position.x;  // �θ� ������Ʈ(�÷���)�� x ��ġ
        float xPosition         = meshRenderer.bounds.center.x; // ���� �÷��� �Ž��� �߾� x ��ġ

        Vector3 direction       = ((parentXPosiiton - xPosition) < 0) ? Vector3.right : Vector3.left;
        direction               = (Vector3.up * moveSpeed + direction).normalized;

        float force             = Random.Range(20, 40);
        float torque            = Random.Range(110, 180);

        rigidbody.AddForceAtPosition(direction * force, forcePoint, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.left * torque);
        rigidbody.velocity = Vector3.down;
    }
}
