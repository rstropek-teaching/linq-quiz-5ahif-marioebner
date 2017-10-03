using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
            {
                throw new ArgumentOutOfRangeException();
            } else
            {
                // Tip: Use `Enumerable.Range` instead -> no manual loop necessary.
                int[] numbers = new int[exclusiveUpperLimit - 1];
                for (int i = 0; i < exclusiveUpperLimit - 1; i++)
                {
                    numbers[i] = i + 1;
                }

                // General tip: Use `var` instead of `int[]` to make code easier to read and maintain.
                int[] numQuery = (from num in numbers
                                  where (num % 2) == 0
                                  select num).ToArray();

                return numQuery;
            }
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
            {
                return Array.Empty<int>();
            } else
            {
                int[] numbers = new int[exclusiveUpperLimit-1];
                for (int i = 0; i < exclusiveUpperLimit-1; i++)
                {
                    numbers[i] = i + 1;
                }

                var numQuery = (from num in numbers
                                where (num % 7) == 0
                                orderby num descending
                                select num).ToArray();

                // Tip: Add sqare operation to LINQ statement above using `Select`
                for (int i = 0; i < numQuery.Length; i++)
                {
                    numQuery[i] = checked((int)Math.Pow(numQuery[i], 2));
                }

                return numQuery;
            }
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            if (families == null)
            {
                throw new ArgumentNullException();
            }

            // It would be possible to write foreach statement in a single LINQ statement.
            // Generally: Even if you create your own `foreach`, you should consider using an array
            // instead of a list. That would save you from having to call `ToArray` at the end.
            List<FamilySummary> list = new List<FamilySummary>();
            decimal averageAge;
            foreach (var family in families)
            {
                if (family.Persons.Count == 0)
                {
                    averageAge = 0;
                }
                else
                {
                    averageAge = family.Persons.Average(p => p.Age);
                }
                list.Add(new FamilySummary
                {
                    AverageAge = averageAge,
                    FamilyID = family.ID,
                    NumberOfFamilyMembers = family.Persons.Count
                });
            }

            return list.ToArray();
        }
        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            // Tip: Prefer `var` instead of manually typing the type on the left side of the assignment.
            char[] textLetters = text.ToUpper().ToCharArray();
            List<int> letters = Enumerable.Range('A', 'Z').ToList();
            List<(char letter, int numberOfOccurences)> solution = new List<(char letter, int numberOfOccurences)>();

            foreach(var letter in letters)
            {
                var count = textLetters.Count(l => l == letter);
                if (count > 0)
                {
                    solution.Add(((char)letter, count));
                }
            }

            return solution.ToArray();
        }
    }
}
