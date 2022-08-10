using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonPlayer : MonoBehaviour
{
    public static PokemonPlayer instance;

    public string namePokemonPlayer;
    public int levelPokemonPlayer;
    public int xpPokemonPlayer;
    public float hpMaxPokemonPlayer;
    public float hpPokemonPlayer;
    public float hpPercentagePokemonPlayer;

    public string[] skillsPokemonPlayer;
    public int[] damageSkillsPokemonPlayer;

    private string actionPokemonPlayer;

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
