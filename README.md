# Master-slave

In this example, we have an interface ISlaveNode that represents the behavior of a slave node. The SlaveNode class implements this interface and simulates the processing of a task by multiplying the task data by 2.

The MasterNode class represents the master node that coordinates the task execution. It takes a list of slave nodes during construction. The ExecuteTaskAsync method distributes the task data among the slave nodes by invoking the ProcessTaskAsync method on each slave node.

The Program class sets up multiple slave nodes and a master node. It generates a sample list of task data and executes the task by calling the ExecuteTaskAsync method on the master node. The results are then displayed on the console.

In a real-world scenario, you might need to handle error scenarios, implement fault tolerance mechanisms, and design the communication protocol
