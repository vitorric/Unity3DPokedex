using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokedexPage : MonoBehaviour
{
    public Button BtnFechar;
    public Button BtnPokeAnterior;
    public Button BtnPokeProximo;
    public RawImage Icon;
    public TextMeshProUGUI TxtNome;
    [Space]
    public Transform ScvTypes;
    public PokedexType PokedexType;
    [Space]
    public Transform ScvWeaknesses;
    [Space]
    public Transform PnlEvolutions;
    public GameObject TxtEvolutions;
    public PokemonEvolution PokemonEvolution;

    private Pokedex.Pokemon pokemon;

    private void Awake()
    {
        BtnFechar.onClick.AddListener(() => btnFecharInfoPokemon());
        BtnPokeProximo.onClick.AddListener(() => btnMovimentarPokedex(true));
        BtnPokeAnterior.onClick.AddListener(() => btnMovimentarPokedex(false));
    }

    public void SetInfo(Pokedex.Pokemon pokemon, Texture2D img)
    {
        limparTela();
        exibirBotoesNavegacao();
        this.pokemon = pokemon;
        PokedexManager.Instance.SetarPokemonAtual(pokemon.id);
        TxtNome.text = $"{pokemon.num} - {pokemon.name}";
        Icon.texture = img;
        instanciarTypes();
        instanciarWeaknesses();

        if (pokemon.next_evolution != null || pokemon.prev_evolution != null)
            instanciarEvolucoes();
    }

    void btnFecharInfoPokemon()
    {
        limparTela();
        PokedexManager.Instance.FecharInfoPokemon();
    }

    void btnMovimentarPokedex(bool ehProximo)
    {
        PokedexManager.Instance.MovimentarPokedex(ehProximo, (newPokemon, texture2D) =>
        {
            SetInfo(newPokemon, texture2D);
        });
    }

    void exibirBotoesNavegacao()
    {
        int currentPokemon = PokedexManager.Instance.GetPokemonAtual();

        BtnPokeAnterior.gameObject.SetActive(currentPokemon > 0);
        BtnPokeProximo.gameObject.SetActive(currentPokemon < PokedexManager.Instance.GetTotalPokemonsPokedex());
    }

    void limparTela()
    {
        PnlEvolutions.gameObject.SetActive(false);
        TxtEvolutions.SetActive(false);
        ScvTypes.GetComponentsInChildren<PokedexType>().ToList().ForEach(x => Destroy(x.gameObject));
        ScvWeaknesses.GetComponentsInChildren<PokedexType>().ToList().ForEach(x => Destroy(x.gameObject));
        PnlEvolutions.GetComponentsInChildren<PokemonEvolution>().ToList().ForEach(x => Destroy(x.gameObject));
    }

    void instanciarTypes()
    {
        for (int i = 0; i < pokemon.type.Count; i++)
        {
            PokedexType pokedexType = Instantiate(PokedexType, ScvTypes);
            pokedexType.SetInfo(pokemon.type[i]);
        }
    }

    void instanciarWeaknesses()
    {
        for (int i = 0; i < pokemon.weaknesses.Count; i++)
        {
            PokedexType pokedexType = Instantiate(PokedexType, ScvWeaknesses);
            pokedexType.SetInfo(pokemon.weaknesses[i]);
        }
    }

    void instanciarEvolucoes()
    {
        PnlEvolutions.gameObject.SetActive(true);
        TxtEvolutions.SetActive(true);

        if (pokemon.prev_evolution != null)
        {
            for (int i = 0; i < pokemon.prev_evolution.Count; i++)
            {
                PokemonEvolution pokemonEvolution = Instantiate(PokemonEvolution, PnlEvolutions);
                (Pokedex.Pokemon pokemonEv, Texture2D icon) = PokedexManager.Instance.BuscarPokemon(pokemon.prev_evolution[i].num);
                pokemonEvolution.SetInfo(pokemonEv, icon, "Anterior");
            }
        }

        if (pokemon.next_evolution != null)
        {
            for (int i = 0; i < pokemon.next_evolution.Count; i++)
            {
                PokemonEvolution pokemonEvolution = Instantiate(PokemonEvolution, PnlEvolutions);
                (Pokedex.Pokemon pokemonEv, Texture2D icon) = PokedexManager.Instance.BuscarPokemon(pokemon.next_evolution[i].num);
                pokemonEvolution.SetInfo(pokemonEv, icon, "Próxima");
            }
        }
    }
}
