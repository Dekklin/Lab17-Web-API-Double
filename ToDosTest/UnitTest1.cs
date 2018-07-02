using Lab17_WebApi.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Lab17_WebApi.Models;
using Lab17_WebApi.Controllers;
using System;
using System.Net;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ToDosTest
{
    public class UnitTest1
    {
        [Fact]
        public void CanCreateToDo()
        {
            DbContextOptions<ToDoContext> options = new
                DbContextOptionsBuilder<ToDoContext>().UseInMemoryDatabase("Test2").Options;
        }
    }
}
