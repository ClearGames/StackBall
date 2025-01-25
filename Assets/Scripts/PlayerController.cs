using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float bounceForce = 5f; // 점프 힘

    [Header("SFX")]
    [SerializeField] private AudioClip bounceClip;      // 점프 사운드

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 플랫폼이 겹쳐지는 부분에 부딧혔을 때 로직이 두 번 실행될 수 있습니다
        // 한 번 실행으로 y 속력이 bounceForce로 설정된 상태에서는 다시 실행하지 않도록 설정
        if (rigidbody.velocity.y > 0) return;

        // 발판(Platform)에 부딪치면 y 속력을 bounceForce로 설정
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        // 사운드 재생 : Bounce
        PlaySound(bounceClip);
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
}