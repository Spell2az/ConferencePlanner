using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Infrastructure;
using ConferenceDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Speaker = BackEnd.Data.Speaker;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SpeakersController(ApplicationDbContext db
        )
        {
            _db = db;
        }

        // GET: api/Speakers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeakerResponse>>> GetSpeakers()
        {
            var speakers = await _db.Speakers.AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .Select(s => s.MapSpeakerResponse())
                .ToListAsync();
            return speakers;
        }

        // GET: api/Speakers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConferenceDTO.Speaker>> GetSpeaker(int id)
        {
            var speaker = await _db.Speakers.AsNoTracking()
                .Include(s => s.SessionSpeakers)
                .ThenInclude(ss => ss.Session)
                .SingleOrDefaultAsync(s => s.ID == id);

            if (speaker == null)
            {
                return NotFound();
            }

            return speaker.MapSpeakerResponse();
        }

        // PUT: api/Speakers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpeaker(int id, Speaker speaker)
        {
            if (id != speaker.ID)
            {
                return BadRequest();
            }

            _db.Entry(speaker).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpeakerExists(id))
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

        // POST: api/Speakers
        [HttpPost]
        public async Task<ActionResult<Speaker>> PostSpeaker(Speaker speaker)
        {
            _db.Speakers.Add(speaker);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetSpeaker", new { id = speaker.ID }, speaker);
        }

        // DELETE: api/Speakers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Speaker>> DeleteSpeaker(int id)
        {
            var speaker = await _db.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }

            _db.Speakers.Remove(speaker);
            await _db.SaveChangesAsync();

            return speaker;
        }

        private bool SpeakerExists(int id)
        {
            return _db.Speakers.Any(e => e.ID == id);
        }
    }
}
