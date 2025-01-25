using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float bounceForce = 5f; // ���� ��

    [Header("SFX")]
    [SerializeField] private AudioClip bounceClip;      // ���� ����

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �÷����� �������� �κп� �ε����� �� ������ �� �� ����� �� �ֽ��ϴ�
        // �� �� �������� y �ӷ��� bounceForce�� ������ ���¿����� �ٽ� �������� �ʵ��� ����
        if (rigidbody.velocity.y > 0) return;

        // ����(Platform)�� �ε�ġ�� y �ӷ��� bounceForce�� ����
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        // ���� ��� : Bounce
        PlaySound(bounceClip);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}