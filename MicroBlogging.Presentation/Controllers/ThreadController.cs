using System.Collections.Generic;
using MicroBlogging.Entity;
using MicroBlogging.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroBlogging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        private readonly ThreadService _threadService;

        public ThreadController(ThreadService threadService)
        {
            _threadService = threadService;
        }

        [HttpGet]
        public ActionResult<List<Thread>> Get() =>
            _threadService.Get();

        [HttpGet("{id:length(24)}", Name = "GetThread")]
        public ActionResult<Thread> Get(string id)
        {
            var thread = _threadService.Get(id);

            if (thread == null)
            {
                return NotFound();
            }

            return thread;
        }

        [HttpPost]
        public ActionResult<Thread> Create(Thread thread)
        {
            _threadService.Create(thread);

            return CreatedAtRoute("GetThread", new { id = thread.Id.ToString() }, thread);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Thread threadIn)
        {
            var thread = _threadService.Get(id);

            if (thread == null)
            {
                return NotFound();
            }

            _threadService.Update(id, threadIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var thread = _threadService.Get(id);

            if (thread == null)
            {
                return NotFound();
            }

            _threadService.Remove(thread.Id);

            return NoContent();
        }
    }
}