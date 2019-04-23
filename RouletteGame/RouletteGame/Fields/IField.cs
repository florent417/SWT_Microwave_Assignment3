namespace RouletteGame.Fields
{
    public enum FieldColor
    {
        Red,
        Black,
        Green
    }

    public enum Parity
    {
        Even,
        Odd,
        Neither
    }

    public interface IField
    {
        uint Number { get; }
        FieldColor Color { get; }
        Parity Parity { get; }
        string ToString();
    }
}