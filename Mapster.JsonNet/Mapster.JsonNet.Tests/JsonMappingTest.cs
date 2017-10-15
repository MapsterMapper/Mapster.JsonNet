using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace Mapster.JsonNet.Tests
{
    [TestClass]
    public class JsonMappingTest
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            TypeAdapterConfig.GlobalSettings.EnableJsonMapping();
        }

        [TestMethod]
        public void JsonToJson()
        {
            var json = new JObject();
            var result = json.Adapt<JObject>();
            result.ShouldBe(json);
        }

        [TestMethod]
        public void FromString()
        {
            var str = @"{ ""foo"": ""bar"" }";
            var result = str.Adapt<JObject>();
            result["foo"].ShouldBe("bar");
        }

        [TestMethod]
        public void ToString()
        {
            var json = new JObject {["foo"] = "bar"};
            var result = json.Adapt<string>();
            result.ShouldContainWithoutWhitespace(@"{""foo"":""bar""}");
        }

        [TestMethod]
        public void FromObject()
        {
            var obj = new Mock {foo = "bar"};
            var result = obj.Adapt<JObject>();
            result["foo"].ShouldBe("bar");
        }

        [TestMethod]
        public void ToObject()
        {
            var json = new JObject { ["foo"] = "bar" };
            var result = json.Adapt<Mock>();
            result.foo.ShouldBe("bar");
        }
    }

    public class Mock
    {
        public string foo { get; set; }
    }
}
