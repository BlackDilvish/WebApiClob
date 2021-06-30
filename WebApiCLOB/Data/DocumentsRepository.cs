using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCLOB.Models;

namespace WebApiCLOB.Data
{
    /// <summary>
    /// Repozytorium implementujace interfejs IDocumentsRepository,
    /// zawierajacy implementacje api do przetwarzania danych typu CLOB.
    /// </summary>
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly ClobDbContext m_context;

        public DocumentsRepository(ClobDbContext context)
        {
            m_context = context;
        }

        /// <summary>
        /// Funkcja zwracająca jeden okreslony dokument.
        /// </summary>
        /// <param name="id">
        /// Id dokumentu ktory ma zostac znaleziony w bazie.
        /// </param>
        /// <returns>
        /// Dokument znaleziony w bazie.
        /// </returns>
        public Document GetDocument(int id)
        {
            return m_context.Documents.Find(id);
        }

        /// <summary>
        /// Funkcja zwracająca wszystkie dokumenty z bazy danych.
        /// </summary>
        /// <returns>
        /// Dokumenty znajdujace sie aktualnie w bazie danych.
        /// </returns>
        public IEnumerable<Document> GetDocuments()
        {
            return m_context.Documents.ToList();
        }

        /// <summary>
        /// Funkcja zwracajaca dokumenty z bazy danych zawierajace okreslony ciag znakow.
        /// </summary>
        /// <param name="value">
        /// Ciag znakow, ktory ma zostac znaleziony w dokumentach z bazy.
        /// </param>
        /// <returns>
        /// Dokumenty z bazy zawierajace przekazany ciag znakow.
        /// </returns>
        public IEnumerable<Document> GetDocumentsByDescription(string value)
        {
            return m_context.Documents.Where(d => d.Description.Contains(value));
        }

        /// <summary>
        /// Funkcja dodajaca nowy dokument do bazy danych.
        /// </summary>
        /// <param name="document">
        /// Dokument ktory ma zostac dodany.
        /// </param>
        public void AddDocument(Document document)
        {
            m_context.Documents.Add(document);
            m_context.SaveChanges();
        }


        /// <summary>
        /// Funkcja ktora zmienia wartosci w istniejacym juz w bazie dokumencie.
        /// </summary>
        /// <param name="document">
        /// Dokument ze zmienionymi wartosciami.
        /// </param>
        public void EditDocument(Document document)
        {
            m_context.Entry(document).State = EntityState.Modified;
            m_context.SaveChanges();
        }

        /// <summary>
        /// Funkcja usuwajaca dany dokument z bazy danych.
        /// </summary>
        /// <param name="document">
        /// Dokument, ktory ma zostac usuniety.
        /// </param>
        public void DeleteDocument(Document document)
        {
            m_context.Documents.Remove(document);
            m_context.SaveChanges();
        }

        /// <summary>
        /// Funkcja sprawdzajaca czy dany dokument istnieje w bazie danych.
        /// </summary>
        /// <param name="id">
        /// Id szukanego dokumentu.
        /// </param>
        /// <returns>
        /// true - jesli dokument znajduje sie w bazie, w przeciwnym wypadku - false.
        /// </returns>
        public bool DocumentExists(int id)
        {
            return m_context.Documents.Any(e => e.Id == id);
        }
    }
}
