using UnityEngine;
using UnityEngine.UI;  // Import UI namespace

public class Reel : MonoBehaviour
{
    public Sprite[] symbolImages;  // Array of symbol sprites
    private Image reelImage;        // UI Image component

    void Start()
    {
        reelImage = GetComponent<Image>();  // Get UI Image component
        ShowRandomSymbol();                  // Show a random symbol at start
    }

    public void ShowRandomSymbol()
    {
        int randomIndex = Random.Range(0, symbolImages.Length);
        reelImage.sprite = symbolImages[randomIndex];  // Change the Image's sprite
    }
}
