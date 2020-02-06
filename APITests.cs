using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpTestProject
{
    [TestFixture]
    public class APITests
    {
        [Test]
        public void GetWithJsonDeserializer()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}", Method.GET);
            request.AddUrlSegment("postid", 1);
            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var result = deserialize.Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["author"], Is.EqualTo("typicode"), "Author not correct");
        }

        [Test]
        public void PostWithAnonymousBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/profile", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { name = "Raj" });
            request.AddUrlSegment("postid", 1);
            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var result = deserialize.Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["name"], Is.EqualTo("Raj"), "Profile name not correct");
        }

        [Test]
        public void PostWithTypeClassBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Posts() {id="4",title="REST API Testing",author="Nani" });
            var response = client.Execute(request);

            var deserialize = new JsonDeserializer();
            var result = deserialize.Deserialize<Dictionary<string, string>>(response);
            Assert.That(result["author"], Is.EqualTo("Nani"), "Author not correct");
        }

        [Test]
        public void PostWithGenericExecute()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new Posts() { id = "5", title = "RESTSharp", author = "Swathi" });
            var response = client.Execute<Posts>(request);

            Assert.That(response.Data.author, Is.EqualTo("Swathi"), "Author not correct");
        }
    }
}
