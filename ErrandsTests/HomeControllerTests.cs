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

namespace ErrandsTests
{
    public class HomeControllerTests
    {
        private List<Errand> GetTestErrands()
        {
            var errand = new List<Errand>
            {
                new Errand { Id = Guid.NewGuid(), CreationDate = DateTime.Now, Cost = 200, Title = "Презентация", Description = "Сделать презентацию"},
                new Errand { Id = Guid.NewGuid(), CreationDate = DateTime.Now, Cost = 300, Title = "Дискретная математика", Description = "Сделать задания (1, 2, 3, 7, 8,) до 12:00, желательно расписывать."},
                new Errand { Id = Guid.NewGuid(), CreationDate = DateTime.Now, Cost = 200, Title = "Основи програмування", Description = "Срочно нужно зделать контрольную роботу ( модуль )"},
                new Errand { Id = Guid.NewGuid(), CreationDate = DateTime.Now, Cost = 200, Title = "Английский", Description = "Нужно перевести 5 текстов на английский, 5 на украинский и сделать к ним глоссарий на 50 слов."},

            };
            return errand;
        }
        [Fact]
        public void Index_ReturnAllErrans()
        {
            // Arrange
            var mock = new Mock<IErrandsRepository>();
            mock.Setup(repo => repo.Errands).Returns(GetTestErrands());

            HomeController controller = new HomeController(mock.Object);
            controller.ItemsPerPage = 10;

            // Act
            ErrandsListViewModel result = controller.Index(pageNumber: 1)
                .ViewData.Model as ErrandsListViewModel;

            // Assert
            Errand[] errandsResult = result.Errands.ToArray();
            Errand[] errandsTestData = GetTestErrands().OrderByDescending(d => d.CreationDate).ToArray();

            Assert.Equal(GetTestErrands().Count, result.Errands.Count());
            Assert.Equal(errandsTestData[0].Title, errandsResult[0].Title);
            Assert.Equal(errandsTestData[1].Title, errandsResult[1].Title);
            Assert.Equal(errandsTestData[2].Title, errandsResult[2].Title);
        }
        
        [Fact]
        public void Index_CanPaginate()
        {
            // Arrange
            var mock = new Mock<IErrandsRepository>();
            mock.Setup(repo => repo.Errands).Returns(GetTestErrands());

            HomeController controller = new HomeController(mock.Object) 
            { 
                ItemsPerPage = 3 
            };
            

            // Act
            ErrandsListViewModel result = controller.Index(pageNumber: 2)
                .ViewData.Model as ErrandsListViewModel;

            // Assert
            Errand[] errandArr = result.Errands.ToArray();

            Assert.True(errandArr.Length == 1);
        }
        [Fact]
        public void Index_CanSendPadinationViewModel()
        {
            // Arrange
            var mock = new Mock<IErrandsRepository>();
            mock.Setup(repo => repo.Errands).Returns(GetTestErrands());

            HomeController controller = new HomeController(mock.Object)
            {
                ItemsPerPage = 3
            };

            // Act
            ErrandsListViewModel result = controller.Index(pageNumber: 2)
                .ViewData.Model as ErrandsListViewModel;

            // Assert
            PageInfo pageInfo = result.PageInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(4, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);

        }
    }
}
