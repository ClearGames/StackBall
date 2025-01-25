using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float bounceForce = 5f; // ���� ��

    [Header("SFX")]
    [SerializeField] private AudioClip bounceClip;      // ���� ����

    [Header("VFX")]
    [SerializeField] private Material playerMaterial;   // �÷��̾ �����ϴ� material ����
    [SerializeField] private Transform splashImage;     // �÷��̾ �÷����� �浹���� �� �÷����� �����ϴ� �̹���
    [SerializeField] private ParticleSystem[] splashParticles;  // �÷��̾ �÷����� �浿���� �� �÷����� �����ϴ� ��ƼŬ

    private new Rigidbody rigidbody;
    private AudioSource audioSource;

    // Splash Image, Particle�� ���� ��ġ ������
    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);

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
        // �浹 ȿ�� ��� : Splash Image
        OnSplashImage(collision.transform);
        // �浹 ȿ�� ��� : Splash Particle
        OnSplashParticle();
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void OnSplashImage(Transform target)
    {
        // ���÷��� �̹����� �����ϰ�, target�� �ڽ����� ��ġ
        Transform image = Instantiate(splashImage, target);
        // ���÷��� �̹����� ��ġ, ȸ��, ũ�� ����
        image.position          = transform.position - splashWeight;
        image.localEulerAngles  = new Vector3(0, 0, Random.Range(0, 360));
        float randomScale       = Random.Range(0.3f, 0.5f);
        image.localScale        = new Vector3(randomScale, randomScale, 1);
        // ���÷��� �̹����� ���� ����
        image.GetComponent<MeshRenderer>().material.color = playerMaterial.color;
    }

    void OnSplashParticle()
    {
        // ���� ��Ȱ��ȭ ������ ���÷��� ��ƼŬ �� �ϳ��� ������ Ȱ��ȭ �� ���
        for (int i = 0; i < splashParticles.Length; i++)
        {
            if (splashParticles[i].gameObject.activeSelf) continue;

            // ���÷��� ��ƼŬ Ȱ��ȭ
            splashParticles[i].gameObject.SetActive(true);
            // ���÷��� ��ƼŬ ��ġ ����
            splashParticles[i].transform.position = transform.position - splashWeight;
            // ���÷��� ��ƼŬ ���� ����
            ParticleSystem.MainModule mainModule    = splashParticles[i].main;
            mainModule.startColor                   = playerMaterial.color;
            break;
        }
    }
}