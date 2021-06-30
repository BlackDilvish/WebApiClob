using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiCLOB.Models;

namespace WebApiCLOB.Data
{
    /// <summary>
    /// Interfejs repozytorium zawierajacego implementacje api
    /// </summary>
    public interface IDocumentsRepository
    {
        IEnumerable<Document> GetDocuments();
        Document GetDocument(int id);
        IEnumerable<Document> GetDocumentsByDescription(string value);
        void EditDocument(Document document);
        void AddDocument(Document document);
        void DeleteDocument(Document document);
        bool DocumentExists(int id);
    }
}
