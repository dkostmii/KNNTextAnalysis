public class Scenario
{
    public int K { get; private init; }
    public int TrainDataRatio { get; private init; }
    public IMetrics Metrics { get; private init; }
    public Dimensions Dimensions { get; private init; }
    public string Metadata { get; init; } = "";

    public Scenario(int k, int trainDataRatio, IMetrics metrics, Dimensions dimensions)
    {
        K = k;
        TrainDataRatio = trainDataRatio;
        Metrics = metrics;
        Dimensions = dimensions;
    }
}
