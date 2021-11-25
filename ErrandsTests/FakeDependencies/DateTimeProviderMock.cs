using System;
using System.Collections.Generic;
using System.Text;
using Errands.Mvc.Services;
using Moq;

namespace ErrandsTests.FakeDependencies
{
    public class DateTimeProviderMock 
    {
        public static IDateTimeProvider Create
        {
            get
            {
                var moq = new Mock<IDateTimeProvider>();
                moq.Setup(m => m.Now())
                    .Returns(new DateTime(1, 1, 1));
                return moq.Object;
            }
        }
    }
}
