using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class UIPlayerUpdate : MonoBehaviour
{
    public TextMeshProUGUI magazineDisplay;
    public TextMeshProUGUI reloadingDisplay;
    public Player player;
    
    void Update(){
        updateMagazine();
        updateReloading();
    }

    
    
    
    private void updateMagazine() {
        string displayText = "";
        for (int i = 0; i < player.getMagazine(); i++) {
            displayText = displayText + "O";
        }
        magazineDisplay.text = displayText;
    }

    private void updateReloading() {
        if (player.isReloading()) {
            reloadingDisplay.enabled = true;
        } else {
            reloadingDisplay.enabled = false;
        }
    }
}
