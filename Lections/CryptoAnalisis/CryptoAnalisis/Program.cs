var text = File.ReadAllText("ШИФРОТЕКСТ.txt");

//Dictionary<string, List<int>> groups = new();

//for (int i = 0; i < text.Length - 2; i++)
//{
//    var key = text.Substring(i, 3);

//    if (!groups.ContainsKey(key))
//        groups.Add(key, new List<int>());

//    groups[key].Add(i);
//}

//foreach (var group in groups.Where(x => x.Value.Count > 1).OrderByDescending(x => x.Value.Count))
//{
//    Console.WriteLine($"{group.Key}:");
//    foreach (var item in group.Value)
//        Console.Write($"{item} ,");
//    Console.WriteLine();

//    var indexes = group.Value;

//    for (int i = 1; i < indexes.Count; i++)
//    {
//        Console.WriteLine(indexes[i] - indexes[i - 1]);
//    }
//}

Dictionary<char, int> freq = new();

for (int i = 0; i < text.Length; i += 5)
{
    if (!freq.ContainsKey(text[i]))
        freq[text[i]] = 0;

    freq[text[i]]++;

}

foreach (var item in freq.OrderByDescending(x=>x.Value).Take(3))
{
    Console.WriteLine(item.Key);
}