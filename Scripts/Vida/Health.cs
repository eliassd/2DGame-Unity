using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IObserver
{
    [SerializeField] Subject _playerSubject;
    public playerMovement pl;

    public int health;
    public int numOfHeart;

    public Image[] hearths;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    private GameObject hud;
    void Start()
    {
        hud = GameObject.Find("HUD");

        if (health > numOfHeart)
        {
            health = numOfHeart;
        }

        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < health)
            {
                hearths[i].sprite = fullHearth;
            }
            else
            {
                hearths[i].sprite = emptyHearth;
            }

            if (i < numOfHeart)
            {
                hearths[i].enabled = true;
            }
            else
            {
                hearths[i].enabled = false;
            }

        }
    }

    public void OnNotify(PlayerActions action)
    {
        if(action == PlayerActions.SofreuDano)
        {
            AtualizaVida();
        }
    }

    private void OnEnable()
    {
        _playerSubject.addObserver(this);
    }

    private void OnDisable()
    {
        _playerSubject.removeObserver(this);
    }

    private void AtualizaVida()
    {
        health = pl.getLife();
        for (int i = 0; i < hearths.Length; i++)
        {
            if (i < health)
            {
                hearths[i].sprite = fullHearth;
            }
            else
            {
                hearths[i].sprite = emptyHearth;
            }
        }
    }
}
