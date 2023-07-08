using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] CrystalData crystalData;
    [SerializeField] PlayerMove player;
    Sprite sprite;
    int expValue;
    int speed;

    void Awake()
    {
        Initialize();
       
    }

    internal virtual void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        expValue = crystalData.GetExpValue();
        speed = 7;
        GetCrystal();
       
    }

    public int GetExpValue()
    {
        return expValue;
    }




    void GetCrystal()
    {
        ObjectPooling.ReturnObject(gameObject, crystalData.GetCristalType());
        player.GetComponent<Level>().GetExp((int)(expValue * Player.GetInstance().GetExpAdditional() / 100f));
        gameObject.SetActive(false);

    }


}
