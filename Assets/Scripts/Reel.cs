using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Reel : MonoBehaviour
{
    public Sprite[] symbolSprites;     // All symbols to display
    public string[] symbolNames;       // Corresponding names for paytable logic (must match sprites order)
    private Image reelImage;           // UI Image reference for the reel display
    private int currentSymbolIndex;    // Index of current symbol shown

    [Header("Spin Settings")]
    public float spinDuration = 2.5f;      // Total spin duration for this reel
    public float minSpinSpeed = 0.02f;     // Fastest cycling delay at start (in seconds)
    public float maxSpinSpeed = 0.2f;      // Slowest cycling delay near stop
    public bool isSpinning = false;        // Whether reel is currently spinning

    void Start()
    {
        reelImage = GetComponent<Image>();
        ShowRandomSymbol();
    }

    // Show a random symbol instantly (used for initialization or quick change)
    public void ShowRandomSymbol()
    {
        currentSymbolIndex = SecureRandom.GetSecureRandomInt(0, symbolSprites.Length);
        reelImage.sprite = symbolSprites[currentSymbolIndex];
    }

    // Get the current symbol name for payout evaluation
    public string GetCurrentSymbolName()
    {
        return symbolNames[currentSymbolIndex];
    }

    // Coroutine that spins the reel with animation and gradually slows down
    public IEnumerator SpinAnimation(float startDelay)
    {
        yield return new WaitForSeconds(startDelay); // Stagger start time for sequential reels
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
