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
				return source.ElementAt(i);
			}
		}

		return source.First();
	}

	public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
	{
		return source.OrderBy(x => Guid.NewGuid());
	}
}
