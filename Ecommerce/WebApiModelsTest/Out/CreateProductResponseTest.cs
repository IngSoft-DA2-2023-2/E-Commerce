using ApiModels.Out;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.Out
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateProductRequestTest
    {
        private CreateProductResponse productResponseExample;
        private const string _nameSample = "name sample";
        private const int _priceSample = 100;
        private const string _descriptionSample = "description sample";
        private const string _brandSample = "brand sample";
        private const string _categorySample = "category sample";
        private readonly List<string> _coloursSample = new List<string> { "Red", "Blue" };
        private readonly Guid _guidSample = new Guid();

        [TestInitialize]
        public void Init()
        {
            productResponseExample = new CreateProductResponse();
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsGUID()
        {
            productResponseExample.Id = _guidSample;
            Assert.AreEqual(_guidSample, productResponseExample.Id);
        }


        [TestMethod]
        public void GivenProductResponseReturnsItsName()
        {
            productResponseExample.Name = _nameSample;
            Assert.AreEqual(_nameSample, productResponseExample.Name);
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsPrice()
        {
            productResponseExample.Price = _priceSample;
            Assert.AreEqual(_priceSample, productResponseExample.Price);
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsDescription()
        {
            productResponseExample.Description = _descriptionSample;
            Assert.AreEqual(_descriptionSample, productResponseExample.Description);
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsBrand()
        {
            productResponseExample.Brand = _brandSample;
            Assert.AreEqual(_brandSample, productResponseExample.Brand);
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsCategory()
        {
            productResponseExample.Category = _categorySample;
            Assert.AreEqual(_categorySample, productResponseExample.Category);
        }

        [TestMethod]
        public void GivenProductResponseReturnsItsColour()
        {
            productResponseExample.Colours = _coloursSample;
            Assert.AreEqual(_coloursSample, productResponseExample.Colours);
        }
    }
}