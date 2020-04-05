using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokedexOptions : MonoBehaviour
{
    public RawImage Icon;
    public TextMeshProUGUI TxtNome;
    public Button BtnInfo;
    [Space]
    public Transform ScvTypes;
    public PokedexType PokedexType;

    private Pokedex.Pokemon pokemon;

    private void Awake()
    {
        BtnInfo.onClick.AddListener(() => PokedexManager.Instance.AbrirInfoPokemon(pokemon));
    }

    public void SetInfo(Pokedex.Pokemon pokemon, Texture2D img)
    {
        this.pokemon = pokemon;

        TxtNome.text = $"{pokemon.num} - {pokemon.name}";
        Icon.texture = img;
        instanciarTypes();
    }

    void instanciarTypes()
    {
        for (int i = 0; i < pokemon.type.Count; i++)
        {
            PokedexType pokedexType = Instantiate(PokedexType, ScvTypes);
            pokedexType.SetInfo(pokemon.type[i]);
        }
    }

}
