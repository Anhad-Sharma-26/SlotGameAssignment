using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotGameController : MonoBehaviour
{
    public Reel[] reels;  // Assign three Reel instances here
    public Button spinButton; // Reference to your UI Button
    public Sprite buttonNormalSprite; // Normal button sprite
    public Sprite buttonPressedSprite; // Pressed button sprite
    private Image buttonImage;

    public int playerCredits = 1000; // Starting credits
    public int currentBet = 10;      // Default bet amount
    public Text creditsText;         // UI element for credits
    public Text betText;             // UI element for bet
    public Text resultText;          // UI element for result messages

    void Start()
    {
        buttonImage = spinButton.GetComponent<Image>();
        buttonImage.sprite = buttonNormalSprite;
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
        // Visual button press effect
        buttonImage.sprite = buttonPressedSprite;

        if (playerCredits < currentBet)
        {
            resultText.text = "Not enough credits!";
            StartCoroutine(RevertButtonImageAfterDelay(0.2f));
            return;
        }

        playerCredits -= currentBet;
        UpdateUI();

        // Spin reels and collect results
        string[] resultSymbols = new string[reels.Length];
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].ShowRandomSymbol();
            resultSymbols[i] = reels[i].GetCurrentSymbolName();
        }

        // Payout evaluation
        int payout = CalculatePayout(resultSymbols);
        playerCredits += payout;
        UpdateUI();

        if (payout > 0)
            resultText.text = $"YOU WIN! +{payout}";
        else
            resultText.text = "No win, try again!";

        StartCoroutine(RevertButtonImageAfterDelay(0.2f));
    }

    IEnumerator RevertButtonImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonImage.sprite = buttonNormalSprite;
    }

    public int CalculatePayout(string[] symbols)
    {
        // Basic paytable
        if (symbols[0] == "Seven" && symbols[1] == "Seven" && symbols[2] == "Seven")
            return currentBet * 100;
        if (symbols[0] == "Cherry" && symbols[1] == "Cherry" && symbols[2] == "Cherry")
            return currentBet * 20;

        int cherryCount = 0;
        foreach (string s in symbols)
        {
            if (s == "Cherry") cherryCount++;
        }

        if (cherryCount == 2)
            return currentBet * 3;

        return 0;
    }
}
