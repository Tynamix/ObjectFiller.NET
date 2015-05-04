using System;
using System.Collections.Generic;
using System.Text;

namespace Tynamix.ObjectFiller
{

    public enum LipsumFlavor
    {
        /// <summary>
        /// Standard Lorem Ipsum words.
        /// </summary>
        LoremIpsum,

        /// <summary>
        /// Words from Child Harold by Lord Byron.
        /// </summary>
        ChildHarold,

        /// <summary>
        /// Words from In der Fremde by Heinrich Hiene (German)
        /// </summary>
        InDerFremde,

        /// <summary>
        /// Words from Le Masque by Arthur Rembaud (French)
        /// </summary>
        LeMasque
    }

    /// <summary>
    /// Generates nonsensical text using preset words from one of several texts.
    /// </summary>
    public class Lipsum : IRandomizerPlugin<string>
    {
        private readonly LipsumFlavor flavor;
        private readonly int paragraphs;
        private readonly int minSentences;
        private readonly int maxSentences;
        private readonly int minWords;
        private readonly int maxWords;
        private readonly int seed;

        /// <summary>
        /// Words for the standard lorem ipsum text.
        /// </summary>
        private static readonly string[] LoremIpsum =
        {
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetur", "adipisicing", "elit", "sed", "do", "eiusmod",
            "tempor", "incididunt", "ut", "labore", "et", "dolore", "magna", "aliqua", "enim", "ad", "minim", "veniam",
            "quis", "nostrud", "exercitation", "ullamco", "laboris", "nisi", "aliquip", "ex", "ea", "commodo",
            "consequat", "duis", "aute", "irure", "in", "reprehenderit", "voluptate", "velit", "esse", "cillum", "eu",
            "fugiat", "nulla", "pariatur", "excepteur", "sint", "occaecat", "cupidatat", "non", "proident", "sunt",
            "culpa", "qui", "officia", "deserunt", "mollit", "anim", "id", "est", "laborum",
        };

        /// <summary>
        /// Words for the childe harold text
        /// </summary>
        private static readonly string[] ChildeHarold =
        {
            "oh", "thou", "in", "hellas", "deemed", "of", "heavenly", "birth", "muse", "formed", "or", "fabled", "at",
            "the", "minstrels", "will", "since", "shamed", "full", "oft", "by", "later", "lyres", "on", "earth", "mine",
            "dares", "not", "call", "thee", "from", "thy", "sacred", "hill", "yet", "there", "ive", "wandered",
            "vaunted", "rill", "yes", "sighed", "oer", "delphis", "longdeserted", "shrine", "where", "save", "that",
            "feeble", "fountain", "all", "is", "still", "nor", "mote", "my", "shell", "awake", "weary", "nine", "to",
            "grace", "so", "plain", "a", "talethis", "lowly", "lay", "whilome", "albions", "isle", "dwelt", "youth",
            "who", "ne", "virtues", "ways", "did", "take", "delight", "but", "spent", "his", "days", "riot", "most",
            "uncouth", "and", "vexed", "with", "mirth", "drowsy", "ear", "night", "ah", "me", "sooth", "he", "was",
            "shameless", "wight", "sore", "given", "revel", "ungodly", "glee", "few", "earthly", "things", "found",
            "favour", "sight", "concubines", "carnal", "companie", "flaunting", "wassailers", "high", "low", "degree",
            "childe", "harold", "hight", "whence", "name", "lineage", "long", "it", "suits", "say", "suffice",
            "perchance", "they", "were", "fame", "had", "been", "glorious", "another", "day", "one", "sad", "losel",
            "soils", "for", "aye", "however", "mighty", "olden", "time", "heralds", "rake", "coffined", "clay", "florid",
            "prose", "honeyed", "lines", "rhyme", "can", "blazon", "evil", "deeds", "consecrate", "crime", "basked",
            "him", "noontide", "sun", "disporting", "like", "any", "other", "fly", "before", "little", "done", "blast",
            "might", "chill", "into", "misery", "ere", "scarce", "third", "passed", "worse", "than", "adversity",
            "befell", "felt", "fulness", "satiety", "then", "loathed", "native", "land", "dwell", "which", "seemed",
            "more", "lone", "eremites", "cell",
        };

        /// <summary>
        /// Words for the text: In der Fremde.
        /// </summary>
        private static readonly string[] InderFremde =
        {
            "es", "treibt", "dich", "fort", "von", "ort", "zu", "du", "weißt", "nicht", "mal", "warum", "im", "winde",
            "klingt", "ein", "sanftes", "wort", "schaust", "verwundert", "um", "die", "liebe", "dahinten", "blieb",
            "sie", "ruft", "sanft", "zurück", "o", "komm", "ich", "hab", "lieb", "bist", "mein", "einz'ges", "glück",
            "doch", "weiter", "sonder", "rast", "darfst", "stillestehn", "was", "so", "sehr", "geliebet", "hast",
            "sollst", "wiedersehn", "ja", "heut", "grambefangen", "wie", "lange", "geschaut", "perlet", "still",
            "deinen", "wangen", "und", "deine", "seufzer", "werden", "laue", "denkst", "der", "heimat", "ferne",
            "nebelferne", "dir", "verschwand", "gestehe", "mir's", "wärest", "gerne", "manchmal", "teuren", "vaterland",
            "dame", "niedlich", "mit", "kleinem", "zürnen", "ergötzt", "oft", "zürntest", "dann", "ward", "friedlich",
            "immer", "lachtet", "ihr", "zuletzt", "freunde", "da", "sanken", "an", "brust", "in", "großer", "stund",
            "herzen", "stürmten", "gedanken", "jedoch", "verschwiegen", "mund", "mutter", "schwester", "beiden",
            "standest", "gut", "glaube", "gar", "schmilzt", "bester", "deiner", "wilde", "mut", "vögel", "bäume", "des",
            "schönen", "gartens", "wo", "geträumt", "junge", "träume", "gezagt", "gehofft", "ist", "schon", "spät",
            "nacht", "helle", "trübhell", "gefärbt", "vom", "feuchten", "schnee", "ankleiden", "muß", "mich", "nun",
            "schnelle", "gesellschaft", "gehn", "weh",
        };

