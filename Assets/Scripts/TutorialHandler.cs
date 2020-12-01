using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public GameObject imagesShower;
    private int _currentSprite = 0;
    public Sprite[] spritesIos;
    public Sprite[] spritesMac;
    public Sprite[] spritesTv;

    public void Start()
    {
        UpdateSprite();
    }
    
    public void NextSprite()
    {
        if (_currentSprite == spritesIos.Length - 1)
        {
            _currentSprite = 0;
        }
        else
        {
            _currentSprite++;
        }
        UpdateSprite();
    }

    public void PreviousSprite()
    {
        if (_currentSprite == 0)
        {
            _currentSprite = spritesIos.Length - 1;
        }
        else
        {
            _currentSprite--;
        }
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL
            imagesShower.GetComponent<Image>().sprite = spritesMac[_currentSprite];
        #elif UNITY_IOS
           imagesShower.GetComponent<Image>().sprite = spritesIos[_currentSprite];
        #elif UNITY_TVOS
            imagesShower.GetComponent<Image>().sprite = spritesTv[_currentSprite];
        #endif
    }
}
