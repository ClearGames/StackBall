using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameController gameController;

    [Header("Parameters")]
    [SerializeField] private float bounceForce = 5f; // ���� ��
    [SerializeField] private float dropForce = -10f; // ���� ��

    [Header("SFX")]
    [SerializeField] private AudioClip bounceClip;      // ���� ����
    [SerializeField] private AudioClip normalBreakClip; // �Ϲ� ���¿��� �÷����� �ı��ϴ� ����
    [SerializeField] private AudioClip powerBreakClip;  // �Ŀ� ���¿��� �÷����� �ı��ϴ� ����

    [Header("VFX")]
    [SerializeField] private Material playerMaterial;   // �÷��̾ �����ϴ� material ����
    [SerializeField] private Transform splashImage;     // �÷��̾ �÷����� �浹���� �� �÷����� �����ϴ� �̹���
    [SerializeField] private ParticleSystem[] splashParticles;  // �÷��̾ �÷����� �浿���� �� �÷����� �����ϴ� ��ƼŬ

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private PlayerPowerMode playerPowerMode;

    // Splash Image, Particle�� ���� ��ġ ������
    private Vector3 splashWeight = new Vector3(0, 0.22f, 0.1f);
    private bool isClicked = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerPowerMode = GetComponent<PlayerPowerMode>();
    }

    private void Update()
    {
        if (!gameController.IsGamePlay) return;

        UpdateMouseButton();
        UpdateDropToSmash();

        playerPowerMode.UpdatePowerMode(isClicked);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isClicked)
        {
            // �÷����� �������� �κп� �ε����� �� ������ �� �� ����� �� �ֽ��ϴ�
            // �� �� �������� y �ӷ��� bounceForce�� ������ ���¿����� �ٽ� �������� �ʵ��� ����
            if (rigidbody.velocity.y > 0) return;
            OnJumpProcess(collision);
        }
        // ���콺 Ŭ�� ���� �� (�÷����� �浹 & �÷��� �ı�)
        else
        {
            //if(collision.gameObject.CompareTag("BreakPart"))
            //{
            //    PlatformController platform = collision.transform.parent.GetComponent<PlatformController>();

            //    if(platform.IsCollision == false)
            //    {
            //        platform.BreakAllParts();
            //        // ���� ��� : normal break or power break
            //        PlaySound(normalBreakClip);
            //        gameController.OnCollisionWithPlatform();
            //    }
            //}else if (collision.gameObject.CompareTag("NonBreakPart"))
            //{
            //    // ����, �߷��� ���� �ʵ��� ����
            //    rigidbody.isKinematic = false;
            //    Debug.Log("GameOver");
            //}
            if (playerPowerMode.IsPowerMode)
            {
                if (collision.gameObject.CompareTag("BreakPart") ||
                    collision.gameObject.CompareTag("NonBreakPart"))
                {
                    OnCollisionWithBreakPart(collision, powerBreakClip, 2);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("BreakPart"))
                {
                    OnCollisionWithBreakPart(collision, normalBreakClip, 1);
                }
                else if (collision.gameObject.CompareTag("NonBreakPart"))
                {
                    rigidbody.isKinematic = false;
                    Debug.Log("GameOver");
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rigidbody.velocity.y > 0) return;
        if (isClicked) return;

        //Debug.Log("OnCollisionStay");
        OnJumpProcess(collision);
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

    private void OnSplashParticle()
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

    private void OnJumpProcess(Collision collision)
    {
        // ����(Platform)�� �ε����� y �ӷ��� bounceForce�� ����
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        // ���� ��� : Bounce
        PlaySound(bounceClip);
        // �浹 ȿ�� ��� : Splash Image
        OnSplashImage(collision.transform);
        // �浹 ȿ�� ��� : Splash Particle
        OnSplashParticle();
    }

    private void UpdateMouseButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    private void UpdateDropToSmash()
    {
        if(Input.GetMouseButton(0) && isClicked)
        {
            rigidbody.velocity = new Vector3(0, dropForce, 0);
        }
    }

    private void OnCollisionWithBreakPart(Collision collision, AudioClip clip, int addedScore)
    {
        // �ε��� �÷����� ��� ���� ������
        PlatformController platform = collision.transform.parent.GetComponent<PlatformController>();

        if (platform.IsCollision == false)
        {
            platform.BreakAllParts();
            PlaySound(clip);
            gameController.OnCollisionWithPlatform(addedScore);
        }
    }
}