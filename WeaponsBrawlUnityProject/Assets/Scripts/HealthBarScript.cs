using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour {
    private Transform bar;
    private GameObject barSprite;

    [Range(0, 100)] [SerializeField] public int HP = 100;

    void Start()
    {
        bar = transform.Find("Bar");
        barSprite = bar.Find("BarSprite").gameObject;
        //barSprite.GetComponent<SpriteRenderer>().color = Color.yellow;
    }

    public void SetSize(float sizeNormalized)
    {
        barSprite.transform.localScale = new Vector3(sizeNormalized, 1f);
    }
}
