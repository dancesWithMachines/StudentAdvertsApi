using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using NUnit;
using StudentAdverts.Controllers;
using StudentAdverts.Models;
using System.Net.Http;
using StudentAdvertsTests.Fakes;
using NUnit.Framework;
using System.Linq;

namespace StudentAdvertsTests
{
    [TestFixture]
    public class TestSimpleProductController
    {
        
        public TestSimpleProductController()
        {
           
        }

        [Test]
        public void GetAllAdverts_ShouldReturnNotNull()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAll();
            // Assert
            Assert.IsNotNull(adverts);
        }

        [Test]
        public void GetAllAdverts_ShouldReturnCorrectType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAll();
            // Assert
            Assert.IsInstanceOf<List<Advert>>(adverts);
        }

        [Test]
        public void GetAllAdverts_ShouldReturnCorrectNumberOfAdverts()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAll();
            // Assert
            Assert.That(adverts.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetAdvertById_ShouldReturnNotNullForCorrectId()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var advert = advertsServiceFake.GetAdvertById(1);
            // Assert
            Assert.IsNotNull(advert);
        }

        [Test]
        public void GetAdvertById_ShouldReturnCorrectAdvert()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var advert = advertsServiceFake.GetAdvertById(1);
            // Assert
            Assert.That(advert.id, Is.EqualTo(1));
        }

        [Test]
        public void GetAdvertById_ShouldReturnCorrectAmount()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();
            var advertsList = new List<Advert>();

            // Act
            var advert = advertsServiceFake.GetAdvertById(1);
            advertsList.Add(advert);

            // Assert
            Assert.That(advertsList.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAdvertById_ShouldReturnCorrectType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var advert = advertsServiceFake.GetAdvertById(1);
            // Assert
            Assert.IsInstanceOf<Advert>(advert);
        }

        [Test]
        public void GetAdvertByTitle_ShouldReturnCorrectType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByTitle("matma");
            // Assert
            Assert.IsInstanceOf<List<Advert>>(adverts);
        }

        [Test]
        public void GetAdvertByTitle_ShouldReturnAdvertsWithCorrectTitle()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByTitle("matma");
            // Assert
            Assert.IsTrue(adverts.Select(a => a.title=="matma").ToList().ElementAt(0));
        }

        [Test]
        public void GetAdvertByTitle_ShouldReturnCorrectTitle()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByTitle("matma");
            // Assert
            Assert.That(adverts[0].title, Is.EqualTo("matma"));
        }

        [Test]
        public void GetAdvertByTitle_ShouldReturnNotNullForExistingType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByTitle("matma");
            // Assert
            Assert.IsNotNull(adverts);
        }

        [Test]
        public void GetAdvertByAuthor_ShouldReturnCorrectType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByTitle("autor1");
            // Assert
            Assert.IsInstanceOf<List<Advert>>(adverts);
        }

        [Test]
        public void GetAdvertByTitle_ShouldReturnAdvertsMadeByAuthor()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByAuthor("autor1");
            // Assert
            Assert.IsTrue(adverts.Select(a => a.author == "autor1").ToList().ElementAt(0));
        }

        [Test]
        public void GetAdvertByAuthor_ShouldReturnCorrectAuthor()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByAuthor("autor1");
            // Assert
            Assert.That(adverts[0].author, Is.EqualTo("autor1"));
        }

        [Test]
        public void GetAdvertByAuthor_ShouldReturnNotNullForExistingType()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByAuthor("autor1");
            // Assert
            Assert.IsNotNull(adverts);
        }

        [Test]
        public void GetAdvertByAuthor_ShouldReturnCorrectAmountOfAdverts()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var adverts = advertsServiceFake.GetAdvertByAuthor("autor1");
            // Assert
            Assert.That(adverts.Count, Is.EqualTo(1));
        }

        [Test]
        public void PutAdvert_ShouldAddAdvert()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var advert = new Advert()
            {
                id = 5,
                title = "tytul",
                description = "opis",
                author = "autor5",
                email = "mail5",
                phone = 123123123,
                dateAndTime = DateTime.Now,
            };
            var countBefore = advertsServiceFake.GetAll().Count;
            advertsServiceFake.PutAdvert(advert);
            var adverts = advertsServiceFake.GetAll();
            // Assert
            Assert.That(adverts.Count, Is.EqualTo(countBefore+1));
        }

        [Test]
        public void PutAdvert_ShouldDeleteAdvert()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var countBefore = advertsServiceFake.GetAll().Count;
            advertsServiceFake.DeleteAdvertById(3);
            var countAfter = advertsServiceFake.GetAll().Count;
            // Assert
            Assert.That(countAfter, Is.EqualTo(countBefore - 1));
        }

        [Test]
        public void AdvertExists_ShouldReturnTrueForExistingAdvert()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var doesExist = advertsServiceFake.AdvertExists(1);
            // Assert
            Assert.IsTrue(doesExist);
        }

        [Test]
        public void AdvertExists_ShouldReturnFalseForNotExistingAdvert()
        {
            // Arrange
            AdvertsServiceFake advertsServiceFake = new AdvertsServiceFake();

            // Act
            var doesExist = advertsServiceFake.AdvertExists(100);
            // Assert
            Assert.IsFalse(doesExist);
        }
    }
}