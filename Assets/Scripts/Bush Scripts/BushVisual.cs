using System.Collections;
using UnityEngine;

public class BushVisual : MonoBehaviour
{
    [SerializeField]
    private Sprite[] bushSprites, fruitSprites, drySprites;

    [SerializeField]
    private SpriteRenderer[] fruitsRenderers;

    public enum BushVariant { Green, Cyan, Yellow }
    private BushVariant bushVariant;

    public float hideTimePerFruit = 0.2f;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        bushVariant = (BushVariant)Random.Range(0, bushSprites.Length);
        sr.sprite = bushSprites[(int)bushVariant];

        // randomize the rotation of the sprite
        if (Random.Range(0, 2) == 1)
            sr.flipX = true;

        for (int i = 0; i < fruitsRenderers.Length; i++)
        {
            fruitsRenderers[i].sprite = fruitSprites[(int)bushVariant];
            fruitsRenderers[i].enabled = false;
        }
    }

    public BushVariant GetBushVariant()
    {
        return bushVariant;
    }

    // harvesting UI
    public void SetToDry()
    {
        sr.sprite = drySprites[(int)bushVariant];
    }

    // hide the fruits
    IEnumerator _HideFruits(float time, int index)
    {
        yield return new WaitForSeconds(time);
        fruitsRenderers[index].enabled = false;
    }

    // harvest the fruits in the bush
    public void HideFruits()
    {
        float waitTimeForFruit = hideTimePerFruit;

        for (int i = 0; i < fruitsRenderers.Length; i++)
        {
            StartCoroutine(_HideFruits(waitTimeForFruit, i));
            waitTimeForFruit += hideTimePerFruit;
        }
    }

    // regenerate fruits in bush
    public void ShowFruits()
    {
        for (int i = 0; i < fruitsRenderers.Length; i++)
            fruitsRenderers[i].enabled = true;
    }
}
