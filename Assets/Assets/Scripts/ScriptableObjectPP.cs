using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectPP : MonoBehaviour
{
    public static ScriptableObjectPP instance;

    public PokemonPlayerObject[] allPokemonPlayerObject;
    public PokemonPlayerObject currentPokemonPlayerObject;

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
