using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    public Sprite[] symbolSprites;     // All symbols to display
    public string[] symbolNames;       // Must match order of sprites
    private Image reelImage;           // Image component for reel
    private int currentSymbolIndex;    // Track current symbol index

    void Start()
    {
        reelImage = GetComponent<Image>();
        ShowRandomSymbol();
    }

    public void ShowRandomSymbol()
    {
        // Use secure RNG instead of UnityEngine.Random
        currentSymbolIndex = SecureRandom.GetSecureRandomInt(0, symbolSprites.Length);
        reelImage.sprite = symbolSprites[currentSymbolIndex];
    }

    public string GetCurrentSymbolName()
    {
        return symbolNames[currentSymbolIndex];
    }
}
