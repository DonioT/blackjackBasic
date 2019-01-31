using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjack
{
    public enum Suit
    {
        Hearts,
        Spades,
        Clubs,
        Diamonds
    }
    public enum CardValue
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public class Card
    {
        public Suit Suit { get; set; }
        public CardValue CardValue { get; set; }
        public int CardScore { get; set; }
    }

    public class CardHand
    {
        public String Name { get; set; }
        public List<Card> Cards { get; set; }
        public int HandScore { get; set; }
        public Boolean Dealer { get; set; } = false;
    }
    public class Program
    {

        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.UnitTest(instance.GenerateTestObjects());
            Console.Read();
        }

        public Boolean clientWins(CardHand dealerHand, CardHand clientHand)
        {
            Boolean clientWins = false;

            if (clientHand.HandScore >= dealerHand.HandScore && clientHand.HandScore <= 21 || clientHand.Cards.Count() > 4 && clientHand.HandScore <= 21)
            {
                clientWins = true;
            }
            return clientWins;
        }

        public int HandValueCheck(List<Card> hand)
        {
            //calculate card values
            foreach (Card c in hand)
            {
                switch (c.CardValue)
                {
                    case CardValue.Ace:
                        c.CardScore = 11; break;
                    case CardValue.Two:
                        c.CardScore = 2; break;
                    case CardValue.Three:
                        c.CardScore = 3; break;
                    case CardValue.Four:
                        c.CardScore = 4; break;
                    case CardValue.Five:
                        c.CardScore = 5; break;
                    case CardValue.Six:
                        c.CardScore = 6; break;
                    case CardValue.Seven:
                        c.CardScore = 7; break;
                    case CardValue.Eight:
                        c.CardScore = 8; break;
                    case CardValue.Nine:
                        c.CardScore = 9; break;
                    case CardValue.Ten:
                        c.CardScore = 10; break;
                    case CardValue.Jack:
                        c.CardScore = 10; break;
                    case CardValue.Queen:
                        c.CardScore = 10; break;
                    case CardValue.King:
                        c.CardScore = 10; break;
                }
            }

            int handValue = hand.Sum(e => e.CardScore);
            if (handValue > 21)
            {
                int fullAce = hand.Count(e => e.CardValue == CardValue.Ace);
                while (handValue > 21 && fullAce > 0)
                {
                    handValue = handValue - 10;
                    fullAce--;
                }
            }
            return handValue;
        }

        public List<CardHand> GenerateTestObjects()
        {

            //Create Dealer Object
            List<Card> dealerHand = new List<Card>
            {
                new Card {CardValue = CardValue.Jack, Suit = Suit.Spades },
                new Card {CardValue = CardValue.Nine, Suit = Suit.Diamonds }
            };
            CardHand dealer = new CardHand { Cards = dealerHand, HandScore = HandValueCheck(dealerHand), Name = "Dealer", Dealer = true };

            //Create Carla object
            List<Card> carlaHand = new List<Card>
            {
                new Card {CardValue = CardValue.Queen, Suit = Suit.Clubs},
                new Card {CardValue = CardValue.Ace, Suit = Suit.Spades },
                new Card {CardValue = CardValue.Nine, Suit = Suit.Diamonds }
                
            };
            CardHand carla = new CardHand { Cards = carlaHand, HandScore = HandValueCheck(carlaHand), Name = "Carla" };

            //Create Andrew Object
            List<Card> andrewHand = new List<Card>
            {
                new Card {CardValue = CardValue.King, Suit = Suit.Diamonds},
                new Card {CardValue = CardValue.Four, Suit = Suit.Spades },
                new Card {CardValue = CardValue.Four, Suit = Suit.Clubs }
            };
            CardHand andrew = new CardHand { Cards = andrewHand, HandScore = HandValueCheck(andrewHand), Name = "Andrew" };

            //Create Lemmy Object
            List<Card> lemmyHand = new List<Card>
            {
                new Card {CardValue = CardValue.Ace, Suit = Suit.Spades},
                new Card {CardValue = CardValue.Nine, Suit = Suit.Hearts},
                new Card {CardValue = CardValue.Ace, Suit = Suit.Diamonds},
            };
            CardHand lemmy = new CardHand { Cards = lemmyHand, HandScore = HandValueCheck(lemmyHand), Name = "Lemmy" };

            //Create Billy Object
            List<Card> billyHand = new List<Card>
            {
                new Card {CardValue = CardValue.Two, Suit = Suit.Spades},
                new Card {CardValue = CardValue.Two, Suit = Suit.Diamonds},
                new Card {CardValue = CardValue.Two, Suit = Suit.Hearts},
                new Card {CardValue = CardValue.Four, Suit = Suit.Diamonds},
                new Card {CardValue = CardValue.Five, Suit = Suit.Clubs},
            };
            CardHand billy = new CardHand { Cards = billyHand, HandScore = HandValueCheck(billyHand), Name = "Billy" };

            List<CardHand> listOfHands = new List<CardHand>
            {
                dealer,
                billy,
                lemmy,
                andrew,
                carla
            };

            return listOfHands;
        }

        public void UnitTest(List<CardHand> listOfHands)
        {
            CardHand dealerHand = listOfHands.Where(e => e.Dealer).FirstOrDefault();

            //output Dealer Text
            Console.WriteLine("---" + dealerHand.Name + "---");
            foreach (Card card in dealerHand.Cards)
            {
                Console.WriteLine(card.CardValue + " of " + card.Suit);
            }
            Console.WriteLine("Score: " + dealerHand.HandScore + "\n");

            List<CardHand> clientHands = listOfHands.Where(e => !(e.Dealer)).ToList();

            //Output client text
            foreach (CardHand cardHand in clientHands)
            {
                Console.WriteLine("---" + cardHand.Name + "---");
                foreach (Card card in cardHand.Cards)
                {
                    Console.WriteLine(card.CardValue + " of " + card.Suit);
                }
                Console.WriteLine("Score: " + cardHand.HandScore);
                if (clientWins(dealerHand, cardHand))
                {
                    Console.WriteLine("Beats Dealer\n");
                }
                else
                {
                    Console.WriteLine("Loses \n");
                }
            }
        }
    }
}

