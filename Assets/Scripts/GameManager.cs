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
	public float startTime;
	public float elapsedTime;
	public GameObject[] cards;
	bool waitForNew = false;
	private IEnumerator coroutine;

	




	private void Start()
	{
		camAnim = Camera.main.GetComponent<Animator>();
		startTime = Time.realtimeSinceStartup;
		coroutine = delayDraw(2.0f);
	}

	public void DrawCard()
	{
		if (deck.Count >= 1)
		{
			camAnim.SetTrigger("shake");

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

	private void FixedUpdate()
	{
		deckSizeText.text = deck.Count.ToString();
		discardPileSizeText.text = discardPile.Count.ToString();
		if (deck.Count <= 3)
		{
			Invoke("Shuffle",0.0f);
		}
		if (cardInHand < 3 && !waitForNew)
        {
            Invoke("DrawCard",0.5f);
        }
        elapsedTime = Time.realtimeSinceStartup - startTime;
		if(elapsedTime >= 5f)
		{
			//Invoke("timeToDestroy",0.5f);
		}
	}
	private void timeToDestroy()
	{
		waitForNew = true;
		StartCoroutine(coroutine);

		for(int i = 0; i < cards.Length; i++)
		{
			if(cards[i].activeSelf)
			{
				Debug.Log(cards[i]);
				cards[i].GetComponent<Card>().destroyCard();
				if(cards.Length >=3)
				{
					elapsedTime = 0f;
					startTime = Time.realtimeSinceStartup;
				}
			}
		}
		
	}
	private IEnumerator delayDraw(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		waitForNew = false;
	}
	
}
