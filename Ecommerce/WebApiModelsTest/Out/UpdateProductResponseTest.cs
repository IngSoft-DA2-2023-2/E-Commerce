using ApiModels.Out;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.Out
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UpdateProductResponseTest
    {
        private UpdateProductResponse updateProductResponseExample;
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
            updateProductResponseExample = new UpdateProductResponse();
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsGUID()
        {
            updateProductResponseExample.Id = _guidSample;
            Assert.AreEqual(_guidSample, updateProductResponseExample.Id);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsName()
        {
            updateProductResponseExample.Name = _nameSample;
            Assert.AreEqual(_nameSample, updateProductResponseExample.Name);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsPrice()
        {
            updateProductResponseExample.Price = _priceSample;
            Assert.AreEqual(_priceSample, updateProductResponseExample.Price);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsDescription()
        {
            updateProductResponseExample.Description = _descriptionSample;
            Assert.AreEqual(_descriptionSample, updateProductResponseExample.Description);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsBrand()
        {
            updateProductResponseExample.Brand = _brandSample;
            Assert.AreEqual(_brandSample, updateProductResponseExample.Brand);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsCategory()
        {
            updateProductResponseExample.Category = _categorySample;
            Assert.AreEqual(_categorySample, updateProductResponseExample.Category);
        }

        [TestMethod]
        public void GivenUpdateProductResponseReturnsItsColour()
        {
            updateProductResponseExample.Colours = _coloursSample;
            Assert.AreEqual(_coloursSample, updateProductResponseExample.Colours);
        }
    }
}