using Errands.Domain.Models;
using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using ErrandsTests.FakeDependencies;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Errands.Application.Common.Services;
using Errands.Mvc.Services;
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
                    a => a
                        .AllowingAnonymousRequests());
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
                .Calling(c => c
                    .GetErrand(ErrandsTestData.GenerateGuid(1)))
                
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
                        a.Id.ShouldBe(ErrandsTestData.GenerateGuid(1));
                    }));
        }
        [Fact]
        public void ListMyErrand_ShouldReturn_ViewWithModel()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithUser(TestUser.Identifier, TestUser.Username)
                    .WithData(ErrandsTestData.GetErrands(2)))
                .Calling(m => m
                    .ListMyErrand(With.Any<int>()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<ListMyErrandsViewModel>()
                    .Passing(e =>
                        {
                            e.Errands.ShouldNotBeEmpty();
                            e.Errands.SingleOrDefault(a => a.Title == "Title1")
                                .ShouldNotBeNull();
                            e.Errands.SingleOrDefault(a => a.Title == "Title2")
                                .ShouldNotBeNull();
                        }
                    ));
        }
        [Theory]
        [InlineData(20, 2, ControllerConstants.ItemPerMyErrandsPage)]
        [InlineData(18, 1, ControllerConstants.ItemPerMyErrandsPage)]
        public void ListMyErrand__ShouldHave_Pagination(int total, int page, int expectedCount)
            => MyController<ErrandController>
                .Instance(instance => instance
                    .WithData(ErrandsTestData.GetErrands(total))
                    .WithUser(TestUser.Identifier, TestUser.Username))
                .Calling(c => c.ListMyErrand(page))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListMyErrandsViewModel>()
                    .Passing(model =>
                    {
                        model.Errands.Count().ShouldBe(expectedCount);
                        model.PageInfo.TotalItems.ShouldBe(total);
                        model.PageInfo.CurrentPage.ShouldBe(page);
                    }));
        [Fact]
        public void ListErrandToDo_ShouldReturn_ViewWithModel()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithUser(TestUser.Identifier, TestUser.Username)
                    .WithData(ErrandsTestData.GetErrands(2, helperUserId: TestUser.Identifier)))
                .Calling(m => m.ListErrandToDo(With.Any<int>()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<ListErrandsToDoViewModel>()
                    .Passing(e =>
                        {
                            e.Errands.ShouldNotBeEmpty();
                            e.Errands.SingleOrDefault(a => a.Title == "title1").ShouldNotBeNull();
                            e.Errands.SingleOrDefault(a => a.Title == "title2").ShouldNotBeNull();
                        }
                    ));
        }
        [Theory]
        [InlineData(20, 2, ControllerConstants.ItemPerToDoErrandsPage)]
        [InlineData(18, 1, ControllerConstants.ItemPerToDoErrandsPage)]
        public void ListErrandToDo__ShouldHave_Pagination(int total, int page, int expectedCount)
            => MyController<ErrandController>
                .Instance(instance => instance
                    .WithData(ErrandsTestData.GetErrands(total, helperUserId:TestUser.Identifier))
                    .WithUser(TestUser.Identifier, TestUser.Username))
                .Calling(c => c.ListErrandToDo(page))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListErrandsToDoViewModel>()
                    .Passing(model =>
                    {
                        model.Errands.Count().ShouldBe(expectedCount);
                        model.PageInfo.TotalItems.ShouldBe(total);
                        model.PageInfo.CurrentPage.ShouldBe(page);
                    }));
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
        [InlineData("title", "description", 1)]
        public void PostCreate_ShouldReturnViewWithSameModel_WhenModelStateValid(
            string title, string description, decimal cost)
        {
            MyController<ErrandController>
                .Calling(m => m.Create(new CreateErrandModel()
                {
                    Title = title,
                    Desc = description,
                    Cost = cost
                }))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpPostAttribute>())
                .AndAlso()
                .ShouldHave()
                .ValidModelState()
                .AndAlso()
                .ShouldHave()
                .Data(data => data
                    .WithSet<Errand>(
                        set =>
                        {
                            set.ShouldNotBeEmpty();
                            set.SingleOrDefault(a => a.Title == title).ShouldNotBeNull();
                        }

                    ))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ListMyErrand");
        }

        [Fact]
        public void EditGet_ShouldReturnNotFound_WhenInvalidId()
            => MyController<ErrandController>
                .Calling(c => c.Edit(With.Any<Guid>()))
                .ShouldReturn()
                .NotFound();
       
        [Fact]
        public void GetEdit_ShouldHave_GetActionAttribute_AndReturnCorrectViewModel()
        {
            MyController<ErrandController>
                .Instance(instance => instance
                    .WithData(ErrandsTestData.GetErrands(2)))
                .Calling(m => m.Edit(
                    ErrandsTestData.GenerateGuid(1)
                    ))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpGetAttribute>())
                .AndAlso()
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<EditErrandModel>()
                    .Passing(errand => errand.Title.ShouldBeEquivalentTo("title1"))
                );
        }

        [Fact]
        public void Take_ShouldRedirectToHomeView_WhenHelperUserAlreadySet()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithData(ErrandsTestData.GetErrands(2, helperUserId: "someId"))
                    .WithUser(TestUser.Identifier, TestUser.Username)
                )
                .Calling(m => m.Take(ErrandsTestData.GenerateGuid(1)))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpPostAttribute>())
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index", "Home");
        }

        [Fact]
        public void Take_ShouldTakeErrand_AndSetTempDataMessage_AndRedirect()
        {
            MyController<ErrandController>
                .Instance(i => i
                    .WithData(ErrandsTestData.GetErrands(1,sameUser:false , helperUserId: null))
                    .WithUser("helperUserId", "helperUserName")
                )
                .Calling(m => m.Take(ErrandsTestData.GenerateGuid(1)))
                .ShouldHave()
                .Data(d => d
                    .WithSet<Errand>(d =>
                    {
                        d.ShouldNotBeEmpty();
                        var er = d.SingleOrDefault(e => e.Id == ErrandsTestData.GenerateGuid(1));
                         er.Active.ShouldBe(false);
                         er.HelperUserId = "helperUserId";
                    }))
                .AndAlso()  
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(TempDataResult.SuccessMessage))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("ListErrandToDo");
        }

        [Fact]
        public void DeleteShould_ReturnNotFoundWhenInvalidId()
            => MyController<ErrandController>
                .Calling(c => c.Delete(With.Any<Guid>()))
                .ShouldReturn()
                .NotFound();

        
    }
}
