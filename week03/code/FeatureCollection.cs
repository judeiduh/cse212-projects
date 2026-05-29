using System.Collections.Generic;

/// <summary>
/// Represents the root JSON object returned by the USGS earthquake feed.
/// </summary>
public class FeatureCollection
{
    public List<Feature> features { get; set; }
    public List<Feature> Features => features;
}

/// <summary>
/// Represents each earthquake feature.
/// </summary>
public class Feature
{
    public Properties properties { get; set; }
    public Properties Properties => properties;
}

/// <summary>
/// Represents the earthquake details we need.
/// </summary>
public class Properties
{
    public string place { get; set; }
    public string Place => place;

    public double? mag { get; set; }
    public double? Mag => mag;
}