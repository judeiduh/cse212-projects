using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        // Problem 1 - Solved in O(n) time using a HashSet
        var seen = new HashSet<string>();
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // Skip self-matching cases like "aa"
            if (word[0] == word[1]) continue;

            // Compute the target symmetric partner
            string reversed = $"{word[1]}{word[0]}";

            // If partner was already cataloged, we found our pair match
            if (seen.Contains(reversed))
            {
                pairs.Add($"{reversed} & {word}");
            }
            else
            {
                // Otherwise, save this word for future incoming partner matches
                seen.Add(word);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        
        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var fields = line.Split(",");
            
            // Problem 2 - Read column 4 (Index 3), trim whitespace, and tally
            if (fields.Length > 3)
            {
                string degree = fields[3].Trim();

                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams using a dictionary.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Problem 3 - Dictionary mapping letter counts ignoring casing and spaces
        var letterCounts = new Dictionary<char, int>();

        // Load and increment using word1
        foreach (char c in word1.ToLower())
        {
            if (c == ' ') continue;

            if (letterCounts.ContainsKey(c))
                letterCounts[c]++;
            else
                letterCounts[c] = 1;
        }

        // Validate and decrement using word2
        foreach (char c in word2.ToLower())
        {
            if (c == ' ') continue;

            if (!letterCounts.ContainsKey(c))
            {
                return false; 
            }

            letterCounts[c]--;

            if (letterCounts[c] == 0)
            {
                letterCounts.Remove(c);
            }
        }

        // If the balance sheets cleared completely, it is a valid anagram
        return letterCounts.Count == 0;
    }

    /// <summary>
    /// Reads JSON data from USGS and summarizes locations and magnitudes.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        
        // Mandatory modern API header addition to prevent remote server blocking/throttling
        client.DefaultRequestHeaders.Add("User-Agent", "BYUI-CSE212-StudentTask");

        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Problem 5 - Extract deserialized data structures into custom string array format
        var results = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features) 
            {
                if (feature?.Properties == null) continue;

                string place = feature.Properties.Place ?? "Unknown Location";
                string mag = feature.Properties.Mag.HasValue ? feature.Properties.Mag.Value.ToString() : "0.0";

                // Formats exactly matching: [Place] - Mag [Value]
                results.Add($"{place} - Mag {mag}");
            }
        }

        return results.ToArray();
    }
}