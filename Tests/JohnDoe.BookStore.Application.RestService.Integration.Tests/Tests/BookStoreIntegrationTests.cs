using JohnDoe.BookStore.Application.RestService.Integration.Tests.TestFixtures;
using JohnDoe.BookStore.Application.Shared.DomainModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace JohnDoe.BookStore.Application.RestService.Integration.Tests.Tests
{
    [Collection("Integration test collection")]
    public class BookStoreIntegrationTests
    {
        
        private readonly HttpClient client;

        private readonly WireMockServer wireMockServer;

        public BookStoreIntegrationTests(IntegrationTestFixture<Startup> testFixture)
        {
            this.client = testFixture.CreateClient();
            this.wireMockServer = testFixture.WireMockServer;
            SetupWireMockResponse();
        }

        private void SetupWireMockResponse()
        {

            wireMockServer.Given(Request.Create()
               .UsingMethod("GET")
               .WithPath("api/BookStore")
               .WithParam("Id", new[] { "" }))
           .RespondWith(Response.Create()
               .WithBodyAsJson(new Book { })
               .WithStatusCode(HttpStatusCode.OK));



            //wireMockServer.Given(Request.Create()
            //    .UsingMethod("POST")
            //    .WithPath("api/BookOrder"))
            //.RespondWith(Response.Create()
            //    .WithBodyAsJson(new GenericResponse { })
            //    .WithStatusCode(HttpStatusCode.OK));

        }

        [Fact]
        public async Task GetBookStore_Valid_200Ok()
        {
            var response = await client.GetAsync("api/BookStore");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var BookResponse = JsonConvert.DeserializeObject<IEnumerable<Book>>(responseBody);
            Assert.Equal(13, BookResponse.ToList().Count);
        }

        [Fact]
        public async Task GetBookStorebyName_Valid_200Ok()
        {

            var bookInfo = new Book
            {
                ISBNCode = "978-3-16-177050-0",
                Author = "J. K. Rowling",
                Name = "Harry Potter and the Sorcerer’s Stone ",
                Count = 2,
                MinPrice = 55.80m,
                MaxPrice = 55.80m
            };
            var response = await client.GetAsync("api/BookStore/GetByname/Harry Potter and the Sorcerer’s Stone");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var BookResponse = JsonConvert.DeserializeObject<Book>(responseBody);
            Assert.Equal(bookInfo.Count, BookResponse.Count);
            Assert.Equal(bookInfo.MinPrice, BookResponse.MinPrice);
            Assert.Equal(bookInfo.MaxPrice, BookResponse.MaxPrice);
        }



        [Fact]
        public async Task GetGetByISBN_Valid_200Ok()
        {
            var isBn = "978-3-16-177050-0";
            var bookInfo = new BaseBookInfo
            {
                ISBNCode = isBn,
                Author = "J. K. Rowling",
                Name = "Harry Potter and the Sorcerer’s Stone ",
                BookInfoListByStore = new List<BookInfoByStore>()
                   
            };
            bookInfo.BookInfoListByStore.Add(new BookInfoByStore
            {
                Count = 2,
                StoreIdentifier = "StoreB-001",
                Price = 55.80m,
                StoreName = "StoreB"
            });
            
            var response = await client.GetAsync($"api/BookStore/GetByISBN/{isBn}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            var BookResponse = JsonConvert.DeserializeObject<BaseBookInfo>(responseBody);
            Assert.Equal(bookInfo.ISBNCode, BookResponse.ISBNCode);
            Assert.Equal(bookInfo.BookInfoListByStore.Count, BookResponse.BookInfoListByStore.Count);
        }

    }
}
