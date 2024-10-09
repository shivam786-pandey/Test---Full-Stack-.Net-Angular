namespace API_Project.Test
{

    public class BinaryStringEvaluator
    {
        public static bool IsGoodBinaryString(string binaryString)
        {
            if (string.IsNullOrEmpty(binaryString))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            int count0 = 0, count1 = 0;

            foreach (var ch in binaryString)
            {
                if (ch == '0')
                {
                    count0++;
                }
                else if (ch == '1')
                {
                    count1++;
                }
                else
                {
                    throw new ArgumentException("Input can only contain binary digits (0 and 1).");
                }

                // Condition: For every prefix, the number of 1's is not less than the number of 0's
                if (count1 < count0)
                {
                    return false; // If at any point 0s exceed 1s, return false
                }
            }

            // Final condition: Equal number of 0's and 1's
            return count0 == count1;
        }
    }
}
