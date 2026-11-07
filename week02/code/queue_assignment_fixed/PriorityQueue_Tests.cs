using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three values with different priorities.
    // Expected Result: ToString shows the values in the same order they were enqueued,
    // proving that Enqueue adds each item to the back regardless of priority.
    // Defect(s) Found: No defect found in Enqueue; this test verifies the required insertion order.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 10);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("[First (Pri:1), Second (Pri:10), Third (Pri:5)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest-priority item is last, then dequeue it.
    // Expected Result: The item with priority 9 is returned first, then the next-highest item.
    // Defect(s) Found: The original loop stopped before checking the final item, and Dequeue
    // returned the selected value without removing it from the queue.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Middle", 4);
        priorityQueue.Enqueue("Highest", 9);

        Assert.AreEqual("Highest", priorityQueue.Dequeue());
        Assert.AreEqual("Middle", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with the same highest priority and one lower-priority item.
    // Expected Result: The first item enqueued at the tied highest priority is dequeued first,
    // followed by the second tied item, then the lower-priority item.
    // Defect(s) Found: The original >= comparison selected the later item when priorities tied;
    // the original Dequeue also failed to remove returned items.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstHigh", 7);
        priorityQueue.Enqueue("SecondHigh", 7);
        priorityQueue.Enqueue("Low", 2);

        Assert.AreEqual("FirstHigh", priorityQueue.Dequeue());
        Assert.AreEqual("SecondHigh", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty priority queue.
    // Expected Result: InvalidOperationException is thrown with the message "The queue is empty."
    // Defect(s) Found: No defect found; the original implementation already handled this requirement.
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}
