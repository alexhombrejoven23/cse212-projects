using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add 3 items with different priorities: ("low", 1), ("high", 5), ("mid", 3).
    // Dequeue should return the highest priority item first.
    // Expected Result: "high" is returned first.
    // Defect(s) Found: The loop uses _queue.Count - 1 so it skips the last element,
    //                  meaning the highest priority item is not always found correctly.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("low", 1);
        priorityQueue.Enqueue("high", 5);
        priorityQueue.Enqueue("mid", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("high", result);

    }

    [TestMethod]
    // Scenario: Add 3 items where two share the highest priority: ("first", 5), ("second", 5), ("third", 1).
    // Expected Result: "first" is returned (FIFO among equal priorities).
    // Defect(s) Found: None for this scenario after fixing the loop bug.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("first", 5);
        priorityQueue.Enqueue("second", 5);
        priorityQueue.Enqueue("third", 1);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("first", result);
    }

     [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException thrown with message "The queue is empty."
    // Defect(s) Found: No defect, exception is thrown correctly.
    public void TestPriorityQueue_3()
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
            Assert.Fail($"Unexpected exception type {e.GetType()}: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Enqueue one item and dequeue it. Queue should be empty afterwards.
    // Expected Result: Item value is returned, then next dequeue throws exception.
    // Defect(s) Found: Item was never removed from queue because _queue.RemoveAt() was missing.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("only", 3);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("only", result);

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Add 3 items and dequeue all in priority order.
    // Items: ("c", 3), ("a", 1), ("b", 2)
    // Expected Result: "c", "b", "a"
    // Defect(s) Found: Both bugs affected this - skipping last element and not removing items.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("c", 3);
        priorityQueue.Enqueue("a", 1);
        priorityQueue.Enqueue("b", 2);

        Assert.AreEqual("c", priorityQueue.Dequeue());
        Assert.AreEqual("b", priorityQueue.Dequeue());
        Assert.AreEqual("a", priorityQueue.Dequeue());
    }

}