public class ScenarioFactory
{
    private readonly Scenario initial;

    private readonly IEnumerable<IMetrics> metrics;

    private readonly Dimensions dimensions;

    private readonly IEnumerable<ExclusionSubset> exclusionSubsets;

    public ScenarioFactory(Dimensions dimensions, IEnumerable<ExclusionSubset> exclusionSubsets)
    {
        metrics = new List<IMetrics>
        {
            new EuclideanMetrics(),
            new ManhattanMetrics(),
            new ChebyshevMetrics()
        };

        this.dimensions = dimensions;

        initial = new Scenario(4, 10, metrics.First(), dimensions);

        this.exclusionSubsets = exclusionSubsets;
    }

    public List<Scenario> CreateScenarios()
    {
        return (
            VariateK()
            .Concat(VariateTrainDataRatio())
            .Concat(VariateMetrics())
            .Concat(VariateDimensions())
            .ToList()
        );
    }

    private List<Scenario> VariateK()
    {
        var result = new List<Scenario>();

        for (var i = initial.K; i < 15; i += 1)
        {
            result.Add(
                new Scenario(i, initial.TrainDataRatio, initial.Metrics, initial.Dimensions));
        }

        return result;
    }

    private List<Scenario> VariateTrainDataRatio()
    {
        var result = new List<Scenario>();

        for (var i = initial.TrainDataRatio; i < 70; i += 10)
        {
            result.Add(
                new Scenario(initial.K, i, initial.Metrics, initial.Dimensions));
        }

        return result;
    }

    private List<Scenario> VariateMetrics()
    {
        var result = new List<Scenario>();

        for (var i = 0; i < metrics.Count(); i++)
        {
            result.Add(
                new Scenario(initial.K, initial.TrainDataRatio, metrics.Skip(i).Take(1).First(), initial.Dimensions));
        }

        return result;
    }

    private List<Scenario> VariateDimensions()
    {
        var result = new List<Scenario>();

        foreach (var subset in exclusionSubsets)
        {
            var d = dimensions.Exclude(subset);

            var scenario = new Scenario(initial.K, initial.TrainDataRatio, initial.Metrics, d)
            {
                Metadata = "Excluded dimensions: " + subset.ToString()
            };

            result.Add(scenario);
        }

        return result;
    }
}
