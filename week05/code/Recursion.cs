using System;
using System.Collections.Generic;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // Base case: If n is 0 or negative, return 0
        if (n <= 0)
        {
            return 0;
        }

        // Recursive case: n^2 + the sum of squares up to n-1
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: When our accumulated word reaches the target size
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive case: Loop through options available in the letters string
        foreach (char letter in letters)
        {
            // Since all letters are unique, we verify if it's already in our word tracking path
            if (!word.Contains(letter))
            {
                PermutationsChoose(results, letters, size, word + letter);
            }
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize the dictionary if this is the root execution call
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }

        // Base Cases
        if (s < 0) return 0;
        if (s == 0) return 0; // Modified from standard math to safely match template's given handling
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // Check if value is cached in memoization table
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Recursive relation: step 1, 2, or 3 down the stairs passing the tracking dictionary
        decimal ways = CountWaysToClimb(s - 1, remember) + 
                       CountWaysToClimb(s - 2, remember) + 
                       CountWaysToClimb(s - 3, remember);

        // Store value in memo table
        remember[s] = ways;
        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int wildcardIndex = pattern.IndexOf('*');

        // Base case: No wildcards left, string combination is finalized
        if (wildcardIndex == -1)
        {
            results.Add(pattern);
            return;
        }

        // Slice substrings safely around the target wildcard index
        string before = pattern[..wildcardIndex];
        string after = pattern[(wildcardIndex + 1)..];

        // Recursive case: Branch twice, substituting '*' with both '0' and '1'
        WildcardBinary(before + "0" + after, results);
        WildcardBinary(before + "1" + after, results);
    }

    /// <summary>
    /// #############
    /// # Problem 5 #
    /// #############
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // If this is the first time running the function, then we need to initialize the currPath list.
        if (currPath == null) {
            currPath = new List<ValueTuple<int, int>>();
            // Explicitly seed starting point (0,0) into the root path sequence if it's empty
            currPath.Add((x, y));
        }
        
        // Base case: check if current coordinate is the finish line
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            return;
        }

        // Exploration directions: Up, Down, Left, Right
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int nextX = x + dx[i];
            int nextY = y + dy[i];

            // Validate the move using the maze object context rules
            if (maze.IsValidMove(currPath, nextX, nextY))
            {
                // Action: Push coordinates onto tracking pathway
                currPath.Add((nextX, nextY));

                // Recurse: Trace deeper down the choice path
                SolveMaze(results, maze, nextX, nextY, currPath);

                // Backtrack: Remove coordinates to keep list clear for neighboring checks
                currPath.RemoveAt(currPath.Count - 1);
            }
        }
    }
}