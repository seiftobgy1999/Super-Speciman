using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
    public Slider epSlider;
    public Text hpVal;
    public Text epVal;

    public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpSlider.maxValue = unit.HC;
		hpSlider.value = unit.currentHP;
        hpVal.text = (unit.currentHP).ToString();
        epSlider.maxValue = unit.EC;
        epSlider.value = unit.currentEP;
        epVal.text = (unit.currentEP).ToString();

    }

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
        hpVal.text = hp.ToString();
	}

    public void SetEP(int ep)
    {
        epSlider.value = ep;
        epVal.text = ep.ToString();
    }

}
