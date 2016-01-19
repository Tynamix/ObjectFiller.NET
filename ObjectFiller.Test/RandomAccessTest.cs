namespace ObjectFiller.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

        using Xunit;

    using ObjectFiller.Test.TestPoco.Library;
    using ObjectFiller.Test.TestPoco.Person;

    using Tynamix.ObjectFiller;


    public class RandomAccessTest
    {
        [Fact]
        public void GetRandomIntOnDifferentThreadsGetsDifferentResults()
        {
            var numberToGenerate = 1000;
            var intRange = new IntRange(1, 10);
            var task1 = Task.Factory.StartNew(() =>
                {
                    var taskResults = new List<int>();
                    for (int i = 0; i < numberToGenerate; ++i)
                    {
                        taskResults.Add(Randomizer<int>.Create(intRange));
                    }
                    return taskResults;
                },
                TaskCreationOptions.LongRunning
            );
            var task2 = Task.Factory.StartNew(() =>
                {
                    var taskResults = new List<int>();
                    for (int i = 0; i < numberToGenerate; ++i)
                    {
                        taskResults.Add(Randomizer<int>.Create(intRange));
                    }
                    return taskResults;
                },
                TaskCreationOptions.LongRunning
            );
            var results = Task.WhenAll(task1, task2).Result;
            List<int> firstResults = results[0];
            List<int> secondResults = results[1];
            Assert.NotEqual(firstResults, secondResults);
        }
    }
}