using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBattleUI : MonoBehaviour
{
    TMP_Text text;
    void Start()
    {
        if (text == null)
            text = GetComponent<TMP_Text>();
    }

    public void UpdateHealth(int hp, int max)
    {
        if(text == null)
            text = GetComponent<TMP_Text>();

        text.text = "HP: "+hp + "/" + max;
    }
}
