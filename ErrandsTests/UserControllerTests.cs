using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Errands.Application.Common.Services;
using Errands.Domain.Models;
using Errands.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace ErrandsTests
{
    public class UserControllerTests
    {
        public class UsersTestData
        {
            public static IEnumerable<User> GetUsers(int count, bool oneUser = false)
            {
                return oneUser ? new List<User>
                {
                    new User{Id = TestUser.Identifier, UserName = TestUser.Username}

                } : Enumerable.Range(1, count).Select(u => new User()
                {
                    Id = u.ToString(),
                    UserName = $"name{u}",
                    Email = $"name{u}gmail.com",
                    FirstName = "name",
                    LastName = "name"
                });
            }
        
        }
        [Fact]
        public void ProfileGet_ShouldReturn_CurrentUserWithCorrectModel_WhenRoutDataValueEmpty()
        {
            MyController<UserController>
                .Instance(i => i
                    .WithData(UsersTestData.GetUsers(1, true))
                    .WithUser(TestUser.Identifier, TestUser.Username))
                .Calling(m => m
                    .Profile(With.No<string>()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<UserProfileModel>()
                    .Passing(sh =>
                        {
                            sh.ShouldNotBeNull();
                            sh.UserName.ShouldBe(TestUser.Username);
                            sh.Id.ShouldBe(TestUser.Identifier);
                        }
                    ));
        }
        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("3")]
        public void ProfileGet_ShouldReturn_UserProfileModelForId(string identity)
        {
            MyController<UserController>
                .Instance(i => i
                    .WithData(UsersTestData.GetUsers(3))
                   )
                .Calling(m => m
                    .Profile(identity))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<UserProfileModel>()
                    .Passing(sh =>
                        {
                            sh.ShouldNotBeNull();
                            sh.Id.ShouldBe(identity);
                        }
                    ));
        }
        [Theory]
        [InlineData("1", "name1")]
        [InlineData("2", "name2")]
        [InlineData("3", "name3")]
        [InlineData("4", "name4")]
        [InlineData("5", "name5")]
        public void ChangeInfoGet_ShouldReturn_UserProfileModelCurrentUser(string identity, string userName)
        {
            MyController<UserController>
                .Instance(i => i
                    .WithData(UsersTestData.GetUsers(5))
                    .WithUser(identity, userName))
                .Calling(m => m
                    .ChangeInfo())
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<UserProfileModel>()
                    .Passing(sh =>
                        {
                            sh.ShouldNotBeNull();
                            sh.UserName.ShouldBe(userName);
                            sh.Id.ShouldBe(identity);
                        }
                    ));
        }

        [Fact]
        public void ChangeInfoPost_ShouldReturn_ViewWhenInvalidModel()
        {
            MyController<UserController>
                .Calling(m => m
                    .ChangeInfo(
                        With.Default<UserProfileModel>()))
                .ShouldHave()
                .ActionAttributes(a => a
                    .ContainingAttributeOfType<HttpPostAttribute>())
                .AndAlso()
                .ShouldHave()
                .InvalidModelState()
                .AndAlso()

                .ShouldReturn()
                .View(With.Default<UserProfileModel>());
        }
        [Fact]
        public void ChangeInfoPost_ShouldRedirect_ViewWhenValidModelState()
        {
            MyController<UserController>
                .Instance(d =>
                {
                    d.WithUser(TestUser.Identifier, TestUser.Username);
                    d.WithData(UsersTestData.GetUsers(1, true));
                })
                .Calling(m => m
                    .ChangeInfo(
                        With.Default<UserProfileModel>()))
                .ShouldReturn()
                .RedirectToAction("Profile");
        }

    }
}
