using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{

	public List<Card> deck;
	public TextMeshProUGUI deckSizeText;

	public Transform[] cardSlots;
	public bool[] availableCardSlots;

	public List<Card> discardPile;
	public TextMeshProUGUI discardPileSizeText;

	private Animator camAnim;

	public GameObject prefabToInstantiate;
	public int numberOfCards = 3;
    public int cardInHand = 0;
    public float delayBeforeRestart = 1f;
	private List<GameObject> clones = new List<GameObject>();
	public float startTime, elapsedTime;




	private void Start()
	{
		camAnim = Camera.main.GetComponent<Animator>();
		for(int i = 0; i <= 3; i++)
		{
			Invoke("DrawCard",0f);
		}
		startTime = Time.time;
	}

	public void DrawCard()
	{
		if (deck.Count >= 1)
		{
			Card randomCard = deck[Random.Range(0, deck.Count)];
			for (int i = 0; i < availableCardSlots.Length; i++)
			{
				if (availableCardSlots[i] == true)
				{
					randomCard.gameObject.SetActive(true);
					randomCard.handIndex = i;
					randomCard.transform.position = cardSlots[i].position;
					randomCard.hasBeenPlayed = false;
					deck.Remove(randomCard);
					availableCardSlots[i] = false;

					cardInHand++;
					return;
				}
			}
		}
	}

	public void Shuffle()
	{
		if (discardPile.Count >= 1)
		{
			foreach (Card card in discardPile)
			{
				deck.Add(card);
			}
			discardPile.Clear();
		}
	}

	private void Update()
	{
		deckSizeText.text = deck.Count.ToString();
		discardPileSizeText.text = discardPile.Count.ToString();
		if (deck.Count <= 3)
		{
			Invoke("Shuffle",0.0f);
		}
		elapsedTime = Time.time - startTime;
		if (elapsedTime >= 5f)
        {
			startTime = elapsedTime;

        }
		if(cardInHand < 3)
		{
			Invoke("DrawCard",0.5f);
		}
	}
}
