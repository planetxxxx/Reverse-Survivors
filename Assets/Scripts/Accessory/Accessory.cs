using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessory : MonoBehaviour
{
    [SerializeField] AccessoryData accessoryData;
    Sprite accessorySprite;
    int level = 1;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        accessorySprite = accessoryData.GetSprite();
    }

    public AccessoryData.AccessoryType GetAccessoryType()
    {
        return accessoryData.GetAccessoryType();
    }

    public Sprite GetSprite()
    {
        return accessorySprite;
    }

    public int GetLevel()
    {
        return level;
    }

    public void IncreaseLevel()
    {
        level++;
        ApplyEffect();
    }

    public void ApplyEffect()
    {
        switch (accessoryData.GetAccessoryType())
        {
            case AccessoryData.AccessoryType.Spinach:
                // �ñ�ġ = ���ݷ� 10% 
                Player.GetInstance().IncreaseAttackPower(10);
                break;
            case AccessoryData.AccessoryType.Crown:
                // �հ� = ����ġ ȹ�淮 8%
                Player.GetInstance().IncreaseExpAdditional(10);
                break;
            case AccessoryData.AccessoryType.Clover:
                // Ŭ�ι� = ��� 20%
                Player.GetInstance().IncreaseLuck(20);
                break;
            case AccessoryData.AccessoryType.Wings:
                // ���� = �̵��ӵ� 10%
                Player.GetInstance().IncreaseSpeed(10);
                break;
            case AccessoryData.AccessoryType.Armor:
                // �Ƹ� = ���� 10%
                Player.GetInstance().IncreaseDefencePower(10);
                break;
            case AccessoryData.AccessoryType.EmptyTome:
                // �� å = ���� 8%
                Player.GetInstance().DecreaseAttackSpeed(8);
                break;
        }
    }
}
