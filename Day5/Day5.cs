class Day5
{
    static void Main(string[] args) {
        string filePath = "Day5Input.txt";
        string[] lines = File.ReadAllLines(filePath);
        Console.WriteLine(SafetyManualOrder(lines, false));
    }

    public static int SafetyManualOrder(string[] input, bool sortedOnly) {
        var (rules, updates) = ParseInput(input);
        int total = updates.Select(update => {
            string original = string.Join(',', update);
            var sorted = update.ToList();
            sorted.Sort((x, y) =>{
                if (rules.FirstOrDefault(r => r[0] == x && r[1] == y) != null) {
                    return -1;
                }
                if (rules.FirstOrDefault(r => r[0] == y && r[1] == x) != null) {
                    return 1;
                }
                return 0;
            });
            bool hasBeenSorted = original != string.Join(',', sorted);
            if (hasBeenSorted == sortedOnly) {
                return 0;
            }
            int midIndex = (int)Math.Floor(sorted.Count / 2d);
            return sorted[midIndex];
        }).Sum();

        return total;   
    }

    public static Tuple<int[][], int[][]> ParseInput(string[] input)
    { // Split the input strings into rules and updates, return as a Tuple Jagged Array
        var resplit = string.Join("\n", input).Split("\n\n");
        var rules = resplit[0].Split("\n")
            .Select(x => x.Split("|").Select(int.Parse).ToArray())
            .ToArray();
        var updates = resplit[1].Split("\n").Select(x => x.Split(",").Select(int.Parse).ToArray()).ToArray();
        return Tuple.Create(rules, updates);
    }
}
