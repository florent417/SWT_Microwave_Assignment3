using RouletteGame.Fields;

namespace RouletteGame.Bets
{
    public abstract class Bet : IBet
    {
        protected Bet(string name, uint amount)
        {
            PlayerName = name;
            Amount = amount;
        }


        public string PlayerName { get; }

        public uint Amount { get; }


        public abstract uint WonAmount(IField field);
    }
}