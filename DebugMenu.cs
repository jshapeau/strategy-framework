using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CombatManager combatManager;
    private IGameState gameState;

    void Start()
    {
        this.combatManager = GameObject.FindObjectOfType<CombatManager>();
    }
    
    void Update()
    {
        gameState = combatManager.GetCurrentGameState();
        text.text = gameState.ToString();
    }
}
