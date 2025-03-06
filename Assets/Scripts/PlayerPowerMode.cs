using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerMode : MonoBehaviour
{
    [SerializeField] private GameObject powerEffect;     // 플레이어가 파워 상태일 때 출력되는 이펙트
    [SerializeField] private GameObject powerGameObject; // 플레이어의 파워 상태 게이지 (게임오브젝트 활성/비활성 제어)
    [SerializeField] private Image      powerGauge;      // 플레이어의 파워 상태 게이지 (실제 게이지)

    private float powerAmount = 0;
    public float PowerAmount
    {
        set {
            powerAmount = value;

            // 파워 모드 활성화
            if(powerAmount >= 1)
            {
                powerAmount = 1;
                IsPowerMode = true;
                powerGauge.color = Color.red;
                powerEffect.SetActive(true);
            }
            // 파워 모드 비활성화
            else if(powerAmount <= 0)
            {
                powerAmount = 0;
                IsPowerMode = false;
                powerGauge.color = Color.white;
                powerEffect.SetActive(false);
            }
        }

        //get;
        get => powerAmount;
    }

    public bool IsPowerMode { private set; get; } = false;

    public void UpdatePowerMode(bool isClicked)
    {
        float increaseAmount = 0.8f;
        float decreaseAmountNormal = 0.5f;
        float decreaseAmountPower = 0.3f;
        float activateAmount = 0.3f;

        if (IsPowerMode)
        {
            PowerAmount -= Time.deltaTime * decreaseAmountPower;
        }
        else
        {
            // 파워모드가 아닐 때
            // 마우스 클릭 상태면 초당 increaseAmount(0.8)만큼 PowerAmount 증가
            // 마우스 클릭 상태가 아니면 decreaseAmountNormal(0.5)만큼 PowerAmount 감소
            if (isClicked)  PowerAmount += Time.deltaTime * increaseAmount;
            else            PowerAmount -= Time.deltaTime * decreaseAmountNormal;
        }

        // PowerAmount 수치가 activeAmount 수치 이상이거나 파워모드 상태면
        if (PowerAmount >= activateAmount || IsPowerMode)   powerGameObject.SetActive(true);
        else                                                powerGameObject.SetActive(false);

        // 파워 게이지가 활성화 되어 있을 때 powerGauge 변수를 이용해 게이지 업데이트
        powerGauge.fillAmount = PowerAmount;
    }

    public void DeactivateAll()
    {
        // 파워 이펙트 비활성화
        powerEffect.SetActive(false);
        // 파워 게이지 비활성화
        powerGameObject.SetActive(false);
    }
}
