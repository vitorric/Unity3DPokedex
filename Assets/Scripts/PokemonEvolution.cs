using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonEvolution : MonoBehaviour
{
    public RawImage Icon;
    public TextMeshProUGUI TxtNome;
    public TextMeshProUGUI TxtOrdem;
    public Button BtnInfo;

    private Pokedex.Pokemon pokemon;

    private void Awake()
    {
        BtnInfo.onClick.AddListener(() => PokedexManager.Instance.AbrirInfoPokemon(pokemon));
    }

    public void SetInfo(Pokedex.Pokemon pokemon, Texture2D img, string ordem)
    {
        this.pokemon = pokemon;

        TxtNome.text = $"{pokemon.num} - {pokemon.name}";
        Icon.texture = img;
        TxtOrdem.text = ordem;
    }

}
