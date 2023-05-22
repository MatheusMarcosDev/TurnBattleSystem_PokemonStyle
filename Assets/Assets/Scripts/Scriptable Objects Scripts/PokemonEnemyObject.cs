using UnityEngine;

[CreateAssetMenu (fileName = "New Pokemon", menuName = "Pokemon/Pokemon Enemy")]

public class PokemonEnemyObject : ScriptableObject
{
    [Header ("Pokemon Info/Stats")]
    public string namePE;
    public float maxLifePE;
    public float currentLifePE;
    public int currentLevelPE;
    public int defeatExpPE;

    [Header ("Skills Settings")]
    public GameObject[] animationsSkillsPE;
    public string[] skillsNamePE;
    public int[] damagesSkillsPE;
}
