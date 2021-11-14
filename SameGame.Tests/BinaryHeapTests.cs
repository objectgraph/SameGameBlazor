using Xunit;
using SameGame.Data.BoardGen;
using SameGame.Data;
using System;

namespace SameGame.Tests;

public class BinaryHeapTests
{
    [Fact]
    public void TestEnqueueDequeue()
    {
        BinaryHeap<int> heap = new BinaryHeap<int>();
        heap.Enque(10);
        heap.Enque(20);
        heap.Enque(30);
        heap.Enque(40);
        heap.Enque(50);
        
        Assert.True(heap.Count==5);
        Assert.True(heap.Dequeue()==50);
        Assert.True(heap.Count==4);
        Assert.True(heap.Dequeue()==40);
        Assert.True(heap.Count==3);
        Assert.True(heap.Dequeue()==30);
        Assert.True(heap.Count==2);
        Assert.True(heap.Dequeue()==20);
        Assert.True(heap.Count==1);
        Assert.True(heap.Dequeue()==10);
        

    }
}