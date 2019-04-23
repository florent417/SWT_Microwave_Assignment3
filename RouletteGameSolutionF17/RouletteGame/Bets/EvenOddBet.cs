using RouletteGame.Fields;

namespace RouletteGame.Bets
{
    public class EvenOddBet : Bet
    {
        public Parity Parity { private get; set; }

        public EvenOddBet(string name, uint amount, Parity parity) : base(name, amount)
        {
            Parity = parity;
        }

        public override uint WonAmount(IField field)
        {
            if (field.Parity == Parity) return 2*Amount;
            return 0;
        }

        public override string ToString()
        {
            string parityString="";
            switch (Parity)
            {
                case Parity.Even:
                    parityString = "Even";
                    break;
                case Parity.Odd:
                    parityString = "Odd";
                    break;
                case Parity.Neither:
                    parityString = "Neither";
                    break;

            }

            return $"{Amount}$ even/odd bet on {parityString}";
        }
    }
}