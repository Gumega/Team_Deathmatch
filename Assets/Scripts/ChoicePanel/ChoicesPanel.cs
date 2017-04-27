using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesPanel : MonoBehaviour
{
    [Header("Player Stats")]
    public string playerName;
    public int level;
    public int skillPoints;
    public int rangeOfView;
    public int viewAngle;
    public int speed;
    public int health;
    [Space(10)]

    public Sprite[] weapponsSprite;
    public Sprite[] patentSprite;

    public int[] weapponsInPanel;

    public int[] magazinesSpace;
    public int[] firePower;
    public int[] range;

    public int weapponSelected;



    public Text levelTXT, skillPointsTXT, rangeOfViewTXT, viewAngleTXT, speedTXT, healthTXT, ammunationTXT, magazinesSpaceTXT, firePowerTXT, weapponRangeTXT;
    public Image patentIMG, inHandWeapponIMG, slotWeapponIMG, granadeIMG, selectedWeapponIMG;
    public Image[] weapponsInPanelIMG;




    void Start()
    {
        weapponsInPanel = new int[3];

        for (int i = 0; i < weapponsInPanel.Length; i++)
        {
            weapponsInPanel[i] = i;
        }
        if (level < 5)
        {
            patentIMG.sprite = patentSprite[0];
        }
        refreshSkillPoints();
    }

    public void ChangeWepponsInPanel(bool next)
    {
        if (next)
            for (int i = 0; i < weapponsInPanel.Length; i++)
            {
                if (weapponsInPanel[i] < weapponsSprite.Length - 1)
                    weapponsInPanel[i]++;
                else
                {
                    weapponsInPanel[i] = 0;
                }
            }
        else
        {
            for (int i = 0; i < weapponsInPanel.Length; i++)
            {
                if (weapponsInPanel[i] > 0)
                    weapponsInPanel[i]--;
                else
                {
                    weapponsInPanel[i] = weapponsSprite.Length - 1;
                }
            }
        }
        refreshImageInAmmoPanel();
    }

    public void refreshImageInAmmoPanel()
    {
        for (int i = 0; i < weapponsInPanel.Length; i++)
        {
            weapponsInPanelIMG[i].sprite = weapponsSprite[weapponsInPanel[i]];
        }
    }

    public void selectWeappon(int indexInPanel)
    {
        weapponSelected = weapponsInPanel[indexInPanel];
        refreshSelectedPanel();
    }

    public void refreshSelectedPanel()
    {
        selectedWeapponIMG.sprite = weapponsSprite[weapponSelected];
    }

    public void addSkillPoint(int index)
    {
        if (skillPoints > 0)
        {
            if (index == 1)
            {
                rangeOfView++;
                skillPoints--;
            }
            if (index == 2)
            {
                viewAngle++;
                skillPoints--;
            }
            if (index == 3)
            {
                speed++;
                skillPoints--;
            }
            if (index == 4)
            {
                health++;
                skillPoints--;
            }
        }
        refreshSkillPoints();
    }

    public void refreshSkillPoints()
    {
        rangeOfViewTXT.text = "Range of View : " + rangeOfView;
        viewAngleTXT.text = "View Angle : " + viewAngle;
        speedTXT.text = "Speed : " + speed;
        healthTXT.text = "Health : " + health;
        skillPointsTXT.text = "Sp : " + skillPoints;
    }


}
