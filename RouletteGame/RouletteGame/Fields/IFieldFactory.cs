using System.Collections.Generic;

namespace RouletteGame.Fields
{
    public interface IFieldFactory
    {
        List<IField> CreateFields();
    }
}