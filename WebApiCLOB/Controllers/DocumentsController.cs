using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCLOB.Data;
using WebApiCLOB.Models;

namespace WebApiCLOB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsRepository m_repository;

        public DocumentsController(IDocumentsRepository repository)
        {
            m_repository = repository;
        }

        // GET: api/Documents
        [HttpGet]
        public ActionResult<IEnumerable<Document>> GetDocuments()
        {
            return m_repository.GetDocuments().ToList();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public ActionResult<Document> GetDocument(int id)
        {
            var document = m_repository.GetDocument(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // GET: api/Documents/find/{value}
        [HttpGet("find/{value}")]
        public ActionResult<IEnumerable<Document>> GetDocumentByDescription(string value)
        {
            return m_repository.GetDocumentsByDescription(value).ToList();
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            try
            {
                m_repository.EditDocument(document);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!m_repository.DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Document> PostDocument(Document document)
        {
            m_repository.AddDocument(document);

            return CreatedAtAction("GetDocument", new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(int id)
        {
            var document = m_repository.GetDocument(id);
            if (document == null)
            {
                return NotFound();
            }

            m_repository.DeleteDocument(document);
            return NoContent();
        }
    }
}
