internal class Program
{
    private static void Main(string[] args)
    {
        const int columnsCount = 5;
        const int rowsCount = 5;

        var random = new Random();
        var targetPosition = (columnNumber: random.Next(1, columnsCount), rowNumber: random.Next(1, rowsCount));
        var visualTable = new PointState[columnsCount, rowsCount];

        while (true)
        {
            Console.WriteLine("All set. Get ready to rumble.");
            RenderTable();
            Console.Write("Please enter a column number: ");
            int columnNumber;
            TryEnterNumber(out columnNumber, 1, columnsCount);
            int columnIndex = columnNumber - 1;

            Console.Write("Please enter a row number: ");
            int rowNumber;
            TryEnterNumber(out rowNumber, 1, rowsCount);
            int rowIndex = rowNumber - 1;

            if (columnNumber == targetPosition.columnNumber && rowNumber == targetPosition.rowNumber)
            {
                Console.Clear();
                visualTable[rowIndex, columnIndex] = PointState.IsTarget;
                RenderTable();
                Console.WriteLine("You WIN!");
                break;
            }
            else
            {
                visualTable[rowIndex, columnIndex] = PointState.IsEmpty;
                Console.Clear();
            }
        }

        void RenderTable()
        {
            for (var i = 0; i <= columnsCount; i++)
            {
                Console.Write(i + (i < columnsCount ? " " : ""));
            }

            Console.WriteLine();

            for (var i = 0; i < rowsCount; i++)
            {
                Console.Write($"{i + 1} ");

                for (var j = 0; j < columnsCount; j++)
                {
                    Console.Write(GetMarkPoint(visualTable![i, j]).ToString());
                    Console.Write(j < columnsCount ? " " : "");
                }

                Console.WriteLine();
            }
        }
    }

    static void TryEnterNumber(out int inputBuffer, int minNumber, int maxNumber)
    {
        if (!int.TryParse(Console.ReadLine(), out inputBuffer) ||
            inputBuffer < minNumber ||
            inputBuffer > maxNumber)
        {
            Console.Write("Invalid input, please try again: ");
            TryEnterNumber(out inputBuffer, minNumber, maxNumber);
        }
    }


    static void TryEnterNumber(out int inputBuffer, int maxNumber)
    {
        TryEnterNumber(out inputBuffer, 0, maxNumber);
    }

    static char GetMarkPoint(PointState pointState)
    {
        switch (pointState)
        {
            case PointState.Indefinite:
                {
                    return '-';
                }
            case PointState.IsTarget:
                {
                    return 'X';
                }
            case PointState.IsEmpty:
                {
                    return '*';
                }
            default:
                {
                    throw new NotImplementedException();
                }
        }
    }
}

enum PointState
{
    Indefinite,
    IsEmpty,
    IsTarget
}