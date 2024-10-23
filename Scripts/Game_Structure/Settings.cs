using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace ClanGenDotNet.Scripts.Game_Structure;

public class Settings
{
	[JsonProperty("general", Required = Required.Always)]
	/*public Dictionary<string, List<General>> General { get; set; }*/
	public Dictionary<string, List<object>> General { get; set; } = [];

	/*[JsonProperty("__other", Required = Required.Always)]
	public Other Other { get; set; }*/
	[JsonProperty("__other", Required = Required.Always)]
	public Dictionary<string, List<object>> Other { get; set; } = [];
}

public partial class Other
{
	[JsonProperty("language", Required = Required.Always)]
	public required List<string> Language { get; set; }

	[JsonProperty("text size", Required = Required.Always)]
	[JsonConverter(typeof(DecodeArrayConverter))]
	public required List<long> TextSize { get; set; }

	[JsonProperty("fullscreen", Required = Required.Always)]
	public required List<bool> Fullscreen { get; set; }

	[JsonProperty("music_volume", Required = Required.Always)]
	public required List<long> MusicVolume { get; set; }

	[JsonProperty("sound_volume", Required = Required.Always)]
	public required List<long> SoundVolume { get; set; }

	[JsonProperty("audio_mute", Required = Required.Always)]
	public required List<bool> AudioMute { get; set; }
}

public partial struct General
{
	public bool? Bool;
	public string String;

	public static implicit operator General(bool Bool)
	{
		return new General { Bool = Bool };
	}

	public static implicit operator General(string String)
	{
		return new General { String = String };
	}

	public override string ToString()
	{
		return $"General Setting {String}: {Bool}";
	}
}

internal static class Converter
{
	public static readonly JsonSerializerSettings Settings = new()
	{
		MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
		DateParseHandling = DateParseHandling.None,
		Converters =
		{
			GeneralConverter.Singleton,
			new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
		},
	};
}

internal class DecodeArrayConverter : JsonConverter
{
	public override bool CanConvert(Type t)
	{
		return t == typeof(List<long>);
	}

	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	{
		_ = reader.Read();
		List<long> value = [];
		while (reader.TokenType != JsonToken.EndArray)
		{
			ParseStringConverter converter = ParseStringConverter.Singleton;
			long arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
			value.Add(arrayItem);
			_ = reader.Read();
		}
		return value;
	}

	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	{
		List<long> value = (List<long>)untypedValue;
		writer.WriteStartArray();
		foreach (long arrayItem in value)
		{
			ParseStringConverter converter = ParseStringConverter.Singleton;
			converter.WriteJson(writer, arrayItem, serializer);
		}
		writer.WriteEndArray();
		return;
	}

	public static readonly DecodeArrayConverter Singleton = new();
}

internal class ParseStringConverter : JsonConverter
{
	public override bool CanConvert(Type t)
	{
		return t == typeof(long) || t == typeof(long?);
	}

	public override object? ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
		{
			return null;
		}

		string? value = serializer.Deserialize<string>(reader);
		return long.TryParse(value, out long l) ? (object)l : throw new Exception("Cannot unmarshal type long");
	}

	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	{
		if (untypedValue == null)
		{
			serializer.Serialize(writer, null);
			return;
		}
		long value = (long)untypedValue;
		serializer.Serialize(writer, value.ToString());
		return;
	}

	public static readonly ParseStringConverter Singleton = new();
}

internal class GeneralConverter : JsonConverter
{
	public override bool CanConvert(Type t)
	{
		return t == typeof(General) || t == typeof(General?);
	}

	public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
	{
		switch (reader.TokenType)
		{
			case JsonToken.Boolean:
				bool boolValue = serializer.Deserialize<bool>(reader);
				return new General { Bool = boolValue };
			case JsonToken.String:
			case JsonToken.Date:
				string? stringValue = serializer.Deserialize<string>(reader);
				return new General { String = stringValue };
		}
		throw new Exception("Cannot unmarshal type General");
	}

	public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
	{
		General value = (General)untypedValue;
		if (value.Bool != null)
		{
			serializer.Serialize(writer, value.Bool.Value);
			return;
		}
		if (value.String != null)
		{
			serializer.Serialize(writer, value.String);
			return;
		}
		throw new Exception("Cannot marshal type General");
	}

	public static readonly GeneralConverter Singleton = new();
}