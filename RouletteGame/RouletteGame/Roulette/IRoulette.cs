using RouletteGame.Fields;

namespace RouletteGame.Roulette
{
    public interface IRoulette
    {
        void Spin();
        IField GetResult();
    }
}