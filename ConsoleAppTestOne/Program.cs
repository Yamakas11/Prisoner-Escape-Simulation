public static class EnumerableExtensions
{
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var arr = source.ToArray();
        var random = new Random();

        for (var i = arr.Length - 1; i > 0; i--)
        {
            var idx = random.Next(i + 1);
            (arr[i], arr[idx]) = (arr[idx], arr[i]);
        }
        return arr;
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        int winCount = 0;
        int loseCount = 0;

        for (var i = 0; i < 100; i++)
        {

            List<int> peopleList = GenerateAndShuffleNumbers(100).ToList();
            List<int> boxList = GenerateAndShuffleNumbers(100).ToList();

            List<bool> result = CheckIfAllPrisonersEscaped(peopleList, boxList);

            if (result.All(x => x))
                winCount++;
            else
                loseCount++;   
        }

        Console.WriteLine($"Win = {winCount};\nLose = {loseCount};");
    }

    private static List<bool> CheckIfAllPrisonersEscaped(List<int> prisonerNumbers, List<int> boxNumbers)
    {
        var result = new List<bool>();

        foreach (var numberPeople in prisonerNumbers)
        {
            int currentIndex = numberPeople; 
            bool isFound = false;

            for (int j = 0; j < 50; j++)
            {
                if (boxNumbers[currentIndex - 1] == numberPeople)
                {
                    result.Add(true);
                    isFound = true;
                    break;
                }
                else
                    currentIndex = boxNumbers[currentIndex - 1];
            }

            if (!isFound)
                result.Add(false);
        }

        return result;
    }

    private static IEnumerable<int> GenerateAndShuffleNumbers(int count)
    {
        var numbers = Enumerable.Range(1, count);
        return numbers.Shuffle();
    }
}
