internal class Program
{
    private const int columnsCount = 5;
    private const int rowsCount = 5;

    private static readonly PointState[,] _visualTable = new PointState[columnsCount, rowsCount];

    private static void Main(string[] args)
    {
        var random = new Random();
        var targetPosition = new { columnNumber = random.Next(1, columnsCount), rowNumber = random.Next(1, rowsCount) };

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
                _visualTable[rowIndex, columnIndex] = PointState.IsTarget;
                RenderTable();
                Console.WriteLine("YOU WIN!");
                break;
            }
            else
            {
                _visualTable[rowIndex, columnIndex] = PointState.IsEmpty;
                Console.Clear();
            }
        }
    }

    private static void RenderTable()
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
                Console.Write(GetMarkPoint(_visualTable![i, j]).ToString());
                Console.Write(j < columnsCount ? " " : "");
            }

            Console.WriteLine();
        }
    }

    private static void TryEnterNumber(out int inputBuffer, int minNumber, int maxNumber)
    {
        if (!int.TryParse(Console.ReadLine(), out inputBuffer) ||
            inputBuffer < minNumber ||
            inputBuffer > maxNumber)
        {
            Console.Write("Invalid number input, please try again: ");
            TryEnterNumber(out inputBuffer, minNumber, maxNumber);
        }
    }


    private static void TryEnterNumber(out int inputBuffer, int maxNumber)
    {
        TryEnterNumber(out inputBuffer, 0, maxNumber);
    }

    private static char GetMarkPoint(PointState pointState)
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