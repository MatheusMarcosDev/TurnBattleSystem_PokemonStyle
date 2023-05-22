using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectPE : MonoBehaviour
{
    public static ScriptableObjectPE instance;

    public PokemonEnemyObject[] allPokemonEnemyObject;
    public PokemonEnemyObject currentPokemonEnemyObject;

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
