using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using SSO.Entities;
using SSO.Helpers;
using System;
using System.Threading.Tasks;

namespace SSO.Controllers
{
    //[CustomizeAuthorize]
    [SecurityHeaders]
    [Route("[controller]/[action]")]
    public class DecryptorController : Controller
    {
        private IDecryptorProvider decryptor;
        public DecryptorController(IDecryptorProvider decryptor)
        {
            this.decryptor = decryptor;
        }
        public IActionResult Index()
        {
            DecryptorDTO model = new DecryptorDTO() { InputValue = "", OutputValue = "" };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DecryptorDTO model, string button)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (String.IsNullOrEmpty(model.InputValue))
            {
                return View("Index", model);
            }
            var t = await Task.Run(() =>
            {
                if (button == "Decrypt")
                {
                    if (model.Type == "Secret")
                    {
                        model.OutputValue = model.InputValue;
                    }
                    else
                    {
                        model.OutputValue = decryptor.Decrypt(model.InputValue);
                    }
                }
                else
                {
                    if (model.Type == "Secret")
                    {
                        model.OutputValue = model.InputValue.Sha256();
                    }
                    else
                    {
                        model.OutputValue = decryptor.Encrypt(model.InputValue);
                    }
                }
                return model;
            });
            return View(t);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Decrypt(DecryptorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (String.IsNullOrEmpty(model.InputValue))
            {
                return View("Index", model);
            }

            model.OutputValue = decryptor.Decrypt(model.InputValue);
            return View("Index", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Encrypt(DecryptorDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (String.IsNullOrEmpty(model.InputValue))
            {
                return View("Index", model);
            }

            model.OutputValue = decryptor.Encrypt(model.InputValue);
            return View("Index", model);
        }
    }
}
