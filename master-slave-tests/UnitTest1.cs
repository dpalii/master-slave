using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Tests
{
    [TestClass]
    public class MasterNodeTests
    {
        [TestMethod]
        public async Task ExecuteTaskAsync_DistributesTaskDataAmongSlaveNodes()
        {
            // Arrange
            var slaveNodes = new List<ISlaveNode>
            {
                MockSlaveNode(),
                MockSlaveNode(),
                MockSlaveNode()
            };

            var masterNode = new MasterNode(slaveNodes);

            var taskDataList = new List<int> { 1, 2, 3, 4, 5 };

            // Act
            List<int> taskResults = await masterNode.ExecuteTaskAsync(taskDataList);

            // Assert
            Assert.AreEqual(taskDataList.Count, taskResults.Count);
            for (int i = 0; i < taskDataList.Count; i++)
            {
                int expectedTaskResult = taskDataList[i] * 2;
                Assert.AreEqual(expectedTaskResult, taskResults[i]);
            }
        }

        private ISlaveNode MockSlaveNode()
        {
            var slaveNodeMock = new Mock<ISlaveNode>();
            slaveNodeMock.Setup(s => s.ProcessTaskAsync(It.IsAny<int>()))
                .Returns<int>(taskData => Task.FromResult(taskData * 2));
            return slaveNodeMock.Object;
        }
    }
}