using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario:
    // Add several items with different priorities into the queue.
    // Remove one item from the queue.
    //
    // Expected Result:
    // The item with the highest priority should be removed first.
    //
    // Defect(s) Found:
    // The queue was not always selecting the highest priority item correctly.
    // The last item in the queue was skipped during the search because
    // the loop condition stopped too early.
    public void TestPriorityQueue_HighestPriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 5);
        priorityQueue.Enqueue("Sue", 3);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("Tim", result);
    }

    [TestMethod]
    // Scenario:
    // Add multiple items with the same highest priority.
    //
    // Expected Result:
    // If priorities are equal, the item closest to the front
    // of the queue should be removed first (FIFO behavior).
    //
    // Defect(s) Found:
    // FIFO order was broken because the queue used >= instead of >
    // when comparing priorities, causing later items to incorrectly
    // replace earlier items with the same priority.
    public void TestPriorityQueue_FIFOForSamePriority()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Tim", 5);
        priorityQueue.Enqueue("Sue", 3);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("Bob", result);
    }

    [TestMethod]
    // Scenario:
    // Remove an item from the queue and verify it no longer exists.
    //
    // Expected Result:
    // The dequeued item should be removed from the queue completely.
    //
    // Defect(s) Found:
    // The dequeue method returned the correct item but failed
    // to actually remove it from the queue.
    public void TestPriorityQueue_ItemRemoved()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Tim", 5);

        var first = priorityQueue.Dequeue();
        var second = priorityQueue.Dequeue();

        Assert.AreEqual("Tim", first);
        Assert.AreEqual("Bob", second);
    }

    [TestMethod]
    // Scenario:
    // Attempt to dequeue from an empty queue.
    //
    // Expected Result:
    // An InvalidOperationException should be thrown with the message:
    // "The queue is empty."
    //
    // Defect(s) Found:
    // Error handling needed to properly throw the required exception
    // and display the exact required message.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format(
                    "Unexpected exception of type {0} caught: {1}",
                    e.GetType(),
                    e.Message
                )
            );
        }
    }
}