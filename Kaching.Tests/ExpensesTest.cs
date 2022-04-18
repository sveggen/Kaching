using Kaching.Controllers;
using Kaching.Repositories;
using Moq;
using NUnit.Framework;

namespace Kaching.Tests;

[TestFixture]
public class ExpensesTest
{
    private Mock<IExpenseRepository> _mockExpenseRepository;
    private Mock<IPersonRepository> _mockPersonRepository;
    private ExpensesController _controller;

    [SetUp]
    public void Setup()
    {
        _mockExpenseRepository = new Mock<IExpenseRepository>();
        _mockPersonRepository = new Mock<IPersonRepository>();
        _controller = new ExpensesController(
            _mockExpenseRepository.Object, _mockPersonRepository.Object);

    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}