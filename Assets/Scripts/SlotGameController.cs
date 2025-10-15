using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotGameController : MonoBehaviour
{
    public Reel[] reels;  // Assign three Reel instances here
    public Button spinButton;                   // Reference to your UI Button
    public Sprite buttonNormalSprite;           // Normal button sprite
    public Sprite buttonPressedSprite;          // Pressed button sprite
    private Image buttonImage;

    public int playerCredits = 1000;       // Initial credits player has
    public int currentBet = 10;             // Default bet amount
    public Text creditsText;                // UI Text for showing credits
    public Text betText;  

    void Start()
    {
        buttonImage = spinButton.GetComponent<Image>();
        buttonImage.sprite = buttonNormalSprite;  // Initialize to normal
        UpdateUI();
    }
    
    void UpdateUI()
    {
        creditsText.text = "Credits: " + playerCredits;
        betText.text = "Bet: " + currentBet;
    }

    public void SetBet(int amount)
    {
        currentBet = amount;
        UpdateUI();

    }

    public void OnSpinButtonClick()
    {
        // Swap button image
        if (buttonImage.sprite == buttonNormalSprite)
            buttonImage.sprite = buttonPressedSprite;
        else
            buttonImage.sprite = buttonNormalSprite;
        
        if (playerCredits < currentBet)
        {
            Debug.Log("Not enough credits to bet!");
            return;
        }

        playerCredits -= currentBet;  // Deduct bet from credits
        UpdateUI();

        // Spin reels
        SpinReels();

        StartCoroutine(RevertButtonImageAfterDelay(0.2f));
    }

    IEnumerator RevertButtonImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonImage.sprite = buttonNormalSprite;
    }



    public void SpinReels()
    {
        foreach (Reel reel in reels)
        {
            reel.ShowRandomSymbol();  // Spin each reel to a random symbol
        }

    }
}


