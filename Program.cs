using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace
{
    // Interface for the slave nodes
    public interface ISlaveNode
    {
        Task<int> ProcessTaskAsync(int taskData);
    }

    // Implementation of a slave node
    public class SlaveNode : ISlaveNode
    {
        public async Task<int> ProcessTaskAsync(int taskData)
        {
            // Simulate processing time
            await Task.Delay(TimeSpan.FromSeconds(1));

            // Perform the task and return the result
            int result = taskData * 2;
            return result;
        }
    }

    // Master node
    public class MasterNode
    {
        private readonly List<ISlaveNode> _slaveNodes;

        public MasterNode(List<ISlaveNode> slaveNodes)
        {
            _slaveNodes = slaveNodes;
        }

        public async Task<List<int>> ExecuteTaskAsync(List<int> taskDataList)
        {
            var taskResults = new List<int>();

            // Divide the task data among the slave nodes
            int slaveNodeCount = _slaveNodes.Count;
            int taskDataCount = taskDataList.Count;

            for (int i = 0; i < taskDataCount; i++)
            {
                ISlaveNode slaveNode = _slaveNodes[i % slaveNodeCount];
                int taskData = taskDataList[i];

                // Send task to the slave node for processing
                Task<int> task = slaveNode.ProcessTaskAsync(taskData);
                taskResults.Add(await task);
            }

            return taskResults;
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            // Create slave nodes
            var slaveNodes = new List<ISlaveNode>
            {
                new SlaveNode(),
                new SlaveNode(),
                new SlaveNode()
            };

            // Create the master node
            var masterNode = new MasterNode(slaveNodes);

            // Generate sample task data
            var taskDataList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Execute the task
            List<int> taskResults = await masterNode.ExecuteTaskAsync(taskDataList);

            // Display the results
            Console.WriteLine("Task Results:");
            foreach (int result in taskResults)
            {
                Console.WriteLine(result);
            }
        }
    }
}
