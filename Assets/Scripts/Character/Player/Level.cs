using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] Slider expSlider;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Image levelUpBar;
    [SerializeField] ParticleSystem[] particles = new ParticleSystem[3];

    [SerializeField] GameObject levelUpWindow;
    [SerializeField] Transform weaponSelectTemplate;

    RectTransform[] weaponSelect = new RectTransform[slotNum];
    Image[] weaponIcon = new Image[slotNum];
    TextMeshProUGUI[] nameText = new TextMeshProUGUI[slotNum];
    TextMeshProUGUI[] description = new TextMeshProUGUI[slotNum];
    TextMeshProUGUI[] levelText = new TextMeshProUGUI[slotNum];
    Button[] button = new Button[slotNum];
    GameObject[] selectArrow = new GameObject[slotNum];

    const int slotNum = 4;
    const float slotSize = 130f;

    int maxExpValue=50;
    int curExpValue=0;
    static int level=1;
    static bool isLevelUpTime;

    enum type
    {
        Weapon,
        Accessory
    }

    private void FixedUpdate()
    {
        expSlider.value = curExpValue;
        if (curExpValue >= maxExpValue)
        {
            LevelUp();
            curExpValue = curExpValue- maxExpValue;
        }
    }





    public static bool GetIsLevelUpTime()
    {
        return isLevelUpTime;
    }

    public static int GetPlayerLevel()
    {
        return level;
    }

    public void GetExp(int value)
    {
      
            curExpValue += value;
    }

    void LevelUp()
    {
        level++;

        text.text = "LV " + level.ToString();

        maxExpValue = 50 * level;
        expSlider.maxValue = maxExpValue;
        if (level==2)
        {
            Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Axe);
        }
        else if (level ==3)
        {
            Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Bible);
        }
        else
        {
<<<<<<< Updated upstream
            int x = Random.Range(0, 3);
=======
            int x = Random.Range(0, 6);
>>>>>>> Stashed changes
            if (x ==0)
            {
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Whip);
            }
            else if (x == 1)
            {
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Axe);
            }
            else if (x == 2)
            {
                Inventory.GetInstance().AddWeapon(WeaponData.WeaponType.Bible);
            }
<<<<<<< Updated upstream
=======
            else if (x == 3)
            {
                Inventory.GetInstance().AddAccessory(AccessoryData.AccessoryType.Spinach);
            }
            else if (x == 4)
            {
                Inventory.GetInstance().AddAccessory(AccessoryData.AccessoryType.EmptyTome);
            }
            else if (x == 5)
            {
                Inventory.GetInstance().AddAccessory(AccessoryData.AccessoryType.Armor);
            }
>>>>>>> Stashed changes

        }
       
       


      
    }

    IEnumerator GetNewItem()
    {
        Time.timeScale = 0f;
        ShowSelectWindow();

        while (true)
        {
            if (!isLevelUpTime) break;

            yield return null;
        }

        foreach(GameObject arrow in selectArrow)
            arrow.SetActive(false);

        levelUpWindow.SetActive(false);
        Time.timeScale = 1f;
    }

    void ShowSelectWindow()
    {
        levelUpWindow.SetActive(true);
        List<string> checkDuplicate = new List<string>(3);

        for (int i = 0; i < slotNum; i++)
        {
            if (Random.Range(0, 10) < 4)    // ¹«±â
            {
                WeaponData.WeaponType weapon;

                do weapon = GetRandomWeapon();
                while (checkDuplicate.Contains(weapon.ToString()));
                checkDuplicate.Add(weapon.ToString());

                weaponIcon[i].sprite = ItemAssets.GetInstance().GetWeaponData(weapon).GetSprite();
                nameText[i].text = weapon.ToString();

                int level;
                if (Inventory.GetWeaponInventory().TryGetValue(weapon, out level))
                {
                    description[i].text = ItemAssets.GetInstance().GetWeaponData(weapon).GetDescription();
                    levelText[i].text = "LV " + (level + 1).ToString();
                }
                else
                {
                    description[i].text = ItemAssets.GetInstance().GetWeaponData(weapon).GetDescription();
                    levelText[i].text = "New !";
                }

                button[i].onClick.RemoveAllListeners();
                button[i].onClick.AddListener(delegate { 
                    Inventory.GetInstance().AddWeapon(weapon); 
                    isLevelUpTime = false; 
                });
            }
       
        }

        if(Random.Range(0,100) < Player.GetInstance().GetLuck())
            weaponSelect[3].gameObject.SetActive(true);
        else
            weaponSelect[3].gameObject.SetActive(false);
    }

    WeaponData.WeaponType GetRandomWeapon()
    {
        return (WeaponData.WeaponType)Random.Range(0, System.Enum.GetValues(typeof(WeaponData.WeaponType)).Length);
    }

    AccessoryData.AccessoryType GetRandomAccessory()
    {
        return (AccessoryData.AccessoryType)Random.Range(0, System.Enum.GetValues(typeof(AccessoryData.AccessoryType)).Length);
    }

    public void AddWeapon(WeaponData.WeaponType weapon)
    {
        Inventory.GetInstance().AddWeapon(weapon);
    }

    public void AddA(WeaponData.WeaponType weapon)
    {
        Inventory.GetInstance().AddWeapon(weapon);
    }

    IEnumerator LevelUpEffects()
    {
        levelUpBar.gameObject.SetActive(true);
        StartParticles();

        foreach (ParticleSystem particle in particles)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }

        while (true)
        {
            if (!isLevelUpTime) break;

            for (float i = 0f; i < 1f; i += 0.1f)
            {
                levelUpBar.color = Color.Lerp(new Color(1f, 0f, 1f), new Color(0f, 1f, 1f), i);
                yield return new WaitForSecondsRealtime(0.01f);
                if (!isLevelUpTime) break;
            }

            for (float i = 0f; i < 1f; i += 0.1f)
            {
                levelUpBar.color = Color.Lerp(new Color(0f, 1f, 1f), new Color(1f, 0f, 1f), i);
                yield return new WaitForSecondsRealtime(0.01f);
                if (!isLevelUpTime) break;
            }
        }

        levelUpBar.gameObject.SetActive(false);
        StopParticles();
    }

    void StartParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

    void StopParticles()
    {
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }
}
