using StudentAdverts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentAdvertsTests.Fakes
{
    public class AdvertsServiceFake
    {
        private readonly List<Advert> _adverts;
        public AdvertsServiceFake()
        {
            _adverts = new List<Advert>()
            {
                 new Advert()
                 {
                     id = 1,
                     title = "matma",
                     description = "opis",
                     author = "autor1",
                     email = "mail1",
                     phone = 123123123,
                     dateAndTime = DateTime.Now,
                 },

                 new Advert()
                 {
                     id = 2,
                     title = "tytul",
                     description = "opis",
                     author = "autor2",
                     email = "mail2",
                     phone = 123123123,
                     dateAndTime = DateTime.Now,
                 },

                 new Advert()
                 {
                     id = 3,
                     title = "tytul",
                     description = "opis",
                     author = "autor3",
                     email = "mail3",
                     phone = 123123123,
                     dateAndTime = DateTime.Now,
                 },
            };
        }

        public List<Advert> GetAll()
        {
            return _adverts;
        }

        public Advert GetAdvertById(int id)
        {
            return _adverts.First(x => x.id == id);
        }

        public List<Advert> GetAdvertByTitle(string title)
        {
            return _adverts.Where(x => x.title == title).ToList();
        }

        public List<Advert> GetAdvertByAuthor(string author)
        {
            return _adverts.Where(x => x.author == author).ToList();
        }

        public void PutAdvert(Advert advert)
        {
            _adverts.Add(advert);
        }

        public void DeleteAdvertById(int id)
        {
            var advert = GetAdvertById(id);
            _adverts.Remove(advert);
        }

        public bool AdvertExists(int id)
        {
            return _adverts.Count(x => x.id == id) > 0;
        }

    }
}