        /// <summary>
        /// Words for the text: Le Masque.
        /// </summary>
        private static readonly string[] LeMasque =
        {
            "contemplons", "ce", "trésor", "de", "grâces", "florentines", "dans", "l'ondulation", "corps", "musculeux",
            "l'elégance", "et", "la", "force", "abondent", "soeurs", "divines", "cette", "femme", "morceau", "vraiment",
            "miraculeux", "divinement", "robuste", "adorablement", "mince", "est", "faite", "pour", "trôner", "sur",
            "des", "lits", "somptueux", "charmer", "les", "loisirs", "d'un", "pontife", "ou", "prince", "aussi", "vois",
            "souris", "fin", "voluptueux", "fatuité", "promene", "son", "extase", "long", "regard", "sournois",
            "langoureux", "moqueur", "visage", "mignard", "tout", "encadré", "gaze", "dont", "chaque", "trait", "nous",
            "dit", "avec", "un", "air", "vainqueur", "«la", "volupté", "m'appelle", "l'amour", "me", "couronne»", "a",
            "cet", "etre", "doué", "tant", "majesté", "quel", "charme", "excitant", "gentillesse", "donne", "approchons",
            "tournons", "autour", "sa", "beauté", "ô", "blaspheme", "l'art", "surprise", "fatale", "au", "divin",
            "promettant", "le", "bonheur", "par", "haut", "se", "termine", "en", "monstre", "bicéphale", "mais", "non",
            "n'est", "qu'un", "masque", "décor", "suborneur", "éclairé", "d'une", "exquise", "grimace", "regarde",
            "voici", "crispée", "atrocement", "véritable", "tete", "sincere", "face", "renversée", "l'abri", "qui",
            "ment", "pauvre", "grande", "magnifique", "fleuve", "tes", "pleurs", "aboutit", "mon", "coeur", "soucieux",
            "ton", "mensonge", "m'enivre", "âme", "s'abreuve", "aux", "flots", "que", "douleur", "fait", "jaillir",
            "yeux", "pourquoi", "pleure-t-elle", "elle", "parfaite", "mettrait", "ses", "pieds", "genre", "humain",
            "vaincu", "mal", "mystérieux", "ronge", "flanc", "d'athlete", "pleure", "insensé", "parce", "qu'elle",
            "vécu", "vit", "déplore", "surtout", "frémir", "jusqu'aux", "genoux", "c'est", "demain", "hélas", "il",
            "faudra", "vivre", "encore", "apres-demain", "toujours", "comme",
        };

        /// <summary>
        /// The map between <see cref="LipsumFlavor"/> and the words for this flavor
        /// </summary>
        private readonly Dictionary<LipsumFlavor, string[]> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lipsum"/> class.
        /// </summary>
        /// <param name="flavor">
        /// The flavor for the generated text
        /// </param>
        /// <param name="paragraphs">
        /// The count of generated paragraphs.
        /// </param>
        /// <param name="minSentences">
        /// The min sentences of the generated text
        /// </param>
        /// <param name="maxSentences">
        /// The max sentences of the generated text
        /// </param>
        /// <param name="minWords">
        /// The min words of the generated text.
        /// </param>
        /// <param name="maxWords">
        /// The max words of the generated text.
        /// </param>
        /// <param name="seed">
        /// The seed for the random to get the same result with the same seed.
        /// </param>
        public Lipsum(LipsumFlavor flavor, int paragraphs = 3, int minSentences = 3, int maxSentences = 8,
            int minWords = 10, int maxWords = 50, int? seed = null)
        {
            this.flavor = flavor;
            this.paragraphs = paragraphs;
            this.minSentences = minSentences;
            this.maxSentences = maxSentences < minSentences ? minSentences : maxSentences;
            this.minWords = minWords;
            this.maxWords = maxWords < minWords ? minWords : maxWords;

            this.map = new Dictionary<LipsumFlavor, string[]>()
                           {
                               { LipsumFlavor.LoremIpsum, LoremIpsum },
                               { LipsumFlavor.ChildHarold, ChildeHarold },
                               { LipsumFlavor.InDerFremde, InderFremde },
                               { LipsumFlavor.LeMasque, LeMasque }
            };

            this.seed = seed.HasValue ? seed.Value : Environment.TickCount;
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            System.Random rnd = new System.Random(this.seed);
            var array = this.map[this.flavor];

            var result = new StringBuilder();

            for (var i = 0; i < this.paragraphs; i++)
            {
                var sentences = rnd.Next(this.minSentences, this.maxSentences + 1);
                for (var j = 0; j < sentences; j++)
                {
                    var words = rnd.Next(this.minWords, this.maxWords + 1);
                    for (var k = 0; k < words; k++)
                    {
                        var word = array[rnd.Next(array.Length)];
                        if (k == 0)
                        {
                            word = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word);
                        }

                        result.Append(word);
                        result.Append(k == words - 1 ? ". " : " ");
                    }
                }

                result.Append(Environment.NewLine);
                result.Append(Environment.NewLine);
            }

            return result.ToString();
        }
    }
}