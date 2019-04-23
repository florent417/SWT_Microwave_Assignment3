using RouletteGame.Bets;

namespace RouletteGame.Game
{
    public interface IGame
    {
        void OpenBets();
        void CloseBets();
        void PlaceBet(IBet bet);
        void SpinRoulette();
        void PayUp();
    }
}