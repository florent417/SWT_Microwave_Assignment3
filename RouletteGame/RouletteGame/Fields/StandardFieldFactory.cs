using System.Collections.Generic;

namespace RouletteGame.Fields
{
    public class StandardFieldFactory : IFieldFactory
    {
        public List<IField> CreateFields()
        {
            var fields = new List<IField>
            {
                new Field(0, FieldColor.Green),
                new Field(1, FieldColor.Red),
                new Field(2, FieldColor.Black),
                new Field(3, FieldColor.Red),
                new Field(4, FieldColor.Black),
                new Field(5, FieldColor.Red),
                new Field(6, FieldColor.Black),
                new Field(7, FieldColor.Red),
                new Field(8, FieldColor.Black),
                new Field(9, FieldColor.Red),
                new Field(10, FieldColor.Black),
                new Field(11, FieldColor.Black),
                new Field(12, FieldColor.Red),
                new Field(13, FieldColor.Black),
                new Field(14, FieldColor.Red),
                new Field(15, FieldColor.Black),
                new Field(16, FieldColor.Red),
                new Field(17, FieldColor.Black),
                new Field(18, FieldColor.Red),
                new Field(19, FieldColor.Red),
                new Field(20, FieldColor.Black),
                new Field(21, FieldColor.Red),
                new Field(22, FieldColor.Black),
                new Field(23, FieldColor.Red),
                new Field(24, FieldColor.Black),
                new Field(25, FieldColor.Red),
                new Field(26, FieldColor.Black),
                new Field(27, FieldColor.Red),
                new Field(28, FieldColor.Black),
                new Field(29, FieldColor.Black),
                new Field(30, FieldColor.Red),
                new Field(31, FieldColor.Black),
                new Field(32, FieldColor.Red),
                new Field(33, FieldColor.Black),
                new Field(34, FieldColor.Red),
                new Field(35, FieldColor.Black),
                new Field(36, FieldColor.Red)
            };

            return fields;
        }
    }
}