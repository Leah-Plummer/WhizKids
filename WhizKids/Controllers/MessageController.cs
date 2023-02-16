using Microsoft.AspNetCore.Mvc;
using WhizKids.Repositories;
using System.Collections.Generic;
using WhizKids.Models;
using System;
using WhizKids.Models.ViewModels;

namespace WhizKids.Controllers
{
    public class MessageController : Controller
    {

        private readonly IMessageRepository _messageRepo;
        private readonly IUserProfileRepository _userProfileRepo;
        public MessageController(IMessageRepository messageRepository, IUserProfileRepository userProfileRepository)
        {
            _messageRepo = messageRepository;
            _userProfileRepo = userProfileRepository;
        }

        // GET: Messages
        public ActionResult Index(int id)
        {
            List<Message> messages = _messageRepo.GetAllMessagesById(id);

            return View(messages);
        }

        // GET:Messages/Details/5
        public ActionResult Details(int id)
        {

            Message message = _messageRepo.GetMessageById(id);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: MessagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessagesController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {
            try
            {
                _messageRepo.AddMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(message);
            }
        }

        // GET: MessagesController/Edit/5
        public ActionResult Edit(int id)
        {
            Message message = _messageRepo.GetMessageById(id);

            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: MessagesController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Message message)
        {
            try
            {
                _messageRepo.UpdateMessage(message);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(message);
            }
        }

        // GET: MessagesController/Delete/5
        public ActionResult Delete(int id)
        {
            Message message = _messageRepo.GetMessageById(id);

            return View(message);
        }

        // POST: MessagesController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Message message)
        {
            try
            {
                _messageRepo.DeleteMessage(id);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(message);
            }
        }
    }
}