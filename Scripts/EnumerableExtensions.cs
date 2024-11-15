namespace ClanGenDotNet.Scripts;

public static class EnumerableExtension
{
	public static T PickRandom<T>(this IEnumerable<T> source)
	{
		return source.PickRandom(1).Single();
	}

	public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
	{
		return source.Shuffle().Take(count);
	}

	//improve this
	public static T PickRandomWeighted<T>(this IEnumerable<T> source, int[] weights)
	{
		int randomWeight = Rand.Next(0, weights.Sum());
		for (int i = 0; i < weights.Length; ++i)
		{
			randomWeight -= weights[i];

			if (randomWeight < 0)
			{
				if (i >= source.Count())
				{
					return source.ElementAt(Rand.Next(0, source.Count()));
				}
				return source.ElementAt(i);
			}
		}

		return source.First();
	}

	//pulls a random list of n items.
	public static IEnumerable<T> Sample<T>(this IEnumerable<T> sample, int number)
	{
		return sample.Shuffle().Take(number);
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
		return source.OrderBy(x => Guid.NewGuid());
	}

	public static Vector2 Vector2Ify(this IList<int> source)
	{
		return new Vector2(source[0], source[1]);
	}
}
