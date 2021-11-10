using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ErrandsTests
{
    public class ErrandControllerTests
    {
        private List<Errand> GetTestErrands()
        {
            var user = new User() { Id = "111111111111111111111111111aaa", UserName = "Bob" };
            var errand = new List<Errand>
            {
                new Errand { Id = new Guid() , CreationDate = DateTime.Now, Cost = 200, Title = "Презентация", Description = "Сделать презентацию", User = user},
                //new Errand { Id = new Guid("7c9e6679-7425-40de-944b-e07fc1f90ae7"), CreationDate = DateTime.Now, Cost = 300, Title = "Дискретная математика", Description = "Сделать задания (1, 2, 3, 7, 8,) до 12:00, желательно расписывать.", User = user},
                //new Errand { Id = new Guid("1f8frd5b-d9cb-469f-a165-70867728150e"), CreationDate = DateTime.Now, Cost = 200, Title = "Основи програмування", Description = "Срочно нужно зделать контрольную роботу ( модуль )", User = user},
                ///new Errand { Id = new Guid("3u8fad5b-d9cb-469f-a165-10267728950e"), CreationDate = DateTime.Now, Cost = 200, Title = "Английский", Description = "Нужно перевести 5 текстов на английский, 5 на украинский и сделать к ним глоссарий на 50 слов.", User = user},

            };
            return errand;
        }
        [Fact]
        public void List_ContainsAllMyErrands()
        {
            // Arrange - create the mock repository
            var mock = new Mock<IErrandsRepository>();
            mock.Setup(p => p.GetErrandsByUserId("111111111111111111111111111aaa")).Returns(GetTestErrands);

            // Arrange - create a controller
            ErrandController controller = new ErrandController(mock.Object, null, null);

            // Action
            var result = controller.ListMyErrand().ViewData.Model as GetMyErrandViewModel;

            // Assert
            Assert.Equal(result.Title, result.Title);
            //Assert.Equal(GetTestErrands().ToArray()[0].Title, result.ToArray()[0].Title);
        }
        [Fact]
        public async void Create_CanCreateAndRedirect()
        {
            // Arrange
            Mock<IErrandsRepository> mock = new Mock<IErrandsRepository>();
            mock.Setup(m => m.GetErrandsByUserId("")).Returns(GetTestErrands());

            // Arrange - create the controller
            ErrandController controller = new ErrandController(mock.Object, null, null);

            // Act
            var errands = GetTestErrands().ToArray();
            var err1 = GetViewModel<Errand>(await controller.Edit(errands[0].Id));
            //var err2 = GetViewModel<Errand>(await controller.Edit(errands[1].Id));
            //var err3 = GetViewModel<Errand>(await controller.Edit(errands[2].Id));

            // Assert
            Assert.Equal("", err1.Id.ToString());
        }
        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
