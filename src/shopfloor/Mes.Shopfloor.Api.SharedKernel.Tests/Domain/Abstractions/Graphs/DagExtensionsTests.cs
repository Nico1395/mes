using Mes.Shopfloor.Api.SharedKernel.Domain.Abstractions.Graphs;

namespace Mes.Shopfloor.Api.SharedKernel.Tests.Domain.Abstractions.Graphs;

public class DagExtensionsTests(DagFixture _fixture) : IClassFixture<DagFixture>
{
    #region Flatten Tests

    [Fact]
    public void Flatten_SingleNode_ReturnsOnlyThatNode()
    {
        // Arrange
        var node = new DagMock();

        // Act
        var result = node.Flatten().ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(node.Id, result[0].Id);
    }

    [Fact]
    public void Flatten_LinearGraph_ReturnsAllNodesInOrder()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node3);
        node3.Previous.Add(node2);

        // Act
        var result = node1.Flatten().ToList();

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(node1.Id, result[0].Id);
        Assert.Equal(node2.Id, result[1].Id);
        Assert.Equal(node3.Id, result[2].Id);
    }

    [Fact]
    public void Flatten_WithCycle_VisitsEachNodeOnce()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node1); // Cycle (should not happen in valid DAG)
        node1.Previous.Add(node2);

        // Act
        var result = node1.Flatten().ToList();

        // Assert - Should visit each node once due to visited tracking
        Assert.Equal(2, result.Count);
    }

    #endregion

    #region InsertAfter Tests

    [Fact]
    public void InsertAfter_SimpleLinearGraph_InsertsNodeCorrectly()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var newNode = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        // Initialize edges (must be synchronized with Next/Previous)
        node1.InsertEdge(node2.Id);
        node2.InsertEdge(node1.Id);

        // Act
        var result = node1.InsertAfter(newNode, node1.Id);

        // Assert
        Assert.True(result);
        Assert.Equal(2, node1.Next.Count);
        Assert.Contains(newNode, node1.Next);
        Assert.Single(newNode.Previous);
        Assert.Contains(node1, newNode.Previous);
        Assert.Contains(newNode, node2.Previous);
        Assert.Contains(node2, newNode.Next);
    }

    [Fact]
    public void InsertAfter_ValidatesEdgesAreSynchronized()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var newNode = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node1.InsertEdge(node2.Id);
        node2.InsertEdge(node1.Id);

        // Act
        var result = node1.InsertAfter(newNode, node1.Id);

        // Assert - edges should be properly synchronized
        Assert.True(result);
        // newNode has edges to node1 and node2 (as FROM)
        Assert.Contains(node1.Id, newNode.Edges.Select(e => e.ToId));
        Assert.Contains(node2.Id, newNode.Edges.Select(e => e.ToId));
    }

    [Fact]
    public void InsertAfter_NonExistentNode_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var newNode = new DagMock();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = node1.InsertAfter(newNode, nonExistentId);

        // Assert
        Assert.False(result);
        Assert.Empty(newNode.Previous);
        Assert.Empty(newNode.Next);
    }

    [Fact]
    public void InsertAfter_WouldCreateCycle_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);

        // Act - try to insert node2 after node1, which would create cycle
        var result = node1.InsertAfter(node2, node1.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void InsertAfter_RemoveEdgeFailure_RollsBack()
    {
        // Arrange
        var node1 = new DagMockWithFailingRemoveEdge();
        var node2 = new DagMock();
        var newNode = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);

        // Act - RemoveEdge will fail because node1 always returns false
        var result = node1.InsertAfter(newNode, node1.Id);

        // Assert - should return false when RemoveEdge fails
        Assert.False(result);
    }

    #endregion

    #region InsertBefore Tests

    [Fact]
    public void InsertBefore_SimpleLinearGraph_InsertsNodeCorrectly()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var newNode = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        // Initialize edges (must be synchronized with Next/Previous)
        node1.InsertEdge(node2.Id);
        node2.InsertEdge(node1.Id);

        // Act
        var result = node1.InsertBefore(newNode, node2.Id);

        // Assert
        Assert.True(result);
        Assert.Single(node1.Next);
        Assert.Contains(newNode, node1.Next);
        Assert.Contains(node1, newNode.Previous);
        Assert.Contains(node2, newNode.Next);
        Assert.Single(node2.Previous);
        Assert.Contains(newNode, node2.Previous);
    }

    [Fact]
    public void InsertBefore_MultipleIncomingEdges_TransfersAllCorrectly()
    {
        // Arrange
        var nodeStart = new DagMock();  // Common start node
        var nodeA = new DagMock();
        var nodeB = new DagMock();
        var nodeC = new DagMock(); // target node
        var newNode = new DagMock();

        // Build graph: start → A, start → B, A → C, B → C
        nodeStart.Next.AddRange(new[] { nodeA, nodeB });
        nodeA.Previous.Add(nodeStart);
        nodeB.Previous.Add(nodeStart);
        
        nodeA.Next.Add(nodeC);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.AddRange(new[] { nodeA, nodeB });

        // Act
        var result = nodeStart.InsertBefore(newNode, nodeC.Id);

        // Assert
        Assert.True(result);
        // newNode should have both A and B as previous
        Assert.Equal(2, newNode.Previous.Count);
        Assert.Contains(nodeA, newNode.Previous);
        Assert.Contains(nodeB, newNode.Previous);
        // C should only have newNode as previous
        Assert.Single(nodeC.Previous);
        Assert.Contains(newNode, nodeC.Previous);
        // A and B should now point to newNode, not C
        Assert.Contains(newNode, nodeA.Next);
        Assert.Contains(newNode, nodeB.Next);
        Assert.DoesNotContain(nodeC, nodeA.Next);
        Assert.DoesNotContain(nodeC, nodeB.Next);
    }

    [Fact]
    public void InsertBefore_NonExistentNode_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var newNode = new DagMock();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = node1.InsertBefore(newNode, nonExistentId);

        // Assert
        Assert.False(result);
        Assert.Empty(newNode.Previous);
        Assert.Empty(newNode.Next);
    }

    [Fact]
    public void InsertBefore_WouldCreateCycle_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);

        // Act - try to insert node1 before node2, which would create cycle
        var result = node1.InsertBefore(node1, node2.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void InsertBefore_InsertEdgeFailure_RollsBack()
    {
        // Arrange - use failing node for testing error handling
        var failingNode = new DagMockWithFailingRemoveEdge();
        var node2 = new DagMock();
        var newNode = new DagMock();

        failingNode.Next.Add(node2);
        node2.Previous.Add(failingNode);

        // Act - RemoveEdge will fail, so InsertBefore should return false
        var result = failingNode.InsertBefore(newNode, node2.Id);

        // Assert - should return false due to RemoveEdge failure
        Assert.False(result);
    }

    #endregion

    #region Replace Tests

    [Fact]
    public void Replace_SimpleLinearGraph_ReplacesNodeCorrectly()
    {
        // Arrange
        var nodeA = new DagMock();
        var nodeB = new DagMock(); // target
        var nodeC = new DagMock();
        var newNode = new DagMock();

        nodeA.Next.Add(nodeB);
        nodeB.Previous.Add(nodeA);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.Add(nodeB);
        
        // Initialize edges
        nodeA.InsertEdge(nodeB.Id);
        nodeB.InsertEdge(nodeA.Id);
        nodeB.InsertEdge(nodeC.Id);
        nodeC.InsertEdge(nodeB.Id);

        // Act
        var result = nodeA.Replace(newNode, nodeB.Id);

        // Assert
        Assert.True(result);
        Assert.Contains(newNode, nodeA.Next);
        Assert.DoesNotContain(nodeB, nodeA.Next);
        Assert.Contains(newNode, nodeC.Previous);
        Assert.DoesNotContain(nodeB, nodeC.Previous);
        Assert.Empty(nodeB.Next);
        Assert.Empty(nodeB.Previous);
    }

    [Fact]
    public void Replace_VerifiesTargetNodeEdgesCleared()
    {
        // Arrange
        var nodeA = new DagMock();
        var nodeB = new DagMock();
        var nodeC = new DagMock();
        var newNode = new DagMock();

        nodeA.Next.Add(nodeB);
        nodeB.Previous.Add(nodeA);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.Add(nodeB);

        nodeA.InsertEdge(nodeB.Id);
        nodeB.InsertEdge(nodeA.Id);
        nodeB.InsertEdge(nodeC.Id);
        nodeC.InsertEdge(nodeB.Id);

        // Act
        var result = nodeA.Replace(newNode, nodeB.Id);

        // Assert
        Assert.True(result);
        // Verify that targetNode.RemoveEdge was called (edges cleared from RAM and Edges)
        Assert.Empty(nodeB.Next);
        Assert.Empty(nodeB.Previous);
        Assert.Empty(nodeB.GetEdges());  // Verify Edges collection is also cleared
    }

    [Fact]
    public void Replace_NonExistentTarget_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var newNode = new DagMock();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = node1.Replace(newNode, nonExistentId);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Replace_WouldCreateCycle_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);

        // Act - try to replace node1 with node2, which would create cycle
        var result = node1.Replace(node2, node1.Id);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region Remove Tests

    [Fact]
    public void Remove_SimpleLinearGraph_RemovesMiddleNodeCorrectly()
    {
        // Arrange
        var nodeA = new DagMock();
        var nodeB = new DagMock(); // to remove
        var nodeC = new DagMock();

        nodeA.Next.Add(nodeB);
        nodeB.Previous.Add(nodeA);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.Add(nodeB);
        
        // Initialize edges
        nodeA.InsertEdge(nodeB.Id);
        nodeB.InsertEdge(nodeA.Id);
        nodeB.InsertEdge(nodeC.Id);
        nodeC.InsertEdge(nodeB.Id);

        // Act
        var result = nodeA.Remove(nodeB);

        // Assert
        Assert.True(result);
        Assert.Contains(nodeC, nodeA.Next);
        Assert.Contains(nodeA, nodeC.Previous);
        Assert.Empty(nodeB.Next);
        Assert.Empty(nodeB.Previous);
    }

    [Fact]
    public void Remove_VerifiesEdgesSynchronized()
    {
        // Arrange
        var nodeA = new DagMock();
        var nodeB = new DagMock();
        var nodeC = new DagMock();

        nodeA.Next.Add(nodeB);
        nodeB.Previous.Add(nodeA);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.Add(nodeB);

        nodeA.InsertEdge(nodeB.Id);
        nodeB.InsertEdge(nodeA.Id);
        nodeB.InsertEdge(nodeC.Id);
        nodeC.InsertEdge(nodeB.Id);

        // Act
        var result = nodeA.Remove(nodeB);

        // Assert
        Assert.True(result);
        // Verify edges from A to C exist (A has edge to C)
        Assert.Contains(nodeC.Id, nodeA.Edges.Select(e => e.ToId));
        // Verify edges to/from B are removed from A
        Assert.DoesNotContain(nodeB.Id, nodeA.Edges.Select(e => e.ToId));
        // Verify B's edges are cleared
        Assert.Empty(nodeB.Edges);
    }

    [Fact]
    public void Remove_MultipleNextNodes_ConnectsAllPreviousToAllNext()
    {
        // Arrange
        var nodeA = new DagMock();
        var nodeB = new DagMock(); // to remove
        var nodeC = new DagMock();
        var nodeD = new DagMock();

        // A → B → C, B → D
        nodeA.Next.Add(nodeB);
        nodeB.Previous.Add(nodeA);
        nodeB.Next.AddRange(new[] { nodeC, nodeD });
        nodeC.Previous.Add(nodeB);
        nodeD.Previous.Add(nodeB);

        // Act
        var result = nodeA.Remove(nodeB);

        // Assert
        Assert.True(result);
        Assert.Equal(2, nodeA.Next.Count);
        Assert.Contains(nodeC, nodeA.Next);
        Assert.Contains(nodeD, nodeA.Next);
    }

    [Fact]
    public void Remove_MultiplePreviousNodes_ConnectsAllPreviousToAllNext()
    {
        // Arrange
        var nodeStart = new DagMock();  // Common start node
        var nodeA = new DagMock();
        var nodeB = new DagMock();
        var nodeC = new DagMock(); // to remove
        var nodeD = new DagMock();

        // Build graph: start → A, start → B, A → C, B → C, C → D
        nodeStart.Next.AddRange(new[] { nodeA, nodeB });
        nodeA.Previous.Add(nodeStart);
        nodeB.Previous.Add(nodeStart);
        
        nodeA.Next.Add(nodeC);
        nodeB.Next.Add(nodeC);
        nodeC.Previous.AddRange(new[] { nodeA, nodeB });
        nodeC.Next.Add(nodeD);
        nodeD.Previous.Add(nodeC);

        // Act
        var result = nodeStart.Remove(nodeC);

        // Assert
        Assert.True(result);
        Assert.Contains(nodeD, nodeA.Next);
        Assert.Contains(nodeD, nodeB.Next);
        Assert.Equal(2, nodeD.Previous.Count);
        Assert.Contains(nodeA, nodeD.Previous);
        Assert.Contains(nodeB, nodeD.Previous);
    }

    [Fact]
    public void Remove_StartNodeWithMultipleNext_ReturnsFalse()
    {
        // Arrange
        var nodeA = new DagMock(); // start node
        var nodeB = new DagMock();
        var nodeC = new DagMock();

        nodeA.Next.AddRange(new[] { nodeB, nodeC });
        nodeB.Previous.Add(nodeA);
        nodeC.Previous.Add(nodeA);

        // Act
        var result = nodeA.Remove(nodeA);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Remove_NonExistentNode_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        // Act
        var result = node1.Remove(node2);

        // Assert
        Assert.False(result);
    }

    #endregion

    #region ToGraph Tests

    [Fact]
    public void ToGraph_SingleNode_ReturnsNode()
    {
        // Arrange
        var node = new DagMock();
        var nodes = new Dictionary<Guid, DagMock> { { node.Id, node } };

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(node.Id, result.Id);
    }

    [Fact]
    public void ToGraph_LinearGraph_ConnectsNodesCorrectly()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Edges.Add(new DagEdgeMock { FromId = node1.Id, ToId = node2.Id });
        node2.Edges.Add(new DagEdgeMock { FromId = node2.Id, ToId = node3.Id });

        var nodes = new Dictionary<Guid, DagMock>
        {
            { node1.Id, node1 },
            { node2.Id, node2 },
            { node3.Id, node3 }
        };

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(node1.Id, result.Id);
        Assert.Single(result.Next);
        Assert.Equal(node2.Id, result.Next[0].Id);
    }

    [Fact]
    public void ToGraph_MultipleStartNodes_ReturnsNull()
    {
        // Arrange
        var node1 = new DagMock(); // no incoming edges
        var node2 = new DagMock(); // no incoming edges
        var node3 = new DagMock();

        node1.Edges.Add(new DagEdgeMock { FromId = node1.Id, ToId = node3.Id });
        node2.Edges.Add(new DagEdgeMock { FromId = node2.Id, ToId = node3.Id });

        var nodes = new Dictionary<Guid, DagMock>
        {
            { node1.Id, node1 },
            { node2.Id, node2 },
            { node3.Id, node3 }
        };

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToGraph_EdgeToNonExistentNode_ReturnsNull()
    {
        // Arrange
        var node1 = new DagMock();
        var nonExistentId = Guid.NewGuid();

        node1.Edges.Add(new DagEdgeMock { FromId = node1.Id, ToId = nonExistentId });

        var nodes = new Dictionary<Guid, DagMock> { { node1.Id, node1 } };

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToGraph_CyclicGraph_ReturnsNull()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Edges.Add(new DagEdgeMock { FromId = node1.Id, ToId = node2.Id });
        node2.Edges.Add(new DagEdgeMock { FromId = node2.Id, ToId = node1.Id }); // cycle

        var nodes = new Dictionary<Guid, DagMock>
        {
            { node1.Id, node1 },
            { node2.Id, node2 }
        };

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToGraph_EmptyDictionary_ReturnsNull()
    {
        // Arrange
        var nodes = new Dictionary<Guid, DagMock>();

        // Act
        var result = nodes.ToGraph();

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region Cycle Detection Tests

    [Fact]
    public void HasCycle_LinearGraph_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node3);
        node3.Previous.Add(node2);

        // Act
        var result = node1.HasCycle();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void HasCycle_WithDirectCycle_ReturnsTrue()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node1); // cycle
        node1.Previous.Add(node2);

        // Act
        var result = node1.HasCycle();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void HasCycle_WithIndirectCycle_ReturnsTrue()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node3);
        node3.Previous.Add(node2);
        node3.Next.Add(node1); // cycle back to node1
        node1.Previous.Add(node3);

        // Act
        var result = node1.HasCycle();

        // Assert
        Assert.True(result);
    }

    #endregion

    #region IsAfter / IsBefore Tests

    [Fact]
    public void IsAfter_NodeIsAfter_ReturnsTrue()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node3);
        node3.Previous.Add(node2);

        // Act
        var result = node1.IsAfter(node3.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsAfter_NodeIsNotAfter_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        // Act
        var result = node1.IsAfter(node2.Id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsBefore_NodeIsBefore_ReturnsTrue()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();
        var node3 = new DagMock();

        node1.Next.Add(node2);
        node2.Previous.Add(node1);
        node2.Next.Add(node3);
        node3.Previous.Add(node2);

        // Act
        var result = node3.IsBefore(node1.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsBefore_NodeIsNotBefore_ReturnsFalse()
    {
        // Arrange
        var node1 = new DagMock();
        var node2 = new DagMock();

        // Act
        var result = node1.IsBefore(node2.Id);

        // Assert
        Assert.False(result);
    }

    #endregion
}

/// <summary>
/// Mock DAG node with controllable RemoveEdge failures for testing error handling
/// </summary>
internal class DagMockWithFailingRemoveEdge : DagMock
{
    public override bool RemoveEdge(Guid otherId)
    {
        return false; // Simulate failure
    }
}