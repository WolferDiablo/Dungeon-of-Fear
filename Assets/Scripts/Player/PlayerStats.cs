using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    private int level;
    private int xpToNextLevel = 200;
    public int xp;

    private float magicDamage;

    [Header("XP Bar References")]
    public Slider xpBarSlider;
    public Image fill;
    public TextMeshProUGUI levelText;

    public PlayerAttack playerAttack;

    void Start() {
        level = 1;
        xp = 0;
        xpBarSlider.maxValue = xpToNextLevel;
        levelText.text = "Level: " + level.ToString();
        magicDamage = 50;
    }

    public void updateXP(int addXp)  {
        xp += addXp;
    }

    void Update() {
        xpBarSlider.value = xp;
        if (xp >= xpToNextLevel) {
            xp -= xpToNextLevel;
            level ++;
            levelText.text = "Level: " + level.ToString();

            magicDamage = (level * 10) + 50;
        }
        
        playerAttack.fireBallDamage = magicDamage;
    }

}
