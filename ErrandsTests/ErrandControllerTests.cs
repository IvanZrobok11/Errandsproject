using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Errands.Data.Services;
using Errands.Domain.Models;
using Errands.Mvc.Controllers;
using Errands.Mvc.Models.ViewModels;
using Errands.Mvc.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Security.Claims;
using System.Threading.Tasks;
using Errands.Mvc;

using Microsoft.AspNetCore.Mvc.Testing;
using MyTested.AspNetCore.Mvc;

namespace ErrandsTests
{
    //[TestFixture]
    public class ErrandControllerTests //: IClassFixture<>
    {
        public ErrandControllerTests()
        {
            randomGuid = Guid.NewGuid();
        }
        private Guid randomGuid;

        [Fact]
        public void GetErrand_ShouldBe_RoutedCorrectly()
        {
            MyRouting
                .Configuration()
                .ShouldMap($"/Errand/GetErrand/{Guid.NewGuid()}")
                .To<ErrandController>(c => c.GetErrand(Guid.NewGuid()));
        }
        [Fact]
        public void GetErrand_ShouldBe_ReturnView()
        {
            MyController<HomeController>
                .Instance()
                .Calling(c => c.Index(1))
                .ShouldReturn().View();
        }






    }
}
