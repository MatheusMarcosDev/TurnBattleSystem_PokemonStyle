using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonEnemy : MonoBehaviour
{
    public static PokemonEnemy instance;

    public string namePokemonEnemy;
    public int levelPokemonEnemy;
    public int xpPokemonEnemy;
    public float hpMaxPokemonEnemy;
    public float hpPokemonEnemy;
    public float hpPercentagePokemonEnemy;

    public string[] skillsPokemonEnemy;
    public int[] damageSkillsPokemonEnemy;

    private string actionPokemonEnemy;

    private Transform hpBarSize;
    private Vector3 vector3;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpPokemonEnemy = hpMaxPokemonEnemy;
        hpBarSize = GameObject.Find("PokemonEnemyHP").transform;
        hpPercentagePokemonEnemy = hpPokemonEnemy / hpMaxPokemonEnemy;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonEnemy;
        hpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        hpPokemonEnemy -= hit;

        if (hpPokemonEnemy < 0)
        {
            hpPokemonEnemy = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        hpPercentagePokemonEnemy = hpPokemonEnemy / hpMaxPokemonEnemy;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonEnemy;
        hpBarSize.localScale = vector3;
    }
}
