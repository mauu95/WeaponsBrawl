using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {

    private Transform barSprite;

    [Range(0, 100)] [SerializeField] public int HP = 100;

    void Start()
    {
        barSprite = transform.Find("Bar/BarSprite");
    }

    public void SetSize(float sizeNormalized)
    {
        barSprite.localScale = new Vector3(sizeNormalized, 1f);
        if(sizeNormalized < 0.3f)
            barSprite.Find("whiteBar").gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
