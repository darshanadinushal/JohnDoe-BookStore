using JohnDoe.BookStore.Application.RestService.Integration.Tests.TestFixtures;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using JohnDoe.BookStore.Application.Shared.DomainModel.common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace JohnDoe.BookStore.Application.RestService.Integration.Tests.Tests
{
    [Collection("Integration test collection")]
    public class BookOrderIntegrationTests
    {
        private readonly HttpClient client;

        private readonly WireMockServer wireMockServer;

        public BookOrderIntegrationTests(IntegrationTestFixture<Startup> testFixture)
        {
            this.client = testFixture.CreateClient();
            this.wireMockServer = testFixture.WireMockServer;
            SetupWireMockResponse();
        }

        private void SetupWireMockResponse()
        {

            wireMockServer.Given(Request.Create()
                .UsingMethod("POST")
                .WithPath("api/BookOrder"))
            .RespondWith(Response.Create()
                .WithBodyAsJson(new GenericResponse { })
                .WithStatusCode(HttpStatusCode.OK));

        }



        [Fact]
        public async Task PostBookOrder_Valid_200Ok()
        {
            var bookOrder = new BookOrder
            {
                Email ="darshanadinushal@gmail.com",
                ISBNCode= "978-3-16-148410-0",
                StoreIdentifier= "StoreA-001"
            };
            var response = await client.PostAsync("api/BookOrder", new StringContent(JsonConvert.SerializeObject(bookOrder), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(responseBody);
            Assert.True(!messageResponse.isError);
        }


        [Fact]
        public async Task PostBookOrder_InValidISBNCode()
        {
            var bookOrder = new BookOrder
            {
                Email = "darshanadinushal@gmail.com",
                ISBNCode = "978-3-16-133410-0",
                StoreIdentifier = "StoreA-001"
            };
            var response = await client.PostAsync("api/BookOrder", new StringContent(JsonConvert.SerializeObject(bookOrder), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(responseBody);
            Assert.True(messageResponse.isError);
            Assert.Equal("provide book ISBN code does not have available book", messageResponse.Message.Trim());
        }


        [Fact]
        public async Task PostBookOrder_InValidEmailAddress()
        {
            var bookOrder = new BookOrder
            {
                Email = "darshanadinushal",
                ISBNCode = "978-3-16-148410-0",
                StoreIdentifier = "StoreA-001"
            };
            var response = await client.PostAsync("api/BookOrder", new StringContent(JsonConvert.SerializeObject(bookOrder), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(responseBody);
            Assert.True(messageResponse.isError);
            Assert.Equal("provide email address is not valid", messageResponse.Message.Trim());
        }

    }
}
