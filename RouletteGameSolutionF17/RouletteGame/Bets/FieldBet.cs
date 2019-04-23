using RouletteGame.Fields;

namespace RouletteGame.Bets
{
    public class FieldBet : Bet
    {
        private readonly uint _fieldNumber;

        public FieldBet(string name, uint amount, uint fieldNumber) : base(name, amount)
        {
            _fieldNumber = fieldNumber;
        }

        public override uint WonAmount(IField field)
        {
            if (field.Number == _fieldNumber) return 36*Amount;
            return 0;
        }

        public override string ToString()
        {
            return $"{Amount}$ field bet on {_fieldNumber}";
        }
    }
}