using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotGameController : MonoBehaviour
{
    public Reel[] reels;  // Array to puassign the reel in
    public Button spinButton;
    public Sprite buttonNormalSprite;
    public Sprite buttonPressedSprite;
    private Image buttonImage;

    public int playerCredits = 1000;
    public int currentBet = 10;
    public Text creditsText;
    public Text betText;
    public Text resultText;
    public int ResultDelay = 1; // Delay before showing result

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
        buttonImage.sprite = buttonPressedSprite;

        if (playerCredits < currentBet)
        {
            resultText.text = "Not enough credits!";
            StartCoroutine(RevertButtonImageAfterDelay(0.2f));
            return;
        }

        playerCredits -= currentBet;
        UpdateUI();

        StartCoroutine(SpinAllReels());
    }

    IEnumerator SpinAllReels()
    {
        // Start spinning each reel with delay
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].StartCoroutine(reels[i].SpinAnimation(i * 0.5f));
        }

        // Wait until all reels almost finish spinning
        yield return new WaitUntil(() => AllReelsStopped());

        // Collect result symbols
        string[] resultSymbols = new string[reels.Length];
        for (int i = 0; i < reels.Length; i++)
        {
            resultSymbols[i] = reels[i].GetCurrentSymbolName();
        }

        yield return new WaitForSeconds(ResultDelay);

        // Calculate payout and update credits and UI
        int payout = CalculatePayout(resultSymbols);
        playerCredits += payout;
        UpdateUI();

        resultText.text = payout > 0 ? $"YOU WIN! +{payout}" : "No win, try again!";

        StartCoroutine(RevertButtonImageAfterDelay(0.2f));
    }

    private bool AllReelsStopped()
    {
        foreach (var reel in reels)
        {
            if (reel.isSpinning)
                return false;
        }
        return true;
    }

    IEnumerator RevertButtonImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonImage.sprite = buttonNormalSprite;
    }

    public int CalculatePayout(string[] symbols)
    {
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
