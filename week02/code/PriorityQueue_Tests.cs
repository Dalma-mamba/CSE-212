using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items in order with priorities 1, 3, and 10.
    // Also dequeue twice to verify that the selected item is actually removed.
    // Expected Result: Enqueue order is preserved in ToString; dequeue returns
    // "high" first, then "medium".
    // Defect(s) Found: The original loop stopped before checking the last item,
    // so a highest-priority item at the end could be missed. Dequeue also failed
    // to remove the returned item from the queue.
    public void TestPriorityQueue_HighestPriorityAndRemoval()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("medium", 3);
        priorityQueue.Enqueue("high", 10);

        Assert.AreEqual("[low (Pri:1), medium (Pri:3), high (Pri:10)]", priorityQueue.ToString());
        Assert.AreEqual("high", priorityQueue.Dequeue());
        Assert.AreEqual("medium", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue three items with the same priority.
    // Expected Result: Items are dequeued in their original enqueue order:
    // "first", "second", then "third".
    // Defect(s) Found: The original comparison used >=, which selected a later
    // item when priorities were equal instead of preserving FIFO order.
    public void TestPriorityQueue_EqualPrioritiesUseFifo()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 5);

        Assert.AreEqual("first", priorityQueue.Dequeue());
        Assert.AreEqual("second", priorityQueue.Dequeue());
        Assert.AreEqual("third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from a newly created empty priority queue.
    // Expected Result: An InvalidOperationException is thrown with the exact
    // message "The queue is empty.".
    // Defect(s) Found: No defect; the original implementation already handled
    // the empty-queue exception correctly.
    public void TestPriorityQueue_EmptyQueueThrowsExpectedException()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(
            () => priorityQueue.Dequeue());

        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}
