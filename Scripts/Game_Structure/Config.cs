namespace ClanGenDotNet.Scripts.Game_Structure
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class Config
	{
		[JsonProperty("relationship", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Relationship Relationship { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("mates", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Mates Mates { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("new_cat", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public NewCat NewCat { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("pregnancy", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Pregnancy Pregnancy { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat_generation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CatGeneration CatGeneration { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat_name_controls", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CatNameControls CatNameControls { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("accessory_generation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public AccessoryGeneration AccessoryGeneration { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("fading", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Fading Fading { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("roles", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Roles Roles { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("lost_cat", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public LostCat LostCat { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat_ages", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CatAges CatAges { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat_sprites", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public CatSprites CatSprites { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("patrol_generation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public PatrolGeneration PatrolGeneration { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("event_generation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public EventGeneration EventGeneration { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("death_related", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public DeathRelated DeathRelated { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("condition_related", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ConditionRelated ConditionRelated { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("clan_creation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ClanCreation ClanCreation { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("graduation", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Graduation Graduation { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("outside_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public OutsideEx OutsideEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("focus", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Focus Focus { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("save_load", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public SaveLoad SaveLoad { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("sorting", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Sorting Sorting { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("fun", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Fun Fun { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("theme", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Theme Theme { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("lock_season", Required = Required.Always)]
		public bool LockSeason { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class AccessoryGeneration
	{
		[JsonProperty("base_acc_chance", Required = Required.Always)]
		public int BaseAccChance { get; set; }

		[JsonProperty("med_modifier", Required = Required.Always)]
		public int MedModifier { get; set; }

		[JsonProperty("baby_modifier", Required = Required.Always)]
		public int BabyModifier { get; set; }

		[JsonProperty("elder_modifier", Required = Required.Always)]
		public int ElderModifier { get; set; }

		[JsonProperty("happy_trait_modifier", Required = Required.Always)]
		public int HappyTraitModifier { get; set; }

		[JsonProperty("grumpy_trait_modifier", Required = Required.Always)]
		public int GrumpyTraitModifier { get; set; }

		[JsonProperty("ceremony_modifier", Required = Required.Always)]
		public int CeremonyModifier { get; set; }
	}

	public partial class CatAges
	{
		[JsonProperty("newborn", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> Newborn { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("kitten", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> Kitten { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("adolescent", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> Adolescent { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("young adult", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> YoungAdult { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("adult", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> Adult { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("senior adult", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> SeniorAdult { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("senior", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> Senior { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class CatGeneration
	{
		[JsonProperty("base_permanent_condition", Required = Required.Always)]
		public int BasePermanentCondition { get; set; }

		[JsonProperty("base_male_tortie", Required = Required.Always)]
		public int BaseMaleTortie { get; set; }

		[JsonProperty("base_female_tortie", Required = Required.Always)]
		public int BaseFemaleTortie { get; set; }

		[JsonProperty("base_heterochromia", Required = Required.Always)]
		public int BaseHeterochromia { get; set; }

		[JsonProperty("direct_inheritance", Required = Required.Always)]
		public int DirectInheritance { get; set; }

		[JsonProperty("wildcard_tortie", Required = Required.Always)]
		public int WildcardTortie { get; set; }

		[JsonProperty("vit_chance", Required = Required.Always)]
		public int VitChance { get; set; }

		[JsonProperty("random_point_chance", Required = Required.Always)]
		public int RandomPointChance { get; set; }
	}

	public partial class CatNameControls
	{
		[JsonProperty("always_name_after_appearance", Required = Required.Always)]
		public bool AlwaysNameAfterAppearance { get; set; }

		[JsonProperty("allow_eye_names", Required = Required.Always)]
		public bool AllowEyeNames { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class CatSprites
	{
		[JsonProperty("sick_sprites", Required = Required.Always)]
		public bool SickSprites { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class ClanCreation
	{
		[JsonProperty("rerolls", Required = Required.Always)]
		public int Rerolls { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class ConditionRelated
	{
		[JsonProperty("expanded_illness_chance", Required = Required.Always)]
		public int ExpandedIllnessChance { get; set; }

		[JsonProperty("cruel season_illness_chance", Required = Required.Always)]
		public int CruelSeasonIllnessChance { get; set; }

		[JsonProperty("classic_illness_chance", Required = Required.Always)]
		public int ClassicIllnessChance { get; set; }

		[JsonProperty("classic_injury_chance", Required = Required.Always)]
		public int ClassicInjuryChance { get; set; }

		[JsonProperty("expanded_injury_chance", Required = Required.Always)]
		public int ExpandedInjuryChance { get; set; }

		[JsonProperty("cruel season_injury_chance", Required = Required.Always)]
		public int CruelSeasonInjuryChance { get; set; }

		[JsonProperty("permanent_condition_chance", Required = Required.Always)]
		public int PermanentConditionChance { get; set; }

		[JsonProperty("war_injury_modifier", Required = Required.Always)]
		public int WarInjuryModifier { get; set; }
	}

	public partial class DeathRelated
	{
		[JsonProperty("leader_death_chance", Required = Required.Always)]
		public int LeaderDeathChance { get; set; }

		[JsonProperty("classic_death_chance", Required = Required.Always)]
		public int ClassicDeathChance { get; set; }

		[JsonProperty("expanded_death_chance", Required = Required.Always)]
		public int ExpandedDeathChance { get; set; }

		[JsonProperty("cruel season_death_chance", Required = Required.Always)]
		public int CruelSeasonDeathChance { get; set; }

		[JsonProperty("war_death_modifier_leader", Required = Required.Always)]
		public int WarDeathModifierLeader { get; set; }

		[JsonProperty("war_death_modifier", Required = Required.Always)]
		public int WarDeathModifier { get; set; }

		[JsonProperty("base_random_murder_chance", Required = Required.Always)]
		public int BaseRandomMurderChance { get; set; }

		[JsonProperty("base_murder_kill_chance", Required = Required.Always)]
		public int BaseMurderKillChance { get; set; }

		[JsonProperty("old_age_death_start", Required = Required.Always)]
		public int OldAgeDeathStart { get; set; }

		[JsonProperty("old_age_death_curve", Required = Required.Always)]
		public double OldAgeDeathCurve { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class EventGeneration
	{
		[JsonProperty("debug_ensure_event_id", Required = Required.AllowNull)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public object DebugEnsureEventId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Fading
	{
		[JsonProperty("age_to_fade", Required = Required.Always)]
		public int AgeToFade { get; set; }

		[JsonProperty("opacity_at_fade", Required = Required.Always)]
		public int OpacityAtFade { get; set; }

		[JsonProperty("visual_fading_speed", Required = Required.Always)]
		public int VisualFadingSpeed { get; set; }
	}

	public partial class Focus
	{
		[JsonProperty("duration", Required = Required.Always)]
		public int Duration { get; set; }

		[JsonProperty("hunting", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Hunting Hunting { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("herb gathering", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public HerbGathering HerbGathering { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("outsiders", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Outsiders Outsiders { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("other clans", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public OtherClans OtherClans { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("raid other clans", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Hoarding RaidOtherClans { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("hoarding", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Hoarding Hoarding { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("rest and recover", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public RestAndRecover RestAndRecover { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class HerbGathering
	{
		[JsonProperty("med", Required = Required.Always)]
		public int Med { get; set; }

		[JsonProperty("med_apprentice", Required = Required.Always)]
		public int MedApprentice { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Hoarding
	{
		[JsonProperty("prey_warrior", Required = Required.Always)]
		public int PreyWarrior { get; set; }

		[JsonProperty("herb_medicine", Required = Required.Always)]
		public int HerbMedicine { get; set; }

		[JsonProperty("injury_chance_warrior", Required = Required.Always)]
		public int InjuryChanceWarrior { get; set; }

		[JsonProperty("injury_chance_medicine cat", Required = Required.Always)]
		public int InjuryChanceMedicineCat { get; set; }

		[JsonProperty("injuries", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Injuries Injuries { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("illness_chance", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public int? IllnessChance { get; set; }

		[JsonProperty("illnesses", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Illnesses Illnesses { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("comments", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comments { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("chance_increase_per_clan", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public int? ChanceIncreasePerClan { get; set; }

		[JsonProperty("relation", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
		public int? Relation { get; set; }
	}

	public partial class Illnesses
	{
		[JsonProperty("running nose", Required = Required.Always)]
		public int RunningNose { get; set; }

		[JsonProperty("whitecough", Required = Required.Always)]
		public int Whitecough { get; set; }
	}

	public partial class Injuries
	{
		[JsonProperty("claw-wound", Required = Required.Always)]
		public int ClawWound { get; set; }

		[JsonProperty("cat bite", Required = Required.Always)]
		public int CatBite { get; set; }

		[JsonProperty("torn pelt", Required = Required.Always)]
		public int TornPelt { get; set; }

		[JsonProperty("torn ear", Required = Required.Always)]
		public int TornEar { get; set; }

		[JsonProperty("bite-wound", Required = Required.Always)]
		public int BiteWound { get; set; }

		[JsonProperty("sprain", Required = Required.Always)]
		public int Sprain { get; set; }

		[JsonProperty("bruises", Required = Required.Always)]
		public int Bruises { get; set; }

		[JsonProperty("sore", Required = Required.Always)]
		public int Sore { get; set; }

		[JsonProperty("small cut", Required = Required.Always)]
		public int SmallCut { get; set; }

		[JsonProperty("cracked pads", Required = Required.Always)]
		public int CrackedPads { get; set; }
	}

	public partial class Hunting
	{
		[JsonProperty("warrior", Required = Required.Always)]
		public int Warrior { get; set; }

		[JsonProperty("apprentice", Required = Required.Always)]
		public int Apprentice { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class OtherClans
	{
		[JsonProperty("relation", Required = Required.Always)]
		public int Relation { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Outsiders
	{
		[JsonProperty("reputation", Required = Required.Always)]
		public int Reputation { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public string Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class RestAndRecover
	{
		[JsonProperty("injury_prevent", Required = Required.Always)]
		public int InjuryPrevent { get; set; }

		[JsonProperty("illness_prevent", Required = Required.Always)]
		public int IllnessPrevent { get; set; }

		[JsonProperty("outbreak_prevention", Required = Required.Always)]
		public int OutbreakPrevention { get; set; }

		[JsonProperty("moons_earlier_healed", Required = Required.Always)]
		public int MoonsEarlierHealed { get; set; }

		[JsonProperty("comments", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comments { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Fun
	{
		[JsonProperty("april_fools", Required = Required.Always)]
		public bool AprilFools { get; set; }

		[JsonProperty("all_cats_are_newborn", Required = Required.Always)]
		public bool AllCatsAreNewborn { get; set; }

		[JsonProperty("newborns_can_roam", Required = Required.Always)]
		public bool NewbornsCanRoam { get; set; }

		[JsonProperty("newborns_can_patrol", Required = Required.Always)]
		public bool NewbornsCanPatrol { get; set; }

		[JsonProperty("always_halloween", Required = Required.Always)]
		public bool AlwaysHalloween { get; set; }
	}

	public partial class Graduation
	{
		[JsonProperty("base_app_timeskip_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<List<int>> BaseAppTimeskipEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("base_med_app_timeskip_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<List<int>> BaseMedAppTimeskipEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("max_apprentice_age", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MaxApprenticeAge MaxApprenticeAge { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("min_graduating_age", Required = Required.Always)]
		public int MinGraduatingAge { get; set; }
	}

	public partial class MaxApprenticeAge
	{
		[JsonProperty("medicine cat apprentice", Required = Required.Always)]
		public int MedicineCatApprentice { get; set; }

		[JsonProperty("apprentice", Required = Required.Always)]
		public int Apprentice { get; set; }

		[JsonProperty("mediator apprentice", Required = Required.Always)]
		public int MediatorApprentice { get; set; }
	}

	public partial class LostCat
	{
		[JsonProperty("rejoin_chance", Required = Required.Always)]
		public int RejoinChance { get; set; }
	}

	public partial class Mates
	{
		[JsonProperty("age_range", Required = Required.Always)]
		public int AgeRange { get; set; }

		[JsonProperty("override_same_age_group", Required = Required.Always)]
		public bool OverrideSameAgeGroup { get; set; }

		[JsonProperty("chance_fulfilled_condition", Required = Required.Always)]
		public int ChanceFulfilledCondition { get; set; }

		[JsonProperty("chance_friends_to_lovers", Required = Required.Always)]
		public int ChanceFriendsToLovers { get; set; }

		[JsonProperty("confession", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Confession Confession { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("mate_condition", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition MateCondition { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("platonic_to_romantic", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition PlatonicToRomantic { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("poly", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Poly Poly { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Confession
	{
		[JsonProperty("make_confession", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition MakeConfession { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("accept_confession", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition AcceptConfession { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class MateCondition
	{
		[JsonProperty("romantic", Required = Required.Always)]
		public int Romantic { get; set; }

		[JsonProperty("platonic", Required = Required.Always)]
		public int Platonic { get; set; }

		[JsonProperty("dislike", Required = Required.Always)]
		public int Dislike { get; set; }

		[JsonProperty("admiration", Required = Required.Always)]
		public int Admiration { get; set; }

		[JsonProperty("comfortable", Required = Required.Always)]
		public int Comfortable { get; set; }

		[JsonProperty("jealousy", Required = Required.Always)]
		public int Jealousy { get; set; }

		[JsonProperty("trust", Required = Required.Always)]
		public int Trust { get; set; }
	}

	public partial class Poly
	{
		[JsonProperty("current_mate_condition", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition CurrentMateCondition { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("mates_to_each_other", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition MatesToEachOther { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class NewCat
	{
		[JsonProperty("parent_buff", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public ParentBuff ParentBuff { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("sib_buff", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public SibBuff SibBuff { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("rel_buff", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public RelBuff RelBuff { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat_amount_welcoming", Required = Required.Always)]
		public int CatAmountWelcoming { get; set; }
	}

	public partial class ParentBuff
	{
		[JsonProperty("kit_to_parent", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition KitToParent { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("parent_to_kit", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition ParentToKit { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class RelBuff
	{
		[JsonProperty("new_to_clan_cat", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition NewToClanCat { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("clan_cat_to_new", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition ClanCatToNew { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class SibBuff
	{
		[JsonProperty("cat1_to_cat2", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition Cat1ToCat2 { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("cat2_to_cat1", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public MateCondition Cat2ToCat1 { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class OutsideEx
	{
		[JsonProperty("base_adolescent_timeskip_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<List<int>> BaseAdolescentTimeskipEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("base_adult_timeskip_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<List<int>> BaseAdultTimeskipEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("base_senior_timeskip_ex", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<List<int>> BaseSeniorTimeskipEx { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class PatrolGeneration
	{
		[JsonProperty("classic_difficulty_modifier", Required = Required.Always)]
		public int ClassicDifficultyModifier { get; set; }

		[JsonProperty("expanded_difficulty_modifier", Required = Required.Always)]
		public double ExpandedDifficultyModifier { get; set; }

		[JsonProperty("cruel season_difficulty_modifier", Required = Required.Always)]
		public int CruelSeasonDifficultyModifier { get; set; }

		[JsonProperty("win_stat_cat_modifier", Required = Required.Always)]
		public int WinStatCatModifier { get; set; }

		[JsonProperty("better_stat_modifier", Required = Required.Always)]
		public int BetterStatModifier { get; set; }

		[JsonProperty("best_stat_modifier", Required = Required.Always)]
		public int BestStatModifier { get; set; }

		[JsonProperty("fail_stat_cat_modifier", Required = Required.Always)]
		public int FailStatCatModifier { get; set; }

		[JsonProperty("chance_of_romance_patrol", Required = Required.Always)]
		public int ChanceOfRomancePatrol { get; set; }

		[JsonProperty("debug_ensure_patrol_id", Required = Required.AllowNull)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public object DebugEnsurePatrolId { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("debug_override_patrol_stat_requirements", Required = Required.Always)]
		public bool DebugOverridePatrolStatRequirements { get; set; }

		[JsonProperty("debug_ensure_patrol_outcome", Required = Required.AllowNull)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public object DebugEnsurePatrolOutcome { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Pregnancy
	{
		[JsonProperty("birth_cooldown", Required = Required.Always)]
		public int BirthCooldown { get; set; }

		[JsonProperty("primary_chance_mated", Required = Required.Always)]
		public int PrimaryChanceMated { get; set; }

		[JsonProperty("primary_chance_unmated", Required = Required.Always)]
		public int PrimaryChanceUnmated { get; set; }

		[JsonProperty("random_affair_chance", Required = Required.Always)]
		public int RandomAffairChance { get; set; }

		[JsonProperty("unmated_random_affair_chance", Required = Required.Always)]
		public int UnmatedRandomAffairChance { get; set; }

		[JsonProperty("one_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility OneKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("two_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility TwoKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("three_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility ThreeKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("four_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility FourKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("five_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility FiveKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("max_kit_possibility", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public KitPossibility MaxKitPossibility { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("min_kits", Required = Required.Always)]
		public int MinKits { get; set; }

		[JsonProperty("max_kits", Required = Required.Always)]
		public int MaxKits { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class KitPossibility
	{
		[JsonProperty("young adult", Required = Required.Always)]
		public int YoungAdult { get; set; }

		[JsonProperty("adult", Required = Required.Always)]
		public int Adult { get; set; }

		[JsonProperty("senior adult", Required = Required.Always)]
		public int SeniorAdult { get; set; }

		[JsonProperty("senior", Required = Required.Always)]
		public int Senior { get; set; }
	}

	public partial class Relationship
	{
		[JsonProperty("in_decrease_value", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public InDecreaseValue InDecreaseValue { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("max_interaction", Required = Required.Always)]
		public int MaxInteraction { get; set; }

		[JsonProperty("max_interaction_special", Required = Required.Always)]
		public int MaxInteractionSpecial { get; set; }

		[JsonProperty("compatibility_effect", Required = Required.Always)]
		public int CompatibilityEffect { get; set; }

		[JsonProperty("passive_influence_div", Required = Required.Always)]
		public double PassiveInfluenceDiv { get; set; }

		[JsonProperty("chance_for_neutral", Required = Required.Always)]
		public int ChanceForNeutral { get; set; }

		[JsonProperty("chance_of_special_group", Required = Required.Always)]
		public int ChanceOfSpecialGroup { get; set; }

		[JsonProperty("chance_romantic_not_mate", Required = Required.Always)]
		public int ChanceRomanticNotMate { get; set; }

		[JsonProperty("influence_condition_events", Required = Required.Always)]
		public int InfluenceConditionEvents { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class InDecreaseValue
	{
		[JsonProperty("low", Required = Required.Always)]
		public int Low { get; set; }

		[JsonProperty("medium", Required = Required.Always)]
		public int Medium { get; set; }

		[JsonProperty("high", Required = Required.Always)]
		public int High { get; set; }
	}

	public partial class Roles
	{
		[JsonProperty("mediator_app_chance", Required = Required.Always)]
		public int MediatorAppChance { get; set; }

		[JsonProperty("base_medicine_app_chance", Required = Required.Always)]
		public int BaseMedicineAppChance { get; set; }

		[JsonProperty("become_mediator_chances", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public BecomeMediatorChances BecomeMediatorChances { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class BecomeMediatorChances
	{
		[JsonProperty("warrior", Required = Required.Always)]
		public int Warrior { get; set; }

		[JsonProperty("elder", Required = Required.Always)]
		public int Elder { get; set; }
	}

	public partial class SaveLoad
	{
		[JsonProperty("load_integrity_checks", Required = Required.Always)]
		public bool LoadIntegrityChecks { get; set; }
	}

	public partial class Sorting
	{
		[JsonProperty("sort_dead_by_total_age", Required = Required.Always)]
		public bool SortDeadByTotalAge { get; set; }

		[JsonProperty("sort_rank_by_death", Required = Required.Always)]
		public bool SortRankByDeath { get; set; }

		[JsonProperty("sort_by_rel_total", Required = Required.Always)]
		public bool SortByRelTotal { get; set; }

		[JsonProperty("comment", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<string> Comment { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}

	public partial class Theme
	{
		[JsonProperty("dark_mode_background", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> DarkModeBackground { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

		[JsonProperty("light_mode_background", Required = Required.Always)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public List<int> LightModeBackground { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	}
}