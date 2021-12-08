using System;
using Xunit;
using Moq;
using System.Collections.Generic;
using Errands.Domain.Models;
using Errands.Data.Services;
using Errands.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using ErrandsTests.FakeDependencies;
using MyTested.AspNetCore.Mvc;
using Shouldly;

namespace ErrandsTests
{
    public class HomeControllerTests
    {
        //[Fact]
        [Theory]
        [InlineData(18, 1, ControllerConstants.ItemPerMainPage)]
        [InlineData(18, 2, 6)]
        public void All_ShouldReturn_DefaultViewWithCorrectModel(int total, int page, int expectedCount)
            => MyController<HomeController>
                .Instance(instance => instance
                    .WithData(ErrandsTestData.GetErrands(total)))
                .Calling(c => c.Index(page))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListErrandsViewModel>()
                    .Passing(model =>
                    {
                        model.Errands.Count().ShouldBe(expectedCount);
                        model.PageInfo.TotalItems.ShouldBe(total);
                        model.PageInfo.CurrentPage.ShouldBe(page);

                    }));

    }
}
