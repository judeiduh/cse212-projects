public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number'
    /// followed by multiples of 'number'.
    ///
    /// Example:
    /// MultiplesOf(7, 5) returns:
    /// {7, 14, 21, 28, 35}
    /// </summary>
    /// <returns>
    /// Array of doubles that contain the multiples of the supplied number.
    /// </returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // Step 1:
        // Create an array that will store the multiples.
        double[] multiples = new double[length];

        // Step 2:
        // Loop through each position in the array.
        for (int i = 0; i < length; i++)
        {
            // Step 3:
            // Multiply the number by (i + 1)
            // to generate the correct multiple.
            multiples[i] = number * (i + 1);
        }

        // Step 4:
        // Return the completed array.
        return multiples;
    }

    /// <summary>
    /// Rotate the list to the right by the specified amount.
    ///
    /// Example:
    /// data = {1,2,3,4,5,6,7,8,9}
    /// amount = 3
    ///
    /// Result:
    /// {7,8,9,1,2,3,4,5,6}
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // Step 1:
        // Find where to split the list.
        int splitIndex = data.Count - amount;

        // Step 2:
        // Get the last section of the list.
        List<int> endSection = data.GetRange(splitIndex, amount);

        // Step 3:
        // Get the beginning section of the list.
        List<int> beginningSection = data.GetRange(0, splitIndex);

        // Step 4:
        // Clear the original list.
        data.Clear();

        // Step 5:
        // Add the rotated sections back in order.
        data.AddRange(endSection);
        data.AddRange(beginningSection);
    }
}