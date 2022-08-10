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

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
