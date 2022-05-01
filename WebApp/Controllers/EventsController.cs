using App.Contracts.DAL;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class EventsController : Controller
{
    private readonly IAppUnitOfWork _uow;
    
    public EventsController(IAppUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IActionResult> Event(int id)
    {
        var e = await _uow.Events.FirstOrDefaultAsync(id);
        if (e == null) return RedirectToAction("Index", "Home");
        var vm = new EventVM
        {
            EventId = e.Id,
            EventName = e.EventName,
            Place = e.Place,
            Time = e.Time
        };
        
        foreach (var c in e.Companies)
        {
            var participants = new ParticipantVM
            {
                Id = c.Id,
                Name = c.Name,
                Identifier = c.RegistryCode,
                Type = "Company"
            };
            vm.Participants.Add(participants);
        }
            
        foreach (var p in e.Persons)
        {
            var participants = new ParticipantVM
            {
                Id = p.Id,
                Name = $"{p.FirstName} {p.LastName}",
                Identifier = p.PersonalId.ToString(),
                Type = "Person"
            };
            vm.Participants.Add(participants);
        }

        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Event(int id, EventVM vm)
    {
        if (ModelState.IsValid)
        {
            if (vm.ClientType.Equals("Eraisik"))
            {
                var person = new Person
                {
                    FirstName = vm.Name,
                    LastName = vm.Identifier,
                    PersonalId = vm.Number,
                    Payment = vm.Payment,
                    Comment = vm.Comment,
                    EventId = vm.EventId
                };

                _uow.Persons.Add(person);
                await _uow.SaveChangesAsync();
            }
            else
            {
                var company = new Company
                {
                    Name = vm.Name,
                    RegistryCode = vm.Identifier,
                    NumberOfParticipants = Convert.ToInt32(vm.Number),
                    Payment = vm.Payment,
                    Comment = vm.Comment,
                    EventId = vm.EventId
                };

                _uow.Companies.Add(company);
                await _uow.SaveChangesAsync();
            }
            
            return RedirectToAction("Event", new { id = vm.EventId});
        }
        return View(vm);
    }

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Event e)
    {
        if (ModelState.IsValid)
        {
            _uow.Events.Add(e);
            await _uow.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        return View(e);
    }

    public async Task<IActionResult> Remove(int id)
    {
        return View("RemoveEvent", await _uow.Events.RemoveAsync(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine(id);
        var e = await _uow.Events.FirstOrDefaultAsync(id);
        if (e != null)
        {
            _uow.Events.Remove(e);
            await _uow.SaveChangesAsync();
        }
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> RemoveParticipant(int id, string type, int eventId)
    {
        var vm = new ParticipantVM
        {
            Type = type,
            EventId = eventId,
            Id = id
        };
        
        if (type.Equals("Person"))
        {
            var p = await _uow.Persons.FirstOrDefaultAsync(id);
            if (p == null) return RedirectToAction("Event", new {id = eventId});
            vm.Name = $"{p.FirstName} {p.LastName}";
        }
        else
        {
            var c = await _uow.Companies.FirstOrDefaultAsync(vm.Id);
            if (c == null) return RedirectToAction("Event", new {id = eventId});
            vm.Name = c.Name;
        }
        
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveParticipant(ParticipantVM vm)
    {
        if (vm.Type.Equals("Person"))
        {
            var p = await _uow.Persons.FirstOrDefaultAsync(vm.Id);
            if (p == null) return RedirectToAction("Event", new {id = vm.EventId});
            await _uow.Persons.RemoveAsync(vm.Id);
            await _uow.SaveChangesAsync();
        }
        else
        {
            var c = await _uow.Companies.FirstOrDefaultAsync(vm.Id);
            if (c == null) return RedirectToAction("Event", new {id = vm.EventId});
            await _uow.Companies.RemoveAsync(vm.Id);
            await _uow.SaveChangesAsync();
        }
        return RedirectToAction("Event", new {id = vm.EventId});
    }

    public async Task<IActionResult> Details(int id, string type, int eventId)
    {
        var vm = new ParticipantDetailsVm
        {
            Id = id,
            Type = type,
            EventId = eventId
        };
        if (type.Equals("Person"))
        {
            var p = await _uow.Persons.FirstOrDefaultAsync(id);
            if (p == null) return RedirectToAction("Event", new {id = vm.EventId});
            vm.Name = p.FirstName;
            vm.Identifier = p.LastName;
            vm.Comment = p.Comment;
            vm.Number = p.PersonalId;
            vm.Payment = p.Payment;
        }
        else
        {
            var c = await _uow.Companies.FirstOrDefaultAsync(id);
            if (c == null) return RedirectToAction("Event", new {id = vm.EventId});
            vm.Name = c.Name;
            vm.Identifier = c.RegistryCode;
            vm.Comment = c.Comment;
            vm.Number = c.NumberOfParticipants;
            vm.Payment = c.Payment;
        }
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Details(ParticipantDetailsVm vm)
    {
        if (!ModelState.IsValid) return View(vm);
        if (vm.Type.Equals("Person"))
        {
            var p = new Person
            {
                Id = vm.Id,
                FirstName = vm.Name,
                LastName = vm.Identifier,
                PersonalId = vm.Number,
                Payment = vm.Payment,
                Comment = vm.Comment,
                EventId = vm.EventId
            };
            _uow.Persons.Update(p);
            await _uow.SaveChangesAsync();
            return RedirectToAction("Event", new {id = vm.EventId});
        }
        var c = new Company
        {
            Id = vm.Id,
            Name = vm.Name,
            RegistryCode = vm.Identifier,
            NumberOfParticipants = (int)vm.Number,
            Payment = vm.Payment,
            Comment = vm.Comment,
            EventId = vm.EventId
        };
        _uow.Companies.Update(c);
        await _uow.SaveChangesAsync();
        return RedirectToAction("Event", new {id = vm.EventId});

    }
}