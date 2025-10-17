using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Reel : MonoBehaviour
{
    public Sprite[] symbolSprites;     // All symbols to display
    public string[] symbolNames;       // Corresponding names to use for paytable logic (must match sprites order)
    private Image reelImage;           // UI Image reference for the reel display
    private int currentSymbolIndex;    // Index of current symbol shown

    [Header("Spin Settings")]
    public float spinDuration = 2.5f;      
    public float minSpinSpeed = 0.02f;     
    public float maxSpinSpeed = 0.2f;      
    public bool isSpinning = false;        // Whether reel is currently spinning

    void Start()
    {
        reelImage = GetComponent<Image>();
        ShowRandomSymbol();
    }

    // Show a random symbol instantly
    public void ShowRandomSymbol()
    {
        currentSymbolIndex = SecureRandom.GetSecureRandomInt(0, symbolSprites.Length);
        reelImage.sprite = symbolSprites[currentSymbolIndex];
    }

    
    public string GetCurrentSymbolName()
    {
        return symbolNames[currentSymbolIndex];
    }

    // Coroutine that spins the reel with animation and then slows down
    public IEnumerator SpinAnimation(float startDelay)
    {
        yield return new WaitForSeconds(startDelay); //Time between reel starts
        isSpinning = true;

        float elapsed = 0f;
        float currentSpinSpeed = minSpinSpeed;

        while (elapsed < spinDuration)
        {
            // Lerp spin speed from fast to slow over the duration
            float t = elapsed / spinDuration;
            currentSpinSpeed = Mathf.Lerp(minSpinSpeed, maxSpinSpeed, t);

            // Pick a random symbol at each tick
            currentSymbolIndex = SecureRandom.GetSecureRandomInt(0, symbolSprites.Length);
            reelImage.sprite = symbolSprites[currentSymbolIndex];

            yield return new WaitForSeconds(currentSpinSpeed);
            elapsed += currentSpinSpeed;
        }

        // Final spin tick: pick last symbol for the result
        currentSymbolIndex = SecureRandom.GetSecureRandomInt(0, symbolSprites.Length);
        reelImage.sprite = symbolSprites[currentSymbolIndex];

        isSpinning = false;
    }
}
