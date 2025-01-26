using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerMode : MonoBehaviour
{
    [SerializeField] private GameObject powerEffect;     // �÷��̾ �Ŀ� ������ �� ��µǴ� ����Ʈ
    [SerializeField] private GameObject powerGameObject; // �÷��̾��� �Ŀ� ���� ������ (���ӿ�����Ʈ Ȱ��/��Ȱ�� ����)
    [SerializeField] private Image      powerGauge;      // �÷��̾��� �Ŀ� ���� ������ (���� ������)

    private float powerAmount = 0;
    public float PowerAmount
    {
        set {
            powerAmount = value;

            // �Ŀ� ��� Ȱ��ȭ
            if(powerAmount >= 1)
            {
                powerAmount = 1;
                IsPowerMode = true;
                powerGauge.color = Color.red;
                powerEffect.SetActive(true);
            }
            // �Ŀ� ��� ��Ȱ��ȭ
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
            // �Ŀ���尡 �ƴ� ��
            // ���콺 Ŭ�� ���¸� �ʴ� increaseAmount(0.8)��ŭ PowerAmount ����
            // ���콺 Ŭ�� ���°� �ƴϸ� decreaseAmountNormal(0.5)��ŭ PowerAmount ����
            if (isClicked)  PowerAmount += Time.deltaTime * increaseAmount;
            else            PowerAmount -= Time.deltaTime * decreaseAmountNormal;
        }

        // PowerAmount ��ġ�� activeAmount ��ġ �̻��̰ų� �Ŀ���� ���¸�
        if (PowerAmount >= activateAmount || IsPowerMode)   powerGameObject.SetActive(true);
        else                                                powerGameObject.SetActive(false);

        // �Ŀ� �������� Ȱ��ȭ �Ǿ� ���� �� powerGauge ������ �̿��� ������ ������Ʈ
        powerGauge.fillAmount = PowerAmount;
    }

    public void DeactivateAll()
    {
        // �Ŀ� ����Ʈ ��Ȱ��ȭ
        powerEffect.SetActive(false);
        // �Ŀ� ������ ��Ȱ��ȭ
        powerGameObject.SetActive(false);
    }
}
