using System.Net;
using System.Net.Http;
using Core.IntegrationTest.Fixtures;
using Core.Web.DTOs;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Task = System.Threading.Tasks.Task;

namespace Core.IntegrationTests.Controllers
{
    public class BookControllerTest : IClassFixture<WebFixture<StartupStub>>
    {
        private readonly WebFixture<StartupStub> _fixture;

        public BookControllerTest(WebFixture<StartupStub> fixture)
        {
            _fixture = fixture;
        }

		[Fact]
		public async Task PostAsyncCallWithBook()
		{
			//Arrange            
			var payload = new BookDto { AuthorName = "Mahesh", Name = "DotNetCore" };

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/Book" );
			request.Content = new StringContent(JsonConvert.SerializeObject(payload));

			//Act
			var response = await _fixture.Client.SendAsync(request);

			//Assert                        
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}


		[Fact]
		public async Task GetAsyncCallWithBooks()
		{
			//Arrange            
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "/Book");

			//Act
			var response = await _fixture.Client.SendAsync(request);

			//Assert                        
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task GetByIdAsyncCall()
		{
			//Arrange
			int bookId = 1;
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"/Book/{bookId}");

			//Act
			var response = await _fixture.Client.SendAsync(request);

			//Assert                        
			response.Should().NotBeNull();
			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}		
	}
}


