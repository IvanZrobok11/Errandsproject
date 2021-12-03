using Errands.Domain.Models;
using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using ErrandsTests.FakeDependencies;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Errands.Application.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace ErrandsTests
{
    public class ErrandControllerTests
    {
        [Fact]
        public void ErrandController_ShouldHave_AuthorizeAttribute()
        {
            MyMvc
                .Controller<ErrandController>()
                .ShouldHave()
                .Attributes(a => a.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void GetErrand_ShouldHave_AllowAnonymousFilter()
        {
            MyController<ErrandController>
                .Calling(m => m.GetErrand(Guid.Empty))
                .ShouldHave()
                .ActionAttributes(
                    a => a.AllowingAnonymousRequests());
        }
        [Fact]
        public void GetErrand_ShouldReturn_NotFoundWhenInvalidErrandId()
        {
            Guid randomGuid = Guid.NewGuid();
            MyController<ErrandController>
                .Instance(i => i
                    .WithData(ErrandsTestData.GetErrands(10)))
                .Calling(m => m
                    .GetErrand(randomGuid))
                .ShouldReturn()
                .NotFound();
        }
        [Theory]
        [InlineData(true, TestUser.Username)]
        [InlineData(false, TestUser.Username)]
        public void GetErrand_ShouldReturn_ViewWithModel_AndHaveAllowAnonymousFilter(bool sameUser, string userId)
        {
            MyController<ErrandController>
                .Instance(d => 
                    d.WithData(ErrandsTestData.GetErrands(10)))
                .Calling(c => c.GetErrand(ErrandsTestData.GenerateGuid(1)))
                
                .ShouldHave()

                .ActionAttributes(c => c.AllowingAnonymousRequests()
                    .RestrictingForHttpMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<Errand>()
                    .Passing(a =>
                    {
                        a.ShouldNotBeNull();
                        a.Id.Equals(ErrandsTestData.GenerateGuid(1));
                    }));

        }
        [Fact]
        public void ListMyErrand_ShouldReturn_ViewWithModel()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithUser(TestUser.Identifier, TestUser.Username)
                    .WithData(ErrandsTestData.GetErrands(2)))
                .Calling(m => m.ListMyErrand(With.Any<int>()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<List<GetMyErrandServiceModel>>()
                    .Passing(e =>
                        {
                            e.ShouldNotBeEmpty();
                            e.SingleOrDefault(a => a.Title == "Title1").ShouldNotBeNull();
                            e.SingleOrDefault(a => a.Title == "Title2").ShouldNotBeNull();
                        }
                    ));
        }
        [Fact]
        public void ListErrandToDo_ShouldReturn_ViewWithModel()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithUser("TestHelperUserId","")
                    .WithData(ErrandsTestData.GetErrands(2)))
                .Calling(m => m.ListErrandToDo(With.Any<int>()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<List<GetErrandToDoServiceModel>>()
                    .Passing(e =>
                        {
                            e.ShouldNotBeEmpty();
                            e.SingleOrDefault(a => a.Title == "Title1").ShouldNotBeNull();
                            e.SingleOrDefault(a => a.Title == "Title2").ShouldNotBeNull();
                        }
                    ));
        }

        [Fact]
        public void GetFile_ShouldReturn_VirtualFileResult()
        {
            MyController<ErrandController>
                .Instance(i => 
                    i.WithData(ErrandsTestData.GetFile()))
                .Calling(f => f.GetFile(ErrandsTestData.GenerateGuid(5)))
                .ShouldReturn()
                .File();
        }
        [Fact]
        public void GetCreate_ShouldHave_GetActionAttribute_AndReturnCorrectViewModel()
        {
            MyController<ErrandController>
                .Calling(m => m.Create())
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpGetAttribute>())
                .AndAlso()
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<CreateErrandModel>());
        }
        [Fact]
        public void PostCreate_ShouldHave_PostActionAttribute_AndReturnViewWithSameModel_WhenModelStateInvalid()
        {
            MyController<ErrandController>
                .Calling(m => m.Create(With.Default<CreateErrandModel>()))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpPostAttribute>())
                .AndAlso()
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<CreateErrandModel>());
        }

        [Theory]
        [InlineData("Article Title", "Article Content", 100)]
        public void PostCreate_ShouldReturnViewWithSameModel_WhenModelStateValid(
            string title, string description, decimal cost)
        {
            MyController<ErrandController>
                .Calling(m => m.Create(With.Default<CreateErrandModel>()))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpPostAttribute>())
                .AndAlso()
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()
                .ShouldReturn()
                .View(With.Default<CreateErrandModel>());
        }





    }
}
