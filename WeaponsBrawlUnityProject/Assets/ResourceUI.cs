using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUI : AbstractInGameInterfaces {

    public TextMeshProUGUI ResourceText;

    public void SetResourceUI(int amount)
    {
        ResourceText.text = amount.ToString();
    }
}
