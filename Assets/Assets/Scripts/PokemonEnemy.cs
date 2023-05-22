using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonEnemy : MonoBehaviour
{
    public static PokemonEnemy instance;

    public PokemonEnemyObject pokemonEnemyObject;

    public float hpPercentagePokemonEnemy;
    private Transform hpBarSize;
    private Vector3 vector3;

    private int idCommand;
    private int hit;
    public int idPhase;

    private string actionPokemonEnemy;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pokemonEnemyObject.currentLifePE = pokemonEnemyObject.maxLifePE;
        hpBarSize = GameObject.Find("PokemonEnemyHP").transform;

        BarHPPokemonEnemy();
    }

    public void TakeDamage(int hit)
    {
        pokemonEnemyObject.currentLifePE -= hit;

        if (pokemonEnemyObject.currentLifePE <= 0)
        {
            pokemonEnemyObject.currentLifePE = 0;

            GetComponent<SpriteRenderer>().enabled = false;
        }

        BarHPPokemonEnemy();
    }

    public IEnumerator InitAction()
    {
        idCommand = Random.Range(0, pokemonEnemyObject.skillsNamePE.Length);

        yield return new WaitForSeconds(1);

        StartCoroutine("Command", idCommand);
    }

    public IEnumerator Command(int idAction)
    {
        StartCoroutine(DealDamage(idAction));
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Dialogue(string coroutineText)
    {
        int letter = 0;
        UIController.instance.textGame.text = "";

        while (letter <= coroutineText.Length - 1)
        {
            UIController.instance.textGame.text += coroutineText[letter];
            letter += 1;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1);

        switch (idPhase)
        {
            case 1:
                StartCoroutine("DealDamage");
                break;

            case 2:
                PokemonPlayer.instance.InitPokemonPlayerCommand();
                break;

            case 3:

                break;
        }
    }

    public IEnumerator DealDamage(int idAction)
    {
        GameObject tempPrefab = Instantiate(pokemonEnemyObject.animationsSkillsPE[idCommand]) as GameObject;
        tempPrefab.transform.position = PokemonPlayer.instance.transform.position;

        hit = Random.Range(1, pokemonEnemyObject.damagesSkillsPE[idCommand] + 1);
        actionPokemonEnemy = "Foe " + pokemonEnemyObject.namePE + " used " + pokemonEnemyObject.skillsNamePE[idCommand] + "!";
        StartCoroutine("Dialogue", actionPokemonEnemy);
        yield return new WaitForSeconds(1);
        Destroy(tempPrefab);
        PokemonPlayer.instance.TakeDamage(hit);
        idPhase = 2;
    }

    void BarHPPokemonEnemy()
    {
        hpPercentagePokemonEnemy = pokemonEnemyObject.currentLifePE / pokemonEnemyObject.maxLifePE;
        vector3 = hpBarSize.localScale;
        vector3.x = hpPercentagePokemonEnemy;
        hpBarSize.localScale = vector3;
    }
}
