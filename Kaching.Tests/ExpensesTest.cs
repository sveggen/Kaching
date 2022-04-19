using System.Collections.Generic;
using Kaching.Controllers;
using Kaching.Repositories;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Kaching.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task IndexGet_ReturnsListOfExpenses_WhenExpensesExists()
    {
        var randomMonthString = "March";
        var randomMonthInt = 3;
        var expenseList = new List<Expense>();
        var personList = new List<Person>();

        _mockExpenseRepository.Setup
                (repo => repo.GetExpenses(randomMonthInt))
            .ReturnsAsync(expenseList);

        _mockPersonRepository.Setup
                (repo => repo.GetAllPersons())
            .Returns(personList);


        var result = await _controller.Index(randomMonthString) as ViewResult;

        //Assert.IsInstanceOf<List<Expense>>(result.Model);
        Assert.IsInstanceOf<ViewResult>(result);
    }


    [Test]
    public async Task IndexGet_ReturnsDecimal_WhenSumIsDisplayed()
    {
        var randomMonthString = "March";
        var randomMonthInt = 3;
        var expenseList = new List<Expense>();
        var personList = new List<Person>();

        _mockExpenseRepository.Setup
                (repo => repo.GetExpenses(randomMonthInt))
            .ReturnsAsync(expenseList);
        _mockExpenseRepository.Setup
                (repo => repo.GetExpenseSum(randomMonthInt))
            .Returns(It.IsAny<decimal>());

        _mockPersonRepository.Setup
                (repo => repo.GetAllPersons())
            .Returns(personList);


        var viewResult = await _controller.Index(randomMonthString) as ViewResult;
        var result = viewResult.ViewData["MonthExpenseSum"];

        Assert.IsInstanceOf<decimal>(result);
    }
}