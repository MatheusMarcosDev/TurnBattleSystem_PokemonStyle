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
    private Vector3 vector3;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        hpPokemonPlayer = hpMaxPokemonPlayer;
        hpBarSize = GameObject.Find("PokemonPlayerHP").transform;
        hpPercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonPlayer;
        hpBarSize.localScale = vector3;
    }

    public void TakeDamage(int hit)
    {
        hpPokemonPlayer -= hit;

        if(hpPokemonPlayer < 0)
        {
            hpPokemonPlayer = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        hpPercentagePokemonPlayer = hpPokemonPlayer / hpMaxPokemonPlayer;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonPlayer;
        hpBarSize.localScale = vector3;
    }
}
