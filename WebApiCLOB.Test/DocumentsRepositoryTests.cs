using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using WebApiCLOB.Data;
using WebApiCLOB.Models;

namespace WebApiCLOB.Test
{
    [TestFixture]
    class DocumentsRepositoryTests
    {
        private DbContextOptions<ClobDbContext> m_dbContextOptions;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            m_dbContextOptions = new DbContextOptionsBuilder<ClobDbContext>()
                .UseInMemoryDatabase(databaseName: "DocumentsDb")
                .Options;
        }

        [SetUp]
        public void Setup()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            context.Documents.Add(new Document()
            {
                Author = "Anonymous",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry." +
                              "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s," +
                              "when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                              "It has survived not only five centuries, but also the leap into electronic typesetting, " +
                              "remaining essentially unchanged. It was popularised in the 1960s with the release of " +
                              "Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing " +
                              "software like Aldus PageMaker including versions of Lorem Ipsum.",
                Title = "Lorem ipsum"
            });
            context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            context.Documents.RemoveRange(context.Documents);
            context.SaveChanges();
        }

        [Test]
        public void TestGetDocuments()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            var documents = repository.GetDocuments();
            Assert.AreEqual(1, documents.Count());
        }

        [Test]
        public void TestGetOneDocument()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            int id = context.Documents.FirstOrDefault().Id;

            var document = repository.GetDocument(id);
            Assert.AreEqual("Lorem ipsum", document.Title);
            Assert.AreEqual("Anonymous", document.Author);
        }

        [Test]
        public void TestGetDocumentByDescription()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);
            string query = "five centuries";

            var document = repository.GetDocumentsByDescription(query).FirstOrDefault();
            Assert.AreEqual("Lorem ipsum", document.Title);
            Assert.AreEqual("Anonymous", document.Author);
        }

        [Test]
        public void TestAddNewDocument()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            Document document = new Document()
            {
                Author = "Adam Mickiewicz",
                Title = "Pan Tadeusz",
                Description = "Litwo! Ojczyzno moja! Ty jesteś jak zdrowie," +
                              "Ile cię trzeba cenić, ten tylko się dowie," +
                              "Kto cię stracił. Dziś piękność twą w całej ozdobie" +
                              "Widzę i opisuję, bo tęsknię po tobie" +
                              "Panno święta, co Jasnej bronisz Częstochowy" +
                              "I w Ostrej świecisz Bramie! Ty, co gród zamkowy" +
                              "Nowogródzki ochraniasz z jego wiernym ludem!" +
                              "Jak mnie dziecko do zdrowia powróciłaś cudem," +
                              "(Gdy od płaczącej matki pod Twoją opiekę" +
                              "Ofiarowany, martwą podniosłem powiekę" +
                              "I zaraz mogłem pieszo do Twych świątyń progu" +
                              "Iść za wrócone życie podziękować Bogu)," +
                              "Tak nas powrócisz cudem na Ojczyzny łono." +
                              "Tymczasem przenoś moją duszę utęsknioną" +
                              "Do tych pagórków leśnych, do tych łąk zielonych," +
                              "Szeroko nad błękitnym Niemnem rozciągnionych;" +
                              "Do tych pól malowanych zbożem rozmaitem," +
                              "Wyzłacanych pszenicą, posrebrzanych żytem;" +
                              "Gdzie bursztynowy świerzop, gryka jak śnieg biała," +
                              "Gdzie panieńskim rumieńcem dzięcielina pała," +
                              "A wszystko przepasane jakby wstęgą, miedzą" +
                              "Zieloną, na niej z rzadka ciche grusze siedzą."
            };

            repository.AddDocument(document);

            Assert.IsTrue(context.Documents.Contains(document));
        }

        [Test]
        public void TestEditDocument()
        {
            const string oldTitle = "Lorem ipsum";
            const string newTitle = "New Lorem ipsum";

            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            Document documentToEdit = context.Documents.FirstOrDefault();
            Assert.AreEqual(oldTitle, documentToEdit.Title);

            documentToEdit.Title = newTitle;
            repository.EditDocument(documentToEdit);

            Document editedDocument = context.Documents.FirstOrDefault();
            Assert.AreEqual(newTitle, editedDocument.Title);
        }

        [Test]
        public void TestRemoveDocument()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            Document documentToDelete = context.Documents.FirstOrDefault();
            repository.DeleteDocument(documentToDelete);
            Assert.IsFalse(context.Documents.Contains(documentToDelete));
        }

        [Test]
        public void TestDocumentExists()
        {
            using var context = new ClobDbContext(m_dbContextOptions);
            DocumentsRepository repository = new DocumentsRepository(context);

            int id = context.Documents.FirstOrDefault().Id;
            Assert.AreEqual(true, repository.DocumentExists(id));
        }
    }
}
