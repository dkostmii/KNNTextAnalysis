public class ScenarioRunner
{
    public Action<int, int, string>? OnScenarioRun { get; set; }

    private readonly List<Article> articles;
    private readonly List<Scenario> scenarios;

    public ScenarioRunner(List<Article> articles, List<Scenario> scenarios)
    {
        if (!articles.Any())
            throw new Exception("Expected articles to not be empty");

        this.articles = articles;

        if (!scenarios.Any())
            throw new Exception("Expected scenarios to not be empty");

        this.scenarios = scenarios;
    }

    public async Task Run()
    {
        var counter = 1;
        var count = scenarios.Count;

        foreach (var scenario in scenarios)
        {
            if (OnScenarioRun is not null)
                OnScenarioRun(counter, count, scenario.Metadata);

            var startTime = DateTime.Now;

            await RunScenario(scenario);

            var diff = DateTime.Now - startTime;

            Logger.WriteLine($"Scenario done in {diff.Hours} hours, {diff.Minutes} minutes, {diff.Seconds} seconds");

            counter++;
        }
    }

    public async Task RunScenario(Scenario scenario)
    {
        var datasetControl = new DatasetControl(articles, scenario.Dimensions, scenario.TrainDataRatio);

        var knn = new KNN(scenario.K, datasetControl, scenario.Metrics)
        {
            OnPredictRun = (progress) =>
            {
                var percentage = decimal.Round(progress * 100, 2);

                Logger.SetCursorPosition(0, Logger.CursorTop - 1);
                Logger.WriteLine($"Progress: {percentage}%", true);
            }
        };

        Logger.WriteLine("Starting KNN analysis... Process may take long time.");
        Logger.WriteLine(knn.ToString());
        Logger.WriteLine("\n");

        await knn.Predict();

        Logger.WriteLine(knn.ToString());
    }
}
