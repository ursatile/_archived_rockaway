using NodaTime;
using Rockaway.WebApp.Data.Entities;

namespace Rockaway.WebApp.Data;

public class SampleDataPopulator {

	public static async Task PopulateSampleDataAsync(RockawayDbContext dbc) {
		await dbc.Artists.AddRangeAsync(Artists.AllArtists);
		await dbc.Venues.AddRangeAsync(Venues.AllVenues);
		await dbc.SaveChangesAsync();
		var show = new Show {
			Venue = Venues.Stengade,
			Date = new(2023, 8, 29),
			DoorsOpen = new(18,00)
		};
		show.AddArtists(Artists.DevLeppard, Artists.Erlangst, Artists.QuantumGate);
		show.AddTicketType("Standing", 275m, 100);
		show.AddTicketType("VIP Meet & Greet", 875m, 15);
		dbc.Shows.Add(show);
		await dbc.SaveChangesAsync();
	}

	public static class Venues {
		public static Venue Astoria = new() {
			Name = "The Astoria",
			Address = "157 Charing Cross Road",
			City = "London",
			CultureCode = "en-GB",
			Phone = "020 7412 3400",
			PostalCode = "WC2H 0EL",
			Website = "https://www.astoria.co.uk"
		};

		public static Venue Bataclan = new() {
			Name = "Bataclan",
			Address = "50 Boulevard Voltaire",
			City = "Paris",
			CultureCode = "fr-FR",
			Phone = "+33 1 43 14 00 30",
			PostalCode = "75011",
			Website = "https://www.bataclan.fr/"
		};

		public static Venue Columbia = new() {
			Name = "Columbia Theatre",
			Address = "Columbiadamm 9-11",
			City = "Berlin",
			CultureCode = "de-DE",
			Phone = "+49 30 69817584",
			PostalCode = "10965",
			Website = "https://columbia-theater.de/"
		};

		public static Venue Gagarin = new() {
			Name = "Gagarin 205",
			Address = "Liosion 205",
			City = "Athens",
			CultureCode = "el-GR",
			PostalCode = "104 45",
		};

		public static Venue JohnDee = new() {
			Name = "John Dee Live Club & Pub",
			Address = "Torggata 16",
			City = "Oslo",
			CultureCode = "nn-NO",
			Phone = "+47 22 20 32 32",
			Website = "https://www.rockefeller.no/"
		};
		public static Venue Stengade = new() {
			Name = "Stengade",
			Address = "Stengade 18",
			City = "Copenhagen",
			CultureCode = "dk-DK",
			PostalCode = "2200",
			Phone = "+45 35 35 50 69",
			Website = "https://www.stengade.dk"
		};

		public static IEnumerable<Venue> AllVenues {
			get {
				yield return Astoria;
				yield return Bataclan;
				yield return Columbia;
				yield return Gagarin;
				yield return Stengade;
				yield return JohnDee;
			}
		}
	}
	public static class Artists {
		public static Artist AlterColumn = new("Alter Column",
			"Alter Column are South Africa's hottest math rock export. Founded in Cape Town in 2021, their debut album \"Drop Table Mountain\" was nominated for four Grammy awards.");

		public static Artist BinarySearch = new("Binary Search",
			"Speed metal pioneers from San Francisco, Binary Search helped define the data rock sound in the late 1990s.");

		public static Artist C0DA = new("C0DA",
			"Hailing from a distant future, C0DA is a time-traveling rock band known for their mind-bending melodies that transport audiences through different eras, merging past and future into a harmonious blend of sound.");

		public static Artist DevLeppard = new("Dev Leppard",
			"Rising from the ashes of adversity, Dev Leppard is a fiercely talented rock band that overcame obstacles with sheer determination, captivating fans worldwide with their electrifying performances and showcasing a bond that empowers their music.");

		public static Artist Erlangst = new("Erlangst",
			"Merging the realms of art and emotion, Erlangst is an introspective rock group, infusing their hauntingly beautiful lyrics with mesmerizing melodies that delve into the depths of human existence, leaving listeners immersed in profound reflections.");

		public static Artist FloatingPointFoxes = new("Floating Point Foxes",
			"With an otherworldly allure, Floating Point Foxes is an ethereal rock ensemble, their music transcends reality, leading listeners on a dreamlike journey where celestial harmonies and ethereal instrumentation create a captivating and transformative experience.");

		public static Artist GarbageCollectors = new("Garbage Collectors",
			"Rebel rockers with a cause, Garbage Collectors are raw, raucous and unapologetic, fearlessly tackling social issues and societal norms in their music, energizing crowds with their powerful anthems and unyielding spirit.");

		public static Artist HaskellsAngels = new("Haskell’s Angels",
			"Virtuosos of rhythm and harmony, Haskell’s Angels radiate a divine aura, blending complex melodies and celestial harmonies that resonate deep within the soul.");

