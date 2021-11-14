using Xunit;
using SameGame.Data.BoardGen;
using SameGame.Data;
using System;

namespace SameGame.Tests;

public class UnitTest1
{
    [Fact]
    public void TestGraphDegreeOf()
    {
        Graph<Cell> c= new Graph<Cell>();
        Cell a = new Cell(0,0,0,false,false);
        Cell b = new Cell(0,1,0,false,false);
        c.AddVertex(a);
        c.AddVertex(b);
        c.AddEdgeBetween(a,b);
        Assert.True(c.DegreeOf(a)==1);
    }
}