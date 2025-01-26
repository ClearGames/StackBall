using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameController gameController;

    [Header("Parameters")]
    [SerializeField] private float bounceForce = 5f; // 점프 힘
    [SerializeField] private float dropForce = -10f; // 점프 힘

    [Header("SFX")]
    [SerializeField] private AudioClip bounceClip;      // 점프 사운드
    [SerializeField] private AudioClip normalBreakClip; // 일반 상태에서 플랫폼을 파괴하는 사운드
    [SerializeField] private AudioClip powerBreakClip;  // 파워 상태에서 플랫폼을 파괴하는 사운드

    [Header("VFX")]
    [SerializeField] private Material playerMaterial;   // 플레이어에 적용하는 material 원본
    [SerializeField] private Transform splashImage;     // 플레이어가 플랫폼과 충돌했을 때 플랫폼에 생성하는 이미지
    [SerializeField] private ParticleSystem[] splashParticles;  // 플레이어가 플랫폼과 충동했을 때 플랫폼에 생성하는 파티클

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private PlayerPowerMode playerPowerMode;

    // Splash Image, Particle의 생성 위치 보정값
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
            // 플랫폼이 겹쳐지는 부분에 부딧혔을 때 로직이 두 번 실행될 수 있습니다
            // 한 번 실행으로 y 속력이 bounceForce로 설정된 상태에서는 다시 실행하지 않도록 설정
            if (rigidbody.velocity.y > 0) return;
            OnJumpProcess(collision);
        }
        // 마우스 클릭 중일 때 (플랫폼과 충돌 & 플랫폼 파괴)
        else
        {
            //if(collision.gameObject.CompareTag("BreakPart"))
            //{
            //    PlatformController platform = collision.transform.parent.GetComponent<PlatformController>();

            //    if(platform.IsCollision == false)
            //    {
            //        platform.BreakAllParts();
            //        // 사운드 재생 : normal break or power break
            //        PlaySound(normalBreakClip);
            //        gameController.OnCollisionWithPlatform();
            //    }
            //}else if (collision.gameObject.CompareTag("NonBreakPart"))
            //{
            //    // 물리, 중력을 받지 않도록 설정
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
        // 스플래시 이미지를 생성하고, target의 자식으로 배치
        Transform image = Instantiate(splashImage, target);
        // 스플래시 이미지의 위치, 회전, 크기 설정
        image.position          = transform.position - splashWeight;
        image.localEulerAngles  = new Vector3(0, 0, Random.Range(0, 360));
        float randomScale       = Random.Range(0.3f, 0.5f);
        image.localScale        = new Vector3(randomScale, randomScale, 1);
        // 스플래시 이미지의 색상 설정
        image.GetComponent<MeshRenderer>().material.color = playerMaterial.color;
    }

    private void OnSplashParticle()
    {
        // 현재 비활성화 상태인 스플래시 파티클 중 하나를 선택해 활성화 및 재생
        for (int i = 0; i < splashParticles.Length; i++)
        {
            if (splashParticles[i].gameObject.activeSelf) continue;

            // 스플래시 파티클 활성화
            splashParticles[i].gameObject.SetActive(true);
            // 스플래시 파티클 위치 설정
            splashParticles[i].transform.position = transform.position - splashWeight;
            // 스플래시 파티클 색상 설정
            ParticleSystem.MainModule mainModule    = splashParticles[i].main;
            mainModule.startColor                   = playerMaterial.color;
            break;
        }
    }

    private void OnJumpProcess(Collision collision)
    {
        // 발판(Platform)에 부딪히면 y 속력을 bounceForce로 설정
        rigidbody.velocity = new Vector3(0, bounceForce, 0);
        // 사운드 재생 : Bounce
        PlaySound(bounceClip);
        // 충돌 효과 재생 : Splash Image
        OnSplashImage(collision.transform);
        // 충돌 효과 재생 : Splash Particle
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
        // 부딪힌 플랫폼의 모든 조각 날리기
        PlatformController platform = collision.transform.parent.GetComponent<PlatformController>();

        if (platform.IsCollision == false)
        {
            platform.BreakAllParts();
            PlaySound(clip);
            gameController.OnCollisionWithPlatform(addedScore);
        }
    }
}