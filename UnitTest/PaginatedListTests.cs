using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using TravelSiteWeb.Services;

namespace TravelSiteWeb.Tests
{
    public class PaginatedListTests
    {
        [Fact]
        public void PaginatedList_CorrectlyCalculatesTotalPages()
        {
            // Arrange
            var items = Enumerable.Range(1, 10).ToList();
            int count = 100;
            int pageIndex = 1;
            int pageSize = 10;

            // Act
            var paginatedList = new PaginatedList<int>(items, count, pageIndex, pageSize);

            // Assert
            Assert.Equal(10, paginatedList.TotalPages);
        }

        [Fact]
        public void PaginatedList_HasPreviousPage_ReturnsFalse_WhenOnFirstPage()
        {
            // Arrange
            var items = Enumerable.Range(1, 10).ToList();
            int count = 100;
            int pageIndex = 1;
            int pageSize = 10;

            // Act
            var paginatedList = new PaginatedList<int>(items, count, pageIndex, pageSize);

            // Assert
            Assert.False(paginatedList.HasPreviousPage);
        }

        [Fact]
        public void PaginatedList_HasNextPage_ReturnsTrue_WhenNotOnLastPage()
        {
            // Arrange
            var items = Enumerable.Range(1, 10).ToList();
            int count = 100;
            int pageIndex = 1;
            int pageSize = 10;

            // Act
            var paginatedList = new PaginatedList<int>(items, count, pageIndex, pageSize);

            // Assert
            Assert.True(paginatedList.HasNextPage);
        }
    }
}