		public static Artist IronMedian = new("Iron Median",
			"A force to be reckoned with, Iron Median are known for their thunderous beats and adrenaline-pumping anthems, electrifying audiences with their commanding stage presence and unstoppable energy.");

		public static Artist JavasCrypt = new("Java’s Crypt",
			"Enigmatic and mysterious, Java’s Crypt are shrouded in secrecy, their enigmatic melodies and cryptic lyrics take listeners on a thrilling journey through the unknown realms of music.");

		public static Artist Killerbyte = new("Killerbyte",
			"An electrifying whirlwind, Killerbyte unleash a torrent of energy through their performances, captivating audiences with their explosive riffs and heart-pounding rhythms.");

		public static Artist LambdaOfGod = new("Lambda of God",
			"Pioneers of progressive rock, Lambda of God is an innovative band that pushes the boundaries of musical expression, combining intricate compositions and thought-provoking lyrics that resonate deeply with their dedicated fanbase.");

		public static Artist NullTerminatedStringBand = new("Null Terminated String Band",
			"Quirky and witty, the Null Terminated String Band is a rock group that weaves clever humor and geeky references into their catchy tunes, bringing a smile to the faces of both tech enthusiasts and music lovers alike.");

		public static Artist MottTheTuple = new("Mott the Tuple",
			"A charismatic ensemble, Mott the Tuple blends vintage charm with a modern edge, creating a unique sound that captivates audiences and takes them on a nostalgic journey through time.");

		public static Artist Overflow = new("Överflow",
			"Overflowing with passion and intensity, Överflow is a rock band that immerses listeners in a tsunami of sound, exploring emotions through powerful melodies and soul-stirring performances.");

		public static Artist PascalsWager = new("Pascal’s Wager",
			"Philosophers of rock, Pascal’s Wager delves into existential themes with their intellectually charged songs, prompting listeners to ponder the profound questions of life and purpose.");

		public static Artist QuantumGate = new("Qüantum Gäte",
			"Futuristic and avant-garde, Qüantum Gäte defy conventions, using experimental sounds and innovative compositions to transport listeners to other dimensions of music.");

		public static Artist RunCMD = new("Run CMD",
			"High-energy and rebellious, Run CMD is a rock band that merges technology themes with headbanging-worthy tunes, igniting the stage with their electrifying presence and infectious enthusiasm.");

		public static Artist ScriptKiddies = new("Script Kiddies",
			"Mischievous and bold, Script Kiddies subvert expectations with clever musical pranks, weaving clever wordplay and tongue-in-cheek humor into their audacious performances.");

		public static Artist Terrorform = new("Terrorform",
			"Masters of atmosphere, Terrorform’s dark and atmospheric rock ensembles conjure hauntingly beautiful soundscapes that captivate the senses and evoke a deep emotional response.");

		public static Artist UnsignedIntegers = new("Unsigned Integers",
			"Unsigned Integer harmonize complex equations and melodies, weaving a symphony of logic and emotion in their unique and captivating music.");

		public static Artist VirtualMachine = new("Virtual Machine",
			"Bridging reality and virtuality, Virtual Machine is a surreal rock group that blurs the lines between the tangible and the digital, creating mind-bending performances that leave audiences questioning the nature of existence.");

		public static Artist WebmasterOfPuppets = new("Webmaster of Puppets",
			"Technologically savvy and creatively ambitious, Webmaster of Puppets is a web-inspired rock band, crafting narratives of digital dominance and manipulation with their inventive music.");

		public static Artist XSLTE = new("XSLTE",
			"Mesmerizing and genre-defying, XSLTE is an enchanting rock ensemble that fuses electronic and rock elements, creating a spellbinding sound that enthralls listeners and transports them to new sonic landscapes.");

		public static Artist YAMB = new("YAMB",
			"Youthful and exuberant, YAMB spreads positivity and infectious energy through their music, connecting with fans through their youthful spirit and heartwarming performances.");

		public static Artist ZeroBasedIndex = new("Zero Based Index",
			"Innovative and exploratory, Zero Based Index starts from scratch, building powerful narratives through their dynamic sound, leaving audiences inspired and moved by their expressive and daring music.");

		public static IEnumerable<Artist> AllArtists {
			get {
				yield return AlterColumn;
				yield return BinarySearch;
				yield return C0DA;
				yield return DevLeppard;
				yield return Erlangst;
				yield return FloatingPointFoxes;
				yield return GarbageCollectors;
				yield return HaskellsAngels;
				yield return IronMedian;
				yield return JavasCrypt;
				yield return Killerbyte;
				yield return LambdaOfGod;
				yield return MottTheTuple;
				yield return NullTerminatedStringBand;
				yield return Overflow;
				yield return PascalsWager;
				yield return QuantumGate;
				yield return RunCMD;
				yield return ScriptKiddies;
				yield return Terrorform;
				yield return UnsignedIntegers;
				yield return VirtualMachine;
				yield return WebmasterOfPuppets;
				yield return XSLTE;
				yield return YAMB;
				yield return ZeroBasedIndex;
			}
		}
	}
}
