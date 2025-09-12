using System.Collections.Generic;


namespace ConsoleApp_LatestFeatures
{
 
        // Extension methods (C# 13 doesn't support extension members yet)
        public static class EnumerableExtensions
        {
            public static bool IsEmpty<T>(this IEnumerable<T> source) =>
                !source.Any();

            public static T GetAt<T>(this IEnumerable<T> source, int index) =>
                source.Skip(index).First();
        }

        public class Example
        {
            // Backing field instead of 'field' keyword (C# 14+ only)
            private string _name = "Unknown";
            public string Name
            {
                get => _name;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentException("Name cannot be empty");
                    _name = value.Trim();
                }
            }

            public List<int>? MaybeNumbers { get; set; }
        }

        internal class Program
        {
            static void Main()
            {
                Console.WriteLine("=== C# 13 Console Demo ===\n");

                // nameof with unbound generic (works in C# 13 too)
                Console.WriteLine($"nameof(List<>) => {nameof(List<>)}");

                // field-backed property (classic style)
                var ex = new Example { Name = "   Alice   " };
                Console.WriteLine($"Normalized Name: '{ex.Name}'");

                // null-coalescing + null-conditional assignment
                ex.MaybeNumbers ??= new List<int>();
                ex.MaybeNumbers?.Add(42);
                Console.WriteLine("Numbers: " + string.Join(", ", ex.MaybeNumbers));

                // extension methods usage
                var words = new[] { "zero", "one", "two" };
                Console.WriteLine($"IsEmpty: {words.IsEmpty()}");
                Console.WriteLine($"words[1] => {words.GetAt(1)}");

                // --------- NEW: Span<T> demo (C# 13 feature) ---------
                byte[] buffer = { 1, 2, 3, 4, 5 };

                // Implicit conversion: byte[] → ReadOnlySpan<byte>
                PrintSum(buffer);

                Console.WriteLine("\nDemo complete.");
            }

            // Method that expects a ReadOnlySpan<byte>
            static void PrintSum(ReadOnlySpan<byte> data)
            {
                int sum = 0;
                foreach (var b in data)
                    sum += b;
                Console.WriteLine($"Sum of bytes = {sum}");
            }
        }

        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello, World!");
        //}
    
}
