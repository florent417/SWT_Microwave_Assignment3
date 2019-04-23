using RouletteGame.Fields;

namespace RouletteGame.Bets
{
    public interface IBet
    {
        string PlayerName { get; }
        uint Amount { get; }
        uint WonAmount(IField field);
    }
}