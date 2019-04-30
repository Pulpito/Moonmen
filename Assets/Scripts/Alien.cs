using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    // STATS
    private int life;
    private int maxLife;

    // SPRITE
    public SpriteRenderer faceRenderer;
    [System.Serializable]
    public struct FaceExpression { public Sprite happy, regular, dead, scared; }
    public enum AlienState { HAPPY, REGULAR, DEAD, SCARED };
    public FaceExpression faceExpressionSprites;

    private Dictionary<AlienState, Sprite> faceExpressions;
    private AlienState state;
    private AlienState prevState;

    private void Start()
    {
        faceExpressions = new Dictionary<AlienState, Sprite>();
        faceExpressions.Add(AlienState.HAPPY, faceExpressionSprites.happy);
        faceExpressions.Add(AlienState.REGULAR, faceExpressionSprites.regular);
        faceExpressions.Add(AlienState.DEAD, faceExpressionSprites.dead);
        faceExpressions.Add(AlienState.SCARED, faceExpressionSprites.scared);
        state = AlienState.HAPPY;
        prevState = AlienState.DEAD;
    }

    private void Update()
    {
        if(prevState != state)  faceRenderer.sprite = faceExpressions[state];
        prevState = state;
    }

    public void Damage(int amount)
    {
        life -= amount;
        if (life < 0) life = 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Jajajajaja");
        Thunder isThunder = other.gameObject.GetComponent<Thunder>();
        if(isThunder != null)
        {
            Damage(10);
        }
    }
}
