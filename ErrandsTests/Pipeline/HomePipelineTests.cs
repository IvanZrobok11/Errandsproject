using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using ErrandsTests.FakeDependencies;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace ErrandsTests.Pipeline
{
    public class HomePipelineTests
    {
        //Item per page - 10
        [Theory]
        [InlineData(10, 10, 1)]
        [InlineData(11, 1, 2)]
        [InlineData(35, 10, 1)]
        [InlineData(35, 5, 4)]
        public void GetIndexShouldReturnDefaultViewWithCorrectModel(int total, int expected, int page)
            => MyMvc
                .Pipeline()
                .ShouldMap($"/Home/Index?pageNumber={page}")
                .To<HomeController>(c => c.Index(page))
                .Which(controller => controller
                    .WithData(ErrandsTestData.GetErrands(total)))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListErrandsViewModel>()
                    .Passing(articles => articles.Errands
                        .ToList().Count.ShouldBe(expected)));
    }
}
