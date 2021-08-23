using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Play.Player;

public class CustomizationManager : MonoBehaviour
{
    public PlayerBase player1, player2;
    public CustomizationBase custom1, custom2;

    private static CustomizationManager instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
    }
    public static void Configure() => instance.SetConfigurations();

    public void SetConfigurations()
    {
        player1.playerName = custom1.myName;
        player1.ChangeColor(custom1.myColor);
        
        player2.playerName = custom2.myName;
        player2.ChangeColor(custom2.myColor);
    }
}
