using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public GameObject imagesShower;
    private int _currentSprite = 0;
    public Sprite[] sprites;
    
    public void NextSprite()
    {
        if (_currentSprite == sprites.Length - 1)
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
            _currentSprite = sprites.Length - 1;
        }
        else
        {
            _currentSprite--;
        }
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        imagesShower.GetComponent<Image>().sprite = sprites[_currentSprite];
    }
}
