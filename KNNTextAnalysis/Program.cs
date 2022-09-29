Logger.Destination = Util.GetLoggerDestination();

// Reading
var reader = new Reader(Util.GetDatasetsDirectory());

Logger.WriteLine("Reading files...");
reader.ReadAll();
Logger.WriteLine(reader.ToString());

Logger.WriteLine("Stemming texts... This may take a while.");
var stemmer = new Stemmer(reader.Articles);
Logger.WriteLine(stemmer.ToString());

// Exporting
// Is essential for defining dimensions of analysis
/*
Logger.WriteLine("Exporting articles...");
var exporter = (
    new ArticleExporter(stemmer.Stemmed, Util.GetArticleExportDirectory(), overwrite: true)
    {
        ExportCount = 12,
        StartFrom = 101
    });
exporter.Export();
*/

// Analysis

var dimensions = new Dimensions
{
    // west-germany
    "bundesbank",
    "pharmaceut",
    "pharma",
    "pharmaceuticals",
    "secur",
    "germani",
    "turnov",
    "vereinsbank",
    "landeszentralbank",
    "lzb",
    "frankfurt",
    "otto",
    "karl",
    "berlin",
    "handel",
    "und",
    "hermann",
    // usa
    "permian",
    "usx",
    "dlrs",
    "oil",
    "corp",
    "moodi",
    "dlr",
    "baa3",
    "system",
    "howard",
    "ryan",
    "larsen",
    "mcdonald",
    "chris",
    "dodd",
    "dconn",
    // france
    "french",
    "franc",
    "eurofranc",
    "edouard",
    "balladur",
    "rhonepoulenc",
    "jeanren",
    "fourtou",
    "vieux",
    "bernard",
    "francais",
    "francoisxavi",
    "ortoli",
    "guilbaud",
    "armand",
    // uk
    "britain",
    "london",
    "mori",
    "greenwich",
    "harri",
    "coffe",
    "ico",
    "tesco",
    "eurobond",
    "morgan",
    "stanley",
    "england",
    "uk",
    "sterl",
    "stg",
    // canada
    "ivaco",
    "canadian",
    "canada",
    "marconi",
    "sikorski",
    "paramax",
    "ontario",
    "cornelissen",
    "michael",
    "novacor",
    "sterivet",
    "lttoronto",
    "toronto",
    "korthal",
    "alberta",
    "aluminum",
    "ltcanadian",
    // japan
    "welfar",
    "bureau",
    "japanes",
    "japan",
    "tokyo",
    "nikko",
    "kiichi",
    "miyazawa",
    "yasuhiro",
    "nakason",
    "dokkyo",
    "shiratori",
    "seizaburo",
    "sato",
    "wako",
};

var exclusionSubsets = new List<ExclusionSubset>
{
    new ExclusionSubset(new[] { "berlin", "canada", "toronto", "ontario", "tokyo" }, dimensions),
    new ExclusionSubset(new[] { "bundesbank", "tesco", "balladur", "vereinsbank", "lzb" }, dimensions),
    new ExclusionSubset(new[] { "turnov", "miyazawa", "harri", "sikorski", "handel"  }, dimensions),
    new ExclusionSubset(new[] { "kiichi", "britain", "oil", "mcdonald", "pharma" }, dimensions)
};

var articles = stemmer.Stemmed;

var scenarioFactory = new ScenarioFactory(dimensions, exclusionSubsets);

var scenarios = scenarioFactory.CreateScenarios();

var scenarioRunner = new ScenarioRunner(articles, scenarios)
{
    OnScenarioRun = (counter, count, metadata) =>
    {
        Logger.WriteLine($"Scenario {counter} of {count}");

        if (!string.IsNullOrEmpty(metadata))
            Logger.WriteLine($"Additional info: {metadata}");
    }
};

Logger.WriteLine("Running scenarios. ");
await scenarioRunner.Run();