﻿using AspNetStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AspNetStore.Data;
using AspNetStore.ViewModel;

namespace AspNetStore.Controllers
{
    public class ShoppingCartController : Controller
    {
     

      
        public async Task<ActionResult> Index()
        {
            var cart = new ShoppingCart(HttpContext);
            var items = await cart.GetCartItemsAsync();

            return View(new ShoppingCartViewModel
            {
                Items = items,
                 Total = ShoppingCart.CalculateCart(items)
            });
        }


        public async Task<ActionResult> AddToCart(int id)
        {
            
            var cart = new ShoppingCart(HttpContext);

            await cart.AddAsync(id);


            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveFromCart(int id)
        {

            var cart = new ShoppingCart(HttpContext);

            await cart.RemoveAsync(id);


            return RedirectToAction("index");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cart = new ShoppingCart(HttpContext);

            var result = await cart.CheckoutAsync(model);
            if (result.Succeeded)
            {

                TempData["transaction"] = result.TransactionId;
                return RedirectToAction("Complete");
            }

            ModelState.AddModelError(string.Empty,result.Message);

            return View(model);



        }

        public ActionResult Complete()
        {
            ViewBag.TransactionId = (string) TempData["transactionId"];

            return View();
        }
    }
}